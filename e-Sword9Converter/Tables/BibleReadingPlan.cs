using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class BibleReadingPlan : Database
    {
        public BibleReadingPlan()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Plan", new Plan());
        }
        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Sunday",DbType.BOOL)]
            public bool Sunday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Monday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Tuesday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Wednesday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Thursday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Friday { get; set; }
            [Column("Sunday", DbType.BOOL)]
            public bool Saturday { get; set; }
            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [Table("Plan")]
        public class Plan : Table<Plan>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }
            
            [Column("Day",DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int Day { get; set; }
            
            [SqlColumn("Book", DbType.INT)]
            [AccessColumn("Book ID", DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int BookId { get; set; }
            
            [AccessColumn("StartChapter", DbType.INT)]
            [SqlColumn("ChapterBegin", DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int StartChapter { get; set; }

            [AccessColumn("EndChapter", DbType.INT)]
            [SqlColumn("ChapterEnd", DbType.INT)]
            public int EndChapter { get; set; }
            
            [Column("Completed", DbType.BOOL)]
            public bool Completed { get; set; }
        }
    }
}
