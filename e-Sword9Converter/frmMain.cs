using System;
using System.Collections;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace eSword9Converter
{
    public partial class frmMain : Form, IParent
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            passwordForm = new frmPassword();
            advancedForm = new frmAdvanced();
            this.AddOwnedForm(passwordForm);
            this.advancedForm.lnkNormal.Click += new EventHandler(lnkNormal_Click);
            this.advancedForm.FormClosed += new FormClosedEventHandler(advancedForm_FormClosed);
            this.FormClosing += new FormClosingEventHandler(frmMain_FormClosing);
        }

        void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.advancedForm.Dispose();
                this.passwordForm.Dispose();
            }
            catch { }
        }
        #endregion

        object threadLock = new object();
        private int progress;
        updateStatus status;
        public int Progress { get { lock (threadLock) { return progress; } } set { lock (threadLock) { progress = value; } } }
        public updateStatus Status { get { lock (threadLock) { return status; } } set { lock (threadLock) { status = value; } } }
        private frmPassword passwordForm;
        private frmAdvanced advancedForm;

        #region Event Handlers
        void advancedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch { }
        }

        void lnkNormal_Click(object sender, EventArgs e)
        {
            this.Show();
            this.advancedForm.Hide();
        }

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            try
            {
                int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
                this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% {1}", Percent, Globalization.CurrentLanguage.Completed));
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }
        private void btnSource_Click(object sender, EventArgs e)
        {
            if (this.ofdSource.ShowDialog() == DialogResult.OK)
            {
                this.txtSource.Text = this.ofdSource.FileName;
                this.ValidateSource();
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
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
                Thread w = new Thread(new ThreadStart(WatchStatus));
                w.Start();
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }

        private void lnkBatch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            advancedForm.Show();
            this.Hide();
        }
        #endregion

        public bool GetPassword(string path, out string password)
        {
            try
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
            catch (Exception ex) { Error.Record(this, ex); password = ""; return false; }
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
                Error.Log(string.Format(Globalization.CurrentLanguage.PasswordFound, path, password));
                outPassword = password;
                return true;
            }
            catch
            {
                try
                {
                    if (tried)
                    { passwordForm.Text = Globalization.CurrentLanguage.InvalidPassword; }
                    else { passwordForm.Text = Globalization.CurrentLanguage.Password; }
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
                catch (Exception ex) { Error.Record(this, ex); outPassword = password; return false; }
            }
        }

        #region IParent Members
        private delegate void SetMaxValueDelegate(int value, updateStatus Status);
        private delegate void UpdateStatusDelegate();
        private delegate bool GetPasswordDelegate(string path, out string password);

        public void SetMaxValue(int value, updateStatus Status)
        {
            try
            {
                if (this.prgMain.InvokeRequired)
                {
                    this.prgMain.Invoke(new SetMaxValueDelegate(SetMaxValue), value, Status);
                }
                else
                {
                    this.prgMain.Maximum = value + 1;
                    this.Progress = 0;
                    this.lblStatus.Text = Status.ToString();
                    this.Status = Status;
                }
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }

        public void UpdateStatus()
        {
            this.Progress++;
            //if (this.prgMain.InvokeRequired)
            //{
            //    this.prgMain.Invoke(new UpdateStatusDelegate(UpdateStatus));
            //}
            //else
            //{
            //    this.prgMain.Value++;
            //}
        }

        #endregion

        private void ValidateSource()
        {
            try
            {
                if (this.txtSource.Text == "")
                    return;
                if (!File.Exists(this.txtSource.Text))
                { MessageBox.Show("", Globalization.CurrentLanguage.SourceFileNotExist, MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (!ValidSource(this.txtSource.Text))
                { MessageBox.Show("", Globalization.CurrentLanguage.SourceFileInvalid, MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                string ext = this.txtSource.Text.Substring(this.txtSource.Text.Length - 4, 4);
                string path = this.ConvertFilePath(this.txtSource.Text);
                this.ofdDest.FileName = this.txtSource.Text.Replace(ext, ext + "x").Replace(path + @"\", "");
                this.ofdDest.InitialDirectory = path;
                this.grpDest.Enabled = true;
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }

        private bool ValidSource(string path)
        {
            try
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
            catch (Exception ex) { Error.Record(this, ex); return false; }
        }

        private bool ValidDestination(string path)
        {
            try
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
            catch (Exception ex) { Error.Record(this, ex); return false; }
        }

        private void ValidateDestination()
        { this.ValidateDestination(false); }

        private void ValidateDestination(bool ofd)
        {
            try
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
            catch (Exception ex) { Error.Record(this, ex); }
        }

        private void WatchStatus()
        {
            try
            {
                if (this.Status != updateStatus.Finished)
                {
                    this.UpdateProgress();
                    Thread.Sleep(100);
                    WatchStatus();
                }
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }
        void UpdateProgress()
        {
            try
            {
                if (this.prgMain.InvokeRequired)
                {
                    this.prgMain.Invoke(new UpdateStatusDelegate(this.UpdateProgress));
                }
                else
                {
                    this.prgMain.Value = this.progress;
                }
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ofdDest.ShowDialog() == DialogResult.OK)
                {
                    this.txtDest.Text = this.ofdDest.FileName;
                    ValidateDestination(true);
                }
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }
        private string ConvertFilePath(string OldPath)
        {
            try
            {
                return new FileInfo(OldPath).DirectoryName;
            }
            catch (Exception ex) { Error.Record(this, ex); return OldPath; }
        }
        public delegate void ProcessParametersDelegate(object sender, string[] args);
        public void ProcessParameters(object sender, string[] args)
        {
            // The form has loaded, and initialization will have been be done.

            // Add the command-line arguments to our textbox, just to confirm that
            // it reached here.
            if (args != null && args.Length != 0)
            {
                //txtArgs.Text += DateTime.Now.ToString("mm:ss.ff") + " ";
                //for (int i = 0; i < args.Length; i++)
                {
                    //txtArgs.Text += args[i] + " ";
                }
                //txtArgs.Text += "\r\n";
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = Globalization.CurrentLanguage.MainTitle;
            this.grpDest.Text = Globalization.CurrentLanguage.ConvertedFile;
            this.grpSource.Text = Globalization.CurrentLanguage.FileToConvert;
            this.btnConvert.Text = Globalization.CurrentLanguage.Convert;
            this.btnDest.Text = Globalization.CurrentLanguage.Destination;
            this.btnSource.Text = Globalization.CurrentLanguage.Source;
            this.lnkBatch.Text = Globalization.CurrentLanguage.BatchMode;
        }
    }

}