using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace e_Sword9Converter
{
    public partial class frmAdvanced : Form, IParent
    {
        public frmAdvanced()
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
    }
}