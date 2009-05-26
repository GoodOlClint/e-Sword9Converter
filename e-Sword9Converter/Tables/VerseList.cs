using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class VerseList : Database
    {
        private VerseReferences VerseReferences;
        public VerseList(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Verses", new Verses());
            this.VerseReferences = new VerseReferences();
            this.Tables["Verses"].Parent = Parent;
        }

        public override void Load(string Path)
        {
            base.Load(Path);
            if (!skip)
            {
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((Verses)this.Tables["Verses"]).Rows
                                                                          select Row).ToArray();
                this.Parent.SetMaxValue(rows.Count(), updateStatus.Converting);
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    int VerseID = Convert.ToInt32(Row["VerseID"]);
                    var reference = (from VerseReference v in this.VerseReferences
                                     where v.EndVerse >= VerseID && v.StartVerse <= VerseID
                                     select v).First<VerseReference>();
                    Row["Book"] = reference.Book;
                    Row["Chapter"] = reference.Chapter;
                    Row["Verse"] = (VerseID - reference.StartVerse) + 1;
                    this.Parent.SetMaxValue(rows.Count(), updateStatus.Converting);
                }
            }
        }

        [Table("Verses")]
        public class Verses : Table<Verses>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }
            
            [AccessColumn("Verse ID", DbType.INT)]
            public int VerseID { get; set; }
            
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Book { get; set; }
            
            [SqlColumn("Chapter", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Chapter { get; set; }
            
            [SqlColumn("Verse", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Verse { get; set; }
            
            [AccessColumn("Order", DbType.INT)]
            [SqlColumn("Position", DbType.INT)]
            public int Order { get; set; }
        }
    }
}
