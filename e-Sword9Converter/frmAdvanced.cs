using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace e_Sword9Converter
{
    public partial class frmAdvanced : Form, IParent
    {
        public frmAdvanced()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(frmAdvanced_FormClosing);
        }

        void frmAdvanced_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Owner.Show();
        }

        #region IParent Members

        public bool GetPassword(string path, out string password) { return this.GetPassword(path, false, out password); }
        public bool GetPassword(string path, bool tried, out string password) { return this.GetPassword(path, tried, 0, out password); }
        public bool GetPassword(string path, bool tried, int passCount, out string password) { return this.GetPassword(path, tried, passCount, "", out password); }
        public bool GetPassword(string path, bool tried, int passCount, string password, out string outPassword)
        {
            outPassword = "";
            return true;
        }

        private delegate void SetMaxValueDelegate(int value, updateStatus Status);
        private delegate void UpdateStatusDelegate();
        private delegate void GetPasswordDelegate(string path);

        public void SetMaxValue(int value, updateStatus Status)
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new SetMaxValueDelegate(SetMaxValue), value);
            }
            else
            {
                this.prgMain.Maximum += value;
            }
        }

        public void UpdateStatus()
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new UpdateStatusDelegate(UpdateStatus));
            }
            else
            {
                this.prgMain.Value++;
                //this.Text = String.Format("e-Sword 9 Converter {0}% Completed", ((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100);
                //this.toolTip.SetToolTip(this.prgMain, String.Format("{0}%", ((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d));
            }
        }

        #endregion
    }
}