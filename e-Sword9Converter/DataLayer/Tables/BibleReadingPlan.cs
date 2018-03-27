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
    public class BibleReadingPlan : Database
    {
        public BibleReadingPlan()
        {
            this.Tables.Add("Details", new Details());
            this.Tables.Add("Plan", new Plan());
        }
        [Table("Details")]
        public class Details : Table<Details>
        {
            [Column("Sunday",DbType.BOOL)]
            public bool Sunday { get; set; }
            [Column("Monday", DbType.BOOL)]
            public bool Monday { get; set; }
            [Column("Tuesday", DbType.BOOL)]
            public bool Tuesday { get; set; }
            [Column("Wednesday", DbType.BOOL)]
            public bool Wednesday { get; set; }
            [Column("Thursday", DbType.BOOL)]
            public bool Thursday { get; set; }
            [Column("Friday", DbType.BOOL)]
            public bool Friday { get; set; }
            [Column("Saturday", DbType.BOOL)]
            public bool Saturday { get; set; }
            [Column("Comments", DbType.TEXT)]
            public string Comments { get; set; }
        }

        [Table("Plan")]
        public class Plan : Table<Plan>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }
            
            [Column("Day",DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int Day { get; set; }
            
            [SqlColumn("Book", DbType.INT)]
            [AccessColumn("Book ID", DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int BookId { get; set; }
            
            [AccessColumn("Start Chapter", DbType.INT)]
            [SqlColumn("ChapterBegin", DbType.INT)]
            [Index("DayBookChapterIndex")]
            public int StartChapter { get; set; }

            [AccessColumn("End Chapter", DbType.INT)]
            [SqlColumn("ChapterEnd", DbType.INT)]
            public int EndChapter { get; set; }
            
            [Column("Completed", DbType.BOOL)]
            public bool Completed { get; set; }
        }
    }
}
