namespace eSword9Converter
{
    partial class frmAdvanced
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
            this.components = new System.ComponentModel.Container();
            this.lnkNormal = new System.Windows.Forms.LinkLabel();
            this.btnDest = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.prgMain = new System.Windows.Forms.ProgressBar();
            this.btnSource = new System.Windows.Forms.Button();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.grpDest = new System.Windows.Forms.GroupBox();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.chkSkip = new System.Windows.Forms.CheckBox();
            this.ofdSource = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdDest = new System.Windows.Forms.FolderBrowserDialog();
            this.chkOverwrite = new System.Windows.Forms.CheckBox();
            this.chkSubDir = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpDest.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // lnkNormal
            // 
            this.lnkNormal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkNormal.AutoSize = true;
            this.lnkNormal.Location = new System.Drawing.Point(370, 173);
            this.lnkNormal.Name = "lnkNormal";
            this.lnkNormal.Size = new System.Drawing.Size(40, 13);
            this.lnkNormal.TabIndex = 12;
            this.lnkNormal.TabStop = true;
            this.lnkNormal.Text = "Normal";
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
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(335, 137);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 8;
            this.btnConvert.Text = "&Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // prgMain
            // 
            this.prgMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prgMain.Enabled = false;
            this.prgMain.Location = new System.Drawing.Point(18, 137);
            this.prgMain.Name = "prgMain";
            this.prgMain.Size = new System.Drawing.Size(311, 23);
            this.prgMain.TabIndex = 11;
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
            // txtSource
            // 
            this.txtSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSource.Location = new System.Drawing.Point(6, 18);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(311, 20);
            this.txtSource.TabIndex = 0;
            // 
            // grpDest
            // 
            this.grpDest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDest.Controls.Add(this.btnDest);
            this.grpDest.Controls.Add(this.txtDest);
            this.grpDest.Enabled = false;
            this.grpDest.Location = new System.Drawing.Point(12, 86);
            this.grpDest.Name = "grpDest";
            this.grpDest.Size = new System.Drawing.Size(404, 45);
            this.grpDest.TabIndex = 10;
            this.grpDest.TabStop = false;
            this.grpDest.Text = "Destination Directory";
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
            // grpSource
            // 
            this.grpSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSource.Controls.Add(this.btnSource);
            this.grpSource.Controls.Add(this.txtSource);
            this.grpSource.Location = new System.Drawing.Point(12, 12);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(404, 45);
            this.grpSource.TabIndex = 9;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Source Directory";
            // 
            // chkSkip
            // 
            this.chkSkip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSkip.AutoSize = true;
            this.chkSkip.Location = new System.Drawing.Point(18, 171);
            this.chkSkip.Name = "chkSkip";
            this.chkSkip.Size = new System.Drawing.Size(169, 17);
            this.chkSkip.TabIndex = 13;
            this.chkSkip.Text = "Skip Password Protected Files";
            this.chkSkip.UseVisualStyleBackColor = true;
            // 
            // chkOverwrite
            // 
            this.chkOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkOverwrite.AutoSize = true;
            this.chkOverwrite.Location = new System.Drawing.Point(193, 172);
            this.chkOverwrite.Name = "chkOverwrite";
            this.chkOverwrite.Size = new System.Drawing.Size(136, 17);
            this.chkOverwrite.TabIndex = 14;
            this.chkOverwrite.Text = "Automatically Overwrite";
            this.chkOverwrite.UseVisualStyleBackColor = true;
            // 
            // chkSubDir
            // 
            this.chkSubDir.AutoSize = true;
            this.chkSubDir.Location = new System.Drawing.Point(18, 63);
            this.chkSubDir.Name = "chkSubDir";
            this.chkSubDir.Size = new System.Drawing.Size(131, 17);
            this.chkSubDir.TabIndex = 15;
            this.chkSubDir.Text = "Include Subdirectories";
            this.chkSubDir.UseVisualStyleBackColor = true;
            // 
            // frmAdvanced
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 198);
            this.Controls.Add(this.chkSubDir);
            this.Controls.Add(this.chkOverwrite);
            this.Controls.Add(this.chkSkip);
            this.Controls.Add(this.lnkNormal);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.prgMain);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.grpSource);
            this.MaximizeBox = false;
            this.Name = "frmAdvanced";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "e-Sword 9 Converter: Batch Mode";
            this.Load += new System.EventHandler(this.frmAdvanced_Load);
            this.grpDest.ResumeLayout(false);
            this.grpDest.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDest;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ProgressBar prgMain;
        private System.Windows.Forms.Button btnSource;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.GroupBox grpDest;
        private System.Windows.Forms.TextBox txtDest;
        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.CheckBox chkSkip;
        private System.Windows.Forms.FolderBrowserDialog ofdSource;
        private System.Windows.Forms.FolderBrowserDialog sfdDest;
        private System.Windows.Forms.CheckBox chkOverwrite;
        private System.Windows.Forms.CheckBox chkSubDir;
        public System.Windows.Forms.LinkLabel lnkNormal;
        private System.Windows.Forms.ToolTip toolTip;


    }
}