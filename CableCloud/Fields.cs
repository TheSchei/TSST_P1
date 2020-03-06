using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace CableCloud
{
    class Fields
    {
        public List<Field> forwarding = new List<Field>();

        public void AddField(IPAddress interface1, IPAddress interface2, int port1, int port2, string enable)
        {
            if (!exists(interface1, interface2))
                forwarding.Add(new Field(interface1, interface2, port1, port2, enable));
        }
        public int forward(IPAddress interface1, IPAddress interface2)
        {
            //podajesz interfejsy 1 i 2, i zwraca ci odpowiedni port
            //jeśli interfejsy to 1 i 2, port 2
            //jeśli interfejsy to 2 i 1, port 1
            //jeśli nie ma połączenia albo jest disabled to np. 0 albo -1, albo wyjątek, jak kto woli
            foreach (Field f in forwarding)
            {
                if ((f.interface1.Equals(interface1)) && f.interface2.Equals(interface2) && f.enable) return f.port2;
                if ((f.interface1.Equals(interface2)) && f.interface2.Equals(interface1) && f.enable) return f.port1;
            }
            throw new Exception("Inactive or not existing connection, package dropped");
        }
        private bool exists(IPAddress interface1, IPAddress interface2)
        {
            foreach(Field f in forwarding)
            {
                if ((f.interface1.Equals(interface1)) && f.interface2.Equals(interface2)) return true;
                if ((f.interface1.Equals(interface2)) && f.interface2.Equals(interface1)) return true;
            }
            return false;
        }
        public bool deleteField(int i)
        {
            if (forwarding.Count < i) return false;
            forwarding.RemoveAt(i);
            return true;
        }
        public string[] fieldStrings()
        {
            string[] output = new string[forwarding.Count];
            for (int i = 0; i < forwarding.Count; i++)
                output[i] = forwarding[i].getString();
            return output;
        }
    }
    class Field
    {
        public IPAddress interface1;
        public IPAddress interface2;
        public int port1;
        public int port2;
        public bool enable;

        public Field(IPAddress interface1, IPAddress interface2, int port1, int port2, string enable)//możemy zrobić tak i w XMLu w enable będzie ON albo OFF
        {
            this.interface1 = interface1;
            this.interface2 = interface2;
            this.port1 = port1;
            this.port2 = port2;
            if (enable == "ON") this.enable = true;
            else if (enable == "OFF") this.enable = false;
            else throw new FormatException("Wrong format of \"enable\" field.");
        }
        public string getString()
        {
            return interface1.ToString() + " <-> " + interface2.ToString();
        }
        public void reverseStatus()
        {
            enable = !enable;
        }
    }
}
