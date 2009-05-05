using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data.OleDb;
namespace e_Sword9Converter
{
    public class Process
    {
        private string sourceDB, destDB;
        public string SourceDB { get { return this.sourceDB; } set { this.sourceDB = value; } }
        private string SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};";
        public string DestDB { get { return this.destDB; } set { this.destDB = value; } }
        private bool run;
        /* These are our querys for mass inserts */
        private const string BibleTable = "INSERT INTO Bible (Book, Chapter, Verse, Scripture) VALUES (?,?,?,?);";
        private const string CommentaryBookTable = "INSERT INTO Books (Book, Comments) VALUES (?,?);";
        private const string CommentaryChapterTable = "INSERT INTO Chapters (Book, Chapter, Comments) VALUES (?,?,?);";
        private const string CommentaryVersesTable = "INSERT INTO Verses (Book, ChapterBegin, ChapterEnd, VerseBegin, VerseEnd, Comments) VALUES (?,?,?,?,?,?);";
        private const string DictionaryTable = "INSERT INTO Dictionary (Topic, Definition) VALUES (?,?);";
        private const string HarmonyPartsTable = "INSERT INTO Parts (Section, Part, Reference) VALUES (?,?,?);";
        private const string HarmonySectionTable = "INSERT INTO Sections (ID, Title) VALUES (?,?);";
        private const string VerseListTable = "INSERT INTO Verses (Book, Chapter, Verse, Position) VALUES (?,?,?,?);";
        private const string GraphicsTable = "INSERT INTO Graphics (Title, Details, Picture) VALUES (?,?,?);";
        private const string NotesTable = "INSERT INTO Verses (Book, Chapter, Verse, Notes) VALUES (?,?,?,?);";
        private const string TopicsTable = "INSERT INTO Topics (Title, Notes) VALUES (?,?);";

        public void Skip() { run = false; }

        public void BuildBible()
        {
            run = true;
            while (run)
            {
                SQLExecuteNonQuery("CREATE TABLE Details (Description NVARCHAR(255), Abbreviation NVARCHAR(50), Comments TEXT, Version INT, Font NVARCHAR(50), RightToLeft BOOL, OT BOOL, NT BOOL, Apocrypha BOOL, Strong BOOL);");
                SQLExecuteNonQuery("CREATE TABLE Bible (Book INT, Chapter INT, Verse INT, Scripture TEXT);");
                SQLExecuteNonQuery("CREATE INDEX BookChapterVerseIndex ON Bible (Book, Chapter, Verse);");
                //first lets add the details table;
                SQLiteParameter Description, Abbreviation, Comments, Version, Font, RTL, OT, NT, Apocrypha, Strongs;
                bool nt = !(bool)((int)MDBExecuteScalar("SELECT COUNT(*) FROM Bible WHERE [Book Id] = 66 AND Scripture <> NULL") == 0);
                bool ot = !(bool)((int)MDBExecuteScalar("SELECT COUNT(*) FROM Bible WHERE [Book Id] = 1 AND Scripture <> NULL") == 0);
                Description = new SQLiteParameter();
                Abbreviation = new SQLiteParameter();
                Comments = new SQLiteParameter();
                Version = new SQLiteParameter();
                Font = new SQLiteParameter();
                RTL = new SQLiteParameter();
                OT = new SQLiteParameter();
                NT = new SQLiteParameter();
                Apocrypha = new SQLiteParameter();
                Strongs = new SQLiteParameter();
                using (odbcReader dr = MDBExecuteReader("SELECT Description, Abbreviation, Comments, Font, Apocrypha, Strongs FROM Details"))
                {

                    dr.Read();
                    try
                    {
                        odbc.BBL.Details details = new odbc.BBL.Details(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5]);
                        SQL.BBLX.Details newDetails = new SQL.BBLX.Details();
                        newDetails.FromODBC(details);
                        Description.Value = newDetails.Description;
                        Abbreviation.Value = newDetails.Abbreviation;
                        Comments.Value = newDetails.Comments;
                        Version.Value = newDetails.Version;
                        Font.Value = newDetails.Font;
                        Apocrypha.Value = newDetails.Apocrypha;
                        Strongs.Value = newDetails.Strong;
                        RTL.Value = newDetails.RightToLeft;
                        OT.Value = ot;
                        NT.Value = nt;
                    }
                    catch (Exception ex)
                    { Error.Record(this, ex); }
                    SQLExecuteNonQuery("INSERT INTO Details (Description, Abbreviation, Comments, Version, Font, RightToLeft, OT, NT, Apocrypha, Strong) VALUES (?,?,?,?,?,?,?,?,?,?);", Description, Abbreviation, Comments, Version, Font, RTL, OT, NT, Apocrypha, Strongs);
                }
                int count = (int)MDBExecuteScalar("SELECT COUNT(*) FROM Bible WHERE Scripture <> NULL");
                ThreadSafeCollection<odbc.BBL.Bible> bbls = new ThreadSafeCollection<odbc.BBL.Bible>();

                using (odbcReader dr = MDBExecuteReader("SELECT [Book Id], Chapter, Verse, Scripture FROM Bible WHERE Scripture <> NULL"))
                {
                    bool nextResult = true;
                    while (nextResult)
                    {
                        while (dr.Read())
                        {
                            try
                            {
                                odbc.BBL.Bible Bible = new e_Sword9Converter.odbc.BBL.Bible(dr[0], dr[1], dr[2], dr[3]);
                                bbls.Add(Bible);
                            }
                            catch (Exception ex) { Error.Record(this, ex); }
                        }
                        nextResult = dr.NextResult();
                    }
                }
                ThreadSafeCollection<SQL.BBLX.Bible> bblxs = new ThreadSafeCollection<SQL.BBLX.Bible>();
                foreach (odbc.BBL.Bible Bible in bbls)
                {
                    SQL.BBLX.Bible bblx = new e_Sword9Converter.SQL.BBLX.Bible();
                    bblx.FromODBC(Bible);
                    bblxs.Add(bblx);
                }
                using (SQLiteConnection sqlCon = new SQLiteConnection("data source=\"" + DestDB + "\""))
                {
                    try { sqlCon.Open(); }
                    catch (Exception ex) { Error.Record(this, ex); return; }
                    using (SQLiteCommand sqlCmd = sqlCon.CreateCommand())
                    {
                        sqlCmd.CommandText = BibleTable;
                        SQLiteParameter Book = new SQLiteParameter();
                        SQLiteParameter Chapter = new SQLiteParameter();
                        SQLiteParameter Verse = new SQLiteParameter();
                        SQLiteParameter Scripture = new SQLiteParameter();
                        sqlCmd.Parameters.Add(Book);
                        sqlCmd.Parameters.Add(Chapter);
                        sqlCmd.Parameters.Add(Verse);
                        sqlCmd.Parameters.Add(Scripture);
                        using (SQLiteTransaction sqlTran = sqlCon.BeginTransaction())
                        {
                            try
                            {
                                foreach (SQL.BBLX.Bible Bible in bblxs)
                                {
                                    try
                                    {
                                        Book.Value = Bible.Book;
                                        Chapter.Value = Bible.Chapter;
                                        Verse.Value = Bible.Verse;
                                        Scripture.Value = Bible.Scripture;
                                        sqlCmd.ExecuteNonQuery();
                                    }
                                    catch (Exception ex)
                                    { Error.Record(this, ex); }
                                }
                            }
                            catch { sqlTran.Rollback(); }
                            finally { sqlTran.Commit(); }
                        }
                    }
                    sqlCon.Close();
                }
                run = false;
            }
        }

        public int SQLExecuteNonQuery(string query, params SQLiteParameter[] Parameters)
        {
            int results = 0;
            using (SQLiteConnection sqlCon = new SQLiteConnection())
            {
                sqlCon.ConnectionString = "data source=\"" + DestDB + "\"";
                sqlCon.Open();
                using (SQLiteCommand sqlCmd = sqlCon.CreateCommand())
                {
                    sqlCmd.CommandText = query;
                    sqlCmd.Parameters.AddRange(Parameters);
                    results = sqlCmd.ExecuteNonQuery();
                }
            }
            return results;
        }
        public int SQLExecuteNonQuery(string query)
        {
            int results = 0;
            using (SQLiteConnection sqlCon = new SQLiteConnection())
            {
                sqlCon.ConnectionString = "data source=\"" + DestDB + "\"";
                sqlCon.Open();
                using (SQLiteCommand sqlCmd = sqlCon.CreateCommand())
                {
                    sqlCmd.CommandText = query;
                    results = sqlCmd.ExecuteNonQuery();
                }
            }
            return results;
        }
        public odbcReader MDBExecuteReader(string query, params OleDbParameter[] Parameters)
        {
            OleDbConnection odbcCon = new OleDbConnection();
            OleDbCommand odbcCmd = default(OleDbCommand);
            odbcReader results = new odbcReader();
            string str = this.SourceConnectionString.Replace("{file}", SourceDB);
            odbcCon.ConnectionString = str;
            odbcCon.Open();
            odbcCmd = odbcCon.CreateCommand();
            odbcCmd.Parameters.Add(Parameters);
            odbcCmd.CommandText = query;
            results.odbcCmd = odbcCmd;
            results.odbcCon = odbcCon;
            results.Reader = odbcCmd.ExecuteReader();
            return results;
        }
        public odbcReader MDBExecuteReader(string query)
        {
            OleDbConnection odbcCon = new OleDbConnection();
            OleDbCommand odbcCmd = default(OleDbCommand);
            odbcReader results = new odbcReader();
            string str = this.SourceConnectionString.Replace("{file}", SourceDB);
            odbcCon.ConnectionString = str;
            odbcCon.Open();
            odbcCmd = odbcCon.CreateCommand();
            odbcCmd.CommandText = query;
            results.odbcCmd = odbcCmd;
            results.odbcCon = odbcCon;
            results.Reader = odbcCmd.ExecuteReader();
            return results;
        }
        public object MDBExecuteScalar(string query, params OleDbParameter[] Parameters)
        {
            object results = null;
            using (OleDbConnection odbcCon = new OleDbConnection())
            {
                string str = this.SourceConnectionString.Replace("{file}", SourceDB);
                odbcCon.ConnectionString = str;
                odbcCon.Open();
                using (OleDbCommand odbcCmd = odbcCon.CreateCommand())
                {
                    odbcCmd.CommandText = query;
                    odbcCmd.Parameters.Add(Parameters);
                    results = odbcCmd.ExecuteScalar();
                }
                odbcCon.Close();
            }
            return results;
        }
        public object MDBExecuteScalar(string query)
        {
            object results = null;
            using (OleDbConnection odbcCon = new OleDbConnection())
            {
                string str = this.SourceConnectionString.Replace("{file}", SourceDB);
                odbcCon.ConnectionString = str;
                odbcCon.Open();
                using (OleDbCommand odbcCmd = odbcCon.CreateCommand())
                {
                    odbcCmd.CommandText = query;
                    results = odbcCmd.ExecuteScalar();
                }
            }
            return results;
        }
        public class odbcReader : IDisposable
        {
            public OleDbCommand odbcCmd;
            public OleDbConnection odbcCon;
            public OleDbDataReader Reader;

            #region IDisposable Members

            public void Dispose()
            {
                this.Reader.Close();
                this.odbcCmd.Dispose();
                this.odbcCon.Close();
                this.odbcCon.Dispose();
            }

            #endregion

            public object this[int index]
            {
                get
                {
                    return this.Reader[index];
                }
            }

            public bool Read() { return this.Reader.Read(); }
            public bool NextResult() { return this.Reader.NextResult(); }
        }
    }
}
