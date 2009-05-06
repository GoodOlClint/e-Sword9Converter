using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter.Tables
{
    public class Overlay : Database
    {
        public Overlay(IParent Parent)
            : base(Parent)
        {
        }

        public class OverlayTable : Table<OverlayTable>
        {
            public int ID { get; set; }
            public string Bible { get; set; }
            public int BookID { get; set; }
            public int Chapter { get; set; }
            public string Codes { get; set; }
        }
    }
}
