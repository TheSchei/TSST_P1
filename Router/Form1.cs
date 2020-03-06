using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Simulation;

namespace Router
{
    public partial class Form1 : Form
    {
        private Router router;
       
        public Form1(string filePath)
        {
            try
            {
                router = new Router(filePath);
                InitializeComponent();
                this.Text = router.RouterName;
                LogBox.Text += Logger.Log("Router started working", LogType.INFO);
            }
            catch (System.IO.FileNotFoundException e)
            {
                DialogResult result = MessageBox.Show(e.Message, "Failed to start application", MessageBoxButtons.OK);
                this.Close();
            }
        }

        private void Refresher_Tick(object sender, EventArgs e)
        {
            while (router.messageQueue.Count > 0)
            {
                LogBox.AppendText(router.messageQueue.Dequeue());
            }
        }

        private void MPLSFIB_CheckedChanged(object sender, EventArgs e)
        {
            if (MPLSFIBButton.Checked) ShowTable(ControlParamRouter.ShowMPLSFIB);
        }
        private void MPLSIP_CheckedChanged(object sender, EventArgs e)
        {
            if (IPFIBButton.Checked) ShowTable(ControlParamRouter.ShowMPLSIP);
        }
        private void FTN_CheckedChanged(object sender, EventArgs e)
        {
            if (FTNButton.Checked) ShowTable(ControlParamRouter.ShowFTN);
        }
        private void NHLFE_CheckedChanged(object sender, EventArgs e)
        {
            if (NHLFEButton.Checked) ShowTable(ControlParamRouter.ShowNHLFE);
        }
        private void ILM_CheckedChanged(object sender, EventArgs e)
        {
            if (ILMButton.Checked) ShowTable(ControlParamRouter.ShowILM);
        }
        private void ShowTable(ControlParamRouter x)
        {
            DisplayTableButton.Enabled = true;
            hideAll();
            DataView.AutoResizeColumns();
            switch (x)
            {
                case ControlParamRouter.ShowMPLSFIB:
                    SetupMPLSFIBDataGridView();
                    break;
                case ControlParamRouter.ShowMPLSIP:
                    SetupIPFIBDataGridView();
                    break;
                case ControlParamRouter.ShowFTN:
                    SetupFTNDataGridView();
                    break;
                case ControlParamRouter.ShowNHLFE:
                    SetupNHLFEDataGridView();
                    break;
                case ControlParamRouter.ShowILM:
                    SetupDataILMGridView();
                    break;         
            }
        }
        private void hideAll()
        {
            DataView.Visible = false;
        }

        public void SetupMPLSFIBDataGridView()
        {
            this.Controls.Add(DataView);
            DataView.ColumnCount = 2;
            DataView.Name = "MPLS-FIB";

            DataView.Columns[0].Name = "destIP";
            DataView.Columns[1].Name = "FEC";
            for(int i=0; i<router.PubFIBTableMPLS.Count; i++)
            {
                FIBRecordMPLS FIBMPLSRow = new FIBRecordMPLS(router.PubFIBTableMPLS.ElementAt(i).DestIP, router.PubFIBTableMPLS.ElementAt(i).Fec);
                string[] row = { FIBMPLSRow.DestIP.ToString(), FIBMPLSRow.Fec.Id.ToString() };
                DataView.Rows.Add(row);
            }

            DataView.Show();
        }

        public void SetupIPFIBDataGridView()
        {
            this.Controls.Add(DataView);
            DataView.ColumnCount = 2;
            DataView.Name = "IP-FIB";

            DataView.Columns[0].Name = "destIP";
            DataView.Columns[1].Name = "outInt";
            for (int i = 0; i < router.PubFIBTableIP.Count; i++)
            {
                FIBRecordIP FIBIPRow = new FIBRecordIP(router.PubFIBTableIP.ElementAt(i).DestIP, router.PubFIBTableIP.ElementAt(i).OutInt);
                string[] row = { FIBIPRow.DestIP.ToString(), FIBIPRow.OutInt.ToString() };
                DataView.Rows.Add(row);
            }

            DataView.Show();
        }

        public void SetupFTNDataGridView()
        {
            this.Controls.Add(DataView);
            DataView.ColumnCount = 2;
            DataView.Name = "FTN";

            DataView.Columns[0].Name = "FEC";
            DataView.Columns[1].Name = "nextOperationID";
            for (int i = 0; i < router.PubFTNTable.Count; i++)
            {
                FTNRecord FTNRow = new FTNRecord(router.PubFTNTable.ElementAt(i).Fec, router.PubFTNTable.ElementAt(i).NextOperationID);
                string[] row = { FTNRow.Fec.Id.ToString(), FTNRow.NextOperationID.ToString() };
                DataView.Rows.Add(row);
            }

            DataView.Show();
        }

        public void SetupNHLFEDataGridView()
        {
            this.Controls.Add(DataView);
            DataView.ColumnCount = 5;
            DataView.Name = "NHLFE";

            DataView.Columns[0].Name = "NextOperationID";
            DataView.Columns[1].Name = "Operation";
            DataView.Columns[2].Name = "outLabel";
            DataView.Columns[3].Name = "outInt";
            DataView.Columns[4].Name = "NextOperation";
            for (int i = 0; i < router.PubNHLFETable.Count; i++)
            {
                NHLFERecord NHLFERow = new NHLFERecord(router.PubNHLFETable.ElementAt(i).NextOperationID, 
                                                       router.PubNHLFETable.ElementAt(i).Operation, 
                                                       router.PubNHLFETable.ElementAt(i).OutLabel, 
                                                       router.PubNHLFETable.ElementAt(i).OutInt, 
                                                       router.PubNHLFETable.ElementAt(i).NextOperation);
                string[] row = 
                {
                                NHLFERow.NextOperationID.ToString(),
                                NHLFERow.Operation.ToString(),
                                NHLFERow.OutLabel.Id.ToString(),
                                NHLFERow.OutInt.ToString(),
                                NHLFERow.NextOperation.ToString()
                };
                DataView.Rows.Add(row);
            }

            DataView.Show();
        }

        public void SetupDataILMGridView()
        {
            this.Controls.Add(DataView);
            DataView.ColumnCount = 4;
            DataView.Name = "ILM";
            

            DataView.Columns[0].Name = "intFrom";
            DataView.Columns[1].Name = "incLabel";
            DataView.Columns[2].Name = "poppedLabel";
            DataView.Columns[3].Name = "nextOperationID";
            for (int i = 0; i < router.PubILMTable.Count; i++)
            {
                ILMRecord ILMRow = new ILMRecord(router.PubILMTable.ElementAt(i).IntFrom,
                                                 router.PubILMTable.ElementAt(i).IncLabel,
                                                 router.PubILMTable.ElementAt(i).PoppedLabelStack,
                                                 router.PubILMTable.ElementAt(i).NextOperID
                                                 );
                string[] row = 
                {
                                ILMRow.IntFrom.ToString(),
                                ILMRow.IncLabel.Id.ToString(),
                                ILMRow.PoppedLabelStack.ToStrings(),
                                ILMRow.NextOperID.ToString()
                };
                DataView.Rows.Add(row);

            }

            DataView.Show();
        }

        private void DisplayTableButton_Click(object sender, EventArgs e)
        {
            DataView.Columns.Clear();
            try
            {
                if (MPLSFIBButton.Checked) SetupMPLSFIBDataGridView();
                else if (IPFIBButton.Checked) SetupIPFIBDataGridView();
                else if (NHLFEButton.Checked) SetupNHLFEDataGridView();
                else if (FTNButton.Checked) SetupFTNDataGridView();
                else if (ILMButton.Checked) SetupDataILMGridView();
            }
            catch (Exception ex)
            {
                LogBox.AppendText(Logger.Log(ex.Message, LogType.ERROR));
            }
        }
        
    }
}
