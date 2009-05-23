using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.VisualBasic.ApplicationServices;
using System.Threading;
using System.Globalization;
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
            switch (CultureInfo.CurrentUICulture.Name.Remove(2, 3))
            {
                case "es":
                    Globalization.CurrentLanguage = new Globalization.Spanish();
                    break;
                default:
                    Globalization.CurrentLanguage = new Globalization.English();
                    break;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
