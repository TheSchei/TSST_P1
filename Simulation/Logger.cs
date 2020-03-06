using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulation
{
    public enum LogType { INFO, ERROR };
    static public class Logger
    {
        public static string Log(string message, LogType type)
        {
            StringBuilder sb = new StringBuilder();
            switch (type)
            {
                case LogType.INFO:
                    sb.Append("INFO::");
                    break;
                case LogType.ERROR:
                    sb.Append("ERROR::");
                    break;
            }
            sb.Append(DateTime.Now.ToString());
            //sb.Append(DateTime.Now.ToString(new System.Globalization.CultureInfo("pl-PL")));
            sb.Append("::");
            sb.Append(message);
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }

}
