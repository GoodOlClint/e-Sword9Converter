using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Common;

namespace e_Sword9Converter
{
    public class Database
    {
        public string SourceDB { get; set; }
        public string DestDB { get; set; }
        public void ConvertFormat()
        {
            this.Load(SourceDB);
            this.Save(DestDB);
        }
        public ThreadSafeDictionary<string, ITable> Tables = new ThreadSafeDictionary<string, ITable>();
        private oleDbFactory oleDbFactory = new oleDbFactory();
        private SQLiteDbFactory SQLiteFactory = new SQLiteDbFactory();
        protected IParent Parent;
        private string SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};";
        private string DestConnectionString = "data source=\"{file}\"";

        public Database(IParent Parent) { this.Parent = Parent; }
        public virtual void Save(string Path)
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
            { Table.Value.SaveToDatabase(SQLiteFactory, this.DestConnectionString.Replace("{file}", Path)); }
        }
        public virtual void Load(string Path)
        {
            string pass;
            if (this.Parent.GetPassword(Path, out pass))
            {
                if (pass != "")
                { SourceConnectionString += "Jet OLEDB:Database Password=\"" + pass + "\";"; }
                foreach (KeyValuePair<string, ITable> Table in this.Tables)
                { Table.Value.Load(oleDbFactory, this.SourceConnectionString.Replace("{file}", Path)); }
            }
        }
    }
}
