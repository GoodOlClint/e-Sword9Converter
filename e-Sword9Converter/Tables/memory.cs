using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Memory : Database
    {
        public Memory(IParent Parent)
            : base(Parent)
        {
            this.Tables.Add("Memorize", new Memorize());
        }
        [Table("Memorize")]
        public class Memorize : Table<Memorize>
        {
            public int ID { get; set; }
            public string Reference { get; set; }
            public string Bible { get; set; }
            public string Category { get; set; }
            public string Hint { get; set; }
            public string Start { get; set; }
            public int Frequency { get; set; }

        }
    }
}
