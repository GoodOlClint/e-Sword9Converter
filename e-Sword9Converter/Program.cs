using System;
using System.Diagnostics;
using System.Windows.Forms;
using eSword9Converter.Globalization;
using System.IO;
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
#if DEBUG   //We don't want to mess with creating a listener/debug.log if we're in release mode.
            TraceListener tl = new TraceListener("eSword9Converter.Debug.log");
            Debug.Listeners.Add(tl);
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
