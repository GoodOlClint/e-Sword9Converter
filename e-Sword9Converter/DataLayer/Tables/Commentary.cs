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
    public class Commentary : Database
    {
        public Commentary()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Books", new Books());
            this.Tables.Add("Chapters", new Chapters());
            this.Tables.Add("Verses", new Verses());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!Skip)
            {
                ((Details)this.Tables["Details"]).Version = 2;
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((Verses)this.Tables["Verses"]).Rows
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    Row["ChapterEnd"] = Row["ChapterBegin"];
                    Row["Comments"] = ((string)Row["Comments"]).Trim();
                    count++;
                    Controller.RaiseProgressChanged(this, count);
                }
                ((Details)this.Tables["Details"]).Description = ((Details)this.Tables["Details"]).Description.Trim();
                ((Details)this.Tables["Details"]).Comments = ((Details)this.Tables["Details"]).Comments.Trim();
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
            public string Comments { get { return Convert.ToString(this.Rows[0]["string"]); } set { this.Rows[0]["Font"] = value; } }

            [SqlColumn("Version", DbType.INT)]
            public int Version { get { return Convert.ToInt32(this.Rows[0]["Version"]); } set { this.Rows[0]["Version"] = value; } }
        }

        [SqlTable("Books")]
        [AccessTable("[Book Notes]")]
        public class Books : Table<Books>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book Id", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookIndex")]
            public int BookID { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [SqlTable("Chapters")]
        [AccessTable("[Chapter Notes]")]
        public class Chapters : Table<Chapters>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterIndex")]
            public int BookID { get; set; }

            [Column("Chapter", DbType.INT)]
            [Index("BookChapterIndex")]
            public int Chapter { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [SqlTable("Verses")]
        [AccessTable("Commentary")]
        public class Verses : Table<Verses>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [AccessColumn("Book ID", DbType.INT)]
            [SqlColumn("Book", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int BookID { get; set; }

            [AccessColumn("Chapter", DbType.INT)]
            [SqlColumn("ChapterBegin", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int ChapterBegin { get; set; }

            [SqlColumn("ChapterEnd", DbType.INT)]
            public int ChapterEnd { get; set; }

            [AccessColumn("Start Verse", DbType.INT)]
            [SqlColumn("VerseBegin", DbType.INT)]
            [Index("BookChapterVerseIndex")]
            public int VerseBegin { get; set; }

            [AccessColumn("End Verse", DbType.INT)]
            [SqlColumn("VerseEnd", DbType.INT)]
            public int VerseEnd { get; set; }

            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }
    }
}
