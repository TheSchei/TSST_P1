using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class Label
    {
        private short id;
        public Label(short id) { this.id = id; }

        public short Id { get => id; set => id = value; }

        public bool Equals(Label x) { return id == x.id; }
        public byte[] ToBytes()//bez przesady
        {
            return BitConverter.GetBytes(id);
        }
    }
}
