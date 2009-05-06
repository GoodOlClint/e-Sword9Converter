using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace e_Sword9Converter
{

    public class oleDbFactory : DbProviderFactory
    {
        public override DbCommand CreateCommand() { return new OleDbCommand(); }
        public override DbConnection CreateConnection() { return new OleDbConnection(); }
        public override DbDataAdapter CreateDataAdapter() { return new OleDbDataAdapter(); }
        public override DbCommandBuilder CreateCommandBuilder() { return new OleDbCommandBuilder(); }
        public override DbConnectionStringBuilder CreateConnectionStringBuilder() { return new OleDbConnectionStringBuilder(); }
        public override DbParameter CreateParameter() { return new OleDbParameter(); }
        public override CodeAccessPermission CreatePermission(PermissionState state) { return new OleDbPermission(state); }
        public override bool CanCreateDataSourceEnumerator { get { return false; } }
    }

    public class SQLiteDbFactory : DbProviderFactory
    {
        public override DbCommand CreateCommand() { return new SQLiteCommand(); }
        public override DbConnection CreateConnection() { return new SQLiteConnection(); }
        public override DbDataAdapter CreateDataAdapter() { return new SQLiteDataAdapter(); }
        public override DbCommandBuilder CreateCommandBuilder() { return new SQLiteCommandBuilder(); }
        public override DbConnectionStringBuilder CreateConnectionStringBuilder() { return new SQLiteConnectionStringBuilder(); }
        public override DbParameter CreateParameter() { return new SQLiteParameter(); }
        public override bool CanCreateDataSourceEnumerator { get { return false; } }
    }
    public class Bible : Database
    {
        public Bible()
        {
            this.Tables.Add("Bible", new BibleTable());
            this.Tables.Add("Details", new Details());
        }
        public override void Load(string File)
        {
            base.Load(File);

            //Remove Invalid Scripture Entries;
            foreach (ThreadSafeDictionary<string, object> Row in (from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                  where ((string)Row["Scripture"]) == ""
                                                                  select Row).ToArray())
            {
                ((BibleTable)this.Tables["Bible"]).Rows.Remove(Row);
            }
            ((Details)this.Tables["Details"]).NT = Convert.ToBoolean((from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                      where ((int)Row["BookID"]) == 66
                                                                      select Row).Count() > 0);
            ((Details)this.Tables["Details"]).OT = Convert.ToBoolean((from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                      where ((int)Row["BookID"]) == 1
                                                                      select Row).Count() > 0);
            ((Details)this.Tables["Details"]).Version = 2;
            ((Details)this.Tables["Details"]).RightToLeft = (((Details)this.Tables["Details"]).Font.ToUpper() == "HEBREW");
        }
        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Description", DbType.NVARCHAR, 250)]
            public string Description { get { return Convert.ToString(this.Rows[0]["Description"]); } set { this.Rows[0]["Description"] = value; } }
            [Column("Abbreviation", DbType.NVARCHAR, 50)]
            public string Abbreviation { get { return Convert.ToString(this.Rows[0]["Abbreviation"]); } set { this.Rows[0]["Abbreviation"] = value; } }
            [Column("Comments", DbType.TEXT)]
            public string Comments { get { return Convert.ToString(this.Rows[0]["string"]); } set { this.Rows[0]["Font"] = value; } }
            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }
            [Column("Font", DbType.TEXT)]
            public string Font { get { return Convert.ToString(this.Rows[0]["Font"]); } set { this.Rows[0]["Font"] = value; } }
            [SqlColumn("RightToLeft", DbType.BOOL)]
            public bool RightToLeft { get { return Convert.ToBoolean(this.Rows[0]["RightToLeft"]); } set { this.Rows[0]["RightToLeft"] = value; } }
            [SqlColumn("OT", DbType.BOOL)]
            public bool OT { get { return Convert.ToBoolean(this.Rows[0]["OT"]); } set { this.Rows[0]["OT"] = value; } }
            [SqlColumn("NT", DbType.BOOL)]
            public bool NT { get { return Convert.ToBoolean(this.Rows[0]["NT"]); } set { this.Rows[0]["NT"] = value; } }
            [Column("Apocrypha", DbType.BOOL)]
            public bool Apocrypha { get { return Convert.ToBoolean(this.Rows[0]["Apocrypha"]); } set { this.Rows[0]["Apocrypha"] = value; } }
            [SqlColumn("Strong", DbType.BOOL)]
            [AccessColumn("Strongs", DbType.BOOL)]
            public bool Strong { get { return Convert.ToBoolean(this.Rows[0]["Strong"]); } set { this.Rows[0]["Strong"] = value; } }
        }

        [Table("Bible")]
        public class BibleTable : Table<BibleTable>
        {
            [AccessColumn("Book Id", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int BookID { get; set; }
            [Column("Chapter", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Chapter { get; set; }
            [Column("Verse", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Verse { get; set; }
            [Column("Scripture", DbType.TEXT)]
            public string Scripture { get; set; }
        }
    }

    public class Database
    {
        public ThreadSafeDictionary<string, ITable> Tables = new ThreadSafeDictionary<string, ITable>();
        private oleDbFactory oleDbFactory = new oleDbFactory();
        private SQLiteFactory SQLiteFactory = new SQLiteFactory();
        public virtual void Save(string File)
        {
            foreach (KeyValuePair<string, ITable> Table in this.Tables)
            { Table.Value.SaveToDatabase(SQLiteFactory, "data source=\"" + File + "\""); }
        }
        public virtual void Load(string File)
        {
            foreach (KeyValuePair<string, ITable> Table in this.Tables)
            { Table.Value.Load(oleDbFactory, "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + File); }
        }
    }
    public abstract class Table<T> : ITable where T : Table<T>, new()
    {
        public ThreadSafeDictionary<string, IColumn> Columns = new ThreadSafeDictionary<string, IColumn>();
        public ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>> Indexes = new ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>();
        public ThreadSafeCollection<ThreadSafeDictionary<string, object>> Rows = new ThreadSafeCollection<ThreadSafeDictionary<string, object>>();
        public string tableName;
        public Table()
        {
            tableName = ((TableAttribute)this.GetType().GetCustomAttributes(typeof(TableAttribute), false)[0]).Name;
            var query = from PropertyInfo Prop in this.GetType().GetProperties()
                        let Column = Prop.GetCustomAttributes(typeof(ColumnAttribute), false)
                        let IndexColumn = Prop.GetCustomAttributes(typeof(IndexAttribute), false)
                        where Column.Length > 0
                        select new object[]{
                            Prop,
                            Column,
                            IndexColumn
                        };
            foreach (object[] obj in query)
            {
                PropertyInfo Prop = (PropertyInfo)obj[0];
                foreach (ColumnAttribute ca in (ColumnAttribute[])obj[1])
                {
                    ca.PropertyName = Prop.Name;
                    this.Columns.Add(ca.Name, (IColumn)ca);
                    if (((object[])obj[2]).Length > 0)
                    {
                        foreach (IIndex index in (object[])obj[2])
                        {
                            if (!this.Indexes.ContainsKey(index.Name))
                            { this.Indexes.Add(index.Name, new ThreadSafeCollection<IColumn>()); }
                            this.Indexes[index.Name].Add(ca);
                        }
                    }
                }
            }
        }

        public string SQLCreateStatement()
        {
            string sql = "";
            sql = string.Format("CREATE TABLE '{0}' (", tableName);
            foreach (KeyValuePair<string, IColumn> Column in (from KeyValuePair<string, IColumn> C in this.Columns where C.Value.colType != columnType.Access select C))
            {
                sql += Column.Key;
                if (Column.Value.NotNull)
                    sql += " NOT NULL";
                if (Column.Value.Type != DbType.NULL)
                {
                    sql += string.Format(" {0}", Column.Value.Type.ToString());
                    if (Column.Value.Length > 0)
                        sql += string.Format("({0})", Column.Value.Length);
                }
                sql += ", ";
            }
            sql = sql.Remove(sql.Length - 2, 2);
            sql += ");\r\n";

            if (this.Indexes.Count > 0)
            {
                foreach (KeyValuePair<string, ThreadSafeCollection<IColumn>> index in this.Indexes)
                {
                    sql += string.Format("CREATE INDEX {0} ON {1} (", index.Key, tableName);
                    foreach (ColumnAttribute Column in index.Value)
                    { if (Column.colType != columnType.Access) { sql += string.Format("{0}, ", Column.Name); } }
                    sql = sql.Remove(sql.Length - 2, 2);
                    sql += ");\r\n";
                }
            }
            return sql;
        }

        public static T LoadFromDatabase(DbProviderFactory Factory, string connectionString)
        {
            T Table = new T();
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();
                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    dbCmd.CommandText = string.Format("SELECT * FROM {0};", Table.tableName);
                    using (DbDataReader reader = dbCmd.ExecuteReader())
                    {
                        bool nextResult = true;
                        while (nextResult)
                        {
                            while (reader.Read())
                            {
                                ThreadSafeDictionary<string, object> Row = new ThreadSafeDictionary<string, object>();
                                foreach (KeyValuePair<string, IColumn> Pair in (from KeyValuePair<string, IColumn> c in Table.Columns where c.Value.colType != columnType.Sql select c))
                                {
                                    IColumn Column = Pair.Value;
                                    PropertyInfo Prop = Table.GetType().GetProperty(Column.PropertyName);
                                    try
                                    {
                                        switch (Column.Type)
                                        {
                                            default:
                                            case DbType.BLOB:
                                                Row.Add(Column.PropertyName, reader[Column.Name]);
                                                break;
                                            case DbType.BOOL:
                                                Row.Add(Column.PropertyName, Convert.ToBoolean(reader[Column.Name]));
                                                break;
                                            case DbType.INT:
                                            case DbType.AUTONUMBER:
                                                Row.Add(Column.PropertyName, Convert.ToInt32(reader[Column.Name]));
                                                break;
                                            case DbType.TEXT:
                                            case DbType.NVARCHAR:
                                                Row.Add(Column.PropertyName, Convert.ToString(reader[Column.Name]));
                                                break;
                                            case DbType.REAL:
                                                Row.Add(Column.PropertyName, Convert.ToDouble(reader[Column.Name]));
                                                break;
                                        }
                                    }
                                    catch (Exception ex) { Error.Record(Table, ex); }
                                }
                                Table.Rows.Add(Row);
                            }
                            nextResult = reader.NextResult();
                        }
                    }
                }
            }
            return Table;
        }

        public void SaveToDatabase(DbProviderFactory Factory, string connectionString)
        {
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();

                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    dbCmd.CommandText = this.SQLCreateStatement();
                    dbCmd.ExecuteNonQuery();
                    string Command = string.Format("INSERT INTO {0} (", tableName);
                    IEnumerable<KeyValuePair<string, IColumn>> sqlColumns = (from KeyValuePair<string, IColumn> C in this.Columns
                                                                             where C.Value.colType != columnType.Access
                                                                             select C);
                    string values = "";
                    foreach (KeyValuePair<string, IColumn> Column in sqlColumns)
                    {
                        Command += string.Format("{0}, ", Column.Value.Name);
                        values += string.Format("@{0}, ", Column.Value.PropertyName);
                    }
                    Command = Command.Remove(Command.Length - 2, 2);
                    Command += ") VALUES (" + values;
                    Command = Command.Remove(Command.Length - 2, 2);
                    Command += ");";
                    dbCmd.CommandText = Command;
                    ThreadSafeDictionary<string, DbParameter> dbParams = new ThreadSafeDictionary<string, DbParameter>();
                    foreach (KeyValuePair<string, IColumn> Column in sqlColumns)
                    {
                        DbParameter dbParam = dbCmd.CreateParameter();
                        dbParam.ParameterName = Column.Value.PropertyName;
                        dbParams.Add(Column.Value.PropertyName, dbParam);
                    }

                    using (DbTransaction dbTrans = dbCon.BeginTransaction())
                    {
                        foreach (ThreadSafeDictionary<string, object> Row in this.Rows)
                        {
                            dbCmd.Parameters.Clear();
                            foreach (KeyValuePair<string, object> Column in Row)
                            { dbParams[Column.Key].Value = Column.Value; }
                            dbCmd.Parameters.AddRange(dbParams.Values.ToArray());
                            dbCmd.ExecuteNonQuery();
                        }
                        dbTrans.Commit();
                    }
                }
            }
        }

        #region ITable Members


        public void Load(DbProviderFactory Factory, string connectionString)
        {
            T Table = global::e_Sword9Converter.Table<T>.LoadFromDatabase(Factory, connectionString);
            this.Rows = (ThreadSafeCollection<ThreadSafeDictionary<string, object>>)Table.Rows.Clone();
            this.Columns = (ThreadSafeDictionary<string, IColumn>)Table.Columns.Clone();
            this.Indexes = (ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>)Table.Indexes.Clone();
            this.tableName = Table.tableName;
        }

        #endregion
    }

    public interface IColumn
    {
        DbType Type { get; set; }
        int Length { get; set; }
        string Name { get; set; }
        bool NotNull { get; set; }
        string PropertyName { get; set; }
        columnType colType { get; }
    }

    public interface IIndex
    {
        string Name { get; set; }
    }

    public interface ITable
    {
        void SaveToDatabase(DbProviderFactory Factory, string connectionString);
        string SQLCreateStatement();
        void Load(DbProviderFactory Factory, string connectionString);
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute, IColumn
    {
        private DbType type;
        private int length;
        private string name;
        private string propertyName;
        private bool notNull;
        public DbType Type { get { return this.type; } set { this.type = value; } }
        public int Length { get { return this.length; } set { this.length = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public bool NotNull { get { return this.notNull; } set { this.notNull = value; } }
        public ColumnAttribute() { }
        public ColumnAttribute(string Name) { this.Name = Name; }
        public ColumnAttribute(string Name, DbType DbType) { this.Name = Name; this.Type = DbType; }
        public ColumnAttribute(string Name, DbType DbType, int Length) { this.Name = Name; this.Type = DbType; this.Length = Length; }
        public string PropertyName { get { return this.propertyName; } set { this.propertyName = value; } }
        public virtual columnType colType { get { return columnType.Both; } }

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlColumnAttribute : ColumnAttribute
    {
        public SqlColumnAttribute(string Name) { this.Name = Name; }
        public SqlColumnAttribute(string Name, DbType DbType) { this.Name = Name; this.Type = DbType; }
        public SqlColumnAttribute(string Name, DbType DbType, int Length) { this.Name = Name; this.Type = DbType; this.Length = Length; }
        public override columnType colType { get { return columnType.Sql; } }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AccessColumnAttribute : ColumnAttribute
    {
        public AccessColumnAttribute(string Name) : base(Name) { }
        public AccessColumnAttribute(string Name, DbType DbType) : base(Name, DbType) { }
        public AccessColumnAttribute(string Name, DbType DbType, int Length) : base(Name, DbType, Length) { }
        public override columnType colType { get { return columnType.Access; } }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public TableAttribute(string Name) { this.Name = Name; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IndexAttribute : Attribute, IIndex
    {
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public IndexAttribute(string Name) { this.Name = Name; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrimaryKeyAttribute : Attribute
    { }

    public enum DbType
    {
        NULL = 0,
        AUTONUMBER,
        INT,
        REAL,
        TEXT,
        BLOB,
        NVARCHAR,
        BOOL
    }
    public enum columnType
    {
        Access = -1,
        Both = 0,
        Sql = 1
    }
}