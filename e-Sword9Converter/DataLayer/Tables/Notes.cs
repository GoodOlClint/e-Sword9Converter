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
    public class Notes : Database
    {
        private VerseReferences VerseReferences;
        public Notes()
        {
            this.Tables.Add("VerseNotes", new VerseNotes());
            this.VerseReferences = new VerseReferences();
        }

        public override void Load(string Path)
        {
            base.Load(Path);
            if (!Skip)
            {
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((VerseNotes)this.Tables["VerseNotes"]).Rows
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    int VerseID = Convert.ToInt32(Row["VerseID"]);
                    var reference = (from VerseReference v in this.VerseReferences
                                     where v.EndVerse >= VerseID && v.StartVerse <= VerseID
                                     select v).First<VerseReference>();
                    Row["Book"] = reference.Book;
                    Row["Chapter"] = reference.Chapter;
                    Row["Verse"] = (VerseID - reference.StartVerse) + 1;
                    count++;
                    Controller.RaiseProgressChanged(this, count);
                }
            }
        }

        [AccessTable("[Verse Notes]")]
        [SqlTable("Verses")]
        public class VerseNotes : Table<VerseNotes>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Verse ID", DbType.INT)]
            public int VerseID { get; set; }

            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Book { get; set; }

            [SqlColumn("Chapter", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Chapter { get; set; }

            [SqlColumn("Verse", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int Verse { get; set; }

            [Column("Notes", DbType.TEXT)]
            public string Comments { get; set; }
        }
    }
}
