using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.Threading;
using Simulation;

namespace CableCloud
{
    class CableCloud
    {
        private int cloudPort;
        public IPAddress cloudIP;
        private ManualResetEvent AllDone = new ManualResetEvent(false);
        private Queue<string> bufor = new Queue<string>();
        public Queue<string> messageQueue = new Queue<string>();

        private Socket Receiver;
        private Thread t;

        public Fields fields = new Fields();

        public CableCloud(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            cloudPort = Convert.ToInt32(doc.SelectSingleNode("/Cloud/Port").InnerText);
            cloudIP = IPAddress.Parse(doc.SelectSingleNode("/Cloud/IP").InnerText);
            XmlNodeList Nodes = doc.DocumentElement.SelectNodes("/Cloud/Forwarding/element");
            foreach (XmlNode Node in Nodes)
            {
                fields.AddField(IPAddress.Parse(Node.SelectSingleNode("interface1").InnerText),
                IPAddress.Parse(Node.SelectSingleNode("interface2").InnerText),
                Convert.ToInt32(Node.SelectSingleNode("port1").InnerText),
                Convert.ToInt32(Node.SelectSingleNode("port2").InnerText),
                (Node.SelectSingleNode("enable").InnerText));
            }
            Receiver = new Socket(new IPEndPoint(cloudIP, cloudPort).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            t = new Thread(listen);
            t.Start();
        }


        private void listen()
        {
            Receiver.Bind(new IPEndPoint(cloudIP, cloudPort));
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
            //nie jestem pewien, czy to git zadziala, czy to jest w dobrym miejscu
            AllDone.Set();
            try
            {
                while (reader.Available == 0);
                while (reader.Available > 0)
                {
                    reader.Receive(buffer, buffer.Length, SocketFlags.Partial);
                    temp.AddRange(buffer);
                }
                Package package = new Package(temp.ToArray());

                //Fields
                int port = fields.forward(package.InterfaceFrom, package.InterfaceTo);
                sendMessage(package, port);
            }
            catch (Exception e)
            {
                messageQueue.Enqueue(Logger.Log(e.Message, LogType.ERROR));
                
            }
            finally { if (reader != null) reader.Close(); }
        }

        public bool sendMessage(Package package, int port)
        {
            Socket Sender = new Socket(new IPEndPoint(cloudIP, port).AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                Sender.Connect(new IPEndPoint(cloudIP, port));
                Sender.Send(package.toBytes());
                Sender.Close();
                messageQueue.Enqueue(Logger.Log("Package from " + package.InterfaceFrom + " sent to " + package.InterfaceTo + ":" + port, LogType.INFO));
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
