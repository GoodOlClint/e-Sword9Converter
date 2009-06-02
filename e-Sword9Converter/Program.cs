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
            TraceListener traceListener = new TraceListener("eSword9Converter.log");
            Trace.Listeners.Add(traceListener);
#if DEBUG   //We don't want to mess with creating a listener/Debug.log if we're in release mode.
            //TraceListener debugListener = new TraceListener("eSword9Converter.Debug.log");
            //Debug.Listeners.Add(debugListener);
            using (Process proc = Process.GetCurrentProcess())
            {
                Debug.WriteLine(string.Format("Application {0} started on computer {1} with process ID of {2}", proc.ProcessName, proc.MachineName, proc.Id));
            }
#endif
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller.Initalize();
            Application.Run();
            Debug.WriteLine("Exiting application");
        }
    }
}