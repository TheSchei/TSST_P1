using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation;

namespace Host
{
    public partial class Form1 : Form
    {
        private Host host;

        public Form1(string filePath)
        {
            try
            {
                host = new Host(filePath);
                InitializeComponent();
                InfoBox.Text = "Host: " + host.logicIP.ToString();//do wpisania
                this.Text = host.HostName;
                this.Name = host.HostName;
                DestinationBox.Items.AddRange(host.remoteHostIPs.ToArray());
                //DestinationBox.SetItemChecked(0, true);
                LogBox.Text += Logger.Log("Host started working", LogType.INFO);
            }
            catch (System.IO.FileNotFoundException e)
            {
                DialogResult result = MessageBox.Show(e.Message, "Failed to start application", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void DestinationBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < DestinationBox.Items.Count; ++ix)
                if (ix != e.Index) DestinationBox.SetItemChecked(ix, false);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (host.sendMesage(MessageTextBox.Text, System.Net.IPAddress.Parse(DestinationBox.GetItemText(DestinationBox.SelectedItem))))
                    MessageTextBox.Clear();
            }
            catch(System.FormatException ex)
            {
                host.messageQueue.Enqueue(Logger.Log(ex.Message, LogType.ERROR));
            }
        }

        private void Refresher_Tick(object sender, EventArgs e)
        {
            while (host.messageQueue.Count > 0)
            {
                LogBox.AppendText(host.messageQueue.Dequeue());
            }
        }
    }
}
