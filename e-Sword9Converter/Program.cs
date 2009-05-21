using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.VisualBasic.ApplicationServices;
namespace e_Sword9Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
