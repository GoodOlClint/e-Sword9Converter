using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Threading;

namespace eSword9Converter
{
    public class TraceListener : System.Diagnostics.TraceListener
    {
        public string LogFile
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock)
                { return this.file; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock)
                { this.file = value; }
            }
        }

        private string file;
        private DateTime Now { get { return DateTime.Now; } }
        private SynchronizationContext sync;
        private object threadLock;

        public TraceListener(string LogFile)
        {
            this.threadLock = new object();
            this.LogFile = LogFile;
            if (File.Exists(LogFile)) File.Delete(LogFile);
            sync = SynchronizationContext.Current;
            if (sync == null)
            { sync = new SynchronizationContext(); }
        }

        public override void Write(string message)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            sync.Post(new SendOrPostCallback(delegate { this.write(message, this.LogFile); }), null);
        }

        public override void WriteLine(string message)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            sync.Post(new SendOrPostCallback(delegate { this.writeline(message, this.LogFile); }), null);
        }

        private void write(string message, string path)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                { sw.Write(Now.ToString() + ": " + message); }
            }
        }

        private void writeline(string message, string path)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                { sw.WriteLine(Now.ToString() + ": " + message); }
            }
        }

        private bool disposed;
        protected override void Dispose(bool disposing)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            base.Dispose(disposing);
            this.disposed = true;
        }
    }
}
