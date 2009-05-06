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

        public class Copyright : Table<Copyright>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Author { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Title { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Place { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Publisher { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Year { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Edition { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Copyright1 { get; set; }

            [AccessColumn("ID", DbType.INT)]
            public string Notes { get; set; }
        }
        public class Devotions : Table<Devotions>
        {
            public int ID { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public string Devotion { get; set; }
        }
    }
}
