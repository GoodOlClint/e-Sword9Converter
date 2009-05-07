using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections;
namespace e_Sword9Converter
{
    public partial class frmMain : Form, IParent
    {
        public frmMain()
        {
            InitializeComponent();
        }

        #region IParent Members

        public string GetPassword(string path)
        {
            throw new NotImplementedException();
        }

        public void SetMaxValue(int value)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus()
        {
            throw new NotImplementedException();
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
            this.txtDest.Text = this.txtSource.Text.Replace(ext, ext + "x");
            this.grpDest.Enabled = true;
            this.ValidateDestination();
        }

        private bool ValidSource(string path)
        {
            FileInfo fi = new FileInfo(path);
            switch (fi.Extension)
            {
                case ".bbl":
                    this.ofdDest.Filter = "e-Sword Bible|*.bblx";
                    break;
                case ".cmt":
                    this.ofdDest.Filter = "e-Sword Commentary|*.cmtx";
                    break;
                case ".dct":
                    this.ofdDest.Filter = "e-Sword Dictionary|*.dctx";
                    break;
                case ".har":
                    this.ofdDest.Filter = "e-Sword Gospel Harmoy|*.harx";
                    break;
                case ".lst":
                    this.ofdDest.Filter = "e-Sword Verse List|*.lstx";
                    break;
                case ".map":
                    this.ofdDest.Filter = "e-Sword Graphics|*.mapx";
                    break;
                case ".not":
                    this.ofdDest.Filter = "e-Sword Notes|*.notx";
                    break;
                case ".top":
                    this.ofdDest.Filter = "e-Sword Topic|*.topx";
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
            this.ValidateDestination();
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
            
        }


        //private string ConvertFilePath(string OldPath)
        //{

        //}
    }
}