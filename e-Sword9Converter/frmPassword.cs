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