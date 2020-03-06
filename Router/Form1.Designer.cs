namespace Router
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
                router.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LogBox = new System.Windows.Forms.TextBox();
            this.Refresher = new System.Windows.Forms.Timer(this.components);
            this.MPLSFIBButton = new System.Windows.Forms.RadioButton();
            this.IPFIBButton = new System.Windows.Forms.RadioButton();
            this.FTNButton = new System.Windows.Forms.RadioButton();
            this.NHLFEButton = new System.Windows.Forms.RadioButton();
            this.ILMButton = new System.Windows.Forms.RadioButton();
            this.DisplayTableButton = new System.Windows.Forms.Button();
            this.DataView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).BeginInit();
            this.SuspendLayout();
            // 
            // LogBox
            // 
            this.LogBox.Location = new System.Drawing.Point(16, 15);
            this.LogBox.Margin = new System.Windows.Forms.Padding(4);
            this.LogBox.Multiline = true;
            this.LogBox.Name = "LogBox";
            this.LogBox.ReadOnly = true;
            this.LogBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.LogBox.Size = new System.Drawing.Size(799, 300);
            this.LogBox.TabIndex = 2;
            // 
            // Refresher
            // 
            this.Refresher.Enabled = true;
            this.Refresher.Interval = 300;
            this.Refresher.Tick += new System.EventHandler(this.Refresher_Tick);
            // 
            // MPLSFIBButton
            // 
            this.MPLSFIBButton.AutoSize = true;
            this.MPLSFIBButton.Location = new System.Drawing.Point(35, 360);
            this.MPLSFIBButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MPLSFIBButton.Name = "MPLSFIBButton";
            this.MPLSFIBButton.Size = new System.Drawing.Size(91, 21);
            this.MPLSFIBButton.TabIndex = 3;
            this.MPLSFIBButton.TabStop = true;
            this.MPLSFIBButton.Text = "MPLS-FIB";
            this.MPLSFIBButton.UseVisualStyleBackColor = true;
            // 
            // IPFIBButton
            // 
            this.IPFIBButton.AutoSize = true;
            this.IPFIBButton.Location = new System.Drawing.Point(35, 387);
            this.IPFIBButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.IPFIBButton.Name = "IPFIBButton";
            this.IPFIBButton.Size = new System.Drawing.Size(66, 21);
            this.IPFIBButton.TabIndex = 4;
            this.IPFIBButton.TabStop = true;
            this.IPFIBButton.Text = "IP-FIB";
            this.IPFIBButton.UseVisualStyleBackColor = true;
            // 
            // FTNButton
            // 
            this.FTNButton.AutoSize = true;
            this.FTNButton.Location = new System.Drawing.Point(35, 415);
            this.FTNButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FTNButton.Name = "FTNButton";
            this.FTNButton.Size = new System.Drawing.Size(56, 21);
            this.FTNButton.TabIndex = 5;
            this.FTNButton.TabStop = true;
            this.FTNButton.Text = "FTN";
            this.FTNButton.UseVisualStyleBackColor = true;
            // 
            // NHLFEButton
            // 
            this.NHLFEButton.AutoSize = true;
            this.NHLFEButton.Location = new System.Drawing.Point(35, 443);
            this.NHLFEButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.NHLFEButton.Name = "NHLFEButton";
            this.NHLFEButton.Size = new System.Drawing.Size(74, 21);
            this.NHLFEButton.TabIndex = 6;
            this.NHLFEButton.TabStop = true;
            this.NHLFEButton.Text = "NHLFE";
            this.NHLFEButton.UseVisualStyleBackColor = true;
            // 
            // ILMButton
            // 
            this.ILMButton.AutoSize = true;
            this.ILMButton.Location = new System.Drawing.Point(35, 472);
            this.ILMButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ILMButton.Name = "ILMButton";
            this.ILMButton.Size = new System.Drawing.Size(51, 21);
            this.ILMButton.TabIndex = 7;
            this.ILMButton.TabStop = true;
            this.ILMButton.Text = "ILM";
            this.ILMButton.UseVisualStyleBackColor = true;
            // 
            // DisplayTableButton
            // 
            this.DisplayTableButton.Location = new System.Drawing.Point(158, 387);
            this.DisplayTableButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DisplayTableButton.Name = "DisplayTableButton";
            this.DisplayTableButton.Size = new System.Drawing.Size(151, 78);
            this.DisplayTableButton.TabIndex = 8;
            this.DisplayTableButton.Text = "Display Table";
            this.DisplayTableButton.UseVisualStyleBackColor = true;
            this.DisplayTableButton.Click += new System.EventHandler(this.DisplayTableButton_Click);
            // 
            // DataView
            // 
            this.DataView.AllowUserToAddRows = false;
            this.DataView.AllowUserToDeleteRows = false;
            this.DataView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataView.Location = new System.Drawing.Point(344, 338);
            this.DataView.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DataView.Name = "DataView";
            this.DataView.ReadOnly = true;
            this.DataView.RowTemplate.Height = 24;
            this.DataView.Size = new System.Drawing.Size(459, 170);
            this.DataView.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 543);
            this.Controls.Add(this.DataView);
            this.Controls.Add(this.DisplayTableButton);
            this.Controls.Add(this.ILMButton);
            this.Controls.Add(this.NHLFEButton);
            this.Controls.Add(this.FTNButton);
            this.Controls.Add(this.IPFIBButton);
            this.Controls.Add(this.MPLSFIBButton);
            this.Controls.Add(this.LogBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LogBox;
        private System.Windows.Forms.Timer Refresher;
        private System.Windows.Forms.RadioButton MPLSFIBButton;
        private System.Windows.Forms.RadioButton IPFIBButton;
        private System.Windows.Forms.RadioButton FTNButton;
        private System.Windows.Forms.RadioButton NHLFEButton;
        private System.Windows.Forms.RadioButton ILMButton;
        private System.Windows.Forms.Button DisplayTableButton;
        private System.Windows.Forms.DataGridView DataView;
    }
}

