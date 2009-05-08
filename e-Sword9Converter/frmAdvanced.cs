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
        object threadLock = new object();
        private int progress;
        updateStatus status;
        public int Progress { get { lock (threadLock) { return progress; } } set { lock (threadLock) { progress = value; } } }
        public updateStatus Status { get { lock (threadLock) { return status; } } set { lock (threadLock) { status = value; } } }

        private Database DB;
        public frmAdvanced()
        {
            InitializeComponent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            this.passwordForm = new frmPassword();
            this.FormClosing += new FormClosingEventHandler(frmAdvanced_FormClosing);
        }

        void frmAdvanced_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DB.Running)
                DB.Stop();
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
                    this.passwordForm = new frmPassword();
                    this.AddOwnedForm(passwordForm);
                    if (!this.chkSkip.Checked && passwordForm.ShowDialog() == DialogResult.OK)
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
                        this.passwordForm = new frmPassword();
                        this.AddOwnedForm(passwordForm);
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
                if (Status != updateStatus.Finishing)
                { this.prgMain.Maximum = value + 1; }
                this.progress = 0;
                this.Status = Status;
            }
        }

        public void UpdateStatus()
        { this.Progress++; }

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
                    string ext = fi.FullName.Substring(fi.FullName.Length - 4, 4);
                    switch (ext)
                    {
                        case ".bbl":
                            DB = new Tables.Bible(this);
                            break;
                        case ".brp":
                            DB = new Tables.BibleReadingPlan(this);
                            break;
                        case ".cmt":
                            DB = new Tables.Commentary(this);
                            break;
                        case ".dct":
                            DB = new Tables.Dictionary(this);
                            break;
                        case ".dev":
                            DB = new Tables.Devotion(this);
                            break;
                        case ".map":
                            DB = new Tables.Graphic(this);
                            break;
                        case ".har":
                            DB = new Tables.Harmony(this);
                            break;
                        case ".not":
                            DB = new Tables.Notes(this);
                            break;
                        case ".mem":
                            DB = new Tables.Memory(this);
                            break;
                        case ".ovl":
                            DB = new Tables.Overlay(this);
                            break;
                        case ".prl":
                            DB = new Tables.PrayerRequests(this);
                            break;
                        case ".top":
                            DB = new Tables.Topic(this);
                            break;
                        case ".lst":
                            DB = new Tables.VerseList(this);
                            break;
                        default:
                            return;
                    }
                    DB.SourceDB = fi.FullName;
                    DB.DestDB = DestPath;
                    Thread t = new Thread(new ThreadStart(DB.ConvertFormat));
                    t.Start();
                    Thread w = new Thread(new ThreadStart(WatchStatus));
                    w.Start();
                    while (DB.Running)
                    { Application.DoEvents(); }
                    DB.Clear();
                }
            }
            this.Text = "e-Sword 9 Converter: Batch Mode: Finished";
        }

        private bool ValidateSource(string path)
        {
            try
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
            catch (Exception ex) { Error.Record(this, ex); return false; }
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
            try
            {
                string[] patterns = searchPatterns.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string pattern in patterns)
                {
                    if (chkSubDir.Checked)
                    { files.AddRange(dir.GetFiles(pattern, SearchOption.AllDirectories)); }
                    else
                    { files.AddRange(dir.GetFiles(pattern)); }
                }
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
            return files.ToArray();
        }

        private void WatchStatus()
        {
            if (DB.Running)
            {
                this.UpdateProgress();
                Thread.Sleep(100);
                WatchStatus();
            }
        }
        void UpdateProgress()
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new UpdateStatusDelegate(this.UpdateProgress));
            }
            else
            {
                if (DB.Running)
                {
                    if (this.Progress > this.prgMain.Maximum)
                    { this.prgMain.Value = this.prgMain.Maximum; Error.Record(this, new Exception("Progress exceded max allowed")); }
                    else
                    { this.prgMain.Value = this.Progress; }
                    FileInfo fi = new FileInfo(DB.FileName);
                    this.Text = string.Format("e-Sword 9 Converter: Batch Mode: {2}% {0} {1}", Status.ToString(), DB.FileName.Replace(fi.DirectoryName + @"\", ""), (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d));
                    Application.DoEvents();
                }
                else
                {
                    this.prgMain.Value = 0;
                    this.prgMain.Maximum = 100;
                    this.Text = "e-Sword 9 Converter: Batch Mode Finished";
                }
            }
        }
    }
}