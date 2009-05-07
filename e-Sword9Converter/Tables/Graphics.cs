﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Graphic : Database
    {
        public Graphic(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Graphics", new Graphics());
            this.Tables["Details"].Parent = Parent;
            this.Tables["Graphics"].Parent = Parent;
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
            [Column("Description", DbType.NVARCHAR, 250)]
            public string Description { get { return Convert.ToString(this.Rows[0]["Description"]); } set { this.Rows[0]["Description"] = value; } }

            [Column("Abbreviation", DbType.NVARCHAR, 50)]
            public string Abbreviation { get { return Convert.ToString(this.Rows[0]["Abbreviation"]); } set { this.Rows[0]["Abbreviation"] = value; } }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get { return Convert.ToString(this.Rows[0]["string"]); } set { this.Rows[0]["Font"] = value; } }

            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }
        }

        [Table("Graphics")]
        public class Graphics : Table<Graphics>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Title", DbType.NVARCHAR, 100)]
            public string Title { get; set; }

            [Column("Details", DbType.TEXT)]
            public string Details { get; set; }

            [Column("Picture", DbType.TEXT)]
            public object Picture { get; set; }
        }
    }
}
