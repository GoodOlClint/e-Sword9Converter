using System;
using System.Windows.Forms;

namespace eSword9Converter
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            InitializeComponent();
            this.txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);
            Controller.LanguageChangedEvent += new Controller.LanguageChangedEventHandler(Controller_LanguageChangedEvent);
        }

        void Controller_LanguageChangedEvent()
        {
            this.Text = Globalization.CurrentLanguage.Password;
            this.btnCancel.Text = Globalization.CurrentLanguage.Cancel;
            this.btnOk.Text = Globalization.CurrentLanguage.Ok;
        }

        public string FileName
        {
            get { return this.lblFile.Text; }
            set
            {
                string oldPath = value;
                string[] oldpath = oldPath.Split('\\');
                this.lblFile.Text = oldpath[oldpath.Length - 1];
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

        public string Password { get { return this.txtPassword.Text; } }
    }
}