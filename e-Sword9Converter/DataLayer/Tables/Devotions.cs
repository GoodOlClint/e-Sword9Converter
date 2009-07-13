/*
 * Copyright (c) 2009, GoodOlClint All rights reserved.
 * Redistribution and use in source and binary forms, with or without modification, are permitted
 * provided that the following conditions are met:
 * Redistributions of source code must retain the above copyright notice, this list of conditions
 * and the following disclaimer.
 * Redistributions in binary form must reproduce the above copyright notice, this list of conditions
 * and the following disclaimer in the documentation and/or other materials provided with the distribution.
 * Neither the name of the e-Sword Users nor the names of its contributors may be used to endorse
 * or promote products derived from this software without specific prior written permission.
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS
 * OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER
 * IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT
 * OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eSword9Converter.Tables
{
    public class Devotion : Database
    {
        public Devotion()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Devotion", new Devotions());
        }

        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Description", DbType.NVARCHAR, 255)]
            public string Description { get { return Convert.ToString(this.Rows[0]["Description"]); } set { this.Rows[0]["Description"] = value; } }

            [Column("Abbreviation", DbType.NVARCHAR, 50)]
            public string Abbreviation { get { return Convert.ToString(this.Rows[0]["Abbreviation"]); } set { this.Rows[0]["Abbreviation"] = value; } }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get { return Convert.ToString(this.Rows[0]["Comments"]); } set { this.Rows[0]["Comments"] = value; } }

            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }
        }

        [AccessTable("Copyright")]
        public class Copyright : Table<Copyright>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Author", DbType.TEXT)]
            public string Author { get; set; }

            [Column("Title", DbType.TEXT)]
            public string Title { get; set; }

            [Column("Place", DbType.TEXT)]
            public string Place { get; set; }

            [Column("Publisher", DbType.TEXT)]
            public string Publisher { get; set; }

            [Column("Year", DbType.TEXT)]
            public string Year { get; set; }

            [Column("Edition", DbType.TEXT)]
            public string Edition { get; set; }

            [Column("Copyright", DbType.TEXT)]
            public string Copyright1 { get; set; }

            [Column("Notes", DbType.TEXT)]
            public string Notes { get; set; }
        }

        [Table("Devotions")]
        public class Devotions : Table<Devotions>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Month", DbType.INT)]
            [Index("MonthDayIndex")]
            public int Month { get; set; }

            [Column("Day", DbType.INT)]
            [Index("MonthDayIndex")]
            public int Day { get; set; }

            [Column("Devotion", DbType.TEXT)]
            public string Devotion { get; set; }
        }
    }
}
