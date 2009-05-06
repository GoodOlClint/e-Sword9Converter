using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace e_Sword9Converter
{
    public partial class Form1 : Form, IParent
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bible Bible = new Bible(this);
            Bible.Load("WycliffeNT.bbl");
            Bible.Save("WycliffeNT.bblx");
        }

        #region IParent Members

        public string GetPassword(string path)
        {
            return "";
        }

        public void SetMaxValue(int value)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
