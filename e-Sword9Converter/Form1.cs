﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace e_Sword9Converter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Process proc = new Process();
            Bible Bible = new Bible();
            Bible.Load("WycliffeNT.bbl");
            Bible.Save("WycliffeNT.bblx");
            //BibleTable BibleTable = BibleTable.LoadFromDatabase(new oleDbFactory(), "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BHS+.bbl"); ;
            //BibleTable.SaveToDatabase(new SQLiteDbFactory(), "data source=\"BHS+.bblx\"");
            //proc.SourceDB = "WycliffeNT.bbl";
            //proc.DestDB = "WycliffeNT.bblx";
            //proc.SourceDB = "BHS+.bbl";
            //proc.DestDB = "BHS+.bblx";
            //proc.BuildBible();
        }
    }
}
