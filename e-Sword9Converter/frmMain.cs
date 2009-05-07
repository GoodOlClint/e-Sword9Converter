using System;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace e_Sword9Converter
{
    public partial class frmMain : Form, IParent
    {
        public frmMain()
        {
            InitializeComponent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            passwordForm = new frmPassword();
            advancedForm = new frmAdvanced();
            this.AddOwnedForm(passwordForm);
            this.advancedForm.lnkNormal.Click += new EventHandler(lnkNormal_Click);
            this.advancedForm.FormClosed += new FormClosedEventHandler(advancedForm_FormClosed);

        }

        void advancedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        void lnkNormal_Click(object sender, EventArgs e)
        {
            this.Show();
            this.advancedForm.Hide();
        }

        private frmPassword passwordForm;
        private frmAdvanced advancedForm;

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
            this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% Completed", Percent));
        }

        public bool GetPassword(string path, out string password)
        {
            password = "";
            if (this.InvokeRequired)
            {
                object[] args = new object[] { path, password };
                bool ret = (bool)this.Invoke(new GetPasswordDelegate(this.GetPassword), args);
                password = (string)args[1];
                return ret;
            }
            else
            {
                return this.GetPassword(path, false, out password);
            }
        }
        public static void OpenDatabase(string Path, string password)
        {
            OleDbConnection odbcCon = new OleDbConnection();
            string str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};".Replace("{file}", Path);
            str = str + "Jet OLEDB:Database Password=\"" + password + "\";";
            odbcCon.ConnectionString = str;
            odbcCon.Open();
            odbcCon.Close();
            odbcCon.Dispose();
        }

        private bool GetPassword(string path, bool tried, out string password) { return this.GetPassword(path, tried, 0, out password); }
        private bool GetPassword(string path, bool tried, int passCount, out string password) { return this.GetPassword(path, tried, passCount, "", out password); }
        private bool GetPassword(string path, bool tried, int passCount, string password, out string outPassword)
        {

            try
            {
                OpenDatabase(path, password);
                outPassword = password;
                return true;
            }
            catch
            {
                if (tried)
                { passwordForm.Text = "Invalid Password"; }
                else { passwordForm.Text = "Password"; }
                string pass = "Password";
                if (!System.IO.File.Exists("Passwords.txt"))
                {
                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        pass = passwordForm.Password;
                        tried = true;
                    }
                    else { outPassword = ""; return false; }
                }
                else
                {
                    StreamReader SR = new StreamReader("Passwords.txt", Encoding.Default);
                    ArrayList passList = new ArrayList();
                    while (!SR.EndOfStream)
                    {
                        passList.Add(SR.ReadLine());
                    }
                    if (passCount >= passList.ToArray().Length)
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            pass = passwordForm.Password;
                            tried = true;
                        }
                        else { outPassword = ""; return false; }
                    }
                    else
                    {
                        pass = (string)passList.ToArray()[passCount];
                    }
                }
                return GetPassword(path, tried, passCount + 1, pass, out outPassword);
            }
        }

        #region IParent Members
        private delegate void SetMaxValueDelegate(int value, updateStatus Status);
        private delegate void UpdateStatusDelegate();
        private delegate bool GetPasswordDelegate(string path, out string password);

        public void SetMaxValue(int value, updateStatus Status)
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new SetMaxValueDelegate(SetMaxValue), value, Status);
            }
            else
            {
                this.prgMain.Maximum = value + 1;
                this.prgMain.Value = 0;
                this.lblStatus.Text = Status.ToString();
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
            }
        }

        #endregion

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (this.ofdSource.ShowDialog() == DialogResult.OK)
            {
                this.txtSource.Text = this.ofdSource.FileName;
                this.ValidateSource();
            }
        }

        private void ValidateSource()
        {
            if (this.txtSource.Text == "")
                return;
            if (!File.Exists(this.txtSource.Text))
            { MessageBox.Show("", "Source file does not exist!", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (!ValidSource(this.txtSource.Text))
            { MessageBox.Show("", "Source file invalid", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string ext = this.txtSource.Text.Substring(this.txtSource.Text.Length - 4, 4);
            string path = this.ConvertFilePath(this.txtSource.Text);
            this.ofdDest.FileName = this.txtSource.Text.Replace(ext, ext + "x").Replace(path + @"\", "");
            this.ofdDest.InitialDirectory = path;
            this.grpDest.Enabled = true;
        }

        private bool ValidSource(string path)
        {
            FileInfo fi = new FileInfo(path);
            switch (fi.Extension)
            {
                case ".bbl":
                    this.ofdDest.Filter = "e-Sword Bible|*.bblx";
                    break;
                case ".brp":
                    this.ofdDest.Filter = "e-Sword Bible Reading Plan|*.brpx";
                    break;
                case ".cmt":
                    this.ofdDest.Filter = "e-Sword Commentary|*.cmtx";
                    break;
                case ".dct":
                    this.ofdDest.Filter = "e-Sword Dictionary|*.dctx";
                    break;
                case ".dev":
                    this.ofdDest.Filter = "e-Sword Devotional|*.devx";
                    break;
                case ".map":
                    this.ofdDest.Filter = "e-Sword Graphics|*.mapx";
                    break;
                case ".har":
                    this.ofdDest.Filter = "e-Sword Gospel Harmony|*.harx";
                    break;
                case ".not":
                    this.ofdDest.Filter = "e-Sword Notes|*.notx";
                    break;
                case ".mem":
                    this.ofdDest.Filter = "e-Sword Memory Verses|*.memx";
                    break;
                case ".ovl":
                    this.ofdDest.Filter = "e-Sword Overlay|*.ovlx";
                    break;
                case ".prl":
                    this.ofdDest.Filter = "e-Sword Prayer Requests|*.prlx";
                    break;
                case ".top":
                    this.ofdDest.Filter = "e-Sword Topic|*.topx";
                    break;
                case ".lst":
                    this.ofdDest.Filter = "e-Sword Verse List|*.lstx";
                    break;
                default:
                    return false;
            }
            FileStream fs = new FileStream(path, FileMode.Open);
            System.IO.BinaryReader br = new BinaryReader(fs);
            long pos = 0;
            bool reading = true;
            string header = "";
            br.BaseStream.Position = 4;
            while (reading)
            {
                header += br.ReadChar();
                pos = fs.Position;
                reading = !(pos >= 19);
            }
            br.Close();
            return (header == "Standard Jet DB");
        }

        private bool ValidDestination(string path)
        {
            FileInfo fi = new FileInfo(path);
            switch (fi.Extension)
            {
                case ".bblx":
                case ".brpx":
                case ".cmtx":
                case ".dctx":
                case ".devx":
                case ".mapx":
                case ".harx":
                case ".notx":
                case ".memx":
                case ".ovlx":
                case ".prlx":
                case ".topx":
                case ".lstx":
                    return true;
                default:
                    return false;
            }
        }

        private void ValidateDestination()
        { this.ValidateDestination(false); }

        private void ValidateDestination(bool ofd)
        {
            if (!ofd && File.Exists(this.txtDest.Text))
            {
                this.txtDest.Text = "";
                this.prgMain.Enabled = false;
                this.btnConvert.Enabled = false;
                return;
            }
            this.prgMain.Enabled = true;
            this.btnConvert.Enabled = true;
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            if (this.ofdDest.ShowDialog() == DialogResult.OK)
            {
                this.txtDest.Text = this.ofdDest.FileName;
                ValidateDestination(true);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            this.ValidateSource();
            Database db;
            string ext = this.txtDest.Text.Substring(this.txtDest.Text.Length - 5, 5);
            switch (ext)
            {
                case ".bblx":
                    db = new Tables.Bible(this);
                    break;
                case ".brpx":
                    db = new Tables.BibleReadingPlan(this);
                    break;
                case ".cmtx":
                    db = new Tables.Commentary(this);
                    break;
                case ".dctx":
                    db = new Tables.Dictionary(this);
                    break;
                case ".devx":
                    db = new Tables.Devotion(this);
                    break;
                case ".mapx":
                    db = new Tables.Graphic(this);
                    break;
                case ".harx":
                    db = new Tables.Harmony(this);
                    break;
                case ".notx":
                    db = new Tables.Notes(this);
                    break;
                case ".memx":
                    db = new Tables.Memory(this);
                    break;
                case ".ovlx":
                    db = new Tables.Overlay(this);
                    break;
                case ".prlx":
                    db = new Tables.PrayerRequests(this);
                    break;
                case ".topx":
                    db = new Tables.Topic(this);
                    break;
                case ".lstx":
                    db = new Tables.VerseList(this);
                    break;
                default:
                    return;
            }
            db.DestDB = this.txtDest.Text;
            db.SourceDB = this.txtSource.Text;
            Thread t = new Thread(new ThreadStart(db.ConvertFormat));
            t.Start();

        }

        private void lnkBatch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            advancedForm.Show();
        }


        private string ConvertFilePath(string OldPath)
        {
            return new FileInfo(OldPath).DirectoryName;
        }
    }
}