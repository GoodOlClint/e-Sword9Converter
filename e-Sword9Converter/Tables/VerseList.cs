using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class VerseList : Database
    {
        private VerseReferences VerseReferences;
        public VerseList(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Verses", new Verses());
            this.VerseReferences = new VerseReferences();
        }

        public override void Load(string Path)
        {
            base.Load(Path);
            foreach (ThreadSafeDictionary<string, object> Row in (from ThreadSafeDictionary<string, object> Row in ((Verses)this.Tables["Verses"]).Rows
                                                                  select Row).ToArray())
            {
                int VerseID = Convert.ToInt32(Row["VerseID"]);
                var reference = (from VerseReference v in this.VerseReferences
                                 where v.EndVerse <= VerseID
                                 where v.StartVerse >= VerseID
                                 select v).First<VerseReference>();
                Row["Book"] = reference.Book;
                Row["Chapter"] = reference.Chapter;
                Row["Verse"] = (VerseID - reference.StartVerse) + 1;
            }
        }

        [Table("Verses")]
        public class Verses : Table<Verses>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }
            
            [AccessColumn("VerseID", DbType.INT)]
            public int VerseID { get; set; }
            
            [SqlColumn("Book", DbType.INT)]
            public int Book { get; set; }
            
            [SqlColumn("Chapter", DbType.INT)]
            public int Chapter { get; set; }
            
            [SqlColumn("Verse", DbType.INT)]
            public int Verse { get; set; }
            
            [Column("Order", DbType.INT)]
            public int Order { get; set; }
        }
    }
}
