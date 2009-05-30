using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Notes : Database
    {
        private VerseReferences VerseReferences;
        public Notes()
        {
            this.Tables.Add("VerseNotes", new VerseNotes());
            this.VerseReferences = new VerseReferences();
        }

        public override void Load(string Path)
        {
            base.Load(Path);
            if (!skip)
            {
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((VerseNotes)this.Tables["VerseNotes"]).Rows
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    int VerseID = Convert.ToInt32(Row["VerseID"]);
                    var reference = (from VerseReference v in this.VerseReferences
                                     where v.EndVerse >= VerseID && v.StartVerse <= VerseID
                                     select v).First<VerseReference>();
                    Row["Book"] = reference.Book;
                    Row["Chapter"] = reference.Chapter;
                    Row["Verse"] = (VerseID - reference.StartVerse) + 1;
                    count++;
                    Controller.RaiseProgressChanged(this, count);
                }
            }
        }

        [AccessTable("[Verse Notes]")]
        [SqlTable("Verse")]
        public class VerseNotes : Table<VerseNotes>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Verse ID", DbType.INT)]
            public int VerseID { get; set; }

            [SqlColumn("Book", DbType.INT)]
            public int Book { get; set; }

            [SqlColumn("Chapter", DbType.INT)]
            public int Chapter { get; set; }

            [SqlColumn("Verse", DbType.INT)]
            public int Verse { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }
    }
}
