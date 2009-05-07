using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Commentary : Database
    {
        public Commentary(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Books", new Books());
            this.Tables.Add("Chapters", new Chapters());
            this.Tables.Add("Verses", new Verses());
            this.Tables["Details"].Parent = Parent;
            this.Tables["Books"].Parent = Parent;
            this.Tables["Chapters"].Parent = Parent;
            this.Tables["Verses"].Parent = Parent;
        }
        public override void Load(string File)
        {
            base.Load(File);
            ((Details)this.Tables["Details"]).Version = 2;
            foreach (ThreadSafeDictionary<string, object> Row in (from ThreadSafeDictionary<string, object> Row in ((Verses)this.Tables["Verses"]).Rows
                                                                  select Row).ToArray())
            {
                Row["ChapterEnd"] = Row["ChapterBegin"];
            }
        }
        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Description", DbType.NVARCHAR, 255)]
            public string Description { get { return Convert.ToString(this.Rows[0]["Description"]); } set { this.Rows[0]["Description"] = value; } }
            
            [Column("Abbreviation", DbType.NVARCHAR, 50)]
            public string Abbreviation { get { return Convert.ToString(this.Rows[0]["Abbreviation"]); } set { this.Rows[0]["Abbreviation"] = value; } }
            
            [Column("Comments", DbType.TEXT)]
            public string Comments { get { return Convert.ToString(this.Rows[0]["string"]); } set { this.Rows[0]["Font"] = value; } }
            
            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }
        }

        [SqlTable("Books")]
        [AccessTable("Book Notes")]
        public class Books : Table<Books>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book Id", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookIndex")]
            public int BookID { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [SqlTable("Chapters")]
        [AccessTable("Chapter Notes")]
        public class Chapters : Table<Chapters>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterIndex")]
            public int BookID { get; set; }

            [Column("Chapter", DbType.INT)]
            [Index("BookChapterIndex")]
            public int Chapter { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [SqlTable("Verses")]
        [AccessTable("Comments")]
        public class Verses : Table<Verses>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int BookID { get; set; }

            [AccessColumn("Chapter", DbType.INT)]
            [SqlColumn("ChapterBegin", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int ChapterBegin { get; set; }

            [SqlColumn("ChapterEnd", DbType.INT)]
            public int ChapterEnd { get; set; }

            [AccessColumn("Start Verse", DbType.INT)]
            [SqlColumn("VerseBegin", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int VerseBegin { get; set; }

            [AccessColumn("End Verse", DbType.INT)]
            [SqlColumn("VerseEnd", DbType.INT)]
            public int VerseEnd { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }
    }
}
