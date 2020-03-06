using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Simulation;
using System.Net;

namespace Manager
{
    public partial class Form1 : Form
    {
        private Manager manager;
        public Form1(string filepath)
        {
            manager = new Manager(filepath);
            InitializeComponent();
            Text = "Manager";
            Name = "Manager";
            RouterList.Items.AddRange(manager.getIPs());
        }

        private void RouterList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < RouterList.Items.Count; ++ix)
                if (ix != e.Index) RouterList.SetItemChecked(ix, false);
        }

        private void DeleteIPFIB_CheckedChanged(object sender, EventArgs e)
        {
            if(DeleteIPFIB.Checked) showControlPanel(ControlParam.DeleteIPFIBbyId);
        }

        private void setIPFIB_CheckedChanged(object sender, EventArgs e)
        {
            if (setIPFIB.Checked) showControlPanel(ControlParam.SetIPFIB);
        }

        private void showControlPanel(ControlParam x)
        {
            SendButton.Enabled = true;
            hideAll();
            switch (x)
            {
                case ControlParam.DeleteIPFIBbyId:
                    labelA.Visible = true;
                    textBoxA.Visible = true;
                    labelA.Text = "Id";
                    break;
                case ControlParam.SetIPFIB:
                    labelA.Visible = true;
                    labelB.Visible = true;
                    textBoxA.Visible = true;
                    textBoxB.Visible = true;
                    textBoxA.Clear();
                    textBoxB.Clear();
                    labelA.Text = "Destination";
                    labelB.Text = "Interface Out";
                    break;
                case ControlParam.DeleteMPLSFIBbyId:
                    labelA.Visible = true;
                    textBoxA.Visible = true;
                    labelA.Text = "Id";
                    textBoxA.Clear();
                    break;
                case ControlParam.SetMPLSFIB:
                    labelA.Visible = true;
                    labelB.Visible = true;
                    textBoxA.Visible = true;
                    textBoxB.Visible = true;
                    textBoxA.Clear();
                    textBoxB.Clear();
                    labelA.Text = "Destination";
                    labelB.Text = "Label";
                    break;
                case ControlParam.DeleteFTNbyId:
                    labelA.Visible = true;
                    textBoxA.Visible = true;
                    labelA.Text = "Id";
                    textBoxA.Clear();
                    break;
                case ControlParam.SetFTN:
                    labelA.Visible = true;
                    labelB.Visible = true;
                    textBoxA.Visible = true;
                    textBoxB.Visible = true;
                    textBoxA.Clear();
                    textBoxB.Clear();
                    labelA.Text = "Label";
                    labelB.Text = "Next Operation";
                    break;
                case ControlParam.DeleteIFNbyId:
                    labelA.Visible = true;
                    textBoxA.Visible = true;
                    labelA.Text = "Id";
                    textBoxA.Clear();
                    break;
                case ControlParam.SetIFN:
                    labelA.Visible = true;
                    labelB.Visible = true;
                    labelC.Visible = true;
                    labelD.Visible = true;
                    textBoxA.Visible = true;
                    textBoxB.Visible = true;
                    textBoxC.Visible = true;
                    textBoxD.Visible = true;
                    textBoxA.Clear();
                    textBoxB.Clear();
                    textBoxC.Clear();
                    textBoxD.Clear();
                    labelA.Text = "Interface In";
                    labelB.Text = "Label";
                    labelC.Text = "popped Labels";
                    labelD.Text = "Next Operation";
                    break;
                case ControlParam.DeleteNHLFEbyId:
                    labelA.Visible = true;
                    textBoxA.Visible = true;
                    labelA.Text = "Id";
                    textBoxA.Clear();
                    break;
                case ControlParam.SetNHLFE:
                    labelA.Visible = true;
                    labelB.Visible = true;
                    labelC.Visible = true;
                    labelD.Visible = true;
                    labelE.Visible = true;
                    textBoxA.Visible = true;
                    textBoxB.Visible = true;
                    textBoxC.Visible = true;
                    textBoxD.Visible = true;
                    comboBoxE.Visible = true;
                    textBoxA.Clear();
                    textBoxB.Clear();
                    textBoxC.Clear();
                    textBoxD.Clear();
                    labelA.Text = "Operation Id";
                    labelB.Text = "outLabel";
                    labelC.Text = "Interface Out";
                    labelD.Text = "Next Operation";
                    labelE.Text = "Operation";
                    break;
            }
        }
        private void hideAll()
        {
            textBoxA.Visible = false;
            textBoxB.Visible = false;
            textBoxC.Visible = false;
            textBoxD.Visible = false;
            comboBoxE.Visible = false;
            labelA.Visible = false;
            labelB.Visible = false;
            labelC.Visible = false;
            labelD.Visible = false;
            labelE.Visible = false;
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(RouterList.GetItemText(RouterList.SelectedItem));
                     if (DeleteIPFIB.Checked)   logTextBox.AppendText(manager.DeleteByID(ip, Convert.ToInt32(textBoxA.Text), ControlParam.DeleteIPFIBbyId));
                else if (DeleteMPLSFIB.Checked) logTextBox.AppendText(manager.DeleteByID(ip, Convert.ToInt32(textBoxA.Text), ControlParam.DeleteMPLSFIBbyId));
                else if (DeleteFTN.Checked)     logTextBox.AppendText(manager.DeleteByID(ip, Convert.ToInt32(textBoxA.Text), ControlParam.DeleteFTNbyId));
                else if (DeleteIFN.Checked)     logTextBox.AppendText(manager.DeleteByID(ip, Convert.ToInt32(textBoxA.Text), ControlParam.DeleteIFNbyId));
                else if (DeleteNHLFE.Checked)   logTextBox.AppendText(manager.DeleteByID(ip, Convert.ToInt32(textBoxA.Text), ControlParam.DeleteNHLFEbyId));

                else if (setIPFIB.Checked)   logTextBox.AppendText(manager.setIPFIB(ip, IPAddress.Parse(textBoxA.Text), IPAddress.Parse(textBoxB.Text)));
                else if (SetMPLSFIB.Checked) logTextBox.AppendText(manager.setMPLSFIB(ip, IPAddress.Parse(textBoxA.Text), Convert.ToInt16(textBoxB.Text)));
                else if (SetFTN.Checked)     logTextBox.AppendText(manager.setFTN(ip, Convert.ToInt16(textBoxA.Text), Convert.ToInt32(textBoxB.Text)));
                else if (SetIFN.Checked)     logTextBox.AppendText(manager.setILM(ip, IPAddress.Parse(textBoxA.Text), Convert.ToInt16(textBoxB.Text), textBoxC.Text, Convert.ToInt32(textBoxD.Text)));
                else if (SetNHLFE.Checked)   logTextBox.AppendText(manager.setNHLFE(ip, Convert.ToInt32(textBoxA.Text), comboBoxE.Text, textBoxB.Text, textBoxC.Text, textBoxD.Text));
            }
            catch(Exception ex)
            {
                logTextBox.AppendText(Logger.Log(ex.Message, LogType.ERROR));
            }
        }

        private void DeleteMPLSFIB_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteMPLSFIB.Checked) showControlPanel(ControlParam.DeleteMPLSFIBbyId);
        }

        private void SetMPLSFIB_CheckedChanged(object sender, EventArgs e)
        {
            if (SetMPLSFIB.Checked) showControlPanel(ControlParam.SetMPLSFIB);
        }

        private void DeleteFTN_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteFTN.Checked) showControlPanel(ControlParam.DeleteFTNbyId);
        }

        private void SetFTN_CheckedChanged(object sender, EventArgs e)
        {
            if (SetFTN.Checked) showControlPanel(ControlParam.SetFTN);
        }

        private void DeleteIFN_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteIFN.Checked) showControlPanel(ControlParam.DeleteIFNbyId);
        }

        private void SetIFN_CheckedChanged(object sender, EventArgs e)
        {
            if (SetIFN.Checked) showControlPanel(ControlParam.SetIFN);
        }

        private void DeleteNHLFE_CheckedChanged(object sender, EventArgs e)
        {
            if (DeleteNHLFE.Checked) showControlPanel(ControlParam.DeleteNHLFEbyId);
        }

        private void SetNHLFE_CheckedChanged(object sender, EventArgs e)
        {
            if (SetNHLFE.Checked) showControlPanel(ControlParam.SetNHLFE);
        }
    }
}
