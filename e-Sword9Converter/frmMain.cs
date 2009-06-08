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
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using eSword9Converter.Globalization;

namespace eSword9Converter
{
    public partial class frmMain : Form, IForm
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
            { Trace.WriteLine(ex); }
        }

        #endregion

        #region Event Handlers

        public void Controller_ConversionFinishedEvent(bool error)
        {
            try
            {
                if (!error)
                {
                    this.lblStatus.Text = Globalization.CurrentLanguage.Finished;
                    MessageBox.Show(Globalization.CurrentLanguage.FinishedConverting);
                    this.txtDest.Text = "";
                    this.txtSource.Text = "";
                }
                this.prgMain.Value = 0;
                this.prgMain.Maximum = 100;
                this.lblStatus.Text = "";
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }


        public void Controller_LanguageChangedEvent()
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
            { Trace.WriteLine(ex); }
        }

        void Controller_ProgressChangedEvent(object sender, int count)
        {
            try
            { this.prgMain.Value = count; }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }

        void Controller_MaxValueChangedEvent(object sender, int value)
        {
            try
            {
                Debug.WriteLine("frmMain.prgMain.Maximum set to: " + value);
                this.prgMain.Maximum = value;
                this.prgMain.Value = 0;
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }

        void Controller_StatusChangedEvent(object sender, updateStatus status)
        {
            try
            {
                switch (status)
                {
                    case updateStatus.Loading:
                        this.lblStatus.Text = CurrentLanguage.Loading;
                        break;
                    case updateStatus.Converting:
                        this.lblStatus.Text = CurrentLanguage.Converting;
                        break;
                    case updateStatus.Saving:
                        this.lblStatus.Text = CurrentLanguage.Saving;
                        break;
                    case updateStatus.Optimizing:
                        this.lblStatus.Text = CurrentLanguage.Optimizing;
                        break;
                    case updateStatus.Finished:
                        this.lblStatus.Text = CurrentLanguage.Finished;
                        break;
                }
                Debug.WriteLine("frmMain.lblStatus.Text set to: " + this.lblStatus.Text);
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }

        void prgMain_MouseHover(object sender, EventArgs e)
        {
            Debug.WriteLine("frmMain.prgMain hovered");
            try
            {
                int Percent = (int)(((double)this.prgMain.Value / (double)this.prgMain.Maximum) * 100d);
                this.toolTip.SetToolTip(this.prgMain, string.Format("{0}% {1}", Percent, Globalization.CurrentLanguage.Completed));
            }
            catch (Exception ex) { Trace.WriteLine(ex); }
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
            { Trace.WriteLine(ex); }
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
                this.grpDest.Enabled = false;
                this.btnConvert.Enabled = false;
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
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
            catch (Exception ex)
            { Trace.WriteLine(ex); }
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
                { MessageBox.Show(Globalization.CurrentLanguage.SourceFileNotExist, this.txtSource.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                if (!ValidSource(this.txtSource.Text))
                { MessageBox.Show(Globalization.CurrentLanguage.SourceFileInvalid, this.txtSource.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
                string ext = this.txtSource.Text.Substring(this.txtSource.Text.Length - 4, 4);
                string path = this.ConvertFilePath(this.txtSource.Text);
                this.ofdDest.FileName = this.txtSource.Text.Replace(ext, ext.ToLower() + "x").Replace(path + @"\", "");
                this.ofdDest.InitialDirectory = path;
                this.grpDest.Enabled = true;
            }
            catch (Exception ex) { Trace.WriteLine(ex); }
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
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
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
            catch (Exception ex) { Trace.WriteLine(ex); return false; }
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
            catch (Exception ex) { Trace.WriteLine(ex); return false; }
        }

        private string ConvertFilePath(string OldPath)
        {
            try
            { return new FileInfo(OldPath).DirectoryName; }
            catch (Exception ex) { Trace.WriteLine(ex); return OldPath; }
        }

        ~frmMain()
        {
            Controller.ConversionFinishedEvent -= new Controller.ConversionFinishedEventHandler(this.Controller_ConversionFinishedEvent);
        }
    }

}