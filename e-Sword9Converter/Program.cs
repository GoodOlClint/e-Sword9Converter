﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
namespace e_Sword9Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
