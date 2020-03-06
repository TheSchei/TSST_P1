using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class ILMRecord
    {
        private readonly IPAddress intFrom;
        private readonly Label incLabel;
        private readonly LabelStack poppedLabelStack;
        private readonly int nextOperID;

        public IPAddress IntFrom { get => intFrom; }
        public Label IncLabel { get => incLabel; }
        public LabelStack PoppedLabelStack { get => poppedLabelStack; }
        public int NextOperID { get => nextOperID; }

        public ILMRecord(IPAddress intFrom, Label incLabel, LabelStack poppedLabelStack, int nextOperID)
        {
            this.intFrom = intFrom;
            this.incLabel = incLabel;
            this.poppedLabelStack = poppedLabelStack;
            this.nextOperID = nextOperID;
        }
        public ILMRecord(byte[] bytes)
        {
            poppedLabelStack = LabelStack.FromBytes(bytes);
            int i = poppedLabelStack.GetLength();
            intFrom = new IPAddress(new byte[] { bytes[i], bytes[i + 1], bytes[i + 2], bytes[i + 3] });
            nextOperID = BitConverter.ToInt32(bytes, i + 4);
            incLabel = new Label((short)((bytes[i + 9] << 8) + bytes[i + 8]));
        }
        public byte[] toBytes()
        {
            List<byte> bytes = new List<byte>();
            bytes.AddRange(poppedLabelStack.ToBytes());
            bytes.AddRange(intFrom.GetAddressBytes());
            bytes.AddRange(BitConverter.GetBytes(nextOperID));
            bytes.AddRange(IncLabel.ToBytes());
            return bytes.ToArray();
        }
    }
}
