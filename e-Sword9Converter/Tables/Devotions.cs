using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Devotion : Database
    {
        public Devotion(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Devotion", new Devotions());
            this.Tables["Details"].Parent = Parent;
            this.Tables["Devotion"].Parent = Parent;
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

        [AccessTable("Copyright")]
        public class Copyright : Table<Copyright>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Author", DbType.TEXT)]
            public string Author { get; set; }

            [Column("Title", DbType.TEXT)]
            public string Title { get; set; }

            [Column("Place", DbType.TEXT)]
            public string Place { get; set; }

            [Column("Publisher", DbType.TEXT)]
            public string Publisher { get; set; }

            [Column("Year", DbType.TEXT)]
            public string Year { get; set; }

            [Column("Edition", DbType.TEXT)]
            public string Edition { get; set; }

            [Column("Copyright", DbType.TEXT)]
            public string Copyright1 { get; set; }

            [Column("Notes", DbType.TEXT)]
            public string Notes { get; set; }
        }

        [Table("Devotions")]
        public class Devotions : Table<Devotions>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Month", DbType.INT)]
            [Index("MonthDayIndex")]
            public int Month { get; set; }

            [Column("Day", DbType.INT)]
            [Index("MonthDayIndex")]
            public int Day { get; set; }

            [Column("Devotion", DbType.TEXT)]
            public string Devotion { get; set; }
        }
    }
}
