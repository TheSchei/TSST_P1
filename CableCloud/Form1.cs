using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CableCloud
{
    public partial class Form1 : Form
    {
        CableCloud Cloud;
        public Form1(string filePath)
        {
            InitializeComponent();
            Cloud = new CableCloud(filePath);
            CableViewer.Items.AddRange(Cloud.fields.fieldStrings().ToArray());
            for (int i = 0; i < Cloud.fields.forwarding.Count; i++)
                if (Cloud.fields.forwarding[i].enable) CableViewer.SetItemChecked(i, true);
            CableViewer.CheckOnClick = true;
        }

        private void Refresher_Tick(object sender, EventArgs e)
        {
            while (Cloud.messageQueue.Count > 0)
            {
                LogBox.AppendText(Cloud.messageQueue.Dequeue());
            }
        }

        private void CableViewer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cloud.fields.forwarding[((CheckedListBox)sender).SelectedIndex].reverseStatus();
        }
    }
}
