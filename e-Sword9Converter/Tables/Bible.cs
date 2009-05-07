using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Bible : Database
    {
        public Bible(IParent Parent) : base(Parent)
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Bible", new BibleTable());
            this.Tables["Details"].Parent = Parent;
            this.Tables["Bible"].Parent = Parent;
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!this.skip)
            {
                //Remove Invalid Scripture Entries;
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                          where ((string)Row["Scripture"]) == ""
                                                                          select Row).ToArray();
                this.Parent.SetMaxValue(rows.Count(), updateStatus.Convert);
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    ((BibleTable)this.Tables["Bible"]).Rows.Remove(Row);
                    this.Parent.UpdateStatus();
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
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
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
}
