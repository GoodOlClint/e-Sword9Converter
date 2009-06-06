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
    public class Bible : Database
    {
        public Bible()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Bible", new BibleTable());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!this.Skip)
            {
                //Remove Invalid Scripture Entries;
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                          where ((string)Row["Scripture"]) == ""
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    ((BibleTable)this.Tables["Bible"]).Rows.Remove(Row);
                        count++;
                    Controller.RaiseProgressChanged(this, count);
                }
                ((Details)this.Tables["Details"]).NT = Convert.ToBoolean((from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                          where ((int)Row["BookID"]) == 66
                                                                          select Row).Count() > 0);
                ((Details)this.Tables["Details"]).OT = Convert.ToBoolean((from ThreadSafeDictionary<string, object> Row in ((BibleTable)this.Tables["Bible"]).Rows
                                                                          where ((int)Row["BookID"]) == 1
                                                                          select Row).Count() > 0);
                ((Details)this.Tables["Details"]).Version = 2;
                ((Details)this.Tables["Details"]).RightToLeft = (((Details)this.Tables["Details"]).Font.ToUpper() == "HEBREW");
            }
        }
        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Description", DbType.NVARCHAR, 250)]
            public string Description { get { return Convert.ToString(this.Rows[0]["Description"]); } set { this.Rows[0]["Description"] = value; } }

            [Column("Abbreviation", DbType.NVARCHAR, 50)]
            public string Abbreviation { get { return Convert.ToString(this.Rows[0]["Abbreviation"]); } set { this.Rows[0]["Abbreviation"] = value; } }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get { return Convert.ToString(this.Rows[0]["string"]); } set { this.Rows[0]["Font"] = value; } }

            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }

            [Column("Font", DbType.TEXT)]
            public string Font { get { return Convert.ToString(this.Rows[0]["Font"]); } set { this.Rows[0]["Font"] = value; } }

            [SqlColumn("RightToLeft", DbType.BOOL)]
            public bool RightToLeft { get { return Convert.ToBoolean(this.Rows[0]["RightToLeft"]); } set { this.Rows[0]["RightToLeft"] = value; } }

            [SqlColumn("OT", DbType.BOOL)]
            public bool OT { get { return Convert.ToBoolean(this.Rows[0]["OT"]); } set { this.Rows[0]["OT"] = value; } }

            [SqlColumn("NT", DbType.BOOL)]
            public bool NT { get { return Convert.ToBoolean(this.Rows[0]["NT"]); } set { this.Rows[0]["NT"] = value; } }

            [Column("Apocrypha", DbType.BOOL)]
            public bool Apocrypha { get { return Convert.ToBoolean(this.Rows[0]["Apocrypha"]); } set { this.Rows[0]["Apocrypha"] = value; } }

            [SqlColumn("Strong", DbType.BOOL)]
            [AccessColumn("Strongs", DbType.BOOL)]
            public bool Strong { get { return Convert.ToBoolean(this.Rows[0]["Strong"]); } set { this.Rows[0]["Strong"] = value; } }
        }

        [Table("Bible")]
        public class BibleTable : Table<BibleTable>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int BookID { get; set; }

            [Column("Chapter", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Chapter { get; set; }

            [Column("Verse", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Verse { get; set; }

            [Column("Scripture", DbType.TEXT)]
            public string Scripture { get; set; }
        }
    }
}
