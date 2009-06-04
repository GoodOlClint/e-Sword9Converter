using System;
using System.Security.Cryptography;
using System.Text;

namespace eSword9Converter
{
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
        public string Hash
        {
            get
            {
                byte[] tmpSource;
                byte[] tmpHash;
                //Create a byte array from source data.
                tmpSource = ASCIIEncoding.ASCII.GetBytes(this.Name + this.PropertyName);
                tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                int i;
                StringBuilder sOutput = new StringBuilder(tmpHash.Length);
                for (i = 0; i < tmpHash.Length; i++)
                {
                    sOutput.Append(tmpHash[i].ToString("X2"));
                }
                return sOutput.ToString();
            }
        }
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

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        //private string name;
        public string Name { get; set; }//{ get { return this.name; } set { this.name = value; } }
        public tableType Type { get; set; }
        public TableAttribute(string Name) { this.Name = Name; this.Type = tableType.Both; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SqlTableAttribute : TableAttribute
    {
        public SqlTableAttribute(string Name) : base(Name) { this.Type = tableType.Sql; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AccessTableAttribute : TableAttribute
    {
        public AccessTableAttribute(string Name) : base(Name) { this.Type = tableType.Access; }
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
}
