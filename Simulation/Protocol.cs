using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    /*
     * Dodałem sporo tych opcji, nie musimy wszystkich implementować, traktujcie to bardziej jak miejsce
     * gdzie możecie rzucać pomysły co taki manager mógłby móc.
     * 
     * Przede wszystkim nie przywiązujcie się do getterów, bo w moim zamyśle wszystkie tabele miałyby
     * się wyświetlać w poszczególnych routerach.
     * 
     * Start i Stop też tak rzuciłem bo dlaczego nie, właściwie byłoby to nawet proste w implementacji :p
     * 
     * Najważniejsze to będą przwdopodobnie Settery i DeleteById (te bez ById miałyby rzekomo dopasowywać się po parametrach, 
     * i usuwać odpowiednie wpisy :p komu by się chciało to robić)
     * 
     * W pakiecie MPLS prawdopodobnie wszystkie będą miały podobną formę
     * SetXXXX <kol1> <kol2> ... <kolN> gdzie każda kolumna ma raczej stały rozmiar (int albo short), więc największy problem będzie pewnie z GUI :p
     * DeleteXXXXbyId <Id> no tutaj to się nie ma co rozwodzić, 5 bajtów wiadomości (payload'u)
     * 
     * No i oczywiście giiiiigantyczne drzewko decyzji w routerze, ale to się jebnie w jakiejś oddzielnej metodzie 
     * gdzieś na dole klasu, gdzie i tak nikt nigdy nie zajrzy.
     * 
     * Fajnie się pisze takie konetarze, zwłąszcza ze świadomością, że i tak żaden z was tego nie przeczyta. Poza tym 
     * tak dużo tekstu sprawia wrażenie jakbym się na tym znał, a ja nawet nie wiem jak zrobić specyfikację funkcjonalną
     * modemu DSL :( ...
     * 
     * Jeszcze push mi nie działa....
     */
    public enum ControlParam : byte {
        GetIPFIB,
        EditIPFIB,
        SetIPFIB,
        DeleteIPFIB,
        DeleteIPFIBbyId,
        GetMPLSFIB,
        EditMPLSFIB,
        SetMPLSFIB,
        DeleteMPLSFIB,
        DeleteMPLSFIBbyId,
        GetFTN,
        EditFTN,
        SetFTN,
        DeleteFTN,
        DeleteFTNbyId,
        GetNHLFE,
        EditNHLFE,
        SetNHLFE,
        DeleteNHLFE,
        DeleteNHLFEbyId,
        GetIFN,
        EditIFN,
        SetIFN,
        DeleteIFN,
        DeleteIFNbyId,
        Stop,
        Start
    };
    public enum ControlResponse : byte {
        OK,
        ERROR,
        OutOfIndex,
        NotFound,
        AlreadyExists,
        UnknownError
    };

    public static class Protocol
    {
        public static string CreateMessage(ControlParam param, byte[] payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(payload);
            return Encoding.ASCII.GetString(message.ToArray());
        }
        public static string CreateMessage(ControlParam param, int payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(BitConverter.GetBytes(payload));
            return Encoding.ASCII.GetString(message.ToArray());
        }
        public static byte[] CreateResponse(ControlResponse param, byte[] payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(payload);
            return message.ToArray();
        }
        public static byte[] CreateResponse(ControlResponse param, string payload)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            message.AddRange(Encoding.ASCII.GetBytes(payload));
            return message.ToArray();
        }
        public static byte[] CreateResponse(ControlResponse param)
        {
            List<byte> message = new List<byte>();
            message.Add((byte)param);
            return message.ToArray();
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
        public static ControlParam getControlParam(byte[] payload)
        {
            return (ControlParam)payload[0];
        }
        public static ControlParam getControlParam(string payload)
        {
            return (ControlParam)Encoding.ASCII.GetBytes(payload)[0];
        }
        public static ControlResponse getControlResponse(byte[] payload)
        {
            return (ControlResponse)payload[0];
        }
        public static ControlResponse getControlResponse(string payload)
        {
            return (ControlResponse)Encoding.ASCII.GetBytes(payload)[0];
        }
        public static ControlParam getControlParamAndDelete(ref byte[] payload)
        {
            ControlParam output = (ControlParam)payload[0];
            payload = DeleteControlParam(payload);
            return output;
        }
        public static ControlResponse getResponseParamAndDelete(ref string payload)
        {
            ControlResponse output = (ControlResponse)Encoding.ASCII.GetBytes(payload)[0];
            payload = DeleteParam(payload);
            return output;
        }
    }
}
