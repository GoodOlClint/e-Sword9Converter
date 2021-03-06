﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Memory : Database
    {
        public Memory()
        {
            this.Tables.Add("Memorize", new Memorize());
        }
        [Table("Memorize")]
        public class Memorize : Table<Memorize>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Reference", DbType.TEXT)]
            public string Reference { get; set; }

            [Column("Bible", DbType.TEXT)]
            public string Bible { get; set; }

            [Column("Category", DbType.TEXT)]
            public string Category { get; set; }

            [Column("Hint", DbType.TEXT)]
            public string Hint { get; set; }

            [Column("Start", DbType.TEXT)]
            public string Start { get; set; }

            [Column("Frequency", DbType.INT)]
            public int Frequency { get; set; }

        }
    }
}
