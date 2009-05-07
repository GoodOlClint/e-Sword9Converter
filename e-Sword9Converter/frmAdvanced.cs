using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace e_Sword9Converter
{
    public partial class frmAdvanced : Form, IParent
    {
        private frmPassword passwordForm;

        public frmAdvanced()
        {
            InitializeComponent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            this.passwordForm = new frmPassword();
        }

        #region IParent Members

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
                    if (!this.chkSkip.Checked && passwordForm.ShowDialog() == DialogResult.OK)
                    {
                            pass = passwordForm.Password;
                            tried = true;
                    }else { outPassword = ""; return false; }
                    
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
                        if (!this.chkSkip.Checked && passwordForm.ShowDialog() == DialogResult.OK)
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
            if (this.txtSource.Text == "")
            { this.ofdSource.SelectedPath = @"C:\Program Files\e-Sword"; }
            this.ofdSource.ShowNewFolderButton = false;
            if (this.ofdSource.ShowDialog() == DialogResult.OK)
            {
                this.txtSource.Text = ofdSource.SelectedPath;
                this.txtDest.Text = ofdSource.SelectedPath;
                this.txtSource.Text = this.ofdSource.SelectedPath;
                this.grpDest.Enabled = true;
            }
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            this.sfdDest.SelectedPath = this.txtDest.Text;
            if (this.sfdDest.ShowDialog() == DialogResult.OK)
            {
                this.txtDest.Text = this.sfdDest.SelectedPath;
                this.prgMain.Enabled = true;
                this.btnConvert.Enabled = true;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(this.txtSource.Text);
            FileInfo[] files = GetFiles(di, "*.bbl;*.brp;*.cmt;*.dct;*.dev;*.map;*.har;*.not;*.mem;*.ovl;*.prl;*.top;*.lst", ';');
            foreach (FileInfo fi in files)
            {
                string DestPath = fi.FullName.Replace(ConvertFilePath(fi.FullName), this.txtDest.Text) + "x";
                if (this.ValidateSource(fi.FullName))
                {
                    if (!this.ValidateDest(DestPath) && !this.chkOverwrite.Checked)
                    {
                        if (MessageBox.Show(string.Format("{0} Already exists, do you want to overwrite?", DestPath), "File Already Exists", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                        { break; }
                    }
                    Database db;
                    string ext = fi.FullName.Substring(fi.FullName.Length - 4, 4);
                    switch (ext)
                    {
                        case ".bbl":
                            db = new Tables.Bible(this);
                            break;
                        case ".brp":
                            db = new Tables.BibleReadingPlan(this);
                            break;
                        case ".cmt":
                            db = new Tables.Commentary(this);
                            break;
                        case ".dct":
                            db = new Tables.Dictionary(this);
                            break;
                        case ".dev":
                            db = new Tables.Devotion(this);
                            break;
                        case ".map":
                            db = new Tables.Graphic(this);
                            break;
                        case ".har":
                            db = new Tables.Harmony(this);
                            break;
                        case ".not":
                            db = new Tables.Notes(this);
                            break;
                        case ".mem":
                            db = new Tables.Memory(this);
                            break;
                        case ".ovl":
                            db = new Tables.Overlay(this);
                            break;
                        case ".prl":
                            db = new Tables.PrayerRequests(this);
                            break;
                        case ".top":
                            db = new Tables.Topic(this);
                            break;
                        case ".lst":
                            db = new Tables.VerseList(this);
                            break;
                        default:
                            return;
                    }
                    db.SourceDB = fi.FullName;
                    db.DestDB = DestPath;
                    Thread t = new Thread(new ThreadStart(db.ConvertFormat));
                    t.Start();
                    while (t.IsAlive)
                    { Application.DoEvents(); }
                }
            }
        }

        private bool ValidateSource(string path)
        {
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

        private bool ValidateDest(string path)
        { return !File.Exists(path); }

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
            this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% Completed", Percent));
        }

        private string ConvertFilePath(string OldPath)
        { return new FileInfo(OldPath).DirectoryName; }

        private FileInfo[] GetFiles(DirectoryInfo dir, string searchPatterns, params char[] separator)
        {
            List<FileInfo> files = new List<FileInfo>();
            string[] patterns = searchPatterns.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            foreach (string pattern in patterns)
            {
                if (chkSubDir.Checked)
                { files.AddRange(dir.GetFiles(pattern, SearchOption.AllDirectories)); }
                else
                { files.AddRange(dir.GetFiles(pattern)); }
            }
            return files.ToArray();
        }

    }
}