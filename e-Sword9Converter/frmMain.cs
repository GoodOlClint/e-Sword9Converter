using System;
using System.IO;
using System.Windows.Forms;

namespace eSword9Converter
{
    public partial class frmMain : Form
    {
        #region Constructor
        public frmMain()
        {
            InitializeComponent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            Controller.StatusChangedEvent += new Controller.StatusChangedEventHandler(Controller_StatusChangedEvent);
            Controller.MaxValueChangedEvent += new Controller.MaxValueChangedEventHandler(Controller_MaxValueChangedEvent);
            Controller.ProgressChangedEvent += new Controller.ProgressChangedEventHandler(Controller_ProgressChangedEvent);
            Controller.LanguageChangedEvent += new Controller.LanguageChangedEventHandler(Controller_LanguageChangedEvent);
            Controller.ConversionFinishedEvent += new Controller.ConversionFinishedEventHandler(Controller_ConversionFinishedEvent);
        }

        #endregion

        #region Event Handlers

        void Controller_ConversionFinishedEvent()
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


        void Controller_LanguageChangedEvent()
        {
            this.Text = Globalization.CurrentLanguage.MainTitle;
            this.grpDest.Text = Globalization.CurrentLanguage.ConvertedFile;
            this.grpSource.Text = Globalization.CurrentLanguage.FileToConvert;
            this.btnConvert.Text = Globalization.CurrentLanguage.Convert;
            this.btnDest.Text = Globalization.CurrentLanguage.Destination;
            this.btnSource.Text = Globalization.CurrentLanguage.Source;
            this.lnkBatch.Text = Globalization.CurrentLanguage.BatchMode;
        }

        void Controller_ProgressChangedEvent(object sender, int count)
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new Controller.ProgressChangedEventHandler(this.Controller_ProgressChangedEvent), new object[] { sender, count });
            }
            else
            {
                this.prgMain.Value = count;
            }
        }

        void Controller_MaxValueChangedEvent(object sender, int value)
        {
            if (this.prgMain.InvokeRequired)
            {
                this.prgMain.Invoke(new Controller.MaxValueChangedEventHandler(this.Controller_MaxValueChangedEvent), new object[] { sender, value });
            }
            else
            {
                this.prgMain.Maximum = value;
            }
        }

        void Controller_StatusChangedEvent(object sender, updateStatus status)
        {
            if (this.lblStatus.InvokeRequired)
            {
                this.lblStatus.Invoke(new Controller.StatusChangedEventHandler(this.Controller_StatusChangedEvent), new object[] { sender, status });
            }
            else
            {
                this.lblStatus.Text = status.ToString();
            }
        }

        void lnkNormal_Click(object sender, EventArgs e)
        { Controller.SwitchForms(); }

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
            FileConversionInfo FCI = new FileConversionInfo(this.txtSource.Text, this.txtDest.Text);
            Controller.FileNames.Add(FCI);
            Controller.CurrentForm = this;
            Controller.AutomaticallyOverwrite = true;
            Controller.Begin();
        }

        private void lnkBatch_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { Controller.SwitchForms(); }

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
                switch (fi.Extension)
                {
                    case ".bbl":
                        this.ofdDest.Filter = string.Format("{0} {1}|*.bblx", Globalization.CurrentLanguage.eSword, Globalization.CurrentLanguage.Bible);
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



        private string ConvertFilePath(string OldPath)
        {
            try
            { return new FileInfo(OldPath).DirectoryName; }
            catch (Exception ex) { Error.Record(this, ex); return OldPath; }
        }
    }

}