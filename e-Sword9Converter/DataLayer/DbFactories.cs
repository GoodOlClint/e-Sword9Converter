using System.Data.Common;
using System.Data.OleDb;
using System.Data.SQLite;
using System.Security;
using System.Security.Permissions;

namespace eSword9Converter
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
}