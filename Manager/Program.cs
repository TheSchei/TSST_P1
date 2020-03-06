using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Manager
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            if (Args.Length == 1)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try { Application.Run(new Form1(Args[0])); }
                catch (System.ObjectDisposedException) { }
                catch (Exception e) { DialogResult result = MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK); }
            }
        }
    }
}
