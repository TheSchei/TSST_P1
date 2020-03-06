using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public class LabelStack
    {
        public Stack<Label> labels = new Stack<Label>();

        public LabelStack(){ }
        public LabelStack(string[] stack)
        {
            if (stack[0] != "")
                foreach (string label in stack)
                    if (label != "0") labels.Push(new Label(Convert.ToInt16(label)));
        }
        public byte[] ToBytes()
        {
            byte[] output = new byte[GetLength()];
            if (GetLength() == 1) output[0] = 0x00;//jeśli nie ma żadnego labelka, to ustawia na zerowym bajcie flagę końca i zwraca tylko ten bajt
            else//a jeśli nie
            {
                for (int i = 0; i < labels.Count; i++)//po każdej labelce
                {
                    output[3 * i] = 0xff;//ustawia flagę, coś tu jeszcze jest
                    output[3 * i + 1] = labels.ElementAt(i).ToBytes()[0];//i konwertuje shorta na bajta
                    output[3 * i + 2] = labels.ElementAt(i).ToBytes()[1];//i drugi bajt
                }
                output[GetLength() - 1] = 0x00;//na ostatnim miejscu flaga końca
            }
            return output;
        }
        public static LabelStack FromBytes(byte[] bytes)// zwraca NULL, jeśli stos jest błędny
        {
            LabelStack stack = new LabelStack();
            int i = 0;
            try
            {
                while (bytes[i] != 0x00)//wykonuje się aż nie napotka na flagę końca
                {
                    stack.labels.Push(new Label((short)((bytes[i + 2] << 8) + bytes[i + 1])));//konwertuje 2 bajty na shorta
                    i += 3;//idzie do kolejnej flagi
                }
            }
            catch
            {
                stack = null;//jeśli coś pójdzie nie tak, czyli np. coś jest nie tak, to ustawia nulla do zwrotu, choć mógłby rzucać po prostu wyjątek
            }
            return stack;
            /*LabelStack output = new LabelStack();
            while (stack.labels.Count != 0)
            {
                output.labels.Push(stack.labels.Pop());
            }
            return output;*/
        }
        public int GetLength()//zwraca bajtową długość labelków, czyli (1 + ilość labelek * 3) - 1 to flaga końca, a 3 to 2 bajty na labelkę i 1 na flagę "tu coś jeszcze jest"
        {
            return (1 + labels.Count * 3);//flag + label&flag
        }

        public bool Equals(LabelStack stack)
        {
            //return labels.Equals(stack);
            if (GetLength() == 1 && stack.GetLength() == 1) return true;
            else if (GetLength() != stack.GetLength()) return false;
            else
            {
                for (int i = 0; i < labels.Count; i++)
                {
                    if (this.labels.ElementAt(i).Id != stack.labels.ElementAt(i).Id) return false;
                }
                return true;
            }
        }

        public string ToStrings()
        {
            string labelstack = "";
            foreach(Label label in labels)
            {
                label.ToString();
                labelstack += label.Id.ToString();
            }
            return labelstack;
        }

    }
}
