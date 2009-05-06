using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Data.OleDb;
using System;

namespace e_Sword9Converter
{
    //namespace odbc
    //{
    //    namespace ANL
    //    {
    //        public class Words
    //        {
    //            public string Word;
    //            public Words(object word)
    //            {
    //                try { this.Word = Convert.ToString(word); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace BBL
    //    {
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public string Font;
    //            public bool Apocrypha;
    //            public bool Strongs;
    //            public Details(object Description, object Abbreviation, object Comments, object Font, object Apocrypha, object Strongs)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Font = Convert.ToString(Font); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Apocrypha = Convert.ToBoolean(Apocrypha); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Strongs = Convert.ToBoolean(Strongs); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Bible
    //        {
    //            public int BookID;
    //            public int Chapter;
    //            public int Verse;
    //            public string Scripture;
    //            public Bible(object BookID, object Chapter, object Verse, object Scripture)
    //            {
    //                try { this.BookID = Convert.ToInt32(BookID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Chapter = Convert.ToInt32(Chapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Verse = Convert.ToInt32(Verse); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Scripture = Convert.ToString(Scripture); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace BRP
    //    {
    //        public class Details
    //        {
    //            public bool Sunday;
    //            public bool Monday;
    //            public bool Tuesday;
    //            public bool Wednesday;
    //            public bool Thursday;
    //            public bool Friday;
    //            public bool Saturday;
    //            public string Comments;
    //            public Details(object Sunday, object Monday, object Tuesday, object Wednesday, object Thursday, object Friday, object Saturday, object Comments)
    //            {
    //                try { this.Sunday = Convert.ToBoolean(Sunday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Monday = Convert.ToBoolean(Monday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Tuesday = Convert.ToBoolean(Tuesday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Wednesday = Convert.ToBoolean(Wednesday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Thursday = Convert.ToBoolean(Thursday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Friday = Convert.ToBoolean(Friday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Saturday = Convert.ToBoolean(Saturday); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Plan
    //        {
    //            public int ID;
    //            public int Day;
    //            public int BookId;
    //            public int StartChapter;
    //            public int EndChapter;
    //            public bool Completed;
    //            public Plan(object ID, object Day, object BookId, object StartChapter, object EndChapter, object Completed)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Day = Convert.ToInt32(Day); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.BookId = Convert.ToInt32(BookId); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.StartChapter = Convert.ToInt32(StartChapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.EndChapter = Convert.ToInt32(EndChapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Completed = Convert.ToBoolean(Completed); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace CMT
    //    {
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public Details(object Description, object Abbreviation, object Comments)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class BookNotes
    //        {
    //            public int BookID;
    //            public string Comments;
    //            public BookNotes(object BookID, object Comments)
    //            {
    //                try { this.BookID = Convert.ToInt32(BookID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class ChapterNotes
    //        {
    //            public int BookID;
    //            public int Chapter;
    //            public string Comments;
    //            public ChapterNotes(object BookID, object Chapter, object Comments)
    //            {
    //                try { this.BookID = Convert.ToInt32(BookID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Chapter = Convert.ToInt32(Chapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Commentary
    //        {
    //            public int BookID;
    //            public int Chapter;
    //            public int StartVerse;
    //            public int EndVerse;
    //            public string Comments;
    //            public Commentary(object BookID, object Chapter, object StartVerse, object EndVerse, object Comments)
    //            {
    //                try { this.BookID = Convert.ToInt32(BookID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Chapter = Convert.ToInt32(Chapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.StartVerse = Convert.ToInt32(StartVerse); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.EndVerse = Convert.ToInt32(EndVerse); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace DCT
    //    {
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public Details(object Description, object Abbreviation, object Comments)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Dictionary
    //        {
    //            public string Topic;
    //            public string Definition;
    //            public Dictionary(object Topic, object Definition)
    //            {
    //                try { this.Topic = Convert.ToString(Topic); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Definition = Convert.ToString(Definition); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace DEV
    //    {
    //        public class Copyright
    //        {
    //            public int ID;
    //            public string Author;
    //            public string Title;
    //            public string Place;
    //            public string Publisher;
    //            public string Year;
    //            public string Edition;
    //            public string Copyright1;
    //            public string Notes;
    //            public Copyright(object ID, object Author, object Title, object Place, object Publisher, object Year, object Edition, object Copyright, object Notes)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Author = Convert.ToString(Author); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Title = Convert.ToString(Title); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Place = Convert.ToString(Place); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Publisher = Convert.ToString(Publisher); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Year = Convert.ToString(Year); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Edition = Convert.ToString(Edition); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Copyright1 = Convert.ToString(Copyright); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Notes = Convert.ToString(Notes); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public Details(object Description, object Abbreviation, object Comments)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Devotions
    //        {
    //            public int ID;
    //            public int Month;
    //            public int Day;
    //            public string Devotion;
    //            public Devotions(object ID, object Month, object Day)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Month = Convert.ToInt32(Month); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Day = Convert.ToInt32(Day); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace HAR
    //    {
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public Details(object Description, object Abbreviation, object Comments)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Harmony
    //        {
    //            public int ID;
    //            public int SectionID;
    //            public int Part;
    //            public string Reference;
    //            public Harmony(object ID, object SectionID, object Part, object Reference)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.SectionID = Convert.ToInt32(SectionID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Part = Convert.ToInt32(Part); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Reference = Convert.ToString(Reference); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Sections
    //        {
    //            public int ID;
    //            public string Title;
    //            public Sections(object ID, object Title)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Title = Convert.ToString(Title); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace LST
    //    {
    //        public class Verses
    //        {
    //            public int ID;
    //            public int VerseID;
    //            public int Order;
    //            public Verses(object ID, object VerseID, object Order)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.VerseID = Convert.ToInt32(VerseID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Order = Convert.ToInt32(Order); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace MAP
    //    {
    //        public class Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public Details(object Description, object Abbreviation, object Comments)
    //            {
    //                try { this.Description = Convert.ToString(Description); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Abbreviation = Convert.ToString(Abbreviation); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //        public class Graphics
    //        {
    //            public int ID;
    //            public string Title;
    //            public string Details;
    //            public object Picture;
    //            public Graphics(object ID, object Title, object Details, object Picture)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Title = Convert.ToString(Title); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Details = Convert.ToString(Details); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Picture = Picture; }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace MEM
    //    {
    //        public class Memorize
    //        {
    //            public int ID;
    //            public string Reference;
    //            public string Bible;
    //            public string Category;
    //            public string Hint;
    //            public string Start;
    //            public int Frequency;
    //            public Memorize(object ID, object Reference, object Bible, object Category, object Hint, object Start, object Frequency)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Reference = Convert.ToString(Reference); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Bible = Convert.ToString(Bible); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Category = Convert.ToString(Category); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Hint = Convert.ToString(Hint); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Start = Convert.ToString(Start); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Frequency = Convert.ToInt32(Frequency); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace NOT
    //    {
    //        public class VerseNotes
    //        {
    //            public int ID;
    //            public int VerseID;
    //            public string Comments;
    //            public VerseNotes(object ID, object VerseID, object Comments)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.VerseID = Convert.ToInt32(VerseID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace OVL
    //    {
    //        public class Overlay
    //        {
    //            public int ID;
    //            public string Bible;
    //            public int BookID;
    //            public int Chapter;
    //            public string Codes;
    //            public Overlay(object ID, object Bible, object BookID, object Chapter, object Codes)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Bible = Convert.ToString(Bible); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.BookID = Convert.ToInt32(BookID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Chapter = Convert.ToInt32(Chapter); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Codes = Convert.ToString(Codes); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace PRL
    //    {
    //        public class Requests
    //        {
    //            public int ID;
    //            public string Title;
    //            public int Type;
    //            public int Frequency;
    //            public string Start;
    //            public string Request;
    //            public Requests(object ID, object Title, object Type, object Frequency, object Start, object Request)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Title = Convert.ToString(Title); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Type = Convert.ToInt32(Type); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Frequency = Convert.ToInt32(Frequency); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Start = Convert.ToString(Start); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Request = Convert.ToString(Request); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //    namespace TOP
    //    {
    //        public class TopicNotes
    //        {
    //            public int ID;
    //            public string Title;
    //            public string Comments;
    //            public TopicNotes(object ID, object Title, object Comments)
    //            {
    //                try { this.ID = Convert.ToInt32(ID); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Title = Convert.ToString(Title); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //                try { this.Comments = Convert.ToString(Comments); }
    //                catch (Exception ex) { Error.Record(this, ex); }
    //            }
    //        }
    //    }
    //}
    //namespace SQL
    //{
    //    namespace ANLX
    //    {
    //        public struct Words
    //        {
    //            public string Word;
    //            public void FromODBC(odbc.ANL.Words words)
    //            {
    //                try
    //                {
    //                    this.Word = words.Word;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace BBLX
    //    {
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public int Version;
    //            public string Font;
    //            public bool RightToLeft;
    //            public bool OT;
    //            public bool NT;
    //            public bool Apocrypha;
    //            public bool Strong;
    //            public void FromODBC(odbc.BBL.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                    this.Font = details.Font;
    //                    this.Apocrypha = details.Apocrypha;
    //                    this.Strong = details.Strongs;
    //                    this.RightToLeft = (this.Font.ToUpper() == "HEBREW");
    //                    this.Version = 2;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Bible
    //        {
    //            public int Book;
    //            public int Chapter;
    //            public int Verse;
    //            public string Scripture;
    //            public void FromODBC(odbc.BBL.Bible bible)
    //            {
    //                try
    //                {
    //                    this.Book = bible.BookID;
    //                    this.Chapter = bible.Chapter;
    //                    this.Verse = bible.Verse;
    //                    this.Scripture = bible.Scripture;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace BRPX
    //    {
    //        public struct Details
    //        {
    //            public bool Sunday;
    //            public bool Monday;
    //            public bool Tuesday;
    //            public bool Wednesday;
    //            public bool Thursday;
    //            public bool Friday;
    //            public bool Saturday;
    //            public string Comments;
    //            public void FromODBC(odbc.BRP.Details details)
    //            {
    //                try
    //                {
    //                    this.Sunday = details.Sunday;
    //                    this.Monday = details.Monday;
    //                    this.Tuesday = details.Tuesday;
    //                    this.Wednesday = details.Wednesday;
    //                    this.Thursday = details.Thursday;
    //                    this.Friday = details.Friday;
    //                    this.Saturday = details.Saturday;
    //                    this.Comments = details.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Plan
    //        {
    //            public int Day;
    //            public int Book;
    //            public int StartChapter;
    //            public int EndChapter;
    //            public bool Completed;
    //            public void FromODBC(odbc.BRP.Plan plan)
    //            {
    //                try
    //                {
    //                    this.Day = plan.Day;
    //                    this.Book = plan.BookId;
    //                    this.StartChapter = plan.StartChapter;
    //                    this.EndChapter = plan.EndChapter;
    //                    this.Completed = plan.Completed;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace CMTX
    //    {
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public int Version;
    //            public void FromODBC(odbc.CMT.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                    this.Version = 2;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct BookNotes
    //        {
    //            public int Book;
    //            public string Comments;
    //            public void FromODBC(odbc.CMT.BookNotes notes)
    //            {
    //                try
    //                {
    //                    this.Book = notes.BookID;
    //                    this.Comments = notes.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct ChapterNotes
    //        {
    //            public int Book;
    //            public int Chapter;
    //            public string Comments;
    //            public void FromODBC(odbc.CMT.ChapterNotes notes)
    //            {
    //                try
    //                {
    //                    this.Book = notes.BookID;
    //                    this.Chapter = notes.Chapter;
    //                    this.Comments = notes.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Commentary
    //        {
    //            public int Book;
    //            public int ChapterBegin;
    //            public int ChapterEnd;
    //            public int VerseBegin;
    //            public int VerseEnd;
    //            public string Comments;
    //            public void FromODBC(odbc.CMT.Commentary commentary)
    //            {
    //                try
    //                {
    //                    this.Book = commentary.BookID;
    //                    this.ChapterBegin = commentary.Chapter;
    //                    this.ChapterEnd = commentary.Chapter;
    //                    this.VerseBegin = commentary.StartVerse;
    //                    this.VerseEnd = commentary.EndVerse;
    //                    this.Comments = commentary.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace DCTX
    //    {
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public int Version;
    //            public bool Strong;
    //            public void FromODBC(odbc.DCT.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                    this.Version = 2;
    //                    this.Strong = false;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Dictionary
    //        {
    //            public string Topic;
    //            public string Definition;
    //            public void FromODBC(odbc.DCT.Dictionary dictionary)
    //            {
    //                try
    //                {
    //                    this.Topic = dictionary.Topic;
    //                    this.Definition = dictionary.Definition;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace DEVX
    //    {
    //        public struct Copyright
    //        {
    //            public int ID;
    //            public string Author;
    //            public string Title;
    //            public string Place;
    //            public string Publisher;
    //            public string Year;
    //            public string Edition;
    //            public string Copyright1;
    //            public string Notes;
    //            public void FromODBC(odbc.DEV.Copyright copyright)
    //            {
    //                try
    //                {
    //                    this.ID = copyright.ID;
    //                    this.Author = copyright.Author;
    //                    this.Title = copyright.Title;
    //                    this.Place = copyright.Place;
    //                    this.Publisher = copyright.Publisher;
    //                    this.Year = copyright.Year;
    //                    this.Edition = copyright.Edition;
    //                    this.Copyright1 = copyright.Copyright1;
    //                    this.Notes = copyright.Notes;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public void FromODBC(odbc.DEV.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Devotions
    //        {
    //            public int ID;
    //            public int Month;
    //            public int Day;
    //            public string Devotion;
    //            public void FromODBC(odbc.DEV.Devotions devotions)
    //            {
    //                try
    //                {
    //                    this.ID = devotions.ID;
    //                    this.Month = devotions.Month;
    //                    this.Day = devotions.Day;
    //                    this.Devotion = devotions.Devotion;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace HARX
    //    {
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public int Version;
    //            public void FromODBC(odbc.HAR.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                    this.Version = 2;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Parts
    //        {
    //            public int Section;
    //            public int Part;
    //            public string Reference;
    //            public void FromODBC(odbc.HAR.Harmony harmony)
    //            {
    //                try
    //                {
    //                    this.Section = harmony.SectionID;
    //                    this.Part = harmony.Part;
    //                    this.Reference = harmony.Reference;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Sections
    //        {
    //            public int ID;
    //            public string Title;
    //            public void FromODBC(odbc.HAR.Sections sections)
    //            {
    //                try
    //                {
    //                    this.ID = sections.ID;
    //                    this.Title = sections.Title;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace LSTX
    //    {
    //        public struct Verses
    //        {
    //            public int Book;
    //            public int Chapter;
    //            public int Verse;
    //            public int Position;
    //            public void FromODBC(odbc.LST.Verses verses)
    //            {
    //                try
    //                {
    //                    var reference = (from VerseList v in Program.VerseLists
    //                                     where v.EndVerse <= verses.VerseID
    //                                     where v.StartVerse >= verses.VerseID
    //                                     select v).First<VerseList>();
    //                    this.Book = reference.Book;
    //                    this.Chapter = reference.Chapter;
    //                    this.Verse = (verses.VerseID - reference.StartVerse) + 1;
    //                    this.Position = verses.Order;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace MAPX
    //    {
    //        public struct Details
    //        {
    //            public string Description;
    //            public string Abbreviation;
    //            public string Comments;
    //            public int Version;
    //            public void FromODBC(odbc.MAP.Details details)
    //            {
    //                try
    //                {
    //                    this.Description = details.Description;
    //                    this.Abbreviation = details.Abbreviation;
    //                    this.Comments = details.Comments;
    //                    this.Version = 2;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //        public struct Graphics
    //        {
    //            public string Title;
    //            public string Details;
    //            public object Picture;
    //            public void FromODBC(odbc.MAP.Graphics graphics)
    //            {
    //                try
    //                {
    //                    this.Title = graphics.Title;
    //                    this.Details = graphics.Details;
    //                    this.Picture = graphics.Picture;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace MEMX
    //    {
    //        public struct Memorize
    //        {
    //            public int ID;
    //            public string Reference;
    //            public string Bible;
    //            public string Category;
    //            public string Hint;
    //            public string Start;
    //            public int Frequency;
    //            public void FromODBC(odbc.MEM.Memorize memorize)
    //            {
    //                try
    //                {
    //                    this.ID = memorize.ID;
    //                    this.Reference = memorize.Reference;
    //                    this.Bible = memorize.Bible;
    //                    this.Category = memorize.Category;
    //                    this.Hint = memorize.Hint;
    //                    this.Start = memorize.Start;
    //                    this.Frequency = memorize.Frequency;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace NOTX
    //    {
    //        public struct VerseNotes
    //        {
    //            public int Book;
    //            public int Chapter;
    //            public int Verse;
    //            public string Comments;
    //            public void FromODBC(odbc.NOT.VerseNotes notes)
    //            {
    //                try
    //                {
    //                    var reference = (from VerseList v in Program.VerseLists
    //                                     where v.EndVerse <= notes.VerseID
    //                                     where v.StartVerse >= notes.VerseID
    //                                     select v).First<VerseList>();
    //                    this.Book = reference.Book;
    //                    this.Chapter = reference.Chapter;
    //                    this.Verse = (notes.VerseID - reference.StartVerse) + 1;
    //                    this.Comments = notes.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace OVLX
    //    {
    //        public struct Overlay
    //        {
    //            public int ID;
    //            public string Bible;
    //            public int BookID;
    //            public int Chapter;
    //            public string Codes;
    //            public void FromODBC(odbc.OVL.Overlay overlay)
    //            {
    //                try
    //                {
    //                    this.ID = overlay.ID;
    //                    this.Bible = overlay.Bible;
    //                    this.BookID = overlay.BookID;
    //                    this.Chapter = overlay.Chapter;
    //                    this.Codes = overlay.Codes;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace PRLX
    //    {
    //        public struct Requests
    //        {
    //            public int ID;
    //            public string Title;
    //            public int Type;
    //            public int Frequency;
    //            public string Start;
    //            public string Request;
    //            public void FromODBC(odbc.PRL.Requests requests)
    //            {
    //                try
    //                {
    //                    this.ID = requests.ID;
    //                    this.Title = requests.Title;
    //                    this.Type = requests.Type;
    //                    this.Frequency = requests.Frequency;
    //                    this.Start = requests.Start;
    //                    this.Request = requests.Request;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //    namespace TOPX
    //    {
    //        public struct TopicNotes
    //        {
    //            public string Title;
    //            public string Comments;
    //            public void FromODBC(odbc.TOP.TopicNotes notes)
    //            {
    //                try
    //                {
    //                    this.Title = notes.Title;
    //                    this.Comments = notes.Comments;
    //                }
    //                catch (Exception ex)
    //                {
    //                    Error.Record(this, ex);
    //                }
    //            }
    //        }
    //    }
    //}
    
}