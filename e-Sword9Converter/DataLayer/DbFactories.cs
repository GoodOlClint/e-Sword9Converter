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