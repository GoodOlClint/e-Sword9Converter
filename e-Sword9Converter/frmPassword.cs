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
using System.Windows.Forms;
using System.Diagnostics;

namespace eSword9Converter
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            Debug.WriteLine("Initalizing frmPassword");
            InitializeComponent();
            this.Controller_LanguageChangedEvent();
            Debug.WriteLine("Registering frmMain event handlers");
            this.txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);
            Controller.LanguageChangedEvent += new Controller.LanguageChangedEventHandler(Controller_LanguageChangedEvent);
            Debug.WriteLine("frmPassword.Initalize Finished");
        }

        void Controller_LanguageChangedEvent()
        {
            Debug.WriteLine("frmPassword.LanguageChangedEvent");
            this.Text = Globalization.CurrentLanguage.Password;
            this.btnCancel.Text = Globalization.CurrentLanguage.Cancel;
            this.btnOk.Text = Globalization.CurrentLanguage.Ok;
            Debug.WriteLine("frmPassword.LanguageChangedEvent Finished");
        }

        public string FileName
        {
            get { Debug.WriteLine("frmPassword.FileName is: " + this.lblFile.Text); return this.lblFile.Text; }
            set
            {
                string oldPath = value;
                string[] oldpath = oldPath.Split('\\');
                this.lblFile.Text = oldpath[oldpath.Length - 1];
                Debug.WriteLine("Setting frmPassword.FileName to: " + this.lblFile.Text);
            }
        }

        void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnOk_Click(sender, e);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text != "")
            { this.DialogResult = DialogResult.OK; }
            else
            { MessageBox.Show(Globalization.CurrentLanguage.PasswordBlank); }
        }

        public string Password { get { Debug.WriteLine("frmPassword.Password is: " + this.txtPassword.Text); return this.txtPassword.Text; } }
    }
}