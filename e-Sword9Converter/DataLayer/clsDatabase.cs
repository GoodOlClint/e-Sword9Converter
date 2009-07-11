/*
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
using System.Data.Common;
using System.Diagnostics;

namespace eSword9Converter
{
    public class Database : IDatabase, IDisposable
    {
        private ThreadSafeDictionary<string, bool> BooleanProperties;
        private ThreadSafeDictionary<string, string> StringProperties;
        private ThreadSafeDictionary<string, ITable> tables;
        private object threadLock;
        private oleDbFactory oleDbFactory;
        private SQLiteDbFactory SQLiteFactory;
        private string SourceConnectionString;
        private string DestConnectionString;

        #region Public Properties
        public string SourceDB
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.StringProperties["SourceDB"]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.StringProperties["SourceDB"] = value; }
            }
        }
        public string DestDB
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.StringProperties["DestDB"]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.StringProperties["DestDB"] = value; }
            }
        }
        public string FileName
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.StringProperties["FileName"]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.StringProperties["FileName"] = value; }
            }
        }
        public bool Running
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.BooleanProperties["Running"]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.BooleanProperties["Running"] = value; }
            }
        }
        public bool Skip
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.BooleanProperties["Skip"]; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.BooleanProperties["Skip"] = value; }
            }
        }
        public ThreadSafeDictionary<string, ITable> Tables
        {
            get
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { return this.tables; }
            }
            set
            {
                if (disposed)
                { throw new ObjectDisposedException(this.ToString()); }
                lock (threadLock) { this.tables = value; }
            }
        }
        #endregion


        public Database()
        {
            this.threadLock = new object();
            this.Tables = new ThreadSafeDictionary<string, ITable>();
            this.BooleanProperties = new ThreadSafeDictionary<string, bool>();
            this.StringProperties = new ThreadSafeDictionary<string, string>();
            this.oleDbFactory = new oleDbFactory();
            this.SQLiteFactory = new SQLiteDbFactory();
            this.SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};";
            this.DestConnectionString = "data source=\"{file}\";UseUTF16Encoding=True";

        }

        public void Stop() { Skip = true; }

        public void ConvertFormat()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            try
            {
                this.Skip = false;
                this.Running = true;
                string[] path = DestDB.Split('\\');
                this.FileName = path[path.Length - 1];
                //this.FileName = SourceDB;
                this.Load(SourceDB);
                path = DestDB.Split('\\');
                this.FileName = path[path.Length - 1];
                this.Save(DestDB);
                if (!Skip)
                    Controller.RaiseStatusChanged(this, updateStatus.Finished);
                this.Running = false;
            }
            catch (Exception ex) { Trace.WriteLine(ex); this.Running = false; } //Error.Record(this, ex); }
        }

        public void Clear()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            this.Tables.Clear();
        }



        public virtual void Save(string Path)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            if (!Skip)
            {
                if (File.Exists(Path))
                    File.Delete(Path);
                try
                {
                    using (DbConnection sqlCon = SQLiteFactory.CreateConnection())
                    {
                        sqlCon.ConnectionString = this.DestConnectionString.Replace("{file}", Path);
                        sqlCon.Open();
                        using (DbCommand sqlCmd = sqlCon.CreateCommand())
                        {
                            foreach (string str in (from KeyValuePair<string, ITable> Table in this.Tables
                                                    select Table.Value.SQLCreateStatement()))
                            {
                                sqlCmd.CommandText = str;
                                sqlCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                { Trace.WriteLine(ex); }
                try
                {
                    foreach (KeyValuePair<string, ITable> Table in this.Tables)
                    {
                        try
                        { Table.Value.SaveToDatabase(SQLiteFactory, this.DestConnectionString.Replace("{file}", Path)); }
                        catch (Exception ex) { Trace.WriteLine(ex); }
                    }
                }
                catch (Exception ex)
                { Trace.WriteLine(ex); }
                try
                {
                    Controller.RaiseStatusChanged(this, updateStatus.Optimizing);
                    Controller.RaiseProgressChanged(this, 0);
                    Controller.RaiseMaxValueChanged(this, 100);
                    using (DbConnection sqlCon = SQLiteFactory.CreateConnection())
                    {
                        sqlCon.ConnectionString = this.DestConnectionString.Replace("{file}", Path);
                        sqlCon.Open();
                        using (DbCommand sqlCmd = sqlCon.CreateCommand())
                        {
                            sqlCmd.CommandText = "VACUUM";
                            Controller.RaiseProgressChanged(this, 50);
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                    Controller.RaiseProgressChanged(this, 100);
                }
                catch (Exception ex)
                { Trace.WriteLine(ex); }
            }
        }
        public virtual void Load(string Path)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            if (Controller.NeedPassword(Path))
            {
                string pass = Controller.GetPassword(Path);
                SourceConnectionString += "Jet OLEDB:Database Password=\"" + pass + "\";";
                Skip = (pass == "");
            }
            if (!Skip)
            {
                foreach (KeyValuePair<string, ITable> Table in this.Tables)
                {
                    Table.Value.DB = this;
                    try
                    { Table.Value.Load(oleDbFactory, this.SourceConnectionString.Replace("{file}", Path)); }
                    catch (Exception ex) { Trace.WriteLine(ex); }
                }
            }
            else { this.Running = false; }
        }

        #region IDisposable Members
        protected bool disposed;

        public void Dispose()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            this.Tables.Clear();
            this.Tables.Dispose();
            this.oleDbFactory = null;
            this.SQLiteFactory = null;
            this.DestConnectionString = null;
            this.DestDB = null;
            this.FileName = null;
            this.Running = false;
            this.Skip = false;
            this.SourceDB = null;
            this.threadLock = null;
            this.disposed = true;
        }

        #endregion
    }
}
