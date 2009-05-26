using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Dictionary : Database
    {
        public Dictionary()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Dictionary", new DictionaryTable());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!skip)
            {
                ((Details)this.Tables["Details"]).Version = 2;
                ((Details)this.Tables["Details"]).Strong = false;
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
            
            [SqlColumn("Strong", DbType.BOOL)]
            public bool Strong { get { return Convert.ToBoolean(this.Rows[0]["Strong"]); } set { this.Rows[0]["Strong"] = value; } }
        }

        [Table("Dictionary")]
        public class DictionaryTable : Table<DictionaryTable>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Topic", DbType.TEXT, 100)]
            [Index("TopicIndex")]
            public string Topic { get; set; }
            
            [Column("Definition", DbType.TEXT)]
            public string Definition { get; set; }
        }
    }
}
