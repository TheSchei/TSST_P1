using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Xml;
using Simulation;
using System.Windows.Forms;

namespace Router
{

    enum Option { Input, ToMPLSFIB, ToIFN, ToNHLFE, Output };

    class Router
    {
        public string RouterName;
        private IPAddress IP;
        private IPAddress logicIP;
        private int recPort;
        private int cloudPort;
        private Socket Receiver;
        private Socket Sender;
        private List<FIBRecordMPLS> FIBTableMPLS = new List<FIBRecordMPLS>();
        private List<FIBRecordIP> FIBTableIP = new List<FIBRecordIP>();
        private List<FTNRecord> FTNTable = new List<FTNRecord>();
        private List<NHLFERecord> NHLFETable = new List<NHLFERecord>();
        private List<ILMRecord> ILMTable = new List<ILMRecord>();
        public Queue<String> messageQueue = new Queue<String>();
        private ManualResetEvent AllDone = new ManualResetEvent(false);
        private readonly Thread t;

        public Router(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            RouterName = doc.SelectSingleNode("/Router/RouterName").InnerText;
            IP = IPAddress.Parse(doc.SelectSingleNode("/Router/IP").InnerText);
            logicIP = IPAddress.Parse(doc.SelectSingleNode("/Router/logIP").InnerText);
            recPort = Convert.ToInt32(doc.SelectSingleNode("/Router/RecPort").InnerText);
            cloudPort = Convert.ToInt32(doc.SelectSingleNode("/Router/CloudPort").InnerText);
            Receiver = new Socket((new IPEndPoint(IP, recPort)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            

            //FILL TABLES FROM XML
            XmlNodeList FIBMPLS = doc.DocumentElement.SelectNodes("/Router/FIB-MPLS/Element");
            foreach(XmlNode FIBRec in FIBMPLS)
            {
                IPAddress destIP = IPAddress.Parse(FIBRec["destIP"].InnerText);
                Simulation.Label label = new Simulation.Label(Convert.ToInt16(FIBRec["label"].InnerText));
                FIBTableMPLS.Add(new FIBRecordMPLS(destIP, label));
            }


            XmlNodeList FIBIP = doc.DocumentElement.SelectNodes("/Router/FIB-IP/Element");
            foreach (XmlNode FIBRec in FIBIP)
            {
                IPAddress destIP = IPAddress.Parse(FIBRec["destIP"].InnerText);
                IPAddress outInt = IPAddress.Parse(FIBRec["outInt"].InnerText);
                FIBTableIP.Add(new FIBRecordIP(destIP, outInt));
            }

            XmlNodeList ILM = doc.DocumentElement.SelectNodes("/Router/ILM/Element");
            foreach (XmlNode ILMRec in ILM)
            {
                IPAddress intFrom = IPAddress.Parse(ILMRec["intFrom"].InnerText);
                Simulation.Label incLabel = new Simulation.Label(Convert.ToInt16(ILMRec["incLabel"].InnerText));
                /*Simulation.Label poppedLabel = new Simulation.Label(Convert.ToInt16(ILMRec["poppedLabel"].InnerText));
                LabelStack poppedLabelStack = new LabelStack();
                poppedLabelStack.labels.Push(poppedLabel);*/
                string[] poppedLabelsTab = ILMRec["poppedLabel"].InnerText
                                                                .Replace(" ", "")
                                                                .Split(',');
                LabelStack poppedLabelStack = new LabelStack(poppedLabelsTab);
                int nextOperationID = Convert.ToInt32(ILMRec["nextOperationID"].InnerText);
                ILMTable.Add(new ILMRecord(intFrom, incLabel, poppedLabelStack, nextOperationID));
            }

            XmlNodeList FTN = doc.DocumentElement.SelectNodes("/Router/FTN/Element");
            foreach(XmlNode FTNRec in FTN)
            {
                Simulation.Label fec = new Simulation.Label(Convert.ToInt16(FTNRec["FEC"].InnerText));
                int nextOperationID = Convert.ToInt32(FTNRec["nextOperationID"].InnerText);
                FTNTable.Add(new FTNRecord(fec, nextOperationID));
            }

            XmlNodeList NHLFE = doc.DocumentElement.SelectNodes("/Router/NHLFE/Element");
            foreach(XmlNode NHLFERec in NHLFE)
            {
                int nextOperationID = Convert.ToInt32(NHLFERec["nextOperationID"].InnerText);
                //Operation operation = (Operation)Convert.ToByte(NHLFERec["operation"].InnerText); // nie jestem pewien czy tu int (operation to enum w NHLFE)
                Operation operation = (Operation)Enum.Parse(typeof(Operation), NHLFERec["operation"].InnerText);
                Simulation.Label outLabel = new Simulation.Label(Convert.ToInt16(NHLFERec["outLabel"].InnerText));
                IPAddress outInt = IPAddress.Parse(NHLFERec["outInt"].InnerText);
                int nextOperation = Convert.ToInt32(NHLFERec["nextOperation"].InnerText);
                NHLFETable.Add(new NHLFERecord(nextOperationID, operation, outLabel, outInt, nextOperation));
            }

            //Run listening Thread
            t = new Thread(Listen);
            t.Start();
        }

        public List<FIBRecordMPLS> PubFIBTableMPLS { get => FIBTableMPLS; }
        public List<FIBRecordIP> PubFIBTableIP { get => FIBTableIP; }
        public List<FTNRecord> PubFTNTable { get => FTNTable; }
        public List<NHLFERecord> PubNHLFETable { get => NHLFETable; }
        public List<ILMRecord> PubILMTable { get => ILMTable; }

        //public Simulation.Label IncTunnel(IPAddress intFrom)
        //{
        //    ILMRecord MatchingILM = ILMTable.Find(item => item.IntFrom.Equals(intFrom));
        //    Simulation.Label incLabel = MatchingILM.IncLabel;
        //    return incLabel;
        //}

        public void Dispose()
        {
            //End listening thread at the end
            t.Abort();
        }

        private void Listen()
        {
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
                while (reader.Available == 0) ;
                while (reader.Available > 0)
                {
                    reader.Receive(buffer, buffer.Length, SocketFlags.Partial);
                    temp.AddRange(buffer);
                }
                Package package = new Package(temp.ToArray());

                /*messageQueue.Enqueue(Logger.Log("SOURCE:  " + package.Source.ToString(), LogType.INFO));
                messageQueue.Enqueue(Logger.Log("--> ROUTER INTERFACE FROM  " + package.InterfaceFrom.ToString(), LogType.INFO));
                messageQueue.Enqueue(Logger.Log("ROUTER INTERFACE TO -->  " + package.InterfaceTo.ToString(), LogType.INFO));
                messageQueue.Enqueue(Logger.Log("DESTINATION:  " + package.Destination.ToString(), LogType.INFO));*/

                if (package.Destination == logicIP || package.InterfaceFrom.ToString() == "8.8.8.8")
                {
                    //Configuration message arrived
                    byte[] Response = ProccessManagementMessage(package);
                    messageQueue.Enqueue(Logger.Log("Config changed by Manager." , LogType.INFO));
                    reader.Send(new Package(package.Destination, package.Source, package.Destination, package.Source, Encoding.ASCII.GetString(Response)).toBytes());
                }
                else
                {
                    routeMessage(package);
                }            
            }
            catch (Exception e)
            {
                messageQueue.Enqueue(Logger.Log(e.Message, LogType.ERROR));
            }
            finally { if (reader != null) reader.Close(); }
        }

        //public LabelStack poppedLabels = new LabelStack();
        //public LabelStack swappedLabels = new LabelStack();
        //public LabelStack pushedLabels = new LabelStack();


        public bool routeMessage(Package package)
        {
            int LocalTTL = 60;//jeśli chcecie to wywalcie
            if (--package.ttl == 0) throw new Exception("Time to live expired");
            //Set option as input
            Option option = Option.Input;

            try
            {
                //package.InterfaceFrom = logicIP;
                FIBRecordIP MatchingIP = FIBTableIP.Find(item => item.DestIP.Equals(package.Destination));
                ILMRecord MatchingILM;
                
                LabelStack poppedLabelStack = new LabelStack();
                int nextOperID = 0;
                messageQueue.Enqueue(Logger.Log("SOURCE: " + package.Source.ToString(), LogType.INFO));
                messageQueue.Enqueue(Logger.Log("DESTINATION:  " + package.Destination.ToString(), LogType.INFO));
                while (option != Option.Output)
                {
                    switch (option)
                    {
                        case Option.Input: //On input check if Labelstack is empty
                            if (package.Labels.GetLength() == 1) //Empty Labelstack --> MPLS-FIB
                                option = Option.ToMPLSFIB;
                            else //Labelstack not empty
                                option = Option.ToIFN;
                            break;

                        case Option.ToMPLSFIB:
                            FIBRecordMPLS MatchingMPLS = FIBTableMPLS.Find(item => item.DestIP.Equals(package.Destination));
                            Simulation.Label fec = MatchingMPLS.Fec;
                            if (fec.Id == 0) // jesli FEC=0 => routing po IP
                            {
                                package.InterfaceTo = MatchingIP.OutInt;
                                option = Option.Output;
                            }
                            else //Not empty label in MPLS-FIB --> Go to FTN and then to NHLFE
                            {
                                FTNRecord MatchingFTN = FTNTable.Find(item => item.Fec.Id.Equals(fec.Id));
                                nextOperID = MatchingFTN.NextOperationID;
                                option = Option.ToNHLFE;
                            }

                            break;

                        case Option.ToNHLFE:
                            if (--LocalTTL == 0) throw new Exception("Package stucked in infinite loop");//jeśli chcecie to wywalcie
                            NHLFERecord MatchingNHLFE = NHLFETable.Find(item => item.NextOperationID == nextOperID);
                            Operation operation = MatchingNHLFE.Operation;
                            int nextOper = 0;
                            //Switch between Operation in NHLFE
                            switch (operation)
                            {
                                case Operation.POP:
                                    poppedLabelStack.labels.Push(new Simulation.Label(package.popLabel()));
                                    messageQueue.Enqueue(Logger.Log("POPPED LABELS: " + poppedLabelStack.labels.Peek().Id.ToString(), LogType.INFO));
                                    if (package.Labels.GetLength() == 1) //Empty label stack --> MPLS-FIB
                                        option = Option.ToMPLSFIB;
                                    else //Not empty label stack --> IFN
                                        option = Option.ToIFN;
                                    break;

                                case Operation.PUSH:
                                    package.pushLabel(MatchingNHLFE.OutLabel.Id);
                                    messageQueue.Enqueue(Logger.Log("PUSHED LABELS: " + package.getTopLabel().ToString(), LogType.INFO));
                                    nextOper = MatchingNHLFE.NextOperation;
                                    if (nextOper.Equals(0)) //Route --> BYE BYE
                                    {
                                        package.InterfaceTo = MatchingNHLFE.OutInt;
                                        option = Option.Output;
                                    }
                                    else // There is next operation in NHLFE
                                    {
                                        NHLFERecord MatchingWhatsNext = NHLFETable.Find(item => item.NextOperationID.Equals(nextOper));
                                        nextOperID = nextOper;
                                        option = Option.ToNHLFE;
                                    }
                                    break;

                                case Operation.SWAP: //Simply swap label and route

                                    string txt = package.popLabel().ToString() + " SWAPPED TO: ";
                                    Simulation.Label outLabel = MatchingNHLFE.OutLabel;
                                    package.pushLabel(outLabel.Id);
                                    messageQueue.Enqueue(Logger.Log(txt + outLabel.Id.ToString(), LogType.INFO));
                                    //swappedLabels.labels.Push(MatchingNHLFE.OutLabel);
                                    nextOper = MatchingNHLFE.NextOperation;

                                    if (nextOper == 0) //Send package
                                    {
                                        package.InterfaceTo = MatchingNHLFE.OutInt;
                                        option = Option.Output;
                                    }
                                    else //KIEDY TO SIĘ STANIE? CZY JEST WGL TAKI PRZYPADEK? Z NHLFE LECI DO NHLFE PO SWAPIE?
                                    {
                                        nextOperID = MatchingNHLFE.NextOperation;
                                        option = Option.ToNHLFE;
                                    }
                                    break;
                            }

                            break;

                        case Option.ToIFN: //Do weryfikacji jak będzie funkcjonował poppedlabelstack

                            MatchingILM = ILMTable.Find(item => (item.IntFrom.Equals(package.InterfaceFrom) && item.IncLabel.Id == package.getTopLabel() && item.PoppedLabelStack.Equals(poppedLabelStack)));
                            nextOperID = MatchingILM.NextOperID;
                            option = Option.ToNHLFE;
                            break;

                    }
                }
                messageQueue.Enqueue(Logger.Log("Sent to:  " + package.InterfaceTo.ToString(), LogType.INFO));
                //option == Option.Output --> SendPackage()
                sendMessage(package, package.InterfaceTo);

            }
            catch(Exception e)
            {
                messageQueue.Enqueue(Logger.Log(e.Message, LogType.ERROR));
            }
            
            return true;
        }

        public bool sendMessage(Package package, IPAddress destination)
        {
            try
            {
                package.InterfaceFrom = logicIP;
                Sender = new Socket((new IPEndPoint(IP, cloudPort)).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Sender.Connect(new IPEndPoint(IP, cloudPort));
                Sender.Send(package.toBytes());
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

        public byte[] ProccessManagementMessage(Package package)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(package.Payload);
            ControlParam Param = (ControlParam)bytes[0];
            bytes = Protocol.DeleteControlParam(bytes);
            string Err="";
            int ID = -1;
            
            switch(Param) //Jazdaaaaa!!!
            {
                case ControlParam.SetIPFIB:
                    try
                    {
                        FIBTableIP.Add(new FIBRecordIP(bytes));
                    }
                    catch
                    {
                        Err = "Setting error";
                    }
                    break;

                case ControlParam.DeleteIPFIBbyId:
                    try
                    {
                        ID = BitConverter.ToInt32(bytes, 0);
                        FIBTableIP.RemoveAt(ID);
                    }
                    catch
                    {
                        Err = "Error occured";
                        break;
                    }
                    break;

                case ControlParam.SetMPLSFIB:
                    try
                    {
                        FIBTableMPLS.Add(new FIBRecordMPLS(bytes));
                    }
                    catch
                    {
                        Err = "Setting error";
                    }
                    break;

                case ControlParam.DeleteMPLSFIBbyId:
                    try
                    {
                        ID = BitConverter.ToInt32(bytes, 0);
                        FIBTableMPLS.RemoveAt(ID);
                    }
                    catch
                    {
                        Err = "Error occured";
                        break;
                    }
                    break;

                case ControlParam.SetFTN:
                    try
                    {
                        FTNTable.Add(new FTNRecord(bytes));
                    }
                    catch
                    {
                        Err = "Setting error";
                    }
                    break;

                case ControlParam.DeleteFTNbyId:
                    try
                    {
                        ID = BitConverter.ToInt32(bytes, 0);
                        FTNTable.RemoveAt(ID);
                    }
                    catch
                    {
                        Err = "Error occured";
                        break;
                    }
                    break;

                case ControlParam.SetNHLFE:
                    try
                    {
                        NHLFETable.Add(new NHLFERecord(bytes));
                    }
                    catch
                    {
                        Err = "Setting error";
                    }
                    break;

                case ControlParam.DeleteNHLFEbyId:
                    try
                    {
                        ID = BitConverter.ToInt32(bytes, 0);
                        NHLFETable.RemoveAt(ID);
                    }
                    catch
                    {
                        Err = "Error occured";
                        break;
                    }
                    break;

                case ControlParam.SetIFN:
                    try
                    {
                        ILMTable.Add(new ILMRecord(bytes));
                    }
                    catch
                    {
                        Err = "Setting error";
                    }
                    break;

                case ControlParam.DeleteIFNbyId:
                    try
                    {
                        ID = BitConverter.ToInt32(bytes, 0);
                        ILMTable.RemoveAt(ID);
                    }
                    catch
                    {
                        Err = "Error occured";
                        break;
                    }
                    break;

                default:
                    Err = "Nieobsługiwana opcja";
                    break;
            }
            if(Err == "")
                return Protocol.CreateResponse(ControlResponse.OK);
            else
                return Protocol.CreateResponse(ControlResponse.ERROR, Err);
        }
    }
   
}
