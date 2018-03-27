using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class PrayerRequests : Database
    {
        public PrayerRequests()
        {
            this.Tables.Add("Plan", new Plan());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!this.Skip)
            {
                //Convert Access State DateTime to SQLite Int
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((Plan)this.Tables["Plan"]).Rows
                                                                          orderby Row["ID"] ascending
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    ((Plan)this.Tables["Plan"]).Rows.Remove(Row); 
                    DateTime epoch = new DateTime(1900, 1, 1);
                    TimeSpan span = new TimeSpan();
                    span = ((DateTime)Row["accessStart"]).Subtract(epoch);
                    //span = ((Plan)this.Tables["Plan"]).accessStart.Subtract(epoch);
                    Row["Start"] = (object)(span.Days + 1);
                    ((Plan)this.Tables["Plan"]).Rows.Add(Row);
                    count++;
                    Controller.RaiseProgressChanged(this, count);
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
