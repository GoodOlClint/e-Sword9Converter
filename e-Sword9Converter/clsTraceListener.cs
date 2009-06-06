﻿/*
 * Copyright (c) 2009, GoodOlClint All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * Redistributions of source code must retain the above copyright notice, this list of conditions
 * and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 * and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * Neither the name of the e-Sword Users nor the names of its contributors may be used to endorse
 * or promote products derived from this software without specific prior written permission.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

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
