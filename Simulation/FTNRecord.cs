using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class FTNRecord
    {
        private readonly Label fec;
        private readonly int nextOperationID;

        public Label Fec { get => fec; }
        public int NextOperationID { get => nextOperationID; }

        public FTNRecord(Label fec, int nextOperationID)
        {
            this.fec = fec;
            this.nextOperationID = nextOperationID;
        }

        public FTNRecord(byte[] bytes)
        {
            //Ale konwersja UUU!!!
            short fromBytes = (short)bytes[0];
            fromBytes <<= 8;
            fromBytes += (short)bytes[1];
            this.fec = new Label(fromBytes);
            this.nextOperationID = BitConverter.ToInt32(bytes, 2);
        }

        public byte[] toBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(fec.ToBytes());
            bytes.AddRange(BitConverter.GetBytes(nextOperationID));
            return bytes.ToArray();
        }

    }
}
