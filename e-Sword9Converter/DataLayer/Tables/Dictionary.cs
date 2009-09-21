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
using System.Text.RegularExpressions;

namespace eSword9Converter.Tables
{
    public class Dictionary : Database
    {
        public Dictionary()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Dictionary", new DictionaryTable());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!Skip)
            {
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((DictionaryTable)this.Tables["Dictionary"]).Rows
                                                                          orderby Row["Topic"] ascending
                                                                          select Row).ToArray();

                ((Details)this.Tables["Details"]).Version = 2;
                Regex strongsRegex = new Regex(@"[gGhH]\d+");


                bool strong = ((from ThreadSafeDictionary<string, object> Row in ((DictionaryTable)this.Tables["Dictionary"]).Rows
                                where strongsRegex.Matches((string)Row["Topic"]).Count > 0
                                select Row).Count() > 0);

                ((Details)this.Tables["Details"]).Strong = strong;

                if (!strong)
                {
                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.Clear();

                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.FromArray(rows.ToArray());
                }
                else
                {
                    rows = (from ThreadSafeDictionary<string, object> Row in ((DictionaryTable)this.Tables["Dictionary"]).Rows
                            where strongsRegex.Matches((string)Row["Topic"]).Count == 0
                            orderby ((string)Row["Topic"]) ascending
                            select Row).ToArray();

                    IEnumerable<ThreadSafeDictionary<string, object>> HebRows = (from ThreadSafeDictionary<string, object> Row in ((DictionaryTable)this.Tables["Dictionary"]).Rows
                                                                                 where ((string)Row["Topic"]).ToUpper().StartsWith("H")
                                                                                 where strongsRegex.Matches((string)Row["Topic"]).Count > 0
                                                                                 orderby Convert.ToInt32(((string)Row["Topic"]).Remove(0, 1)) ascending
                                                                                 select Row).ToArray();

                    IEnumerable<ThreadSafeDictionary<string, object>> GreRows = (from ThreadSafeDictionary<string, object> Row in ((DictionaryTable)this.Tables["Dictionary"]).Rows
                                                                                 where ((string)Row["Topic"]).ToUpper().StartsWith("G")
                                                                                 where strongsRegex.Matches((string)Row["Topic"]).Count > 0
                                                                                 orderby Convert.ToInt32(((string)Row["Topic"]).Remove(0, 1)) ascending
                                                                                 select Row).ToArray();
                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.Clear();
                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.FromArray(rows.ToArray());
                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.FromArray(HebRows.ToArray());
                    ((DictionaryTable)this.Tables["Dictionary"]).Rows.FromArray(GreRows.ToArray());
                }
            }
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

            [SqlColumn("Strong", DbType.BOOL)]
            public bool Strong { get { return Convert.ToBoolean(this.Rows[0]["Strong"]); } set { this.Rows[0]["Strong"] = value; } }
        }

        [Table("Dictionary")]
        public class DictionaryTable : Table<DictionaryTable>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Topic", DbType.TEXT, 100)]
            [Index("TopicIndex")]
            public string Topic { get; set; }

            [Column("Definition", DbType.TEXT)]
            public string Definition { get; set; }
        }
    }
}
