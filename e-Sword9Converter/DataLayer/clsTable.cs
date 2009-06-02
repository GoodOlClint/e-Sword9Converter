using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace eSword9Converter
{
    public abstract class Table<T> : ITable, IDisposable where T : Table<T>, new()
    {
        public ThreadSafeDictionary<string, IColumn> Columns = new ThreadSafeDictionary<string, IColumn>();
        public ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>> Indexes = new ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>();
        public ThreadSafeCollection<ThreadSafeDictionary<string, object>> Rows = new ThreadSafeCollection<ThreadSafeDictionary<string, object>>();
        public string TableName { get; set; }
        public IDatabase DB { get; set; }

        private bool stop;

        #region Constructor
        public Table()
        {
            TableName = (from TableAttribute ta in (TableAttribute[])this.GetType().GetCustomAttributes(typeof(TableAttribute), false)
                         where ta.Type != tableType.Sql
                         select ta.Name).First();
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
            this.disposed = false;
        }
        #endregion

        public static T LoadFromDatabase(DbProviderFactory Factory, string connectionString, IDatabase Db)
        {
            T Table = new T();
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();
                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    dbCmd.CommandText = string.Format("SELECT COUNT(*) FROM {0};", Table.TableName);
                    int count = (int)dbCmd.ExecuteScalar();
                    if (count == 0)
                    { Debug.WriteLine("Skipping empty table " + Table.TableName); return Table; }
                    Controller.RaiseStatusChanged(Table, updateStatus.Loading);
                    Controller.SetMaxValue(Table, count);
                    dbCmd.CommandText = string.Format("SELECT * FROM {0};", Table.TableName);
                    int currentCount = 0;
                    using (DbDataReader reader = dbCmd.ExecuteReader())
                    {
                        bool nextResult = true;
                        Debug.WriteLine("Begining to read " + count + " entries from " + Table.TableName + " in " + Db.FileName);
                        while (nextResult)
                        {
                            while (reader.Read())
                            {
                                ThreadSafeDictionary<string, object> Row = new ThreadSafeDictionary<string, object>();
                                foreach (KeyValuePair<string, IColumn> Pair in (from KeyValuePair<string, IColumn> c in Table.Columns where c.Value.colType != columnType.Sql select c))
                                {
                                    if (Db.Skip)
                                    { break; }
                                    IColumn Column = Pair.Value;
                                    //PropertyInfo Prop = Table.GetType().GetProperty(Column.PropertyName);
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
                                            case DbType.DATETIME:
                                                Row.Add(Column.PropertyName, Convert.ToDateTime(reader[Column.Name]));
                                                break;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        //Build useful error message
                                        //string msg = string.Format("{0}\t{1}\tRow:{2}\tColumn:{3}\tType:{4}\tValue:{5}\tMessage:{6}", Db.FileName, Table.TableName, Table.Rows.Count, Column.Name, Column.Type, Convert.ToString(reader[Column.Name]), ex.Message);
                                        string msg = string.Format(Globalization.CurrentLanguage.SqlErrorString, Db.FileName, Table.TableName, Table.Rows.Count, Column.Name, Column.Type, Convert.ToString(reader[Column.Name]), ex.Message);
                                        SQLiteException sqlEx = new SQLiteException(msg);
                                        Trace.WriteLine(sqlEx);//new SQLiteException(msg));
                                    }
                                }
                                try
                                {
                                    if (Db.Skip)
                                    { break; }
                                    Table.Rows.Add(Row);
                                    currentCount++;
                                    Controller.RaiseProgressChanged(Table, currentCount);
                                }
                                catch (Exception ex) { Trace.WriteLine(ex); }
                            }
                            nextResult = reader.NextResult();
                        }
                        Debug.WriteLine("Successfully read " + currentCount + " entries out of " + count + " from " + Table.TableName + " in " + Db.FileName);
                    }
                }
            }
            return Table;
        }

        #region ITable Members

        /// <summary>
        /// Builds a query string that can be run against a SQL database to create the database structure for the current table
        /// </summary>
        /// <returns>a string containing the SQL Create statement for the current table, including all indexes</returns>
        public string SQLCreateStatement()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            Debug.WriteLine("Building SqlCreateStatement");
            TableName = (from TableAttribute ta in (TableAttribute[])this.GetType().GetCustomAttributes(typeof(TableAttribute), false)
                         where ta.Type != tableType.Access
                         select ta.Name).First();
            string sql = "";
            sql = string.Format("CREATE TABLE '{0}' (", TableName);
            /* We're going to loop through all the columns and append the column name to the sql string */
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
            /* remove trailing ', '*/
            sql = sql.Remove(sql.Length - 2, 2);
            /* close the statement and add a new line*/
            sql += ");\r\n";
            /* Check to see if we have any indexes for this table */
            if (this.Indexes.Count > 0)
            {
                /* If we do loop through them all*/
                foreach (KeyValuePair<string, ThreadSafeCollection<IColumn>> index in this.Indexes)
                {
                    sql += string.Format("CREATE INDEX {0} ON {1} (", index.Key, TableName);
                    foreach (ColumnAttribute Column in index.Value)
                    { if (Column.colType != columnType.Access) { sql += string.Format("{0}, ", Column.Name); } }
                    sql = sql.Remove(sql.Length - 2, 2);
                    sql += ");\r\n";
                }
            }
            /* Finally, return our SQL statment */
            Debug.WriteLine("Successfully built sql statement: " + sql);
            return sql;
        }

        public void SaveToDatabase(DbProviderFactory Factory, string connectionString)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();
                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    int count = (from ThreadSafeDictionary<string, object> kvp in this.Rows
                                 select kvp).Count();
                    if (count == 0)
                    { Debug.WriteLine("Skipping empty table " + this.TableName); return; }
                    Controller.RaiseStatusChanged(this, updateStatus.Saving);
                    Controller.SetMaxValue(this, count);
                    Debug.WriteLine("Begining to insert " + count + " entries into table " + this.TableName + " in " + this.DB.FileName);
                    string Command = string.Format("INSERT INTO {0} (", TableName);
                    IDictionary<string, IColumn> sqlColumns = (from KeyValuePair<string, IColumn> C in this.Columns
                                                               where C.Value.colType != columnType.Access
                                                               select C.Value).ToDictionary(v => v.PropertyName);
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
                        int currentCount = 0;
                        foreach (ThreadSafeDictionary<string, object> Row in this.Rows)
                        {
                            try
                            {
                                dbCmd.Parameters.Clear();
                                foreach (KeyValuePair<string, object> Column in (from KeyValuePair<string, object> kvp in Row where sqlColumns.ContainsKey(kvp.Key) select kvp))
                                { dbParams[Column.Key].Value = Column.Value; }
                                dbCmd.Parameters.AddRange(dbParams.Values.ToArray());
                                dbCmd.ExecuteNonQuery();
                                if (stop)
                                { break; }
                            }
                            catch (Exception ex) { Trace.WriteLine(ex); }
                            finally { currentCount++; Controller.RaiseProgressChanged(this, currentCount); }
                        }
                        dbTrans.Commit();
                        Debug.WriteLine("Successfully inserted " + currentCount + " entries out of " + count + " into table " + this.TableName + " in " + this.DB.FileName);
                    }
                }
            }
        }
        public void Load(DbProviderFactory Factory, string connectionString)
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            using (T Table = global::eSword9Converter.Table<T>.LoadFromDatabase(Factory, connectionString, this.DB))
            {
                this.Rows = (ThreadSafeCollection<ThreadSafeDictionary<string, object>>)Table.Rows.Clone();
                this.Columns = (ThreadSafeDictionary<string, IColumn>)Table.Columns.Clone();
                this.Indexes = (ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>)Table.Indexes.Clone();
                this.TableName = Table.TableName;
            }
        }

        #endregion

        #region IDisposable Members
        private bool disposed;
        public void Dispose()
        {
            if (disposed)
            { throw new ObjectDisposedException(this.ToString()); }
            this.stop = true;
            this.Columns.Dispose();
            this.Indexes.Dispose();
            this.Rows.Dispose();
        }

        #endregion
    }
}