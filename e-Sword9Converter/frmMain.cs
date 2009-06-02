using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using eSword9Converter.Globalization;

namespace eSword9Converter
{
    public partial class frmMain : Form
    {

        #region Constructor
        public frmMain()
        {
            try
            {
                Debug.WriteLine("Initializing frmMain");
                InitializeComponent();
                this.Controller_LanguageChangedEvent();
                Debug.WriteLine("Registering frmMain event handlers");
                this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
                Controller.StatusChangedEvent += new Controller.StatusChangedEventHandler(Controller_StatusChangedEvent);
                Controller.MaxValueChangedEvent += new Controller.MaxValueChangedEventHandler(Controller_MaxValueChangedEvent);
                Controller.ProgressChangedEvent += new Controller.ProgressChangedEventHandler(Controller_ProgressChangedEvent);
                Controller.LanguageChangedEvent += new Controller.LanguageChangedEventHandler(Controller_LanguageChangedEvent);
                Controller.ConversionFinishedEvent += new Controller.ConversionFinishedEventHandler(Controller_ConversionFinishedEvent);
                Debug.WriteLine("Initializing frmMain Finished");
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        #endregion

        #region Event Handlers

        void Controller_ConversionFinishedEvent()
        {
            try
            {
                this.grpDest.Enabled = false;
                this.txtDest.Text = "";
                this.txtSource.Text = "";
                this.lblStatus.Text = Globalization.CurrentLanguage.Finished;
                this.btnConvert.Enabled = false;
                this.prgMain.Value = 0;
                this.prgMain.Maximum = 100;
                MessageBox.Show(Globalization.CurrentLanguage.FinishedConverting);
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }


        void Controller_LanguageChangedEvent()
        {
            try
            {
                this.Text = Globalization.CurrentLanguage.MainTitle;
                this.grpDest.Text = Globalization.CurrentLanguage.ConvertedFile;
                this.grpSource.Text = Globalization.CurrentLanguage.FileToConvert;
                this.btnConvert.Text = Globalization.CurrentLanguage.Convert;
                this.btnDest.Text = Globalization.CurrentLanguage.Destination;
                this.btnSource.Text = Globalization.CurrentLanguage.Source;
                this.lnkBatch.Text = Globalization.CurrentLanguage.BatchMode;
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        void Controller_ProgressChangedEvent(object sender, int count)
        {
            try
            {
                if (this.prgMain.InvokeRequired)
                {
                    this.prgMain.Invoke(new Controller.ProgressChangedEventHandler(this.Controller_ProgressChangedEvent), new object[] { sender, count });
                }
                else
                {
                    //Debug.WriteLine("frmMain.prgMain.Value set to: " + count);
                    this.prgMain.Value = count;
                }
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        void Controller_MaxValueChangedEvent(object sender, int value)
        {
            try
            {
                if (this.prgMain.InvokeRequired)
                {
                    this.prgMain.Invoke(new Controller.MaxValueChangedEventHandler(this.Controller_MaxValueChangedEvent), new object[] { sender, value });
                }
                else
                {
                    Debug.WriteLine("frmMain.prgMain.Maximum set to: " + value);
                    this.prgMain.Maximum = value;
                }
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        void Controller_StatusChangedEvent(object sender, updateStatus status)
        {
            try
            {
                if (this.lblStatus.InvokeRequired)
                {
                    this.lblStatus.Invoke(new Controller.StatusChangedEventHandler(this.Controller_StatusChangedEvent), new object[] { sender, status });
                }
                else
                {
                    Debug.WriteLine("frmMain.lblStatus.Text set to: " + status.ToString());
                    this.lblStatus.Text = status.ToString();
                }
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            Debug.WriteLine("frmMain.prgMain hovered");
            try
            {
                int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
                this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% {1}", Percent, Globalization.CurrentLanguage.Completed));
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("frmMain.btnSource clicked");
            try
            {
                if (this.ofdSource.ShowDialog() == DialogResult.OK)
                {
                    this.txtSource.Text = this.ofdSource.FileName;
                    this.ValidateSource();
                }
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("frmMain.btnConvert clicked");
            try
            {
                FileConversionInfo FCI = new FileConversionInfo(this.txtSource.Text, this.txtDest.Text);
                Controller.FileNames.Add(FCI);
                Controller.CurrentForm = this;
                Controller.AutomaticallyOverwrite = true;
                Controller.Begin();
            }
            catch (Exception ex)
            { Error.Record(this, ex); }
        }

        private void lnkBatch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine("frmMain.lnkBatch clicked");
            Controller.SwitchForms();
        }

        private void btnDest_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("frmMain.btnDest clicked");
            try
            {
                if (this.ofdDest.ShowDialog() == DialogResult.OK)
                {
                    this.txtDest.Text = this.ofdDest.FileName;
                    this.prgMain.Enabled = true;
                    this.btnConvert.Enabled = true;
                }
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }
        #endregion

        object threadLock = new object();

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
                switch (fi.Extension.ToLower())
                {
                    case ".bbl":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.bblx", CurrentLanguage.eSword, CurrentLanguage.Bible);
                        break;
                    case ".brp":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.brpx", CurrentLanguage.eSword, CurrentLanguage.BibleReadingPlan);
                        break;
                    case ".cmt":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.cmtx", CurrentLanguage.eSword, CurrentLanguage.Commentary);
                        break;
                    case ".dct":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.dctx", CurrentLanguage.eSword, CurrentLanguage.Dictionary);
                        break;
                    case ".dev":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.devx", CurrentLanguage.eSword, CurrentLanguage.Devotional);
                        break;
                    case ".map":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.mapx", CurrentLanguage.eSword, CurrentLanguage.Graphics);
                        break;
                    case ".har":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.harx", CurrentLanguage.eSword, CurrentLanguage.Harmony);
                        break;
                    case ".not":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.notx", CurrentLanguage.eSword, CurrentLanguage.Notes);
                        break;
                    case ".mem":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.memx", CurrentLanguage.eSword, CurrentLanguage.MemoryVerses);
                        break;
                    case ".ovl":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.ovlx", CurrentLanguage.eSword, CurrentLanguage.Overlay);
                        break;
                    case ".prl":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.prlx", CurrentLanguage.eSword, CurrentLanguage.PrayerRequests);
                        break;
                    case ".top":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.topx", CurrentLanguage.eSword, CurrentLanguage.TopicNotes);
                        break;
                    case ".lst":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.lstx", CurrentLanguage.eSword, CurrentLanguage.VerseLists);
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
                switch (fi.Extension.ToLower())
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

        //private void ValidateDestination()
        //{ this.ValidateDestination(false); }

        //private void ValidateDestination(bool ofd)
        //{
        //    try
        //    {
        //        if (!ofd && File.Exists(this.txtDest.Text))
        //        {
        //            this.txtDest.Text = "";
        //            this.prgMain.Enabled = false;
        //            this.btnConvert.Enabled = false;
        //            return;
        //        }
        //        this.prgMain.Enabled = true;
        //        this.btnConvert.Enabled = true;
        //    }
        //    catch (Exception ex) { Error.Record(this, ex); }
        //}



        private string ConvertFilePath(string OldPath)
        {
            try
            { return new FileInfo(OldPath).DirectoryName; }
            catch (Exception ex) { Error.Record(this, ex); return OldPath; }
        }
    }

}