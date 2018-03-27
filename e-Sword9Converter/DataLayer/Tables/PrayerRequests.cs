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
    public class PrayerRequests : Database
    {
        public PrayerRequests()
        {
            this.Tables.Add("Plan", new Plan());
        }
        public override void Load(string File)
        {
            base.Load(File);
            if (!this.Skip)
            {
                //Convert Access State DateTime to SQLite Int
                IEnumerable<ThreadSafeDictionary<string, object>> rows = (from ThreadSafeDictionary<string, object> Row in ((Plan)this.Tables["Plan"]).Rows
                                                                          orderby Row["ID"] ascending
                                                                          select Row).ToArray();
                Controller.RaiseStatusChanged(this, updateStatus.Converting);
                Controller.SetMaxValue(this, rows.Count());
                int count = 0;
                foreach (ThreadSafeDictionary<string, object> Row in rows)
                {
                    ((Plan)this.Tables["Plan"]).Rows.Remove(Row); 
                    DateTime epoch = new DateTime(1900, 1, 1);
                    TimeSpan span = new TimeSpan();
                    span = ((DateTime)Row["accessStart"]).Subtract(epoch);
                    //span = ((Plan)this.Tables["Plan"]).accessStart.Subtract(epoch);
                    Row["Start"] = (object)(span.Days + 1);
                    ((Plan)this.Tables["Plan"]).Rows.Add(Row);
                    count++;
                    Controller.RaiseProgressChanged(this, count);
                }
            }
        }
        [Table("Requests")]
        public class Plan : Table<Plan>
        {
            [AccessColumn("ID", DbType.INT)]
            public int ID { get; set; }

            [Column("Title", DbType.NVARCHAR, 50)]
            public string Title { get; set; }

            [Column("Type", DbType.INT)]
            public int Type { get; set; }

            [Column("Frequency", DbType.INT)]
            public int Frequency { get; set; }

            [AccessColumn("Start", DbType.DATETIME)]
            public DateTime accessStart { get; set; }

            [SqlColumn("Start", DbType.INT)]
            public int Start { get; set; }

            [Column("Request", DbType.TEXT)]
            public string Request { get; set; }
        }
    }
}
