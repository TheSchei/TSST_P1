using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Simulation
{
    public class FIBRecordIP
    {
        private readonly IPAddress destIP;
        private readonly IPAddress outInt;

        public IPAddress DestIP
        {
            get => destIP;
        }

        public IPAddress OutInt
        {
            get => outInt;
        }

        public FIBRecordIP(IPAddress destIP, IPAddress outInt)
        {
            this.destIP = destIP;
            this.outInt = outInt;
        }

        public FIBRecordIP(byte[] bytes)
        {
            this.destIP = new IPAddress(new byte[] { bytes[0], bytes[1], bytes[2], bytes[3] });
            this.outInt = new IPAddress(new byte[] { bytes[4], bytes[5], bytes[6], bytes[7] });
        }

        public byte[] toBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(destIP.GetAddressBytes());
            bytes.AddRange(outInt.GetAddressBytes());
            return bytes.ToArray();
        }
    }
}
