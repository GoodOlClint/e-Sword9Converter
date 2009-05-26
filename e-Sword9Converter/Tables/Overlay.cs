using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Overlay : Database
    {
        public Overlay(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Overlay", new OverlayTable());
            this.Tables["Overlay"].Parent = Parent;
        }

        public class OverlayTable : Table<OverlayTable>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Bible", DbType.TEXT)]
            public string Bible { get; set; }

            [Column("Book", DbType.INT)]
            public int BookID { get; set; }

            [Column("Chapter", DbType.INT)]
            public int Chapter { get; set; }

            [Column("Codes", DbType.TEXT)]
            public string Codes { get; set; }
        }
    }
}
