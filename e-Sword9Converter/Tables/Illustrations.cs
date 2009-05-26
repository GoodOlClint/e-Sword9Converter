using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Illustration : Database
    {
        public Illustration(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Illustrations", new Illustrations());
            this.Tables["Illustrations"].Parent = Parent;
        }

        [Table("Illustrations")]
        public class Illustrations : Table<Illustrations>
        {
            [AccessColumn("ID", DbType.AUTONUMBER)]
            public int Description { get; set; }

            [Column("Category", DbType.TEXT)]
            public string Abbreviation { get; set; }

            [Column("Title", DbType.TEXT)]
            public string Comments { get; set; }

            [SqlColumn("Scripture", DbType.INT)]
            public string Version { get; set; }

            [Column("Font", DbType.TEXT)]
            public string Font { get; set; }

            [Column("Illustration", DbType.TEXT)]
            public string RightToLeft { get; set; }
        }
    }
}
