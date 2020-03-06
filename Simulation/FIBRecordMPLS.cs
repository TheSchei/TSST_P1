using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class FIBRecordMPLS
    {
        private readonly IPAddress destIP;
        private readonly Label fec;

        public IPAddress DestIP
        {
            get => destIP;
        }

        public Label Fec
        {
            get => fec;
        }

        public FIBRecordMPLS(IPAddress destIP, Label fec)
        {
            this.destIP = destIP;
            this.fec = fec;
        }

        public FIBRecordMPLS(byte[] bytes)
        {
            this.destIP = new IPAddress(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3]});
            
            //Ale konwersja UUU!!!
            short fromBytes = (short)bytes[4];
            fromBytes <<= 8;
            fromBytes += (short)bytes[5];
            this.fec = new Label(fromBytes);
        }

        public byte[] toBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(destIP.GetAddressBytes());
            bytes.AddRange(fec.ToBytes());
            return bytes.ToArray();
        }
    }
}
