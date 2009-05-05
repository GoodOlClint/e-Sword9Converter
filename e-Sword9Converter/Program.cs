using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace e_Sword9Converter
{
    static class Program
    {
        public static VerseLists VerseLists = new VerseLists();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
