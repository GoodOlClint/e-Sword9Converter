using System;
using System.Diagnostics;
using System.Windows.Forms;
using eSword9Converter.Globalization;

namespace eSword9Converter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] Args)
        {
#if DEBUG
            Process proc = Process.GetCurrentProcess();
            Debug.WriteLine(string.Format("Application {0} started on computer {1} with process ID of ", proc.ProcessName, proc.MachineName, proc.Id));
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller.Initalize();
            Application.Run();
        }
    }
}
