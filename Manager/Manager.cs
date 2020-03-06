using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using Simulation;

namespace Manager
{
    class Manager
    {
        private Dictionary<IPAddress, int> remoteRouters = new Dictionary<IPAddress, int>();
        private IPAddress LogicIP;
        private IPAddress IP;
        public Manager(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            LogicIP = IPAddress.Parse(doc.SelectSingleNode("/Manager/LogicIP").InnerText);
            IP = IPAddress.Parse(doc.SelectSingleNode("/Manager/IP").InnerText);
            foreach (XmlNode Node in doc.SelectNodes("/Manager/Routers/Address"))
                remoteRouters.Add(  IPAddress.Parse(Node.SelectSingleNode("IP").InnerText),
                                    Convert.ToInt32(Node.SelectSingleNode("Port").InnerText));
        }
        public IPAddress[] getIPs()
        {
            return remoteRouters.Keys.ToArray();
        }
        public string DeleteByID(IPAddress ip, int id, ControlParam param)
        {
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(param, id)));
        }
        
        public string setIPFIB(IPAddress ip, IPAddress destination, IPAddress interfaceOut)
        {
            //List<byte> message = new List<byte>();//
            //message.Add((byte)ControlParam.SetIPFIB);//
            //message.AddRange(destination.GetAddressBytes());//
            //message.AddRange(interfaceOut.GetAddressBytes());//
            //return manage(new Package(LogicIP, ip, LogicIP, ip, Encoding.ASCII.GetString(message.ToArray())));//
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(ControlParam.SetIPFIB, (new FIBRecordIP(destination, interfaceOut)).toBytes())));
        }

        public string setMPLSFIB(IPAddress ip, IPAddress destination, short Label)
        {
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(ControlParam.SetMPLSFIB, (new FIBRecordMPLS(destination, new Label(Label))).toBytes())));
            //return Logger.Log("Not Implemented", LogType.ERROR);
        }
        public string setFTN(IPAddress ip, short Label, int nextOperation)
        {
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(ControlParam.SetFTN, (new FTNRecord(new Label(Label), nextOperation)).toBytes())));
            //return Logger.Log("Not Implemented", LogType.ERROR);
        }
        public string setILM(IPAddress ip, IPAddress interfaceFrom, short Label, string poppedLabels, int NextOperationId)
        {
            string[] poppedLabelsTab =  poppedLabels.Replace(" ", "")
                                                    .Split(',');
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(ControlParam.SetIFN, (new ILMRecord(interfaceFrom, new Label(Label), new LabelStack(poppedLabelsTab), NextOperationId)).toBytes())));
            //return Logger.Log("Not Implemented", LogType.ERROR);
        }
        public string setNHLFE(IPAddress ip, int OperationId, string OperationName, string Label, string interfaceTo, string nextOperation)//tutaj lekka mziana koncepcji, bo pola są NULLable
        {
            Label labelToSend = null;
            IPAddress interfaceToSend = null;
            int nextOperationToSend = 0;
            if (Label.Replace(" ", "") != "") labelToSend = new Label(Convert.ToInt16(Label));
            IPAddress.TryParse(interfaceTo, out interfaceToSend);
            if (nextOperation.Replace(" ", "") != "")
                nextOperationToSend = Convert.ToInt32(nextOperation);
            return manage(new Package(LogicIP, ip, LogicIP, ip, Protocol.CreateMessage(ControlParam.SetNHLFE, new NHLFERecord(OperationId, (Operation)Enum.Parse(typeof(Operation), OperationName), labelToSend, interfaceToSend, nextOperationToSend).toBytes())));
            //return Logger.Log("Not Implemented", LogType.ERROR);
        }
        private string manage(Package package)
        {
            Socket sender = new Socket(new IPEndPoint(IPAddress.Any, remoteRouters[package.Destination]).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(new IPEndPoint(IP, remoteRouters[package.Destination]));
            sender.Send(package.toBytes());
            while (sender.Available == 0) ;
            List<byte> temp = new List<byte>();
            byte[] buffer = new byte[128];
            while (sender.Available > 0)
            {
                sender.Receive(buffer, buffer.Length, SocketFlags.Partial);
                temp.AddRange(buffer);
            }
            sender.Disconnect(true);
            package = new Package(temp.ToArray());
            switch (Protocol.getControlResponse(package.Payload))
            {
                case ControlResponse.OK:
                    return Logger.Log("Success", LogType.INFO);
                case ControlResponse.ERROR:
                    return Logger.Log(Protocol.DeleteParam(package.Payload), LogType.INFO);
                default:
                    return Logger.Log("Unknown type of response", LogType.ERROR);
            }
        }
    }
}
