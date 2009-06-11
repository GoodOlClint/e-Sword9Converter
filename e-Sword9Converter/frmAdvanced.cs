/*
 * Copyright (c) 2009, GoodOlClint All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * Redistributions of source code must retain the above copyright notice, this list of conditions
 * and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 * and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * Neither the name of the e-Sword Users nor the names of its contributors may be used to endorse
 * or promote products derived from this software without specific prior written permission.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using eSword9Converter.Globalization;

namespace eSword9Converter
{
    public partial class frmAdvanced : Form, IForm
    {
        private string CurrentStatus;
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

        public void Controller_LanguageChangedEvent()
        {
            Debug.WriteLine("frmPassword.LanguageChangedEvent");
            this.Text = CurrentLanguage.AdvancedTitle;
            this.grpDest.Text = CurrentLanguage.DestinationDirectory;
            this.grpSource.Text = CurrentLanguage.SourceDirectory;
            this.btnConvert.Text = CurrentLanguage.Convert;
            this.btnDest.Text = CurrentLanguage.Destination;
            this.btnSource.Text = CurrentLanguage.Source;
            this.lnkNormal.Text = CurrentLanguage.Normal;
            this.chkOverwrite.Text = CurrentLanguage.AutomaticallyOverwrite;
            this.chkSkip.Text = CurrentLanguage.SkipPasswordProtectedFiles;
            this.chkSubDir.Text = CurrentLanguage.IncludeSubdirectories;
            this.chkMirror.Text = CurrentLanguage.MirrorDirectoryStructure;
            Debug.WriteLine("frmPassword.LanguageChangedEvent Finished");
        }

        public void Controller_ConversionFinishedEvent(bool error)
        {
            if (!error)
            {
                this.Text = CurrentLanguage.AdvancedTitle + " " + CurrentLanguage.Finished;
                MessageBox.Show(CurrentLanguage.FinishedConverting);
            }
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
            this.chkMirror.Enabled = this.chkSubDir.Checked;
            
        }

        void Controller_ProgressChangedEvent(object sender, int count)
        {
            this.prgMain.Value = count;
            this.Text = string.Format("{0}: {3}% {1} {2}", CurrentLanguage.AdvancedTitle, this.CurrentStatus, Controller.DB.FileName, (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d));
        }

        void Controller_MaxValueChangedEvent(object sender, int value)
        {
            this.prgMain.Maximum = value;
            this.prgMain.Value = 0;
            Debug.WriteLine("frmAdvanced.prgMain.Maximum set to: " + value);
        }


        void Controller_StatusChangedEvent(object sender, updateStatus status)
        {
            switch (status)
            {
                case updateStatus.Loading:
                    this.CurrentStatus = CurrentLanguage.Loading;
                    break;
                case updateStatus.Converting:
                    this.CurrentStatus = CurrentLanguage.Converting;
                    break;
                case updateStatus.Saving:
                    this.CurrentStatus = CurrentLanguage.Saving;
                    break;
                case updateStatus.Optimizing:
                    this.CurrentStatus = CurrentLanguage.Optimizing;
                    break;
                case updateStatus.Finished:
                    this.CurrentStatus = CurrentLanguage.Finished;
                    break;
            }
            Debug.WriteLine("frmAdvanced.CurrentStatus set to: " + status.ToString());
        }


        private void btnSource_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("frmAdvanced.btnSource Clicked");
            if (this.txtSource.Text == "")
            { this.ofdSource.SelectedPath = Controller.eSwordFolder; }
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
            this.chkMirror.Enabled = false;
            DirectoryInfo di = new DirectoryInfo(this.txtSource.Text);
            string validExtension = "*.bbl;*.brp;*.cmt;*.dct;*.dev;*.map;*.har;*.not;*.mem;*.ovl;*.prl;*.top;*.lst";
            FileInfo[] files = GetFiles(di, validExtension, ';');
            foreach (FileInfo fi in files)
            {
                if (!validExtension.Contains(fi.Extension.ToLower()))
                {
                    string DestPath;
                    if (this.chkMirror.Checked)
                    { DestPath = fi.FullName.Replace(this.txtSource.Text, this.txtDest.Text) + "x"; }
                    else
                    { DestPath = fi.FullName.Replace(ConvertFilePath(fi.FullName), this.txtDest.Text) + "x"; }
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
            catch (Exception ex) { Trace.WriteLine(ex); return false; }
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
            { Trace.WriteLine(ex); }
            return files.ToArray();
        }

        private void chkSubDir_CheckedChanged(object sender, EventArgs e)
        {
            this.chkMirror.Enabled = this.chkSubDir.Checked;
        }

    }
}