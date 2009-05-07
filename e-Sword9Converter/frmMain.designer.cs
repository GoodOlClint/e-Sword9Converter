namespace e_Sword9Converter
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSource = new System.Windows.Forms.Button();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.grpDest = new System.Windows.Forms.GroupBox();
            this.btnDest = new System.Windows.Forms.Button();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.prgMain = new System.Windows.Forms.ProgressBar();
            this.btnConvert = new System.Windows.Forms.Button();
            this.ofdDest = new System.Windows.Forms.SaveFileDialog();
            this.ofdSource = new System.Windows.Forms.OpenFileDialog();
            this.lnkBatch = new System.Windows.Forms.LinkLabel();
            this.txtSource = new AutoComplete.AutoCompleteTextBox();
            this.grpSource.SuspendLayout();
            this.grpDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSource
            // 
            this.btnSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSource.Location = new System.Drawing.Point(323, 16);
            this.btnSource.Name = "btnSource";
            this.btnSource.Size = new System.Drawing.Size(75, 23);
            this.btnSource.TabIndex = 1;
            this.btnSource.Text = "&Source";
            this.btnSource.UseVisualStyleBackColor = true;
            this.btnSource.Click += new System.EventHandler(this.btnSource_Click);
            // 
            // grpSource
            // 
            this.grpSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSource.Controls.Add(this.txtSource);
            this.grpSource.Controls.Add(this.btnSource);
            this.grpSource.Location = new System.Drawing.Point(12, 12);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(404, 45);
            this.grpSource.TabIndex = 4;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "File to Convert";
            // 
            // grpDest
            // 
            this.grpDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDest.Controls.Add(this.btnDest);
            this.grpDest.Controls.Add(this.txtDest);
            this.grpDest.Enabled = false;
            this.grpDest.Location = new System.Drawing.Point(12, 63);
            this.grpDest.Name = "grpDest";
            this.grpDest.Size = new System.Drawing.Size(404, 45);
            this.grpDest.TabIndex = 5;
            this.grpDest.TabStop = false;
            this.grpDest.Text = "Converted File";
            // 
            // btnDest
            // 
            this.btnDest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDest.Location = new System.Drawing.Point(323, 16);
            this.btnDest.Name = "btnDest";
            this.btnDest.Size = new System.Drawing.Size(75, 23);
            this.btnDest.TabIndex = 1;
            this.btnDest.Text = "&Destination";
            this.btnDest.UseVisualStyleBackColor = true;
            this.btnDest.Click += new System.EventHandler(this.btnDest_Click);
            // 
            // txtDest
            // 
            this.txtDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDest.Location = new System.Drawing.Point(6, 18);
            this.txtDest.Name = "txtDest";
            this.txtDest.Size = new System.Drawing.Size(311, 20);
            this.txtDest.TabIndex = 0;
            // 
            // prgMain
            // 
            this.prgMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgMain.Enabled = false;
            this.prgMain.Location = new System.Drawing.Point(18, 113);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(311, 23);
            this.prgMain.TabIndex = 6;
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(335, 113);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "&Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // ofdDest
            // 
            this.ofdDest.Filter = "Bibles|*.bblx|Commentaries|*.cmtx|Dictionaries|*.dctx|Harmonies|*.harx|Topic Note" +
                "s|*.topx|Verse Lists|*.lstx|Graphics|*.mapx|Notes|*.notx";
            // 
            // ofdSource
            // 
            this.ofdSource.Filter = "All e-Sword Modules|*.bbl;*.cmt;*.dct;*.har;*.top;*.lst;*.map;*.not|Bibles|*.bbl|" +
                "Commentaries|*.cmt|Dictionaries|*.dct|Harmonies|*.har|Topic Notes|*.top|Verse Li" +
                "sts|*.lst|Graphics|*.map|Notes|*.not";
            // 
            // lnkBatch
            // 
            this.lnkBatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkBatch.AutoSize = true;
            this.lnkBatch.Location = new System.Drawing.Point(345, 139);
            this.lnkBatch.Name = "lnkBatch";
            this.lnkBatch.Size = new System.Drawing.Size(65, 13);
            this.lnkBatch.TabIndex = 7;
            this.lnkBatch.TabStop = true;
            this.lnkBatch.Text = "Batch Mode";
            // 
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtSource.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtSource.Location = new System.Drawing.Point(6, 16);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(311, 20);
            this.txtSource.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 161);
            this.Controls.Add(this.lnkBatch);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.prgMain);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.grpSource);
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e-Sword 9 Converter";
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.grpDest.ResumeLayout(false);
            this.grpDest.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.GroupBox grpDest;
        private System.Windows.Forms.Button btnDest;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.ProgressBar prgMain;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.SaveFileDialog ofdDest;
        private System.Windows.Forms.OpenFileDialog ofdSource;
        private System.Windows.Forms.LinkLabel lnkBatch;
        private AutoComplete.AutoCompleteTextBox txtSource;
    }
}

