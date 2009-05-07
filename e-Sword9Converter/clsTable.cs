using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace e_Sword9Converter
{
    public abstract class Table<T> : ITable where T : Table<T>, new()
    {
        public ThreadSafeDictionary<string, IColumn> Columns = new ThreadSafeDictionary<string, IColumn>();
        public ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>> Indexes = new ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>();
        public ThreadSafeCollection<ThreadSafeDictionary<string, object>> Rows = new ThreadSafeCollection<ThreadSafeDictionary<string, object>>();
        public string tableName;
        public IParent Parent { get; set; }

        #region Constructor
        public Table()
        {
            tableName = (from TableAttribute ta in (TableAttribute[])this.GetType().GetCustomAttributes(typeof(TableAttribute), false)
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
        }
        #endregion
        public static T LoadFromDatabase(DbProviderFactory Factory, string connectionString,IParent Parent)
        {
            T Table = new T();
            Table.Parent = Parent;
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();
                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    dbCmd.CommandText = string.Format("SELECT COUNT(*) FROM {0};", Table.tableName);
                    int count = (int)dbCmd.ExecuteScalar();
                    Table.Parent.SetMaxValue(count);
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
                                                Row.Add(Column.PropertyName, Convert.ToInt32(Convert.ToDateTime(reader[Column.Name])));
                                                break;
                                        }
                                        
                                    }
                                    catch (Exception ex) { Error.Record(Table, ex); }
                                }
                                Table.Parent.UpdateStatus();
                                Table.Rows.Add(Row);
                            }
                            nextResult = reader.NextResult();
                        }
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
            tableName = (from TableAttribute ta in (TableAttribute[])this.GetType().GetCustomAttributes(typeof(TableAttribute), false)
                         where ta.Type != tableType.Access
                         select ta.Name).First();
            string sql = "";
            sql = string.Format("CREATE TABLE '{0}' (", tableName);
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
                    sql += string.Format("CREATE INDEX {0} ON {1} (", index.Key, tableName);
                    foreach (ColumnAttribute Column in index.Value)
                    { if (Column.colType != columnType.Access) { sql += string.Format("{0}, ", Column.Name); } }
                    sql = sql.Remove(sql.Length - 2, 2);
                    sql += ");\r\n";
                }
            }
            /* Finally, return our SQL statment */
            return sql;
        }

        public void SaveToDatabase(DbProviderFactory Factory, string connectionString)
        {
            using (DbConnection dbCon = Factory.CreateConnection())
            {
                dbCon.ConnectionString = connectionString;
                dbCon.Open();
                using (DbCommand dbCmd = dbCon.CreateCommand())
                {
                    int count = (from ThreadSafeDictionary<string, object> kvp in this.Rows
                                 select kvp).Count();
                    this.Parent.SetMaxValue(count);
                    string Command = string.Format("INSERT INTO {0} (", tableName);
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
                        foreach (ThreadSafeDictionary<string, object> Row in this.Rows)
                        {
                            try
                            {
                                dbCmd.Parameters.Clear();
                                foreach (KeyValuePair<string, object> Column in (from KeyValuePair<string, object> kvp in Row where sqlColumns.ContainsKey(kvp.Key) select kvp))
                                { dbParams[Column.Key].Value = Column.Value; }
                                dbCmd.Parameters.AddRange(dbParams.Values.ToArray());
                                dbCmd.ExecuteNonQuery();
                            }
                            catch (Exception ex) { Error.Record(this, ex); }
                            finally { this.Parent.UpdateStatus(); }
                        }
                        dbTrans.Commit();
                    }
                }
            }
        }
        public void Load(DbProviderFactory Factory, string connectionString)
        {
            T Table = global::e_Sword9Converter.Table<T>.LoadFromDatabase(Factory, connectionString, this.Parent);
            this.Rows = (ThreadSafeCollection<ThreadSafeDictionary<string, object>>)Table.Rows.Clone();
            this.Columns = (ThreadSafeDictionary<string, IColumn>)Table.Columns.Clone();
            this.Indexes = (ThreadSafeDictionary<string, ThreadSafeCollection<IColumn>>)Table.Indexes.Clone();
            this.tableName = Table.tableName;
        }

        #endregion
    }
}