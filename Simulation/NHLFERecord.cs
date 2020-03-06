using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Simulation
{
    public enum Operation : byte { POP, PUSH, SWAP };

    public class NHLFERecord
    {
        private readonly int nextOperationID;
        private readonly Operation operation;
        private readonly Label outLabel;
        private readonly IPAddress outInt;
        private readonly int nextOperation;

        public int NextOperationID { get => nextOperationID; }
        public Operation Operation { get => operation; }
        public Label OutLabel { get => outLabel; }
        public IPAddress OutInt { get => outInt; }
        public int NextOperation { get => nextOperation; }

        public NHLFERecord(int nextOperationID, Operation operation, Label outLabel, IPAddress outInt, int nextOperation)
        {
            this.nextOperationID = nextOperationID;
            this.operation = operation;
            this.outLabel = outLabel;
            this.outInt = outInt;
            this.nextOperation = nextOperation;
        }

        public NHLFERecord(byte[] bytes)
        {
            this.nextOperationID = BitConverter.ToInt32(bytes, 0);
            //Ciekawe czy to przejdzie
            this.operation = (Operation)bytes[4];

            //Ale konwersja UUU!!!
            short fromBytes = (short)bytes[6];
            fromBytes <<= 8;
            fromBytes += (short)bytes[5];
            this.outLabel = new Label(fromBytes);
            outInt = new IPAddress(new byte[] { bytes[7], bytes[8], bytes[9], bytes[10] } );
            nextOperation = BitConverter.ToInt32(bytes, 11);
        }

        public byte[] toBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(BitConverter.GetBytes(nextOperationID));
            bytes.Add((byte)operation);
            bytes.AddRange(outLabel.ToBytes());
            bytes.AddRange(outInt.GetAddressBytes());
            bytes.AddRange(BitConverter.GetBytes(nextOperation));
            return bytes.ToArray();
        }
    }
}
