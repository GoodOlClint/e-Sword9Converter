using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace eSword9Converter
{
    public class TraceListener : System.Diagnostics.TraceListener
    {
        public string LogFile { get; set; }
        private DateTime Now { get { return DateTime.Now; } }

        private object threadLock;

        public TraceListener(string LogFile)
        {
            this.threadLock = new object();
            this.LogFile = LogFile;
            if (File.Exists(LogFile)) File.Delete(LogFile);
        }

        public override void Write(string message)
        {
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter(LogFile, true))
                { sw.Write(Now.ToString() + ": " + message); }
            }
        }

        public override void WriteLine(string message)
        {
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter(LogFile, true))
                { sw.WriteLine(Now.ToString() + ": " + message); }
            }
        }
    }
}
