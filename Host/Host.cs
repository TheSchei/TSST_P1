using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using Simulation;

namespace Host
{
    class Host
    {
        public string HostName;//chyba useless
        private IPAddress IP;
        public IPAddress logicIP;
        private IPAddress Gateway;
        private int recPort;
        private int cloudPort;
        private Socket Receiver;
        private Socket Sender;
        public List<string> remoteHostIPs = new List<string>();
        public Queue<string> messageQueue = new Queue<string>();
        private ManualResetEvent AllDone = new ManualResetEvent(false);
        private Thread t;

        public Host(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            HostName = doc.SelectSingleNode("/Host/HostName").InnerText;
            IP = IPAddress.Parse(doc.SelectSingleNode("/Host/IP").InnerText);
            logicIP = IPAddress.Parse(doc.SelectSingleNode("/Host/logIP").InnerText);
            recPort = Convert.ToInt32(doc.SelectSingleNode("/Host/RecPort").InnerText);
            Gateway = IPAddress.Parse(doc.SelectSingleNode("/Host/Gateway").InnerText);
            cloudPort = Convert.ToInt32(doc.SelectSingleNode("/Host/CloudPort").InnerText);
            XmlNodeList Nodes = doc.DocumentElement.SelectNodes("/Host/RemoteHosts/Host");
            Receiver = new Socket((new IPEndPoint(IP, recPort)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Sender = new Socket((new IPEndPoint(IP, cloudPort)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Sender.Bind(new IPEndPoint(IP, sendPort));
            foreach (XmlNode Node in Nodes)
                remoteHostIPs.Add(Node.InnerText);
            t = new Thread(Listen);
            t.Start();
        }
        private void Listen()
        {
            //Receiver = new Socket((new IPEndPoint(IP, port)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Receiver.Bind(new IPEndPoint(IP, recPort));
            Receiver.Listen(10);
            while (true)
            {
                AllDone.Reset();
                Receiver.BeginAccept(new AsyncCallback(readMessage), Receiver);
                AllDone.WaitOne();
            }
        }
        private void readMessage(IAsyncResult result)
        {
            List<byte> temp = new List<byte>();
            byte[] buffer = new byte[128];
            Socket reader = ((Socket)result.AsyncState).EndAccept(result);
            AllDone.Set();
            try
            {
                while (reader.Available == 0) ;// Thread.Sleep(3);
                while (reader.Available > 0)
                {
                    reader.Receive(buffer, buffer.Length, SocketFlags.Partial);
                    temp.AddRange(buffer);
                }
                reader.Disconnect(true);
                Package package = new Package(temp.ToArray());
                messageQueue.Enqueue(package.Source.ToString() + ": " + package.Payload + Environment.NewLine);
            }
            catch (Exception e)
            {
                messageQueue.Enqueue(Logger.Log(e.Message, LogType.ERROR));
            }
            finally { if (reader != null) reader.Close(); }
        }
        public bool sendMesage(string message, IPAddress destination)
        {
            try
            {
                Sender = new Socket((new IPEndPoint(IP, cloudPort)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Sender.Connect(new IPEndPoint(IP, cloudPort));
                Sender.Send(new Package(logicIP, destination, logicIP, Gateway, message).toBytes());
                messageQueue.Enqueue(Logger.Log("Sent to " + destination.ToString() + ": " + message, LogType.INFO));
                Sender.Close();
            }
            catch (Exception e)
            {
                messageQueue.Enqueue(Logger.Log(e.Message, LogType.ERROR));
                if (Sender != null) Sender.Close();
                return false;
            }
            return true;
        }
        public void Dispose()
        {
            t.Abort();
        }
    }
}
