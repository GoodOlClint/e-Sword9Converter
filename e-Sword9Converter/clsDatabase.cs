using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;

namespace eSword9Converter
{
    public class Database : IDatabase
    {
        public string SourceDB { get; set; }
        public string DestDB { get; set; }
        public string FileName { get; set; }
        public void Stop() { skip = true; }
        public bool Running { get { lock (threadLock) { return this.running; } } set { lock (threadLock) { this.running = value; } } }
        private object threadLock = new object();
        protected bool skip;
        private bool running;
        public void ConvertFormat()
        {
            try
            {
                skip = false;
                running = true;
                this.FileName = SourceDB;
                this.Load(SourceDB);
                this.FileName = DestDB;
                this.Save(DestDB);
                Controller.RaiseStatusChanged(this, updateStatus.Finished);
                running = false;
            }
            catch (Exception ex) { Error.Record(this, ex); }
        }
        public void Clear()
        { this.Tables.Clear(); }
        public ThreadSafeDictionary<string, ITable> Tables = new ThreadSafeDictionary<string, ITable>();
        private oleDbFactory oleDbFactory = new oleDbFactory();
        private SQLiteDbFactory SQLiteFactory = new SQLiteDbFactory();
        private string SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};";
        private string DestConnectionString = "data source=\"{file}\"";

        public virtual void Save(string Path)
        {
            if (!skip)
            {
                if (File.Exists(Path))
                    File.Delete(Path);

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
                foreach (KeyValuePair<string, ITable> Table in this.Tables)
                {
                    try
                    { Table.Value.SaveToDatabase(SQLiteFactory, this.DestConnectionString.Replace("{file}", Path)); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }

            }
        }
        public virtual void Load(string Path)
        {
            if (Controller.NeedPassword(Path))
            {
                string pass = Controller.GetPassword(Path);
                SourceConnectionString += "Jet OLEDB:Database Password=\"" + pass + "\";";
                skip = (pass == "");
            }
            if (!skip)
            {
                foreach (KeyValuePair<string, ITable> Table in this.Tables)
                {
                    Table.Value.DB = this;
                    try
                    { Table.Value.Load(oleDbFactory, this.SourceConnectionString.Replace("{file}", Path)); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            else { this.Running = false; }
        }
    }
}
