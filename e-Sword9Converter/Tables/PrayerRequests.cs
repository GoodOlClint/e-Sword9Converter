using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class PrayerRequests : Database
    {
        public PrayerRequests(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Plan", new Plan());
            this.Tables["Plan"].Parent = Parent;
        }
        [Table("Requests")]
        public class Plan : Table<Plan>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Title", DbType.NVARCHAR, 50)]
            public string Title { get; set; }

            [Column("Type", DbType.INT)]
            public int Type { get; set; }

            [Column("Frequency", DbType.INT)]
            public int Frequency { get; set; }

            [AccessColumn("Start", DbType.DATETIME)]
            [SqlColumn("Start", DbType.INT)]
            public int Start { get; set; }

            [Column("Request", DbType.TEXT)]
            public string Request { get; set; }
        }
    }
}
