using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace e_Sword9Converter
{
    public class Error
    {
        private static object threadLock = new object();
        private static TextWriter errorWriter = new StreamWriter("error.log");
        private static TextWriter logWriter = new StreamWriter("app.log");
        public static void Record(object sender, Exception ex)
        {
            lock (threadLock)
            {
                try
                {
                    Database db = (Database)sender;
                    errorWriter.WriteLine(string.Format("{0} {1}", db.DestDB, ex.Message));
                    errorWriter.Flush();
                }
                catch
                {
                    errorWriter.WriteLine(ex.Message);
                    errorWriter.Flush();
                }
            }
        }
        public static void Log(object sender, string message)
        {
            logWriter.WriteLine(sender.ToString() + "\t" + message);
            logWriter.Flush();
        }
    }
}
