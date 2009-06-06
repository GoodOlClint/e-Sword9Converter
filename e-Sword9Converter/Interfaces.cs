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

using System.Data.Common;

namespace eSword9Converter
{
    public interface IForm
    {
        void Controller_LanguageChangedEvent();
        void Controller_ConversionFinishedEvent();
        void AddOwnedForm(System.Windows.Forms.Form form);
    }
    public interface IColumn
    {
        DbType Type { get; set; }
        int Length { get; set; }
        string Name { get; set; }
        bool NotNull { get; set; }
        string PropertyName { get; set; }
        columnType colType { get; }
        string Hash { get; }
    }

    public interface IIndex
    {
        string Name { get; set; }
    }

    public interface ITable
    {
        void SaveToDatabase(DbProviderFactory Factory, string connectionString);
        string SQLCreateStatement();
        void Load(DbProviderFactory Factory, string connectionString);
        string TableName { get; set; }
        IDatabase DB { get; set; }
    }

    public interface IDatabase
    {
        string SourceDB { get; set; }
        string DestDB { get; set; }
        string FileName { get; set; }
        void Stop();
        bool Running { get; set; }
        bool Skip { get; set; }
    }
}
