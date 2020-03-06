using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Simulation
{
    public class Package
    {
        private LabelStack labels;
        private readonly IPAddress source;
        private readonly IPAddress destination;
        public IPAddress InterfaceFrom;
        public IPAddress InterfaceTo;
        private int length;
        public int ttl;
        private string payload;

        public string Payload { get => payload; set => payload = value; }
        public IPAddress Source { get => source;}
        public IPAddress Destination { get => destination;}
        public LabelStack Labels { get => labels; } //nwm czy sie przyda

        public Package(byte[] data)//konstruktor do odczytywania pakietu z bajtów, czyli po odebraniu z socketu
        {
            int index;
            labels = LabelStack.FromBytes(data);//wykpnuję funkcję frombytes na pierwszych bajtach
            index = labels.GetLength();//określa początkowy index na podstawie długości labelków
            source = new IPAddress(new byte[] { data[index], data[index + 1], data[index + 2], data[index + 3] });//konwertuje kolejne 4 bajty na source
            destination =   new IPAddress(new byte[] { data[index +  4], data[index +  5], data[index +  6], data[index +  7] });//konwertuje kolejne 4 bajty na destination
            InterfaceFrom = new IPAddress(new byte[] { data[index +  8], data[index +  9], data[index + 10], data[index + 11] });//konwertujemy kolejne 4 bajty na numer interfacu źródła
            InterfaceTo =   new IPAddress(new byte[] { data[index + 12], data[index + 13], data[index + 14], data[index + 15] });//konwertujemy kolejne 4 bajty na numer interfacu celu
            length = BitConverter.ToInt32(data, index + 16);// długość
            ttl = BitConverter.ToInt32(data, index + 20);//męczy mnie to
            List<byte> myPayload = new List<byte>();
            myPayload.AddRange(data.ToList().GetRange(index + 24, length - (index + 24)));//SPRAWDZIC!!!!!//jak wspomniałem we wcześniejszym komentarzu
                                                                                          //nie mam pojęcia. czy to działa, ale powinno robić cośw rodzaju
                                                                                          //operacji SubArray(), od (koniec labelków + stała długość reszty headera)
                                                                                          //i wczytywać kolejnych długość-<policzony przed chwilą numerek>, czyli do końca
            payload = Encoding.UTF8.GetString(myPayload.ToArray());//konwertujemy na stringa
        }
        public Package(IPAddress source, IPAddress destination, IPAddress InterfaceFrom, IPAddress InterfaceTo, string payload)// konstruktor do tworzenia pakietu (np. w hoście lub managerze?)
        {
            labels = new LabelStack();//pusty labelstack
            this.source = source;
            this.destination = destination;
            this.InterfaceFrom = InterfaceFrom;
            this.InterfaceTo = InterfaceTo;
            this.payload = payload;
            //ttl = 120;//defaultowa wartość
            ttl = 10;//test
            length = labels.GetLength() + 4 + 4 + 4 + 4 + 4 + 4 + Encoding.UTF8.GetBytes(payload).Length;//po prostu długość
        }
        public byte[] toBytes()
        {
            length = labels.GetLength() + 4 + 4 + 4 + 4 + 4 + 4 + Encoding.UTF8.GetBytes(payload).Length; // odświezanie Length
            List<byte> output = new List<byte>();
            output.AddRange(labels.ToBytes());
            output.AddRange(Source.GetAddressBytes());
            output.AddRange(Destination.GetAddressBytes());
            output.AddRange(InterfaceFrom.GetAddressBytes());
            output.AddRange(InterfaceTo.GetAddressBytes());
            output.AddRange(BitConverter.GetBytes(length));
            output.AddRange(BitConverter.GetBytes(ttl));
            output.AddRange(Encoding.UTF8.GetBytes(payload));
            return output.ToArray();
        }

        public short getTopLabel()//zwraca wartość ostatniego labelka
        {
            if (labels.labels.Count > 0)
                return labels.labels.Peek().Id;
            else return 0;//empty label stack?
        }
        public short popLabel()//zwraca wartość ostatniego labelka !!!I GO USUWA!!! (operacja POP/połowa SWAP)
        {
            if (labels.labels.Count > 0)
            {
                length -= 3;
                return labels.labels.Pop().Id;
            }
            else return 0;//empty label stack?
        }
        public void pushLabel(short id)//Dodaje labelka na szczycie stosu (operacja PUSH/druga połowa SWAPa)
        {
            labels.labels.Push(new Label(id));
            length += 3; //2 bajty labelID, i 1 bajt flagi
        }

        //public bool isEmptyLabelStack()
        //{
        //    if (labels.GetLength() > 0)
        //    {
        //        return false;
        //    }
        //    else return true;
        //}
    }
}
