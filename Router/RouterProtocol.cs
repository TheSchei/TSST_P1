using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Router
{
    public enum ControlParamRouter : byte
    {
        ShowMPLSFIB,
        ShowMPLSIP,
        ShowFTN,
        ShowNHLFE,
        ShowILM
    };

    public enum ControlResponseRouter : byte
    {
        OK,
        ERROR,
        OutOfIndex,
        NotFound,
        AlreadyExists,
        UnknownError
    };

    public static class RouterProtocol
    {
        public static string CreateMessage(ControlParamRouter param, byte[] payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(payload);
            return Encoding.ASCII.GetString(message.ToArray());
        }
        public static string CreateMessage(ControlParamRouter param, int payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(BitConverter.GetBytes(payload));
            return Encoding.ASCII.GetString(message.ToArray());
        }
        public static byte[] DeleteControlParam(byte[] payload)
        {
            List<byte> temp = payload.ToList();
            temp.RemoveAt(0);
            return temp.ToArray();
        }
        public static string DeleteParam(string payload)
        {
            List<byte> temp = Encoding.ASCII.GetBytes(payload).ToList();
            temp.RemoveAt(0);
            return Encoding.ASCII.GetString(temp.ToArray());
        }
        
    }
}
