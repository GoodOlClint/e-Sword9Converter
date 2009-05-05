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

    [Table("Details")]
    public class Details : Table<Details>
    {
        private ThreadSafeCollection<string> descriptions = new ThreadSafeCollection<string>();
        private ThreadSafeCollection<string> abbreviation = new ThreadSafeCollection<string>();
        private ThreadSafeCollection<string> comments = new ThreadSafeCollection<string>();
        private ThreadSafeCollection<int> version = new ThreadSafeCollection<int>();
        private ThreadSafeCollection<string> font = new ThreadSafeCollection<string>();
        private ThreadSafeCollection<bool> rtl = new ThreadSafeCollection<bool>();
        private ThreadSafeCollection<bool> ot = new ThreadSafeCollection<bool>();
        private ThreadSafeCollection<bool> nt = new ThreadSafeCollection<bool>();
        private ThreadSafeCollection<bool> apocrypha = new ThreadSafeCollection<bool>();
        private ThreadSafeCollection<bool> strong = new ThreadSafeCollection<bool>();
        [Column("Description", DbType.NVARCHAR, 250)]
        public ThreadSafeCollection<string> Description { get { return this.descriptions; } set { this.descriptions = value; } }
        [Column("Abbreviation", DbType.NVARCHAR, 50)]
        public ThreadSafeCollection<string> Abbreviation { get { return this.abbreviation; } set { this.abbreviation = value; } }
        [Column("Comments", DbType.TEXT)]
        public ThreadSafeCollection<string> Comments { get { return this.comments; } set { this.comments = value; } }
        [SqlColumn("Version", DbType.INT)]
        public ThreadSafeCollection<int> Version { get { return this.version; } set { this.version = value; } }
        [Column("Font", DbType.TEXT)]
        public ThreadSafeCollection<string> Font { get { return this.font; } set { this.font = value; } }
        [SqlColumn("RightToLeft", DbType.BOOL)]
        public ThreadSafeCollection<bool> RightToLeft { get { return this.rtl; } set { this.rtl = value; } }
        [SqlColumn("OT", DbType.BOOL)]
        public ThreadSafeCollection<bool> OT { get { return this.ot; } set { this.ot = value; } }
        [SqlColumn("NT", DbType.BOOL)]
        public ThreadSafeCollection<bool> NT { get { return this.nt; } set { this.nt = value; } }
        [Column("Apocrypha", DbType.BOOL)]
        public ThreadSafeCollection<bool> Apocrypha { get { return this.apocrypha; } set { this.apocrypha = value; } }
        [SqlColumn("Strong", DbType.BOOL)]
        [AccessColumn("Strongs",DbType.BOOL)]
        public ThreadSafeCollection<bool> Strong { get { return this.strong; } set { this.strong = value; } }
    }
    public abstract class Table<T> where T : Table<T>, new()
    {
        private string tableName { get { return ((TableAttribute)this.GetType().GetCustomAttributes(typeof(TableAttribute), false)[0]).Name; } }
        public string SQLCreateStatement()
        {
            string sql = "";
            ThreadSafeCollection<ColumnAttribute> Columns = new ThreadSafeCollection<ColumnAttribute>();
            ThreadSafeDictionary<string, ThreadSafeCollection<ColumnAttribute>> Indexes = new ThreadSafeDictionary<string, ThreadSafeCollection<ColumnAttribute>>();
            object[] tables = this.GetType().GetCustomAttributes(typeof(TableAttribute), false);
            if (tables.Length > 0)
            {
                TableAttribute Table = (TableAttribute)tables[0];
                PropertyInfo[] Properties = typeof(T).GetProperties();
                var query = from PropertyInfo Prop in Properties
                            let Column = Prop.GetCustomAttributes(typeof(ColumnAttribute), false)
                            let AccessColumn = Prop.GetCustomAttributes(typeof(AccessColumnAttribute), false)
                            let IndexColumn = Prop.GetCustomAttributes(typeof(IndexAttribute), false)
                            where ((Column.Length > 0) && (IndexColumn.Length > 0) && (AccessColumn.Length == 0)) || ((Column.Length > 0) && (AccessColumn.Length == 0))
                            select new object[]
                                            {
                                                Prop,
                                                Column,
                                                IndexColumn
                                            };
                foreach (object[] obj in query)
                {
                    PropertyInfo Prop = (PropertyInfo)obj[0];
                    ColumnAttribute ColumnAttribute = new ColumnAttribute();

                    ColumnAttribute = (ColumnAttribute)((ColumnAttribute[])obj[1])[0];


                    if (((IndexAttribute[])obj[2]).Length > 0)
                    {
                        IndexAttribute Index = (IndexAttribute)((IndexAttribute[])obj[2])[0];
                        if (!Indexes.ContainsKey(Index.Name))
                        {
                            ThreadSafeCollection<ColumnAttribute> col = new ThreadSafeCollection<ColumnAttribute>();
                            col.Add(ColumnAttribute);
                            Indexes.Add(Index.Name, col);
                        }
                    }
                    Columns.Add(ColumnAttribute);
                }

                sql = string.Format("CREATE TABLE '{0}' (", Table.Name);
                foreach (ColumnAttribute column in Columns)
                {
                    sql += column.Name;
                    if (column.NotNull)
                        sql += " NOT NULL";
                    if (column.Type != DbType.NULL)
                    {
                        sql += string.Format(" {0}", column.Type.ToString());
                        if (column.Length > 0)
                            sql += string.Format("({0})", column.Length);
                    }
                    sql += ", ";
                }
                sql = sql.Remove(sql.Length - 2, 2);
                sql += ");\r\n";
                if (Indexes.Count > 0)
                {
                    foreach (KeyValuePair<string, ThreadSafeCollection<ColumnAttribute>> index in Indexes)
                    {
                        sql += string.Format("CREATE INDEX {0} ON {1} (", index.Key, Table.Name);
                        foreach (ColumnAttribute Column in index.Value)
                        {
                            sql += string.Format("{0}, ", Column.Name);
                        }
                        sql = sql.Remove(sql.Length - 2, 2);
                        sql += ");\r\n";
                    }
                }
                return sql;
            }
            else { throw new InvalidOperationException("Table class must have the Table Attribute set"); }
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
                                PropertyInfo[] Properties = typeof(T).GetProperties();
                                var query = from PropertyInfo Prop in Properties
                                            let Column = Prop.GetCustomAttributes(typeof(ColumnAttribute), false)
                                            let SqlColumn = Prop.GetCustomAttributes(typeof(SqlColumnAttribute), false)
                                            where Column.Length > 0 && SqlColumn.Length == 0
                                            select new object[]
                                            {
                                                Prop,
                                                Column
                                            };
                                foreach (object[] obj in query)
                                {
                                    PropertyInfo Prop = (PropertyInfo)obj[0];
                                    ColumnAttribute Column = (ColumnAttribute)((ColumnAttribute[])obj[1])[0];
                                    try
                                    {
                                        switch (Column.Type)
                                        {
                                            default:
                                            case DbType.BLOB:
                                                ((ThreadSafeCollection<Object>)Prop.GetValue(Table, null)).Add(reader[Column.Name]);
                                                break;
                                            case DbType.BOOL:
                                                ((ThreadSafeCollection<bool>)Prop.GetValue(Table, null)).Add(Convert.ToBoolean(reader[Column.Name]));
                                                break;
                                            case DbType.INT:
                                            case DbType.AUTONUMBER:
                                                ((ThreadSafeCollection<int>)Prop.GetValue(Table, null)).Add(Convert.ToInt32(reader[Column.Name]));
                                                break;
                                            case DbType.TEXT:
                                            case DbType.NVARCHAR:
                                                ((ThreadSafeCollection<string>)Prop.GetValue(Table, null)).Add(Convert.ToString(reader[Column.Name]));
                                                break;
                                            case DbType.REAL:
                                                ((ThreadSafeCollection<double>)Prop.GetValue(Table, null)).Add(Convert.ToDouble(reader[Column.Name]));
                                                break;
                                        }
                                    }
                                    catch (Exception ex) { Error.Record(Table, ex); }
                                }

                            }
                            nextResult = reader.NextResult();
                        }
                    }
                }
            }
            return Table;
        }
    }
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        private DbType type;
        private int length;
        private string name;
        private bool notNull;
        public DbType Type { get { return this.type; } set { this.type = value; } }
        public int Length { get { return this.length; } set { this.length = value; } }
        public string Name { get { return this.name; } set { this.name = value; } }
        public bool NotNull { get { return this.notNull; } set { this.notNull = value; } }
        public ColumnAttribute() { }
        public ColumnAttribute(string Name) { this.Name = Name; }
        public ColumnAttribute(string Name, DbType DbType) { this.Name = Name; this.Type = DbType; }
        public ColumnAttribute(string Name, DbType DbType, int Length) { this.Name = Name; this.Type = DbType; this.Length = Length; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class SqlColumnAttribute : ColumnAttribute
    {
        public SqlColumnAttribute(string Name) { this.Name = Name; }
        public SqlColumnAttribute(string Name, DbType DbType) { this.Name = Name; this.Type = DbType; }
        public SqlColumnAttribute(string Name, DbType DbType, int Length) { this.Name = Name; this.Type = DbType; this.Length = Length; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AccessColumnAttribute : ColumnAttribute
    {
        public AccessColumnAttribute(string Name) : base(Name) { }
        public AccessColumnAttribute(string Name, DbType DbType) : base(Name, DbType) { }
        public AccessColumnAttribute(string Name, DbType DbType, int Length) : base(Name, DbType, Length) { }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        private string name;
        public string Name { get { return this.name; } set { this.name = value; } }
        public TableAttribute(string Name) { this.Name = Name; }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IndexAttribute : Attribute
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
}