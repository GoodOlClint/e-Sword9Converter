using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace eSword9Converter
{
    public partial class frmAdvanced : Form
    {
        private updateStatus CurrentStatus;
        public frmAdvanced()
        {
            Debug.WriteLine("Initializing frmAdvanced");
            InitializeComponent();
            Debug.WriteLine("Registering frmMain event handlers");
            this.Controller_LanguageChangedEvent();
            this.prgMain.MouseHover += new EventHandler(prgMain_MouseHover);
            Controller.StatusChangedEvent += new Controller.StatusChangedEventHandler(Controller_StatusChangedEvent);
            Controller.MaxValueChangedEvent += new Controller.MaxValueChangedEventHandler(Controller_MaxValueChangedEvent);
            Controller.ProgressChangedEvent += new Controller.ProgressChangedEventHandler(Controller_ProgressChangedEvent);
            Controller.ConversionFinishedEvent += new Controller.ConversionFinishedEventHandler(Controller_ConversionFinishedEvent);
            Controller.LanguageChangedEvent += new Controller.LanguageChangedEventHandler(Controller_LanguageChangedEvent);
            Debug.WriteLine("Initializing frmAdvanced Finished");
        }

        void Controller_LanguageChangedEvent()
        {
            Debug.WriteLine("frmPassword.LanguageChangedEvent");
            this.Text = Globalization.CurrentLanguage.AdvancedTitle;
            this.grpDest.Text = Globalization.CurrentLanguage.DestinationDirectory;
            this.grpSource.Text = Globalization.CurrentLanguage.SourceDirectory;
            this.btnConvert.Text = Globalization.CurrentLanguage.Convert;
            this.btnDest.Text = Globalization.CurrentLanguage.Destination;
            this.btnSource.Text = Globalization.CurrentLanguage.Source;
            this.lnkNormal.Text = Globalization.CurrentLanguage.Normal;
            this.chkOverwrite.Text = Globalization.CurrentLanguage.AutomaticallyOverwrite;
            this.chkSkip.Text = Globalization.CurrentLanguage.SkipPasswordProtectedFiles;
            this.chkSubDir.Text = Globalization.CurrentLanguage.IncludeSubdirectories;
            Debug.WriteLine("frmPassword.LanguageChangedEvent Finished");
        }

        void Controller_ConversionFinishedEvent()
        {
            this.Text = Globalization.CurrentLanguage.AdvancedTitle + " " + Globalization.CurrentLanguage.Finished;
            this.grpDest.Enabled = false;
            this.txtDest.Enabled = true;
            this.btnDest.Enabled = true;
            this.txtDest.Text = "";
            this.btnSource.Enabled = true;
            this.txtSource.Enabled = true;
            this.chkOverwrite.Enabled = true;
            this.chkSkip.Enabled = true;
            this.chkSubDir.Enabled = true;
            this.lnkNormal.Enabled = true;
            MessageBox.Show(Globalization.CurrentLanguage.FinishedConverting);
        }

        void Controller_ProgressChangedEvent(object sender, int count)
        {
            this.prgMain.Value = count;
            //Debug.WriteLine("frmAdvanced.prgMain.Value set to: " + count);
            this.Text = string.Format("{0}: {3}% {1} {2}", Globalization.CurrentLanguage.AdvancedTitle, this.CurrentStatus.ToString(), Controller.DB.FileName, (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d));
            //Debug.WriteLine("frmAdvanced.Text set to: " + this.Text);
        }

        void Controller_MaxValueChangedEvent(object sender, int value)
        {
            this.prgMain.Maximum = value;
            Debug.WriteLine("frmAdvanced.prgMain.Maximum set to: " + value);
        }


        void Controller_StatusChangedEvent(object sender, updateStatus status)
        {
            this.CurrentStatus = status;
            Debug.WriteLine("frmAdvanced.CurrentStatus set to: " + status.ToString());
        }


        private void btnSource_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("frmAdvanced.btnSource Clicked");
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
            Debug.WriteLine("frmAdvanced.btnDest Clicked");
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
            Debug.WriteLine("frmAdvanced.btnConvert Clicked");
            this.RunBatch();
        }

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            Debug.WriteLine("frmAdvanced.prgMain Hovered");
            int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
            this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% {1}", Percent, Globalization.CurrentLanguage.Completed));
        }

        private void lnkNormal_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Debug.WriteLine("frmAdvanced.lnkNormal Clicked");
            Controller.SwitchForms();
        }


        private void RunBatch()
        {
            Application.DoEvents();
            this.btnConvert.Enabled = false;
            this.txtDest.Enabled = false;
            this.txtSource.Enabled = false;
            this.btnDest.Enabled = false;
            this.btnSource.Enabled = false;
            this.chkOverwrite.Enabled = false;
            this.chkSkip.Enabled = false;
            this.chkSubDir.Enabled = false;
            this.lnkNormal.Enabled = false;
            DirectoryInfo di = new DirectoryInfo(this.txtSource.Text);
            FileInfo[] files = GetFiles(di, "*.bbl;*.brp;*.cmt;*.dct;*.dev;*.map;*.har;*.not;*.mem;*.ovl;*.prl;*.top;*.lst", ';');
            foreach (FileInfo fi in files)
            {
                if (!fi.Extension.EndsWith("x"))
                {
                    string DestPath = fi.FullName.Replace(ConvertFilePath(fi.FullName), this.txtDest.Text) + "x";
                    FileConversionInfo fci = new FileConversionInfo(fi.FullName, DestPath);
                    Controller.FileNames.Add(fci);
                }
            }
            Controller.SkipPassword = this.chkSkip.Checked;
            Controller.AutomaticallyOverwrite = this.chkOverwrite.Checked;
            Controller.Begin();
        }
        private bool ValidateSource(string path)
        {
            Debug.WriteLine("Checking that " + path + " is a valid access database");
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
                bool ret = (header == "Standard Jet DB");
                Debug.WriteLineIf(!ret, path + " is not a valid Jet Database");
                Debug.WriteLineIf(!ret, "File header is " + header);
                return ret;
            }
            catch (Exception ex) { Error.Record(this, ex); return false; }
        }

        private bool ValidateDest(string path)
        { return !File.Exists(path); }



        private string ConvertFilePath(string OldPath)
        { return new FileInfo(OldPath).DirectoryName; }
        private string GetFileName(string Path)
        { return new FileInfo(Path).Name; }

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
    }
}