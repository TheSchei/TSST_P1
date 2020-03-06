namespace Manager
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.RouterList = new System.Windows.Forms.CheckedListBox();
            this.DeleteIPFIB = new System.Windows.Forms.RadioButton();
            this.setIPFIB = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxE = new System.Windows.Forms.ComboBox();
            this.textBoxD = new System.Windows.Forms.TextBox();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.textBoxB = new System.Windows.Forms.TextBox();
            this.textBoxA = new System.Windows.Forms.TextBox();
            this.labelE = new System.Windows.Forms.Label();
            this.labelD = new System.Windows.Forms.Label();
            this.labelC = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.SendButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.DeleteMPLSFIB = new System.Windows.Forms.RadioButton();
            this.SetFTN = new System.Windows.Forms.RadioButton();
            this.DeleteFTN = new System.Windows.Forms.RadioButton();
            this.SetMPLSFIB = new System.Windows.Forms.RadioButton();
            this.DeleteNHLFE = new System.Windows.Forms.RadioButton();
            this.SetIFN = new System.Windows.Forms.RadioButton();
            this.DeleteIFN = new System.Windows.Forms.RadioButton();
            this.SetNHLFE = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // RouterList
            // 
            this.RouterList.CheckOnClick = true;
            this.RouterList.FormattingEnabled = true;
            this.RouterList.Location = new System.Drawing.Point(13, 13);
            this.RouterList.Name = "RouterList";
            this.RouterList.Size = new System.Drawing.Size(120, 94);
            this.RouterList.TabIndex = 0;
            this.RouterList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.RouterList_ItemCheck);
            // 
            // DeleteIPFIB
            // 
            this.DeleteIPFIB.AutoSize = true;
            this.DeleteIPFIB.Location = new System.Drawing.Point(13, 113);
            this.DeleteIPFIB.Name = "DeleteIPFIB";
            this.DeleteIPFIB.Size = new System.Drawing.Size(88, 17);
            this.DeleteIPFIB.TabIndex = 1;
            this.DeleteIPFIB.TabStop = true;
            this.DeleteIPFIB.Text = "Delete IP-FIB";
            this.DeleteIPFIB.UseVisualStyleBackColor = true;
            this.DeleteIPFIB.CheckedChanged += new System.EventHandler(this.DeleteIPFIB_CheckedChanged);
            // 
            // setIPFIB
            // 
            this.setIPFIB.AutoSize = true;
            this.setIPFIB.Location = new System.Drawing.Point(13, 136);
            this.setIPFIB.Name = "setIPFIB";
            this.setIPFIB.Size = new System.Drawing.Size(71, 17);
            this.setIPFIB.TabIndex = 2;
            this.setIPFIB.TabStop = true;
            this.setIPFIB.Text = "set IP-FIB";
            this.setIPFIB.UseVisualStyleBackColor = true;
            this.setIPFIB.CheckedChanged += new System.EventHandler(this.setIPFIB_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.comboBoxE);
            this.panel1.Controls.Add(this.textBoxD);
            this.panel1.Controls.Add(this.textBoxC);
            this.panel1.Controls.Add(this.textBoxB);
            this.panel1.Controls.Add(this.textBoxA);
            this.panel1.Controls.Add(this.labelE);
            this.panel1.Controls.Add(this.labelD);
            this.panel1.Controls.Add(this.labelC);
            this.panel1.Controls.Add(this.labelB);
            this.panel1.Controls.Add(this.labelA);
            this.panel1.Location = new System.Drawing.Point(7, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(674, 70);
            this.panel1.TabIndex = 3;
            // 
            // comboBoxE
            // 
            this.comboBoxE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxE.FormattingEnabled = true;
            this.comboBoxE.Items.AddRange(new object[] {
            "POP",
            "SWAP",
            "PUSH"});
            this.comboBoxE.Location = new System.Drawing.Point(538, 39);
            this.comboBoxE.Name = "comboBoxE";
            this.comboBoxE.Size = new System.Drawing.Size(127, 21);
            this.comboBoxE.TabIndex = 7;
            this.comboBoxE.Visible = false;
            // 
            // textBoxD
            // 
            this.textBoxD.Location = new System.Drawing.Point(405, 40);
            this.textBoxD.Name = "textBoxD";
            this.textBoxD.Size = new System.Drawing.Size(127, 20);
            this.textBoxD.TabIndex = 6;
            this.textBoxD.Visible = false;
            // 
            // textBoxC
            // 
            this.textBoxC.Location = new System.Drawing.Point(272, 40);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(127, 20);
            this.textBoxC.TabIndex = 6;
            this.textBoxC.Visible = false;
            // 
            // textBoxB
            // 
            this.textBoxB.Location = new System.Drawing.Point(139, 40);
            this.textBoxB.Name = "textBoxB";
            this.textBoxB.Size = new System.Drawing.Size(127, 20);
            this.textBoxB.TabIndex = 6;
            this.textBoxB.Visible = false;
            // 
            // textBoxA
            // 
            this.textBoxA.Location = new System.Drawing.Point(7, 40);
            this.textBoxA.Name = "textBoxA";
            this.textBoxA.Size = new System.Drawing.Size(126, 20);
            this.textBoxA.TabIndex = 5;
            this.textBoxA.Visible = false;
            // 
            // labelE
            // 
            this.labelE.AutoSize = true;
            this.labelE.Location = new System.Drawing.Point(535, 14);
            this.labelE.Name = "labelE";
            this.labelE.Size = new System.Drawing.Size(35, 13);
            this.labelE.TabIndex = 4;
            this.labelE.Text = "label5";
            this.labelE.Visible = false;
            // 
            // labelD
            // 
            this.labelD.AutoSize = true;
            this.labelD.Location = new System.Drawing.Point(402, 14);
            this.labelD.Name = "labelD";
            this.labelD.Size = new System.Drawing.Size(35, 13);
            this.labelD.TabIndex = 3;
            this.labelD.Text = "label4";
            this.labelD.Visible = false;
            // 
            // labelC
            // 
            this.labelC.AutoSize = true;
            this.labelC.Location = new System.Drawing.Point(269, 14);
            this.labelC.Name = "labelC";
            this.labelC.Size = new System.Drawing.Size(35, 13);
            this.labelC.TabIndex = 2;
            this.labelC.Text = "label3";
            this.labelC.Visible = false;
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(136, 14);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(35, 13);
            this.labelB.TabIndex = 1;
            this.labelB.Text = "label2";
            this.labelB.Visible = false;
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Location = new System.Drawing.Point(12, 14);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(35, 13);
            this.labelA.TabIndex = 0;
            this.labelA.Text = "label1";
            this.labelA.Visible = false;
            // 
            // SendButton
            // 
            this.SendButton.Enabled = false;
            this.SendButton.Location = new System.Drawing.Point(687, 368);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(104, 70);
            this.SendButton.TabIndex = 8;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.SystemColors.Info;
            this.logTextBox.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.logTextBox.Location = new System.Drawing.Point(147, 13);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.Size = new System.Drawing.Size(641, 349);
            this.logTextBox.TabIndex = 9;
            // 
            // DeleteMPLSFIB
            // 
            this.DeleteMPLSFIB.AutoSize = true;
            this.DeleteMPLSFIB.Location = new System.Drawing.Point(13, 159);
            this.DeleteMPLSFIB.Name = "DeleteMPLSFIB";
            this.DeleteMPLSFIB.Size = new System.Drawing.Size(107, 17);
            this.DeleteMPLSFIB.TabIndex = 10;
            this.DeleteMPLSFIB.TabStop = true;
            this.DeleteMPLSFIB.Text = "Delete MPLS-FIB";
            this.DeleteMPLSFIB.UseVisualStyleBackColor = true;
            this.DeleteMPLSFIB.CheckedChanged += new System.EventHandler(this.DeleteMPLSFIB_CheckedChanged);
            // 
            // SetFTN
            // 
            this.SetFTN.AutoSize = true;
            this.SetFTN.Location = new System.Drawing.Point(13, 228);
            this.SetFTN.Name = "SetFTN";
            this.SetFTN.Size = new System.Drawing.Size(65, 17);
            this.SetFTN.TabIndex = 13;
            this.SetFTN.TabStop = true;
            this.SetFTN.Text = "Set FTN";
            this.SetFTN.UseVisualStyleBackColor = true;
            this.SetFTN.CheckedChanged += new System.EventHandler(this.SetFTN_CheckedChanged);
            // 
            // DeleteFTN
            // 
            this.DeleteFTN.AutoSize = true;
            this.DeleteFTN.Location = new System.Drawing.Point(13, 205);
            this.DeleteFTN.Name = "DeleteFTN";
            this.DeleteFTN.Size = new System.Drawing.Size(80, 17);
            this.DeleteFTN.TabIndex = 12;
            this.DeleteFTN.TabStop = true;
            this.DeleteFTN.Text = "Delete FTN";
            this.DeleteFTN.UseVisualStyleBackColor = true;
            this.DeleteFTN.CheckedChanged += new System.EventHandler(this.DeleteFTN_CheckedChanged);
            // 
            // SetMPLSFIB
            // 
            this.SetMPLSFIB.AutoSize = true;
            this.SetMPLSFIB.Location = new System.Drawing.Point(13, 182);
            this.SetMPLSFIB.Name = "SetMPLSFIB";
            this.SetMPLSFIB.Size = new System.Drawing.Size(92, 17);
            this.SetMPLSFIB.TabIndex = 11;
            this.SetMPLSFIB.TabStop = true;
            this.SetMPLSFIB.Text = "Set MPLS-FIB";
            this.SetMPLSFIB.UseVisualStyleBackColor = true;
            this.SetMPLSFIB.CheckedChanged += new System.EventHandler(this.SetMPLSFIB_CheckedChanged);
            // 
            // DeleteNHLFE
            // 
            this.DeleteNHLFE.AutoSize = true;
            this.DeleteNHLFE.Location = new System.Drawing.Point(13, 297);
            this.DeleteNHLFE.Name = "DeleteNHLFE";
            this.DeleteNHLFE.Size = new System.Drawing.Size(94, 17);
            this.DeleteNHLFE.TabIndex = 16;
            this.DeleteNHLFE.TabStop = true;
            this.DeleteNHLFE.Text = "Delete NHLFE";
            this.DeleteNHLFE.UseVisualStyleBackColor = true;
            this.DeleteNHLFE.CheckedChanged += new System.EventHandler(this.DeleteNHLFE_CheckedChanged);
            // 
            // SetIFN
            // 
            this.SetIFN.AutoSize = true;
            this.SetIFN.Location = new System.Drawing.Point(13, 274);
            this.SetIFN.Name = "SetIFN";
            this.SetIFN.Size = new System.Drawing.Size(60, 17);
            this.SetIFN.TabIndex = 15;
            this.SetIFN.TabStop = true;
            this.SetIFN.Text = "set ILM";
            this.SetIFN.UseVisualStyleBackColor = true;
            this.SetIFN.CheckedChanged += new System.EventHandler(this.SetIFN_CheckedChanged);
            // 
            // DeleteIFN
            // 
            this.DeleteIFN.AutoSize = true;
            this.DeleteIFN.Location = new System.Drawing.Point(13, 251);
            this.DeleteIFN.Name = "DeleteIFN";
            this.DeleteIFN.Size = new System.Drawing.Size(77, 17);
            this.DeleteIFN.TabIndex = 14;
            this.DeleteIFN.TabStop = true;
            this.DeleteIFN.Text = "Delete ILM";
            this.DeleteIFN.UseVisualStyleBackColor = true;
            this.DeleteIFN.CheckedChanged += new System.EventHandler(this.DeleteIFN_CheckedChanged);
            // 
            // SetNHLFE
            // 
            this.SetNHLFE.AutoSize = true;
            this.SetNHLFE.Location = new System.Drawing.Point(13, 320);
            this.SetNHLFE.Name = "SetNHLFE";
            this.SetNHLFE.Size = new System.Drawing.Size(79, 17);
            this.SetNHLFE.TabIndex = 17;
            this.SetNHLFE.TabStop = true;
            this.SetNHLFE.Text = "Set NHLFE";
            this.SetNHLFE.UseVisualStyleBackColor = true;
            this.SetNHLFE.CheckedChanged += new System.EventHandler(this.SetNHLFE_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.SetNHLFE);
            this.Controls.Add(this.DeleteNHLFE);
            this.Controls.Add(this.SetIFN);
            this.Controls.Add(this.DeleteIFN);
            this.Controls.Add(this.SetFTN);
            this.Controls.Add(this.DeleteFTN);
            this.Controls.Add(this.SetMPLSFIB);
            this.Controls.Add(this.DeleteMPLSFIB);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.setIPFIB);
            this.Controls.Add(this.DeleteIPFIB);
            this.Controls.Add(this.RouterList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox RouterList;
        private System.Windows.Forms.RadioButton DeleteIPFIB;
        private System.Windows.Forms.RadioButton setIPFIB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelE;
        private System.Windows.Forms.Label labelD;
        private System.Windows.Forms.Label labelC;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.TextBox textBoxD;
        private System.Windows.Forms.TextBox textBoxC;
        private System.Windows.Forms.TextBox textBoxB;
        private System.Windows.Forms.TextBox textBoxA;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.RadioButton DeleteMPLSFIB;
        private System.Windows.Forms.RadioButton SetFTN;
        private System.Windows.Forms.RadioButton DeleteFTN;
        private System.Windows.Forms.RadioButton SetMPLSFIB;
        private System.Windows.Forms.RadioButton DeleteNHLFE;
        private System.Windows.Forms.RadioButton SetIFN;
        private System.Windows.Forms.RadioButton DeleteIFN;
        private System.Windows.Forms.RadioButton SetNHLFE;
        private System.Windows.Forms.ComboBox comboBoxE;
    }
}

