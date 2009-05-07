using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Harmony : Database
    {
        public Harmony(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Sections", new Sections());
            this.Tables.Add("Parts", new Parts());
            this.Tables["Details"].Parent = Parent;
            this.Tables["Sections"].Parent = Parent;
            this.Tables["Parts"].Parent = Parent;
        }

        public override void Load(string File)
        {
            base.Load(File);
            if (!skip)
            { ((Details)this.Tables["Details"]).Version = 2; }
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

        [Table("Sections")]
        public class Sections : Table<Sections>
        {
            [Column("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Title", DbType.NVARCHAR, 255)]
            public string Comments { get; set; }
        }

        [SqlTable("Parts")]
        [AccessTable("Harmony")]
        public class Parts : Table<Parts>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Section ID", DbType.INT)]
            [SqlColumn("Section", DbType.INT)]
            [Index("SectionPartIndex")]
            public int SectionID { get; set; }

            [Column("Part", DbType.INT)]
            [Index("SectionPartIndex")]
            public int Part { get; set; }

            [Column("Reference", DbType.TEXT)]
            public string Reference { get; set; }
        }
    }
}
