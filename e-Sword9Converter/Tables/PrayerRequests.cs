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
        public override void Load(string File)
        {
            base.Load(File);
            if (!this.skip)
            {
                //Convert Access State DateTime to SQLite Int
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((Plan)this.Tables["Plan"]).Rows
                                                                          select Row).ToArray();
                this.Parent.SetMaxValue(rows.Count(), updateStatus.Converting);
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    DateTime epoch = new DateTime(1900, 1, 1);
                    TimeSpan span = new TimeSpan();
                    span = ((Plan)this.Tables["Plan"]).accessStart.Subtract(epoch);
                    ((Plan)this.Tables["Plan"]).Start = span.Days + 1;
                    this.Parent.UpdateStatus();
                }
            }
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
            public DateTime accessStart { get; set; }

            [SqlColumn("Start", DbType.INT)]
            public int Start { get; set; }

            [Column("Request", DbType.TEXT)]
            public string Request { get; set; }
        }
    }
}
