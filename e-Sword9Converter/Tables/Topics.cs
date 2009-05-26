using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Topic : Database
    {
        public Topic(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Topics", new Topics());
            this.Tables["Topics"].Parent = Parent;
        }

        [SqlTable("Topics")]
        [AccessTable("[Topic Notes]")]
        public class Topics : Table<Topics>
        {
            [AccessColumn("ID", DbType.INT)]
            public string ID { get; set; }

            [Column("Title", DbType.NVARCHAR, 100)]
            public string Title { get; set; }

            [AccessColumn("Comments", DbType.TEXT)]
            [SqlColumn("Notes", DbType.TEXT)]
            public string Notes { get; set; }
        }
    }
}
