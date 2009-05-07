using System;
using System.Windows.Forms;

namespace e_Sword9Converter
{
    public partial class frmPassword : Form
    {
        public frmPassword()
        {
            InitializeComponent();
            this.txtPassword.KeyDown += new KeyEventHandler(txtPassword_KeyDown);
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
            { MessageBox.Show("Password cannot be blank"); }
        }

        public string Password { get { return this.txtPassword.Text; } }
    }
}