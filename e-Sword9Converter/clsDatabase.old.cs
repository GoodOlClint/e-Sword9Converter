using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SQLite;
using System.Data.OleDb;
using System;

namespace e_Sword9Converter
{
    namespace odbc
    {
        namespace ANL
        {
            public class Words
            {
                public string Word;
                public Words(object word)
                {
                    try { this.Word = Convert.ToString(word); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace BBL
        {
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public string Font;
                public bool Apocrypha;
                public bool Strongs;
                public Details(object Description, object Abbreviation, object Comments, object Font, object Apocrypha, object Strongs)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Font = Convert.ToString(Font); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Apocrypha = Convert.ToBoolean(Apocrypha); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Strongs = Convert.ToBoolean(Strongs); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Bible
            {
                public int BookID;
                public int Chapter;
                public int Verse;
                public string Scripture;
                public Bible(object BookID, object Chapter, object Verse, object Scripture)
                {
                    try { this.BookID = Convert.ToInt32(BookID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Chapter = Convert.ToInt32(Chapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Verse = Convert.ToInt32(Verse); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Scripture = Convert.ToString(Scripture); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace BRP
        {
            public class Details
            {
                public bool Sunday;
                public bool Monday;
                public bool Tuesday;
                public bool Wednesday;
                public bool Thursday;
                public bool Friday;
                public bool Saturday;
                public string Comments;
                public Details(object Sunday, object Monday, object Tuesday, object Wednesday, object Thursday, object Friday, object Saturday, object Comments)
                {
                    try { this.Sunday = Convert.ToBoolean(Sunday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Monday = Convert.ToBoolean(Monday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Tuesday = Convert.ToBoolean(Tuesday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Wednesday = Convert.ToBoolean(Wednesday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Thursday = Convert.ToBoolean(Thursday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Friday = Convert.ToBoolean(Friday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Saturday = Convert.ToBoolean(Saturday); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Plan
            {
                public int ID;
                public int Day;
                public int BookId;
                public int StartChapter;
                public int EndChapter;
                public bool Completed;
                public Plan(object ID, object Day, object BookId, object StartChapter, object EndChapter, object Completed)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Day = Convert.ToInt32(Day); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.BookId = Convert.ToInt32(BookId); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.StartChapter = Convert.ToInt32(StartChapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.EndChapter = Convert.ToInt32(EndChapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Completed = Convert.ToBoolean(Completed); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace CMT
        {
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public Details(object Description, object Abbreviation, object Comments)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class BookNotes
            {
                public int BookID;
                public string Comments;
                public BookNotes(object BookID, object Comments)
                {
                    try { this.BookID = Convert.ToInt32(BookID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class ChapterNotes
            {
                public int BookID;
                public int Chapter;
                public string Comments;
                public ChapterNotes(object BookID, object Chapter, object Comments)
                {
                    try { this.BookID = Convert.ToInt32(BookID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Chapter = Convert.ToInt32(Chapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Commentary
            {
                public int BookID;
                public int Chapter;
                public int StartVerse;
                public int EndVerse;
                public string Comments;
                public Commentary(object BookID, object Chapter, object StartVerse, object EndVerse, object Comments)
                {
                    try { this.BookID = Convert.ToInt32(BookID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Chapter = Convert.ToInt32(Chapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.StartVerse = Convert.ToInt32(StartVerse); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.EndVerse = Convert.ToInt32(EndVerse); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace DCT
        {
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public Details(object Description, object Abbreviation, object Comments)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Dictionary
            {
                public string Topic;
                public string Definition;
                public Dictionary(object Topic, object Definition)
                {
                    try { this.Topic = Convert.ToString(Topic); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Definition = Convert.ToString(Definition); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace DEV
        {
            public class Copyright
            {
                public int ID;
                public string Author;
                public string Title;
                public string Place;
                public string Publisher;
                public string Year;
                public string Edition;
                public string Copyright1;
                public string Notes;
                public Copyright(object ID, object Author, object Title, object Place, object Publisher, object Year, object Edition, object Copyright, object Notes)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Author = Convert.ToString(Author); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Title = Convert.ToString(Title); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Place = Convert.ToString(Place); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Publisher = Convert.ToString(Publisher); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Year = Convert.ToString(Year); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Edition = Convert.ToString(Edition); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Copyright1 = Convert.ToString(Copyright); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Notes = Convert.ToString(Notes); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public Details(object Description, object Abbreviation, object Comments)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Devotions
            {
                public int ID;
                public int Month;
                public int Day;
                public string Devotion;
                public Devotions(object ID, object Month, object Day)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Month = Convert.ToInt32(Month); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Day = Convert.ToInt32(Day); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace HAR
        {
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public Details(object Description, object Abbreviation, object Comments)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Harmony
            {
                public int ID;
                public int SectionID;
                public int Part;
                public string Reference;
                public Harmony(object ID, object SectionID, object Part, object Reference)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.SectionID = Convert.ToInt32(SectionID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Part = Convert.ToInt32(Part); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Reference = Convert.ToString(Reference); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Sections
            {
                public int ID;
                public string Title;
                public Sections(object ID, object Title)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Title = Convert.ToString(Title); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace LST
        {
            public class Verses
            {
                public int ID;
                public int VerseID;
                public int Order;
                public Verses(object ID, object VerseID, object Order)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.VerseID = Convert.ToInt32(VerseID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Order = Convert.ToInt32(Order); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace MAP
        {
            public class Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public Details(object Description, object Abbreviation, object Comments)
                {
                    try { this.Description = Convert.ToString(Description); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Abbreviation = Convert.ToString(Abbreviation); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
            public class Graphics
            {
                public int ID;
                public string Title;
                public string Details;
                public object Picture;
                public Graphics(object ID, object Title, object Details, object Picture)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Title = Convert.ToString(Title); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Details = Convert.ToString(Details); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Picture = Picture; }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace MEM
        {
            public class Memorize
            {
                public int ID;
                public string Reference;
                public string Bible;
                public string Category;
                public string Hint;
                public string Start;
                public int Frequency;
                public Memorize(object ID, object Reference, object Bible, object Category, object Hint, object Start, object Frequency)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Reference = Convert.ToString(Reference); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Bible = Convert.ToString(Bible); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Category = Convert.ToString(Category); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Hint = Convert.ToString(Hint); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Start = Convert.ToString(Start); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Frequency = Convert.ToInt32(Frequency); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace NOT
        {
            public class VerseNotes
            {
                public int ID;
                public int VerseID;
                public string Comments;
                public VerseNotes(object ID, object VerseID, object Comments)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.VerseID = Convert.ToInt32(VerseID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace OVL
        {
            public class Overlay
            {
                public int ID;
                public string Bible;
                public int BookID;
                public int Chapter;
                public string Codes;
                public Overlay(object ID, object Bible, object BookID, object Chapter, object Codes)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Bible = Convert.ToString(Bible); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.BookID = Convert.ToInt32(BookID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Chapter = Convert.ToInt32(Chapter); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Codes = Convert.ToString(Codes); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace PRL
        {
            public class Requests
            {
                public int ID;
                public string Title;
                public int Type;
                public int Frequency;
                public string Start;
                public string Request;
                public Requests(object ID, object Title, object Type, object Frequency, object Start, object Request)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Title = Convert.ToString(Title); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Type = Convert.ToInt32(Type); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Frequency = Convert.ToInt32(Frequency); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Start = Convert.ToString(Start); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Request = Convert.ToString(Request); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
        namespace TOP
        {
            public class TopicNotes
            {
                public int ID;
                public string Title;
                public string Comments;
                public TopicNotes(object ID, object Title, object Comments)
                {
                    try { this.ID = Convert.ToInt32(ID); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Title = Convert.ToString(Title); }
                    catch (Exception ex) { Error.Record(this, ex); }
                    try { this.Comments = Convert.ToString(Comments); }
                    catch (Exception ex) { Error.Record(this, ex); }
                }
            }
        }
    }
    namespace SQL
    {
        namespace ANLX
        {
            public struct Words
            {
                public string Word;
                public void FromODBC(odbc.ANL.Words words)
                {
                    try
                    {
                        this.Word = words.Word;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace BBLX
        {
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public int Version;
                public string Font;
                public bool RightToLeft;
                public bool OT;
                public bool NT;
                public bool Apocrypha;
                public bool Strong;
                public void FromODBC(odbc.BBL.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                        this.Font = details.Font;
                        this.Apocrypha = details.Apocrypha;
                        this.Strong = details.Strongs;
                        this.RightToLeft = (this.Font.ToUpper() == "HEBREW");
                        this.Version = 2;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Bible
            {
                public int Book;
                public int Chapter;
                public int Verse;
                public string Scripture;
                public void FromODBC(odbc.BBL.Bible bible)
                {
                    try
                    {
                        this.Book = bible.BookID;
                        this.Chapter = bible.Chapter;
                        this.Verse = bible.Verse;
                        this.Scripture = bible.Scripture;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace BRPX
        {
            public struct Details
            {
                public bool Sunday;
                public bool Monday;
                public bool Tuesday;
                public bool Wednesday;
                public bool Thursday;
                public bool Friday;
                public bool Saturday;
                public string Comments;
                public void FromODBC(odbc.BRP.Details details)
                {
                    try
                    {
                        this.Sunday = details.Sunday;
                        this.Monday = details.Monday;
                        this.Tuesday = details.Tuesday;
                        this.Wednesday = details.Wednesday;
                        this.Thursday = details.Thursday;
                        this.Friday = details.Friday;
                        this.Saturday = details.Saturday;
                        this.Comments = details.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Plan
            {
                public int Day;
                public int Book;
                public int StartChapter;
                public int EndChapter;
                public bool Completed;
                public void FromODBC(odbc.BRP.Plan plan)
                {
                    try
                    {
                        this.Day = plan.Day;
                        this.Book = plan.BookId;
                        this.StartChapter = plan.StartChapter;
                        this.EndChapter = plan.EndChapter;
                        this.Completed = plan.Completed;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace CMTX
        {
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public int Version;
                public void FromODBC(odbc.CMT.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                        this.Version = 2;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct BookNotes
            {
                public int Book;
                public string Comments;
                public void FromODBC(odbc.CMT.BookNotes notes)
                {
                    try
                    {
                        this.Book = notes.BookID;
                        this.Comments = notes.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct ChapterNotes
            {
                public int Book;
                public int Chapter;
                public string Comments;
                public void FromODBC(odbc.CMT.ChapterNotes notes)
                {
                    try
                    {
                        this.Book = notes.BookID;
                        this.Chapter = notes.Chapter;
                        this.Comments = notes.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Commentary
            {
                public int Book;
                public int ChapterBegin;
                public int ChapterEnd;
                public int VerseBegin;
                public int VerseEnd;
                public string Comments;
                public void FromODBC(odbc.CMT.Commentary commentary)
                {
                    try
                    {
                        this.Book = commentary.BookID;
                        this.ChapterBegin = commentary.Chapter;
                        this.ChapterEnd = commentary.Chapter;
                        this.VerseBegin = commentary.StartVerse;
                        this.VerseEnd = commentary.EndVerse;
                        this.Comments = commentary.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace DCTX
        {
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public int Version;
                public bool Strong;
                public void FromODBC(odbc.DCT.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                        this.Version = 2;
                        this.Strong = false;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Dictionary
            {
                public string Topic;
                public string Definition;
                public void FromODBC(odbc.DCT.Dictionary dictionary)
                {
                    try
                    {
                        this.Topic = dictionary.Topic;
                        this.Definition = dictionary.Definition;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace DEVX
        {
            public struct Copyright
            {
                public int ID;
                public string Author;
                public string Title;
                public string Place;
                public string Publisher;
                public string Year;
                public string Edition;
                public string Copyright1;
                public string Notes;
                public void FromODBC(odbc.DEV.Copyright copyright)
                {
                    try
                    {
                        this.ID = copyright.ID;
                        this.Author = copyright.Author;
                        this.Title = copyright.Title;
                        this.Place = copyright.Place;
                        this.Publisher = copyright.Publisher;
                        this.Year = copyright.Year;
                        this.Edition = copyright.Edition;
                        this.Copyright1 = copyright.Copyright1;
                        this.Notes = copyright.Notes;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public void FromODBC(odbc.DEV.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Devotions
            {
                public int ID;
                public int Month;
                public int Day;
                public string Devotion;
                public void FromODBC(odbc.DEV.Devotions devotions)
                {
                    try
                    {
                        this.ID = devotions.ID;
                        this.Month = devotions.Month;
                        this.Day = devotions.Day;
                        this.Devotion = devotions.Devotion;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace HARX
        {
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public int Version;
                public void FromODBC(odbc.HAR.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                        this.Version = 2;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Parts
            {
                public int Section;
                public int Part;
                public string Reference;
                public void FromODBC(odbc.HAR.Harmony harmony)
                {
                    try
                    {
                        this.Section = harmony.SectionID;
                        this.Part = harmony.Part;
                        this.Reference = harmony.Reference;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Sections
            {
                public int ID;
                public string Title;
                public void FromODBC(odbc.HAR.Sections sections)
                {
                    try
                    {
                        this.ID = sections.ID;
                        this.Title = sections.Title;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace LSTX
        {
            public struct Verses
            {
                public int Book;
                public int Chapter;
                public int Verse;
                public int Position;
                public void FromODBC(odbc.LST.Verses verses)
                {
                    try
                    {
                        var reference = (from VerseList v in Program.VerseLists
                                         where v.EndVerse <= verses.VerseID
                                         where v.StartVerse >= verses.VerseID
                                         select v).First<VerseList>();
                        this.Book = reference.Book;
                        this.Chapter = reference.Chapter;
                        this.Verse = (verses.VerseID - reference.StartVerse) + 1;
                        this.Position = verses.Order;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace MAPX
        {
            public struct Details
            {
                public string Description;
                public string Abbreviation;
                public string Comments;
                public int Version;
                public void FromODBC(odbc.MAP.Details details)
                {
                    try
                    {
                        this.Description = details.Description;
                        this.Abbreviation = details.Abbreviation;
                        this.Comments = details.Comments;
                        this.Version = 2;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
            public struct Graphics
            {
                public string Title;
                public string Details;
                public object Picture;
                public void FromODBC(odbc.MAP.Graphics graphics)
                {
                    try
                    {
                        this.Title = graphics.Title;
                        this.Details = graphics.Details;
                        this.Picture = graphics.Picture;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace MEMX
        {
            public struct Memorize
            {
                public int ID;
                public string Reference;
                public string Bible;
                public string Category;
                public string Hint;
                public string Start;
                public int Frequency;
                public void FromODBC(odbc.MEM.Memorize memorize)
                {
                    try
                    {
                        this.ID = memorize.ID;
                        this.Reference = memorize.Reference;
                        this.Bible = memorize.Bible;
                        this.Category = memorize.Category;
                        this.Hint = memorize.Hint;
                        this.Start = memorize.Start;
                        this.Frequency = memorize.Frequency;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace NOTX
        {
            public struct VerseNotes
            {
                public int Book;
                public int Chapter;
                public int Verse;
                public string Comments;
                public void FromODBC(odbc.NOT.VerseNotes notes)
                {
                    try
                    {
                        var reference = (from VerseList v in Program.VerseLists
                                         where v.EndVerse <= notes.VerseID
                                         where v.StartVerse >= notes.VerseID
                                         select v).First<VerseList>();
                        this.Book = reference.Book;
                        this.Chapter = reference.Chapter;
                        this.Verse = (notes.VerseID - reference.StartVerse) + 1;
                        this.Comments = notes.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace OVLX
        {
            public struct Overlay
            {
                public int ID;
                public string Bible;
                public int BookID;
                public int Chapter;
                public string Codes;
                public void FromODBC(odbc.OVL.Overlay overlay)
                {
                    try
                    {
                        this.ID = overlay.ID;
                        this.Bible = overlay.Bible;
                        this.BookID = overlay.BookID;
                        this.Chapter = overlay.Chapter;
                        this.Codes = overlay.Codes;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace PRLX
        {
            public struct Requests
            {
                public int ID;
                public string Title;
                public int Type;
                public int Frequency;
                public string Start;
                public string Request;
                public void FromODBC(odbc.PRL.Requests requests)
                {
                    try
                    {
                        this.ID = requests.ID;
                        this.Title = requests.Title;
                        this.Type = requests.Type;
                        this.Frequency = requests.Frequency;
                        this.Start = requests.Start;
                        this.Request = requests.Request;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
        namespace TOPX
        {
            public struct TopicNotes
            {
                public string Title;
                public string Comments;
                public void FromODBC(odbc.TOP.TopicNotes notes)
                {
                    try
                    {
                        this.Title = notes.Title;
                        this.Comments = notes.Comments;
                    }
                    catch (Exception ex)
                    {
                        Error.Record(this, ex);
                    }
                }
            }
        }
    }
    #region Verse List
    public class VerseList
    {
        public int Book, Chapter, StartVerse, EndVerse;
        public VerseList(int book, int chapter, int startVerse, int endVerse)
        { this.Book = book; this.Chapter = chapter; this.StartVerse = startVerse; this.EndVerse = endVerse; }
    }

    public class VerseLists : ThreadSafeCollection<VerseList>
    {
        public VerseLists()
        {
            this.Add(new VerseList(1, 1, 1, 31));
            this.Add(new VerseList(1, 2, 32, 56));
            this.Add(new VerseList(1, 3, 57, 80));
            this.Add(new VerseList(1, 4, 81, 106));
            this.Add(new VerseList(1, 5, 107, 138));
            this.Add(new VerseList(1, 6, 139, 160));
            this.Add(new VerseList(1, 7, 161, 184));
            this.Add(new VerseList(1, 8, 185, 206));
            this.Add(new VerseList(1, 9, 207, 235));
            this.Add(new VerseList(1, 10, 236, 267));
            this.Add(new VerseList(1, 11, 268, 299));
            this.Add(new VerseList(1, 12, 300, 319));
            this.Add(new VerseList(1, 13, 320, 337));
            this.Add(new VerseList(1, 14, 338, 361));
            this.Add(new VerseList(1, 15, 362, 382));
            this.Add(new VerseList(1, 16, 383, 398));
            this.Add(new VerseList(1, 17, 399, 425));
            this.Add(new VerseList(1, 18, 426, 458));
            this.Add(new VerseList(1, 19, 459, 496));
            this.Add(new VerseList(1, 20, 497, 514));
            this.Add(new VerseList(1, 21, 515, 548));
            this.Add(new VerseList(1, 22, 549, 572));
            this.Add(new VerseList(1, 23, 573, 592));
            this.Add(new VerseList(1, 24, 593, 659));
            this.Add(new VerseList(1, 25, 660, 693));
            this.Add(new VerseList(1, 26, 694, 728));
            this.Add(new VerseList(1, 27, 729, 774));
            this.Add(new VerseList(1, 28, 775, 796));
            this.Add(new VerseList(1, 29, 797, 831));
            this.Add(new VerseList(1, 30, 832, 874));
            this.Add(new VerseList(1, 31, 875, 929));
            this.Add(new VerseList(1, 32, 930, 961));
            this.Add(new VerseList(1, 33, 962, 981));
            this.Add(new VerseList(1, 34, 982, 1012));
            this.Add(new VerseList(1, 35, 1013, 1041));
            this.Add(new VerseList(1, 36, 1042, 1084));
            this.Add(new VerseList(1, 37, 1085, 1120));
            this.Add(new VerseList(1, 38, 1121, 1150));
            this.Add(new VerseList(1, 39, 1151, 1173));
            this.Add(new VerseList(1, 40, 1174, 1196));
            this.Add(new VerseList(1, 41, 1197, 1253));
            this.Add(new VerseList(1, 42, 1254, 1291));
            this.Add(new VerseList(1, 43, 1292, 1325));
            this.Add(new VerseList(1, 44, 1326, 1359));
            this.Add(new VerseList(1, 45, 1360, 1387));
            this.Add(new VerseList(1, 46, 1388, 1421));
            this.Add(new VerseList(1, 47, 1422, 1452));
            this.Add(new VerseList(1, 48, 1453, 1474));
            this.Add(new VerseList(1, 49, 1475, 1507));
            this.Add(new VerseList(1, 50, 1508, 1533));
            this.Add(new VerseList(2, 1, 1534, 1555));
            this.Add(new VerseList(2, 2, 1556, 1580));
            this.Add(new VerseList(2, 3, 1581, 1602));
            this.Add(new VerseList(2, 4, 1603, 1633));
            this.Add(new VerseList(2, 5, 1634, 1656));
            this.Add(new VerseList(2, 6, 1657, 1686));
            this.Add(new VerseList(2, 7, 1687, 1711));
            this.Add(new VerseList(2, 8, 1712, 1743));
            this.Add(new VerseList(2, 9, 1744, 1778));
            this.Add(new VerseList(2, 10, 1779, 1807));
            this.Add(new VerseList(2, 11, 1808, 1817));
            this.Add(new VerseList(2, 12, 1818, 1868));
            this.Add(new VerseList(2, 13, 1869, 1890));
            this.Add(new VerseList(2, 14, 1891, 1921));
            this.Add(new VerseList(2, 15, 1922, 1948));
            this.Add(new VerseList(2, 16, 1949, 1984));
            this.Add(new VerseList(2, 17, 1985, 2000));
            this.Add(new VerseList(2, 18, 2001, 2027));
            this.Add(new VerseList(2, 19, 2028, 2052));
            this.Add(new VerseList(2, 20, 2053, 2078));
            this.Add(new VerseList(2, 21, 2079, 2114));
            this.Add(new VerseList(2, 22, 2115, 2145));
            this.Add(new VerseList(2, 23, 2146, 2178));
            this.Add(new VerseList(2, 24, 2179, 2196));
            this.Add(new VerseList(2, 25, 2197, 2236));
            this.Add(new VerseList(2, 26, 2237, 2273));
            this.Add(new VerseList(2, 27, 2274, 2294));
            this.Add(new VerseList(2, 28, 2295, 2337));
            this.Add(new VerseList(2, 29, 2338, 2383));
            this.Add(new VerseList(2, 30, 2384, 2421));
            this.Add(new VerseList(2, 31, 2422, 2439));
            this.Add(new VerseList(2, 32, 2440, 2474));
            this.Add(new VerseList(2, 33, 2475, 2497));
            this.Add(new VerseList(2, 34, 2498, 2532));
            this.Add(new VerseList(2, 35, 2533, 2567));
            this.Add(new VerseList(2, 36, 2568, 2605));
            this.Add(new VerseList(2, 37, 2606, 2634));
            this.Add(new VerseList(2, 38, 2635, 2665));
            this.Add(new VerseList(2, 39, 2666, 2708));
            this.Add(new VerseList(2, 40, 2709, 2746));
            this.Add(new VerseList(3, 1, 2747, 2763));
            this.Add(new VerseList(3, 2, 2764, 2779));
            this.Add(new VerseList(3, 3, 2780, 2796));
            this.Add(new VerseList(3, 4, 2797, 2831));
            this.Add(new VerseList(3, 5, 2832, 2850));
            this.Add(new VerseList(3, 6, 2851, 2880));
            this.Add(new VerseList(3, 7, 2881, 2918));
            this.Add(new VerseList(3, 8, 2919, 2954));
            this.Add(new VerseList(3, 9, 2955, 2978));
            this.Add(new VerseList(3, 10, 2979, 2998));
            this.Add(new VerseList(3, 11, 2999, 3045));
            this.Add(new VerseList(3, 12, 3046, 3053));
            this.Add(new VerseList(3, 13, 3054, 3112));
            this.Add(new VerseList(3, 14, 3113, 3169));
            this.Add(new VerseList(3, 15, 3170, 3202));
            this.Add(new VerseList(3, 16, 3203, 3236));
            this.Add(new VerseList(3, 17, 3237, 3252));
            this.Add(new VerseList(3, 18, 3253, 3282));
            this.Add(new VerseList(3, 19, 3283, 3319));
            this.Add(new VerseList(3, 20, 3320, 3346));
            this.Add(new VerseList(3, 21, 3347, 3370));
            this.Add(new VerseList(3, 22, 3371, 3403));
            this.Add(new VerseList(3, 23, 3404, 3447));
            this.Add(new VerseList(3, 24, 3448, 3470));
            this.Add(new VerseList(3, 25, 3471, 3525));
            this.Add(new VerseList(3, 26, 3526, 3571));
            this.Add(new VerseList(3, 27, 3572, 3605));
            this.Add(new VerseList(4, 1, 3606, 3659));
            this.Add(new VerseList(4, 2, 3660, 3693));
            this.Add(new VerseList(4, 3, 3694, 3744));
            this.Add(new VerseList(4, 4, 3745, 3793));
            this.Add(new VerseList(4, 5, 3794, 3824));
            this.Add(new VerseList(4, 6, 3825, 3851));
            this.Add(new VerseList(4, 7, 3852, 3940));
            this.Add(new VerseList(4, 8, 3941, 3966));
            this.Add(new VerseList(4, 9, 3967, 3989));
            this.Add(new VerseList(4, 10, 3990, 4025));
            this.Add(new VerseList(4, 11, 4026, 4060));
            this.Add(new VerseList(4, 12, 4061, 4076));
            this.Add(new VerseList(4, 13, 4077, 4109));
            this.Add(new VerseList(4, 14, 4110, 4154));
            this.Add(new VerseList(4, 15, 4155, 4195));
            this.Add(new VerseList(4, 16, 4196, 4245));
            this.Add(new VerseList(4, 17, 4246, 4258));
            this.Add(new VerseList(4, 18, 4259, 4290));
            this.Add(new VerseList(4, 19, 4291, 4312));
            this.Add(new VerseList(4, 20, 4313, 4341));
            this.Add(new VerseList(4, 21, 4342, 4376));
            this.Add(new VerseList(4, 22, 4377, 4417));
            this.Add(new VerseList(4, 23, 4418, 4447));
            this.Add(new VerseList(4, 24, 4448, 4472));
            this.Add(new VerseList(4, 25, 4473, 4490));
            this.Add(new VerseList(4, 26, 4491, 4555));
            this.Add(new VerseList(4, 27, 4556, 4578));
            this.Add(new VerseList(4, 28, 4579, 4609));
            this.Add(new VerseList(4, 29, 4610, 4649));
            this.Add(new VerseList(4, 30, 4650, 4665));
            this.Add(new VerseList(4, 31, 4666, 4719));
            this.Add(new VerseList(4, 32, 4720, 4761));
            this.Add(new VerseList(4, 33, 4762, 4817));
            this.Add(new VerseList(4, 34, 4818, 4846));
            this.Add(new VerseList(4, 35, 4847, 4880));
            this.Add(new VerseList(4, 36, 4881, 4893));
            this.Add(new VerseList(5, 1, 4894, 4939));
            this.Add(new VerseList(5, 2, 4940, 4976));
            this.Add(new VerseList(5, 3, 4977, 5005));
            this.Add(new VerseList(5, 4, 5006, 5054));
            this.Add(new VerseList(5, 5, 5055, 5087));
            this.Add(new VerseList(5, 6, 5088, 5112));
            this.Add(new VerseList(5, 7, 5113, 5138));
            this.Add(new VerseList(5, 8, 5139, 5158));
            this.Add(new VerseList(5, 9, 5159, 5187));
            this.Add(new VerseList(5, 10, 5188, 5209));
            this.Add(new VerseList(5, 11, 5210, 5241));
            this.Add(new VerseList(5, 12, 5242, 5273));
            this.Add(new VerseList(5, 13, 5274, 5291));
            this.Add(new VerseList(5, 14, 5292, 5320));
            this.Add(new VerseList(5, 15, 5321, 5343));
            this.Add(new VerseList(5, 16, 5344, 5365));
            this.Add(new VerseList(5, 17, 5366, 5385));
            this.Add(new VerseList(5, 18, 5386, 5407));
            this.Add(new VerseList(5, 19, 5408, 5428));
            this.Add(new VerseList(5, 20, 5429, 5448));
            this.Add(new VerseList(5, 21, 5449, 5471));
            this.Add(new VerseList(5, 22, 5472, 5501));
            this.Add(new VerseList(5, 23, 5502, 5526));
            this.Add(new VerseList(5, 24, 5527, 5548));
            this.Add(new VerseList(5, 25, 5549, 5567));
            this.Add(new VerseList(5, 26, 5568, 5586));
            this.Add(new VerseList(5, 27, 5587, 5612));
            this.Add(new VerseList(5, 28, 5613, 5680));
            this.Add(new VerseList(5, 29, 5681, 5709));
            this.Add(new VerseList(5, 30, 5710, 5729));
            this.Add(new VerseList(5, 31, 5730, 5759));
            this.Add(new VerseList(5, 32, 5760, 5811));
            this.Add(new VerseList(5, 33, 5812, 5840));
            this.Add(new VerseList(5, 34, 5841, 5852));
            this.Add(new VerseList(6, 1, 5853, 5870));
            this.Add(new VerseList(6, 2, 5871, 5894));
            this.Add(new VerseList(6, 3, 5895, 5911));
            this.Add(new VerseList(6, 4, 5912, 5935));
            this.Add(new VerseList(6, 5, 5936, 5950));
            this.Add(new VerseList(6, 6, 5951, 5977));
            this.Add(new VerseList(6, 7, 5978, 6003));
            this.Add(new VerseList(6, 8, 6004, 6038));
            this.Add(new VerseList(6, 9, 6039, 6065));
            this.Add(new VerseList(6, 10, 6066, 6108));
            this.Add(new VerseList(6, 11, 6109, 6131));
            this.Add(new VerseList(6, 12, 6132, 6155));
            this.Add(new VerseList(6, 13, 6156, 6188));
            this.Add(new VerseList(6, 14, 6189, 6203));
            this.Add(new VerseList(6, 15, 6204, 6266));
            this.Add(new VerseList(6, 16, 6267, 6276));
            this.Add(new VerseList(6, 17, 6277, 6294));
            this.Add(new VerseList(6, 18, 6295, 6322));
            this.Add(new VerseList(6, 19, 6323, 6373));
            this.Add(new VerseList(6, 20, 6374, 6382));
            this.Add(new VerseList(6, 21, 6383, 6427));
            this.Add(new VerseList(6, 22, 6428, 6461));
            this.Add(new VerseList(6, 23, 6462, 6477));
            this.Add(new VerseList(6, 24, 6478, 6510));
            this.Add(new VerseList(7, 1, 6511, 6546));
            this.Add(new VerseList(7, 2, 6547, 6569));
            this.Add(new VerseList(7, 3, 6570, 6600));
            this.Add(new VerseList(7, 4, 6601, 6624));
            this.Add(new VerseList(7, 5, 6625, 6655));
            this.Add(new VerseList(7, 6, 6656, 6695));
            this.Add(new VerseList(7, 7, 6696, 6720));
            this.Add(new VerseList(7, 8, 6721, 6755));
            this.Add(new VerseList(7, 9, 6756, 6812));
            this.Add(new VerseList(7, 10, 6813, 6830));
            this.Add(new VerseList(7, 11, 6831, 6870));
            this.Add(new VerseList(7, 12, 6871, 6885));
            this.Add(new VerseList(7, 13, 6886, 6910));
            this.Add(new VerseList(7, 14, 6911, 6930));
            this.Add(new VerseList(7, 15, 6931, 6950));
            this.Add(new VerseList(7, 16, 6951, 6981));
            this.Add(new VerseList(7, 17, 6982, 6994));
            this.Add(new VerseList(7, 18, 6995, 7025));
            this.Add(new VerseList(7, 19, 7026, 7055));
            this.Add(new VerseList(7, 20, 7056, 7103));
            this.Add(new VerseList(7, 21, 7104, 7128));
            this.Add(new VerseList(8, 1, 7129, 7150));
            this.Add(new VerseList(8, 2, 7151, 7173));
            this.Add(new VerseList(8, 3, 7174, 7191));
            this.Add(new VerseList(8, 4, 7192, 7213));
            this.Add(new VerseList(9, 1, 7214, 7241));
            this.Add(new VerseList(9, 2, 7242, 7277));
            this.Add(new VerseList(9, 3, 7278, 7298));
            this.Add(new VerseList(9, 4, 7299, 7320));
            this.Add(new VerseList(9, 5, 7321, 7332));
            this.Add(new VerseList(9, 6, 7333, 7353));
            this.Add(new VerseList(9, 7, 7354, 7370));
            this.Add(new VerseList(9, 8, 7371, 7392));
            this.Add(new VerseList(9, 9, 7393, 7419));
            this.Add(new VerseList(9, 10, 7420, 7446));
            this.Add(new VerseList(9, 11, 7447, 7461));
            this.Add(new VerseList(9, 12, 7462, 7486));
            this.Add(new VerseList(9, 13, 7487, 7509));
            this.Add(new VerseList(9, 14, 7510, 7561));
            this.Add(new VerseList(9, 15, 7562, 7596));
            this.Add(new VerseList(9, 16, 7597, 7619));
            this.Add(new VerseList(9, 17, 7620, 7677));
            this.Add(new VerseList(9, 18, 7678, 7707));
            this.Add(new VerseList(9, 19, 7708, 7731));
            this.Add(new VerseList(9, 20, 7732, 7773));
            this.Add(new VerseList(9, 21, 7774, 7788));
            this.Add(new VerseList(9, 22, 7789, 7811));
            this.Add(new VerseList(9, 23, 7812, 7840));
            this.Add(new VerseList(9, 24, 7841, 7862));
            this.Add(new VerseList(9, 25, 7863, 7906));
            this.Add(new VerseList(9, 26, 7907, 7931));
            this.Add(new VerseList(9, 27, 7932, 7943));
            this.Add(new VerseList(9, 28, 7944, 7968));
            this.Add(new VerseList(9, 29, 7969, 7979));
            this.Add(new VerseList(9, 30, 7980, 8010));
            this.Add(new VerseList(9, 31, 8011, 8023));
            this.Add(new VerseList(10, 1, 8024, 8050));
            this.Add(new VerseList(10, 2, 8051, 8082));
            this.Add(new VerseList(10, 3, 8083, 8121));
            this.Add(new VerseList(10, 4, 8122, 8133));
            this.Add(new VerseList(10, 5, 8134, 8158));
            this.Add(new VerseList(10, 6, 8159, 8181));
            this.Add(new VerseList(10, 7, 8182, 8210));
            this.Add(new VerseList(10, 8, 8211, 8228));
            this.Add(new VerseList(10, 9, 8229, 8241));
            this.Add(new VerseList(10, 10, 8242, 8260));
            this.Add(new VerseList(10, 11, 8261, 8287));
            this.Add(new VerseList(10, 12, 8288, 8318));
            this.Add(new VerseList(10, 13, 8319, 8357));
            this.Add(new VerseList(10, 14, 8358, 8390));
            this.Add(new VerseList(10, 15, 8391, 8427));
            this.Add(new VerseList(10, 16, 8428, 8450));
            this.Add(new VerseList(10, 17, 8451, 8479));
            this.Add(new VerseList(10, 18, 8480, 8512));
            this.Add(new VerseList(10, 19, 8513, 8555));
            this.Add(new VerseList(10, 20, 8556, 8581));
            this.Add(new VerseList(10, 21, 8582, 8603));
            this.Add(new VerseList(10, 22, 8604, 8654));
            this.Add(new VerseList(10, 23, 8655, 8693));
            this.Add(new VerseList(10, 24, 8694, 8718));
            this.Add(new VerseList(11, 1, 8719, 8771));
            this.Add(new VerseList(11, 2, 8772, 8817));
            this.Add(new VerseList(11, 3, 8818, 8845));
            this.Add(new VerseList(11, 4, 8846, 8879));
            this.Add(new VerseList(11, 5, 8880, 8897));
            this.Add(new VerseList(11, 6, 8898, 8935));
            this.Add(new VerseList(11, 7, 8936, 8986));
            this.Add(new VerseList(11, 8, 8987, 9052));
            this.Add(new VerseList(11, 9, 9053, 9080));
            this.Add(new VerseList(11, 10, 9081, 9109));
            this.Add(new VerseList(11, 11, 9110, 9152));
            this.Add(new VerseList(11, 12, 9153, 9185));
            this.Add(new VerseList(11, 13, 9186, 9219));
            this.Add(new VerseList(11, 14, 9220, 9250));
            this.Add(new VerseList(11, 15, 9251, 9284));
            this.Add(new VerseList(11, 16, 9285, 9318));
            this.Add(new VerseList(11, 17, 9319, 9342));
            this.Add(new VerseList(11, 18, 9343, 9388));
            this.Add(new VerseList(11, 19, 9389, 9409));
            this.Add(new VerseList(11, 20, 9410, 9452));
            this.Add(new VerseList(11, 21, 9453, 9481));
            this.Add(new VerseList(11, 22, 9482, 9534));
            this.Add(new VerseList(12, 1, 9535, 9552));
            this.Add(new VerseList(12, 2, 9553, 9577));
            this.Add(new VerseList(12, 3, 9578, 9604));
            this.Add(new VerseList(12, 4, 9605, 9648));
            this.Add(new VerseList(12, 5, 9649, 9675));
            this.Add(new VerseList(12, 6, 9676, 9708));
            this.Add(new VerseList(12, 7, 9709, 9728));
            this.Add(new VerseList(12, 8, 9729, 9757));
            this.Add(new VerseList(12, 9, 9758, 9794));
            this.Add(new VerseList(12, 10, 9795, 9830));
            this.Add(new VerseList(12, 11, 9831, 9851));
            this.Add(new VerseList(12, 12, 9852, 9872));
            this.Add(new VerseList(12, 13, 9873, 9897));
            this.Add(new VerseList(12, 14, 9898, 9926));
            this.Add(new VerseList(12, 15, 9927, 9964));
            this.Add(new VerseList(12, 16, 9965, 9984));
            this.Add(new VerseList(12, 17, 9985, 10025));
            this.Add(new VerseList(12, 18, 10026, 10062));
            this.Add(new VerseList(12, 19, 10063, 10099));
            this.Add(new VerseList(12, 20, 10100, 10120));
            this.Add(new VerseList(12, 21, 10121, 10146));
            this.Add(new VerseList(12, 22, 10147, 10166));
            this.Add(new VerseList(12, 23, 10167, 10203));
            this.Add(new VerseList(12, 24, 10204, 10223));
            this.Add(new VerseList(12, 25, 10224, 10253));
            this.Add(new VerseList(13, 1, 10254, 10307));
            this.Add(new VerseList(13, 2, 10308, 10362));
            this.Add(new VerseList(13, 3, 10363, 10386));
            this.Add(new VerseList(13, 4, 10387, 10429));
            this.Add(new VerseList(13, 5, 10430, 10455));
            this.Add(new VerseList(13, 6, 10456, 10536));
            this.Add(new VerseList(13, 7, 10537, 10576));
            this.Add(new VerseList(13, 8, 10577, 10616));
            this.Add(new VerseList(13, 9, 10617, 10660));
            this.Add(new VerseList(13, 10, 10661, 10674));
            this.Add(new VerseList(13, 11, 10675, 10721));
            this.Add(new VerseList(13, 12, 10722, 10761));
            this.Add(new VerseList(13, 13, 10762, 10775));
            this.Add(new VerseList(13, 14, 10776, 10792));
            this.Add(new VerseList(13, 15, 10793, 10821));
            this.Add(new VerseList(13, 16, 10822, 10864));
            this.Add(new VerseList(13, 17, 10865, 10891));
            this.Add(new VerseList(13, 18, 10892, 10908));
            this.Add(new VerseList(13, 19, 10909, 10927));
            this.Add(new VerseList(13, 20, 10928, 10935));
            this.Add(new VerseList(13, 21, 10936, 10965));
            this.Add(new VerseList(13, 22, 10966, 10984));
            this.Add(new VerseList(13, 23, 10985, 11016));
            this.Add(new VerseList(13, 24, 11017, 11047));
            this.Add(new VerseList(13, 25, 11048, 11078));
            this.Add(new VerseList(13, 26, 11079, 11110));
            this.Add(new VerseList(13, 27, 11111, 11144));
            this.Add(new VerseList(13, 28, 11145, 11165));
            this.Add(new VerseList(13, 29, 11166, 11195));
            this.Add(new VerseList(14, 1, 11196, 11212));
            this.Add(new VerseList(14, 2, 11213, 11230));
            this.Add(new VerseList(14, 3, 11231, 11247));
            this.Add(new VerseList(14, 4, 11248, 11269));
            this.Add(new VerseList(14, 5, 11270, 11283));
            this.Add(new VerseList(14, 6, 11284, 11325));
            this.Add(new VerseList(14, 7, 11326, 11347));
            this.Add(new VerseList(14, 8, 11348, 11365));
            this.Add(new VerseList(14, 9, 11366, 11396));
            this.Add(new VerseList(14, 10, 11397, 11415));
            this.Add(new VerseList(14, 11, 11416, 11438));
            this.Add(new VerseList(14, 12, 11439, 11454));
            this.Add(new VerseList(14, 13, 11455, 11476));
            this.Add(new VerseList(14, 14, 11477, 11491));
            this.Add(new VerseList(14, 15, 11492, 11510));
            this.Add(new VerseList(14, 16, 11511, 11524));
            this.Add(new VerseList(14, 17, 11525, 11543));
            this.Add(new VerseList(14, 18, 11544, 11577));
            this.Add(new VerseList(14, 19, 11578, 11588));
            this.Add(new VerseList(14, 20, 11589, 11625));
            this.Add(new VerseList(14, 21, 11626, 11645));
            this.Add(new VerseList(14, 22, 11646, 11657));
            this.Add(new VerseList(14, 23, 11658, 11678));
            this.Add(new VerseList(14, 24, 11679, 11705));
            this.Add(new VerseList(14, 25, 11706, 11733));
            this.Add(new VerseList(14, 26, 11734, 11756));
            this.Add(new VerseList(14, 27, 11757, 11765));
            this.Add(new VerseList(14, 28, 11766, 11792));
            this.Add(new VerseList(14, 29, 11793, 11828));
            this.Add(new VerseList(14, 30, 11829, 11855));
            this.Add(new VerseList(14, 31, 11856, 11876));
            this.Add(new VerseList(14, 32, 11877, 11909));
            this.Add(new VerseList(14, 33, 11910, 11934));
            this.Add(new VerseList(14, 34, 11935, 11967));
            this.Add(new VerseList(14, 35, 11968, 11994));
            this.Add(new VerseList(14, 36, 11995, 12017));
            this.Add(new VerseList(15, 1, 12018, 12028));
            this.Add(new VerseList(15, 2, 12029, 12098));
            this.Add(new VerseList(15, 3, 12099, 12111));
            this.Add(new VerseList(15, 4, 12112, 12135));
            this.Add(new VerseList(15, 5, 12136, 12152));
            this.Add(new VerseList(15, 6, 12153, 12174));
            this.Add(new VerseList(15, 7, 12175, 12202));
            this.Add(new VerseList(15, 8, 12203, 12238));
            this.Add(new VerseList(15, 9, 12239, 12253));
            this.Add(new VerseList(15, 10, 12254, 12297));
            this.Add(new VerseList(16, 1, 12298, 12308));
            this.Add(new VerseList(16, 2, 12309, 12328));
            this.Add(new VerseList(16, 3, 12329, 12360));
            this.Add(new VerseList(16, 4, 12361, 12383));
            this.Add(new VerseList(16, 5, 12384, 12402));
            this.Add(new VerseList(16, 6, 12403, 12421));
            this.Add(new VerseList(16, 7, 12422, 12494));
            this.Add(new VerseList(16, 8, 12495, 12512));
            this.Add(new VerseList(16, 9, 12513, 12550));
            this.Add(new VerseList(16, 10, 12551, 12589));
            this.Add(new VerseList(16, 11, 12590, 12625));
            this.Add(new VerseList(16, 12, 12626, 12672));
            this.Add(new VerseList(16, 13, 12673, 12703));
            this.Add(new VerseList(17, 1, 12704, 12725));
            this.Add(new VerseList(17, 2, 12726, 12748));
            this.Add(new VerseList(17, 3, 12749, 12763));
            this.Add(new VerseList(17, 4, 12764, 12780));
            this.Add(new VerseList(17, 5, 12781, 12794));
            this.Add(new VerseList(17, 6, 12795, 12808));
            this.Add(new VerseList(17, 7, 12809, 12818));
            this.Add(new VerseList(17, 8, 12819, 12835));
            this.Add(new VerseList(17, 9, 12836, 12867));
            this.Add(new VerseList(17, 10, 12868, 12870));
            this.Add(new VerseList(18, 1, 12871, 12892));
            this.Add(new VerseList(18, 2, 12893, 12905));
            this.Add(new VerseList(18, 3, 12906, 12931));
            this.Add(new VerseList(18, 4, 12932, 12952));
            this.Add(new VerseList(18, 5, 12953, 12979));
            this.Add(new VerseList(18, 6, 12980, 13009));
            this.Add(new VerseList(18, 7, 13010, 13030));
            this.Add(new VerseList(18, 8, 13031, 13052));
            this.Add(new VerseList(18, 9, 13053, 13087));
            this.Add(new VerseList(18, 10, 13088, 13109));
            this.Add(new VerseList(18, 11, 13110, 13129));
            this.Add(new VerseList(18, 12, 13130, 13154));
            this.Add(new VerseList(18, 13, 13155, 13182));
            this.Add(new VerseList(18, 14, 13183, 13204));
            this.Add(new VerseList(18, 15, 13205, 13239));
            this.Add(new VerseList(18, 16, 13240, 13261));
            this.Add(new VerseList(18, 17, 13262, 13277));
            this.Add(new VerseList(18, 18, 13278, 13298));
            this.Add(new VerseList(18, 19, 13299, 13327));
            this.Add(new VerseList(18, 20, 13328, 13356));
            this.Add(new VerseList(18, 21, 13357, 13390));
            this.Add(new VerseList(18, 22, 13391, 13420));
            this.Add(new VerseList(18, 23, 13421, 13437));
            this.Add(new VerseList(18, 24, 13438, 13462));
            this.Add(new VerseList(18, 25, 13463, 13468));
            this.Add(new VerseList(18, 26, 13469, 13482));
            this.Add(new VerseList(18, 27, 13483, 13505));
            this.Add(new VerseList(18, 28, 13506, 13533));
            this.Add(new VerseList(18, 29, 13534, 13558));
            this.Add(new VerseList(18, 30, 13559, 13589));
            this.Add(new VerseList(18, 31, 13590, 13629));
            this.Add(new VerseList(18, 32, 13630, 13651));
            this.Add(new VerseList(18, 33, 13652, 13684));
            this.Add(new VerseList(18, 34, 13685, 13721));
            this.Add(new VerseList(18, 35, 13722, 13737));
            this.Add(new VerseList(18, 36, 13738, 13770));
            this.Add(new VerseList(18, 37, 13771, 13794));
            this.Add(new VerseList(18, 38, 13795, 13835));
            this.Add(new VerseList(18, 39, 13836, 13865));
            this.Add(new VerseList(18, 40, 13866, 13889));
            this.Add(new VerseList(18, 41, 13890, 13923));
            this.Add(new VerseList(18, 42, 13924, 13940));
            this.Add(new VerseList(19, 1, 13941, 13946));
            this.Add(new VerseList(19, 2, 13947, 13958));
            this.Add(new VerseList(19, 3, 13959, 13966));
            this.Add(new VerseList(19, 4, 13967, 13974));
            this.Add(new VerseList(19, 5, 13975, 13986));
            this.Add(new VerseList(19, 6, 13987, 13996));
            this.Add(new VerseList(19, 7, 13997, 14013));
            this.Add(new VerseList(19, 8, 14014, 14022));
            this.Add(new VerseList(19, 9, 14023, 14042));
            this.Add(new VerseList(19, 10, 14043, 14060));
            this.Add(new VerseList(19, 11, 14061, 14067));
            this.Add(new VerseList(19, 12, 14068, 14075));
            this.Add(new VerseList(19, 13, 14076, 14081));
            this.Add(new VerseList(19, 14, 14082, 14088));
            this.Add(new VerseList(19, 15, 14089, 14093));
            this.Add(new VerseList(19, 16, 14094, 14104));
            this.Add(new VerseList(19, 17, 14105, 14119));
            this.Add(new VerseList(19, 18, 14120, 14169));
            this.Add(new VerseList(19, 19, 14170, 14183));
            this.Add(new VerseList(19, 20, 14184, 14192));
            this.Add(new VerseList(19, 21, 14193, 14205));
            this.Add(new VerseList(19, 22, 14206, 14236));
            this.Add(new VerseList(19, 23, 14237, 14242));
            this.Add(new VerseList(19, 24, 14243, 14252));
            this.Add(new VerseList(19, 25, 14253, 14274));
            this.Add(new VerseList(19, 26, 14275, 14286));
            this.Add(new VerseList(19, 27, 14287, 14300));
            this.Add(new VerseList(19, 28, 14301, 14309));
            this.Add(new VerseList(19, 29, 14310, 14320));
            this.Add(new VerseList(19, 30, 14321, 14332));
            this.Add(new VerseList(19, 31, 14333, 14356));
            this.Add(new VerseList(19, 32, 14357, 14367));
            this.Add(new VerseList(19, 33, 14368, 14389));
            this.Add(new VerseList(19, 34, 14390, 14411));
            this.Add(new VerseList(19, 35, 14412, 14439));
            this.Add(new VerseList(19, 36, 14440, 14451));
            this.Add(new VerseList(19, 37, 14452, 14491));
            this.Add(new VerseList(19, 38, 14492, 14513));
            this.Add(new VerseList(19, 39, 14514, 14526));
            this.Add(new VerseList(19, 40, 14527, 14543));
            this.Add(new VerseList(19, 41, 14544, 14556));
            this.Add(new VerseList(19, 42, 14557, 14567));
            this.Add(new VerseList(19, 43, 14568, 14572));
            this.Add(new VerseList(19, 44, 14573, 14598));
            this.Add(new VerseList(19, 45, 14599, 14615));
            this.Add(new VerseList(19, 46, 14616, 14626));
            this.Add(new VerseList(19, 47, 14627, 14635));
            this.Add(new VerseList(19, 48, 14636, 14649));
            this.Add(new VerseList(19, 49, 14650, 14669));
            this.Add(new VerseList(19, 50, 14670, 14692));
            this.Add(new VerseList(19, 51, 14693, 14711));
            this.Add(new VerseList(19, 52, 14712, 14720));
            this.Add(new VerseList(19, 53, 14721, 14726));
            this.Add(new VerseList(19, 54, 14727, 14733));
            this.Add(new VerseList(19, 55, 14734, 14756));
            this.Add(new VerseList(19, 56, 14757, 14769));
            this.Add(new VerseList(19, 57, 14770, 14780));
            this.Add(new VerseList(19, 58, 14781, 14791));
            this.Add(new VerseList(19, 59, 14792, 14808));
            this.Add(new VerseList(19, 60, 14809, 14820));
            this.Add(new VerseList(19, 61, 14821, 14828));
            this.Add(new VerseList(19, 62, 14829, 14840));
            this.Add(new VerseList(19, 63, 14841, 14851));
            this.Add(new VerseList(19, 64, 14852, 14861));
            this.Add(new VerseList(19, 65, 14862, 14874));
            this.Add(new VerseList(19, 66, 14875, 14894));
            this.Add(new VerseList(19, 67, 14895, 14901));
            this.Add(new VerseList(19, 68, 14902, 14936));
            this.Add(new VerseList(19, 69, 14937, 14972));
            this.Add(new VerseList(19, 70, 14973, 14977));
            this.Add(new VerseList(19, 71, 14978, 15001));
            this.Add(new VerseList(19, 72, 15002, 15021));
            this.Add(new VerseList(19, 73, 15022, 15049));
            this.Add(new VerseList(19, 74, 15050, 15072));
            this.Add(new VerseList(19, 75, 15073, 15082));
            this.Add(new VerseList(19, 76, 15083, 15094));
            this.Add(new VerseList(19, 77, 15095, 15114));
            this.Add(new VerseList(19, 78, 15115, 15186));
            this.Add(new VerseList(19, 79, 15187, 15199));
            this.Add(new VerseList(19, 80, 15200, 15218));
            this.Add(new VerseList(19, 81, 15219, 15234));
            this.Add(new VerseList(19, 82, 15235, 15242));
            this.Add(new VerseList(19, 83, 15243, 15260));
            this.Add(new VerseList(19, 84, 15261, 15272));
            this.Add(new VerseList(19, 85, 15273, 15285));
            this.Add(new VerseList(19, 86, 15286, 15302));
            this.Add(new VerseList(19, 87, 15303, 15309));
            this.Add(new VerseList(19, 88, 15310, 15327));
            this.Add(new VerseList(19, 89, 15328, 15379));
            this.Add(new VerseList(19, 90, 15380, 15396));
            this.Add(new VerseList(19, 91, 15397, 15412));
            this.Add(new VerseList(19, 92, 15413, 15427));
            this.Add(new VerseList(19, 93, 15428, 15432));
            this.Add(new VerseList(19, 94, 15433, 15455));
            this.Add(new VerseList(19, 95, 15456, 15466));
            this.Add(new VerseList(19, 96, 15467, 15479));
            this.Add(new VerseList(19, 97, 15480, 15491));
            this.Add(new VerseList(19, 98, 15492, 15500));
            this.Add(new VerseList(19, 99, 15501, 15509));
            this.Add(new VerseList(19, 100, 15510, 15514));
            this.Add(new VerseList(19, 101, 15515, 15522));
            this.Add(new VerseList(19, 102, 15523, 15550));
            this.Add(new VerseList(19, 103, 15551, 15572));
            this.Add(new VerseList(19, 104, 15573, 15607));
            this.Add(new VerseList(19, 105, 15608, 15652));
            this.Add(new VerseList(19, 106, 15653, 15700));
            this.Add(new VerseList(19, 107, 15701, 15743));
            this.Add(new VerseList(19, 108, 15744, 15756));
            this.Add(new VerseList(19, 109, 15757, 15787));
            this.Add(new VerseList(19, 110, 15788, 15794));
            this.Add(new VerseList(19, 111, 15795, 15804));
            this.Add(new VerseList(19, 112, 15805, 15814));
            this.Add(new VerseList(19, 113, 15815, 15823));
            this.Add(new VerseList(19, 114, 15824, 15831));
            this.Add(new VerseList(19, 115, 15832, 15849));
            this.Add(new VerseList(19, 116, 15850, 15868));
            this.Add(new VerseList(19, 117, 15869, 15870));
            this.Add(new VerseList(19, 118, 15871, 15899));
            this.Add(new VerseList(19, 119, 15900, 16075));
            this.Add(new VerseList(19, 120, 16076, 16082));
            this.Add(new VerseList(19, 121, 16083, 16090));
            this.Add(new VerseList(19, 122, 16091, 16099));
            this.Add(new VerseList(19, 123, 16100, 16103));
            this.Add(new VerseList(19, 124, 16104, 16111));
            this.Add(new VerseList(19, 125, 16112, 16116));
            this.Add(new VerseList(19, 126, 16117, 16122));
            this.Add(new VerseList(19, 127, 16123, 16127));
            this.Add(new VerseList(19, 128, 16128, 16133));
            this.Add(new VerseList(19, 129, 16134, 16141));
            this.Add(new VerseList(19, 130, 16142, 16149));
            this.Add(new VerseList(19, 131, 16150, 16152));
            this.Add(new VerseList(19, 132, 16153, 16170));
            this.Add(new VerseList(19, 133, 16171, 16173));
            this.Add(new VerseList(19, 134, 16174, 16176));
            this.Add(new VerseList(19, 135, 16177, 16197));
            this.Add(new VerseList(19, 136, 16198, 16223));
            this.Add(new VerseList(19, 137, 16224, 16232));
            this.Add(new VerseList(19, 138, 16233, 16240));
            this.Add(new VerseList(19, 139, 16241, 16264));
            this.Add(new VerseList(19, 140, 16265, 16277));
            this.Add(new VerseList(19, 141, 16278, 16287));
            this.Add(new VerseList(19, 142, 16288, 16294));
            this.Add(new VerseList(19, 143, 16295, 16306));
            this.Add(new VerseList(19, 144, 16307, 16321));
            this.Add(new VerseList(19, 145, 16322, 16342));
            this.Add(new VerseList(19, 146, 16343, 16352));
            this.Add(new VerseList(19, 147, 16353, 16372));
            this.Add(new VerseList(19, 148, 16373, 16386));
            this.Add(new VerseList(19, 149, 16387, 16395));
            this.Add(new VerseList(19, 150, 16396, 16401));
            this.Add(new VerseList(20, 1, 16402, 16434));
            this.Add(new VerseList(20, 2, 16435, 16456));
            this.Add(new VerseList(20, 3, 16457, 16491));
            this.Add(new VerseList(20, 4, 16492, 16518));
            this.Add(new VerseList(20, 5, 16519, 16541));
            this.Add(new VerseList(20, 6, 16542, 16576));
            this.Add(new VerseList(20, 7, 16577, 16603));
            this.Add(new VerseList(20, 8, 16604, 16639));
            this.Add(new VerseList(20, 9, 16640, 16657));
            this.Add(new VerseList(20, 10, 16658, 16689));
            this.Add(new VerseList(20, 11, 16690, 16720));
            this.Add(new VerseList(20, 12, 16721, 16748));
            this.Add(new VerseList(20, 13, 16749, 16773));
            this.Add(new VerseList(20, 14, 16774, 16808));
            this.Add(new VerseList(20, 15, 16809, 16841));
            this.Add(new VerseList(20, 16, 16842, 16874));
            this.Add(new VerseList(20, 17, 16875, 16902));
            this.Add(new VerseList(20, 18, 16903, 16926));
            this.Add(new VerseList(20, 19, 16927, 16955));
            this.Add(new VerseList(20, 20, 16956, 16985));
            this.Add(new VerseList(20, 21, 16986, 17016));
            this.Add(new VerseList(20, 22, 17017, 17045));
            this.Add(new VerseList(20, 23, 17046, 17080));
            this.Add(new VerseList(20, 24, 17081, 17114));
            this.Add(new VerseList(20, 25, 17115, 17142));
            this.Add(new VerseList(20, 26, 17143, 17170));
            this.Add(new VerseList(20, 27, 17171, 17197));
            this.Add(new VerseList(20, 28, 17198, 17225));
            this.Add(new VerseList(20, 29, 17226, 17252));
            this.Add(new VerseList(20, 30, 17253, 17285));
            this.Add(new VerseList(20, 31, 17286, 17316));
            this.Add(new VerseList(21, 1, 17317, 17334));
            this.Add(new VerseList(21, 2, 17335, 17360));
            this.Add(new VerseList(21, 3, 17361, 17382));
            this.Add(new VerseList(21, 4, 17383, 17398));
            this.Add(new VerseList(21, 5, 17399, 17418));
            this.Add(new VerseList(21, 6, 17419, 17430));
            this.Add(new VerseList(21, 7, 17431, 17459));
            this.Add(new VerseList(21, 8, 17460, 17476));
            this.Add(new VerseList(21, 9, 17477, 17494));
            this.Add(new VerseList(21, 10, 17495, 17514));
            this.Add(new VerseList(21, 11, 17515, 17524));
            this.Add(new VerseList(21, 12, 17525, 17538));
            this.Add(new VerseList(22, 1, 17539, 17555));
            this.Add(new VerseList(22, 2, 17556, 17572));
            this.Add(new VerseList(22, 3, 17573, 17583));
            this.Add(new VerseList(22, 4, 17584, 17599));
            this.Add(new VerseList(22, 5, 17600, 17615));
            this.Add(new VerseList(22, 6, 17616, 17628));
            this.Add(new VerseList(22, 7, 17629, 17641));
            this.Add(new VerseList(22, 8, 17642, 17655));
            this.Add(new VerseList(23, 1, 17656, 17686));
            this.Add(new VerseList(23, 2, 17687, 17708));
            this.Add(new VerseList(23, 3, 17709, 17734));
            this.Add(new VerseList(23, 4, 17735, 17740));
            this.Add(new VerseList(23, 5, 17741, 17770));
            this.Add(new VerseList(23, 6, 17771, 17783));
            this.Add(new VerseList(23, 7, 17784, 17808));
            this.Add(new VerseList(23, 8, 17809, 17830));
            this.Add(new VerseList(23, 9, 17831, 17851));
            this.Add(new VerseList(23, 10, 17852, 17885));
            this.Add(new VerseList(23, 11, 17886, 17901));
            this.Add(new VerseList(23, 12, 17902, 17907));
            this.Add(new VerseList(23, 13, 17908, 17929));
            this.Add(new VerseList(23, 14, 17930, 17961));
            this.Add(new VerseList(23, 15, 17962, 17970));
            this.Add(new VerseList(23, 16, 17971, 17984));
            this.Add(new VerseList(23, 17, 17985, 17998));
            this.Add(new VerseList(23, 18, 17999, 18005));
            this.Add(new VerseList(23, 19, 18006, 18030));
            this.Add(new VerseList(23, 20, 18031, 18036));
            this.Add(new VerseList(23, 21, 18037, 18053));
            this.Add(new VerseList(23, 22, 18054, 18078));
            this.Add(new VerseList(23, 23, 18079, 18096));
            this.Add(new VerseList(23, 24, 18097, 18119));
            this.Add(new VerseList(23, 25, 18120, 18131));
            this.Add(new VerseList(23, 26, 18132, 18152));
            this.Add(new VerseList(23, 27, 18153, 18165));
            this.Add(new VerseList(23, 28, 18166, 18194));
            this.Add(new VerseList(23, 29, 18195, 18218));
            this.Add(new VerseList(23, 30, 18219, 18251));
            this.Add(new VerseList(23, 31, 18252, 18260));
            this.Add(new VerseList(23, 32, 18261, 18280));
            this.Add(new VerseList(23, 33, 18281, 18304));
            this.Add(new VerseList(23, 34, 18305, 18321));
            this.Add(new VerseList(23, 35, 18322, 18331));
            this.Add(new VerseList(23, 36, 18332, 18353));
            this.Add(new VerseList(23, 37, 18354, 18391));
            this.Add(new VerseList(23, 38, 18392, 18413));
            this.Add(new VerseList(23, 39, 18414, 18421));
            this.Add(new VerseList(23, 40, 18422, 18452));
            this.Add(new VerseList(23, 41, 18453, 18481));
            this.Add(new VerseList(23, 42, 18482, 18506));
            this.Add(new VerseList(23, 43, 18507, 18534));
            this.Add(new VerseList(23, 44, 18535, 18562));
            this.Add(new VerseList(23, 45, 18563, 18587));
            this.Add(new VerseList(23, 46, 18588, 18600));
            this.Add(new VerseList(23, 47, 18601, 18615));
            this.Add(new VerseList(23, 48, 18616, 18637));
            this.Add(new VerseList(23, 49, 18638, 18663));
            this.Add(new VerseList(23, 50, 18664, 18674));
            this.Add(new VerseList(23, 51, 18675, 18697));
            this.Add(new VerseList(23, 52, 18698, 18712));
            this.Add(new VerseList(23, 53, 18713, 18724));
            this.Add(new VerseList(23, 54, 18725, 18741));
            this.Add(new VerseList(23, 55, 18742, 18754));
            this.Add(new VerseList(23, 56, 18755, 18766));
            this.Add(new VerseList(23, 57, 18767, 18787));
            this.Add(new VerseList(23, 58, 18788, 18801));
            this.Add(new VerseList(23, 59, 18802, 18822));
            this.Add(new VerseList(23, 60, 18823, 18844));
            this.Add(new VerseList(23, 61, 18845, 18855));
            this.Add(new VerseList(23, 62, 18856, 18867));
            this.Add(new VerseList(23, 63, 18868, 18886));
            this.Add(new VerseList(23, 64, 18887, 18898));
            this.Add(new VerseList(23, 65, 18899, 18923));
            this.Add(new VerseList(23, 66, 18924, 18947));
            this.Add(new VerseList(24, 1, 18948, 18966));
            this.Add(new VerseList(24, 2, 18967, 19003));
            this.Add(new VerseList(24, 3, 19004, 19028));
            this.Add(new VerseList(24, 4, 19029, 19059));
            this.Add(new VerseList(24, 5, 19060, 19090));
            this.Add(new VerseList(24, 6, 19091, 19120));
            this.Add(new VerseList(24, 7, 19121, 19154));
            this.Add(new VerseList(24, 8, 19155, 19176));
            this.Add(new VerseList(24, 9, 19177, 19202));
            this.Add(new VerseList(24, 10, 19203, 19227));
            this.Add(new VerseList(24, 11, 19228, 19250));
            this.Add(new VerseList(24, 12, 19251, 19267));
            this.Add(new VerseList(24, 13, 19268, 19294));
            this.Add(new VerseList(24, 14, 19295, 19316));
            this.Add(new VerseList(24, 15, 19317, 19337));
            this.Add(new VerseList(24, 16, 19338, 19358));
            this.Add(new VerseList(24, 17, 19359, 19385));
            this.Add(new VerseList(24, 18, 19386, 19408));
            this.Add(new VerseList(24, 19, 19409, 19423));
            this.Add(new VerseList(24, 20, 19424, 19441));
            this.Add(new VerseList(24, 21, 19442, 19455));
            this.Add(new VerseList(24, 22, 19456, 19485));
            this.Add(new VerseList(24, 23, 19486, 19525));
            this.Add(new VerseList(24, 24, 19526, 19535));
            this.Add(new VerseList(24, 25, 19536, 19573));
            this.Add(new VerseList(24, 26, 19574, 19597));
            this.Add(new VerseList(24, 27, 19598, 19619));
            this.Add(new VerseList(24, 28, 19620, 19636));
            this.Add(new VerseList(24, 29, 19637, 19668));
            this.Add(new VerseList(24, 30, 19669, 19692));
            this.Add(new VerseList(24, 31, 19693, 19732));
            this.Add(new VerseList(24, 32, 19733, 19776));
            this.Add(new VerseList(24, 33, 19777, 19802));
            this.Add(new VerseList(24, 34, 19803, 19824));
            this.Add(new VerseList(24, 35, 19825, 19843));
            this.Add(new VerseList(24, 36, 19844, 19875));
            this.Add(new VerseList(24, 37, 19876, 19896));
            this.Add(new VerseList(24, 38, 19897, 19924));
            this.Add(new VerseList(24, 39, 19925, 19942));
            this.Add(new VerseList(24, 40, 19943, 19958));
            this.Add(new VerseList(24, 41, 19959, 19976));
            this.Add(new VerseList(24, 42, 19977, 19998));
            this.Add(new VerseList(24, 43, 19999, 20011));
            this.Add(new VerseList(24, 44, 20012, 20041));
            this.Add(new VerseList(24, 45, 20042, 20046));
            this.Add(new VerseList(24, 46, 20047, 20074));
            this.Add(new VerseList(24, 47, 20075, 20081));
            this.Add(new VerseList(24, 48, 20082, 20128));
            this.Add(new VerseList(24, 49, 20129, 20167));
            this.Add(new VerseList(24, 50, 20168, 20213));
            this.Add(new VerseList(24, 51, 20214, 20277));
            this.Add(new VerseList(24, 52, 20278, 20311));
            this.Add(new VerseList(25, 1, 20312, 20333));
            this.Add(new VerseList(25, 2, 20334, 20355));
            this.Add(new VerseList(25, 3, 20356, 20421));
            this.Add(new VerseList(25, 4, 20422, 20443));
            this.Add(new VerseList(25, 5, 20444, 20465));
            this.Add(new VerseList(26, 1, 20466, 20493));
            this.Add(new VerseList(26, 2, 20494, 20503));
            this.Add(new VerseList(26, 3, 20504, 20530));
            this.Add(new VerseList(26, 4, 20531, 20547));
            this.Add(new VerseList(26, 5, 20548, 20564));
            this.Add(new VerseList(26, 6, 20565, 20578));
            this.Add(new VerseList(26, 7, 20579, 20605));
            this.Add(new VerseList(26, 8, 20606, 20623));
            this.Add(new VerseList(26, 9, 20624, 20634));
            this.Add(new VerseList(26, 10, 20635, 20656));
            this.Add(new VerseList(26, 11, 20657, 20681));
            this.Add(new VerseList(26, 12, 20682, 20709));
            this.Add(new VerseList(26, 13, 20710, 20732));
            this.Add(new VerseList(26, 14, 20733, 20755));
            this.Add(new VerseList(26, 15, 20756, 20763));
            this.Add(new VerseList(26, 16, 20764, 20826));
            this.Add(new VerseList(26, 17, 20827, 20850));
            this.Add(new VerseList(26, 18, 20851, 20882));
            this.Add(new VerseList(26, 19, 20883, 20896));
            this.Add(new VerseList(26, 20, 20897, 20945));
            this.Add(new VerseList(26, 21, 20946, 20977));
            this.Add(new VerseList(26, 22, 20978, 21008));
            this.Add(new VerseList(26, 23, 21009, 21057));
            this.Add(new VerseList(26, 24, 21058, 21084));
            this.Add(new VerseList(26, 25, 21085, 21101));
            this.Add(new VerseList(26, 26, 21102, 21122));
            this.Add(new VerseList(26, 27, 21123, 21158));
            this.Add(new VerseList(26, 28, 21159, 21184));
            this.Add(new VerseList(26, 29, 21185, 21205));
            this.Add(new VerseList(26, 30, 21206, 21231));
            this.Add(new VerseList(26, 31, 21232, 21249));
            this.Add(new VerseList(26, 32, 21250, 21281));
            this.Add(new VerseList(26, 33, 21282, 21314));
            this.Add(new VerseList(26, 34, 21315, 21345));
            this.Add(new VerseList(26, 35, 21346, 21360));
            this.Add(new VerseList(26, 36, 21361, 21398));
            this.Add(new VerseList(26, 37, 21399, 21426));
            this.Add(new VerseList(26, 38, 21427, 21449));
            this.Add(new VerseList(26, 39, 21450, 21478));
            this.Add(new VerseList(26, 40, 21479, 21527));
            this.Add(new VerseList(26, 41, 21528, 21553));
            this.Add(new VerseList(26, 42, 21554, 21573));
            this.Add(new VerseList(26, 43, 21574, 21600));
            this.Add(new VerseList(26, 44, 21601, 21631));
            this.Add(new VerseList(26, 45, 21632, 21656));
            this.Add(new VerseList(26, 46, 21657, 21680));
            this.Add(new VerseList(26, 47, 21681, 21703));
            this.Add(new VerseList(26, 48, 21704, 21738));
            this.Add(new VerseList(27, 1, 21739, 21759));
            this.Add(new VerseList(27, 2, 21760, 21808));
            this.Add(new VerseList(27, 3, 21809, 21838));
            this.Add(new VerseList(27, 4, 21839, 21875));
            this.Add(new VerseList(27, 5, 21876, 21906));
            this.Add(new VerseList(27, 6, 21907, 21934));
            this.Add(new VerseList(27, 7, 21935, 21962));
            this.Add(new VerseList(27, 8, 21963, 21989));
            this.Add(new VerseList(27, 9, 21990, 22016));
            this.Add(new VerseList(27, 10, 22017, 22037));
            this.Add(new VerseList(27, 11, 22038, 22082));
            this.Add(new VerseList(27, 12, 22083, 22095));
            this.Add(new VerseList(28, 1, 22096, 22106));
            this.Add(new VerseList(28, 2, 22107, 22129));
            this.Add(new VerseList(28, 3, 22130, 22134));
            this.Add(new VerseList(28, 4, 22135, 22153));
            this.Add(new VerseList(28, 5, 22154, 22168));
            this.Add(new VerseList(28, 6, 22169, 22179));
            this.Add(new VerseList(28, 7, 22180, 22195));
            this.Add(new VerseList(28, 8, 22196, 22209));
            this.Add(new VerseList(28, 9, 22210, 22226));
            this.Add(new VerseList(28, 10, 22227, 22241));
            this.Add(new VerseList(28, 11, 22242, 22253));
            this.Add(new VerseList(28, 12, 22254, 22267));
            this.Add(new VerseList(28, 13, 22268, 22283));
            this.Add(new VerseList(28, 14, 22284, 22292));
            this.Add(new VerseList(29, 1, 22293, 22312));
            this.Add(new VerseList(29, 2, 22313, 22344));
            this.Add(new VerseList(29, 3, 22345, 22365));
            this.Add(new VerseList(30, 1, 22366, 22380));
            this.Add(new VerseList(30, 2, 22381, 22396));
            this.Add(new VerseList(30, 3, 22397, 22411));
            this.Add(new VerseList(30, 4, 22412, 22424));
            this.Add(new VerseList(30, 5, 22425, 22451));
            this.Add(new VerseList(30, 6, 22452, 22465));
            this.Add(new VerseList(30, 7, 22466, 22482));
            this.Add(new VerseList(30, 8, 22483, 22496));
            this.Add(new VerseList(30, 9, 22497, 22511));
            this.Add(new VerseList(31, 1, 22512, 22532));
            this.Add(new VerseList(32, 1, 22533, 22549));
            this.Add(new VerseList(32, 2, 22550, 22559));
            this.Add(new VerseList(32, 3, 22560, 22569));
            this.Add(new VerseList(32, 4, 22570, 22580));
            this.Add(new VerseList(33, 1, 22581, 22596));
            this.Add(new VerseList(33, 2, 22597, 22609));
            this.Add(new VerseList(33, 3, 22610, 22621));
            this.Add(new VerseList(33, 4, 22622, 22634));
            this.Add(new VerseList(33, 5, 22635, 22649));
            this.Add(new VerseList(33, 6, 22650, 22665));
            this.Add(new VerseList(33, 7, 22666, 22685));
            this.Add(new VerseList(34, 8, 22686, 22700));
            this.Add(new VerseList(34, 9, 22701, 22713));
            this.Add(new VerseList(34, 10, 22714, 22732));
            this.Add(new VerseList(35, 1, 22733, 22749));
            this.Add(new VerseList(35, 2, 22750, 22769));
            this.Add(new VerseList(35, 3, 22770, 22788));
            this.Add(new VerseList(36, 1, 22789, 22806));
            this.Add(new VerseList(36, 2, 22807, 22821));
            this.Add(new VerseList(36, 3, 22822, 22841));
            /* New Testament begins here */
            this.Add(new VerseList(37, 1, 22842, 22856));
            this.Add(new VerseList(37, 2, 22857, 22879));
            this.Add(new VerseList(38, 1, 22880, 22900));
            this.Add(new VerseList(38, 2, 22901, 22913));
            this.Add(new VerseList(38, 3, 22914, 22923));
            this.Add(new VerseList(38, 4, 22924, 22937));
            this.Add(new VerseList(38, 5, 22938, 22948));
            this.Add(new VerseList(38, 6, 22949, 22963));
            this.Add(new VerseList(38, 7, 22964, 22977));
            this.Add(new VerseList(38, 8, 22978, 23000));
            this.Add(new VerseList(38, 9, 23001, 23017));
            this.Add(new VerseList(38, 10, 23018, 23029));
            this.Add(new VerseList(38, 11, 23030, 23046));
            this.Add(new VerseList(38, 12, 23047, 23060));
            this.Add(new VerseList(38, 13, 23061, 23069));
            this.Add(new VerseList(38, 14, 23070, 23090));
            this.Add(new VerseList(39, 1, 23091, 23104));
            this.Add(new VerseList(39, 2, 23105, 23121));
            this.Add(new VerseList(39, 3, 23122, 23139));
            this.Add(new VerseList(39, 4, 23140, 23145));
            this.Add(new VerseList(40, 1, 23146, 23170));
            this.Add(new VerseList(40, 2, 23171, 23193));
            this.Add(new VerseList(40, 3, 23194, 23210));
            this.Add(new VerseList(40, 4, 23211, 23235));
            this.Add(new VerseList(40, 5, 23236, 23283));
            this.Add(new VerseList(40, 6, 23284, 23317));
            this.Add(new VerseList(40, 7, 23318, 23346));
            this.Add(new VerseList(40, 8, 23347, 23380));
            this.Add(new VerseList(40, 9, 23381, 23418));
            this.Add(new VerseList(40, 10, 23419, 23460));
            this.Add(new VerseList(40, 11, 23461, 23490));
            this.Add(new VerseList(40, 12, 23491, 23540));
            this.Add(new VerseList(40, 13, 23541, 23598));
            this.Add(new VerseList(40, 14, 23599, 23634));
            this.Add(new VerseList(40, 15, 23635, 23673));
            this.Add(new VerseList(40, 16, 23674, 23701));
            this.Add(new VerseList(40, 17, 23702, 23728));
            this.Add(new VerseList(40, 18, 23729, 23763));
            this.Add(new VerseList(40, 19, 23764, 23793));
            this.Add(new VerseList(40, 20, 23794, 23827));
            this.Add(new VerseList(40, 21, 23828, 23873));
            this.Add(new VerseList(40, 22, 23874, 23919));
            this.Add(new VerseList(40, 23, 23920, 23958));
            this.Add(new VerseList(40, 24, 23959, 24009));
            this.Add(new VerseList(40, 25, 24010, 24055));
            this.Add(new VerseList(40, 26, 24056, 24130));
            this.Add(new VerseList(40, 27, 24131, 24196));
            this.Add(new VerseList(40, 28, 24197, 24216));
            this.Add(new VerseList(41, 1, 24217, 24261));
            this.Add(new VerseList(41, 2, 24262, 24289));
            this.Add(new VerseList(41, 3, 24290, 24324));
            this.Add(new VerseList(41, 4, 24325, 24365));
            this.Add(new VerseList(41, 5, 24366, 24408));
            this.Add(new VerseList(41, 6, 24409, 24464));
            this.Add(new VerseList(41, 7, 24465, 24501));
            this.Add(new VerseList(41, 8, 24502, 24539));
            this.Add(new VerseList(41, 9, 24540, 24589));
            this.Add(new VerseList(41, 10, 24590, 24641));
            this.Add(new VerseList(41, 11, 24642, 24674));
            this.Add(new VerseList(41, 12, 24675, 24718));
            this.Add(new VerseList(41, 13, 24719, 24755));
            this.Add(new VerseList(41, 14, 24756, 24827));
            this.Add(new VerseList(41, 15, 24828, 24874));
            this.Add(new VerseList(41, 16, 24875, 24894));
            this.Add(new VerseList(42, 1, 24895, 24974));
            this.Add(new VerseList(42, 2, 24975, 25026));
            this.Add(new VerseList(42, 3, 25027, 25064));
            this.Add(new VerseList(42, 4, 25065, 25108));
            this.Add(new VerseList(42, 5, 25109, 25147));
            this.Add(new VerseList(42, 6, 25148, 25196));
            this.Add(new VerseList(42, 7, 25197, 25246));
            this.Add(new VerseList(42, 8, 25247, 25302));
            this.Add(new VerseList(42, 9, 25303, 25364));
            this.Add(new VerseList(42, 10, 25365, 25406));
            this.Add(new VerseList(42, 11, 25407, 25460));
            this.Add(new VerseList(42, 12, 25461, 25519));
            this.Add(new VerseList(42, 13, 25520, 25554));
            this.Add(new VerseList(42, 14, 25555, 25589));
            this.Add(new VerseList(42, 15, 25590, 25621));
            this.Add(new VerseList(42, 16, 25622, 25652));
            this.Add(new VerseList(42, 17, 25653, 25689));
            this.Add(new VerseList(42, 18, 25690, 25732));
            this.Add(new VerseList(42, 19, 25733, 25780));
            this.Add(new VerseList(42, 20, 25781, 25827));
            this.Add(new VerseList(42, 21, 25828, 25865));
            this.Add(new VerseList(42, 22, 25866, 25936));
            this.Add(new VerseList(42, 23, 25937, 25992));
            this.Add(new VerseList(42, 24, 25993, 26045));
            this.Add(new VerseList(43, 1, 26046, 26096));
            this.Add(new VerseList(43, 2, 26097, 26121));
            this.Add(new VerseList(43, 3, 26122, 26157));
            this.Add(new VerseList(43, 4, 26158, 26211));
            this.Add(new VerseList(43, 5, 26212, 26258));
            this.Add(new VerseList(43, 6, 26259, 26329));
            this.Add(new VerseList(43, 7, 26330, 26382));
            this.Add(new VerseList(43, 8, 26383, 26441));
            this.Add(new VerseList(43, 9, 26442, 26482));
            this.Add(new VerseList(43, 10, 26483, 26524));
            this.Add(new VerseList(43, 11, 26525, 26581));
            this.Add(new VerseList(43, 12, 26582, 26631));
            this.Add(new VerseList(43, 13, 26632, 26669));
            this.Add(new VerseList(43, 14, 26670, 26700));
            this.Add(new VerseList(43, 15, 26701, 26727));
            this.Add(new VerseList(43, 16, 26728, 26760));
            this.Add(new VerseList(43, 17, 26761, 26786));
            this.Add(new VerseList(43, 18, 26787, 26826));
            this.Add(new VerseList(43, 19, 26827, 26868));
            this.Add(new VerseList(43, 20, 26869, 26899));
            this.Add(new VerseList(43, 21, 26900, 26924));
            this.Add(new VerseList(44, 1, 26925, 26950));
            this.Add(new VerseList(44, 2, 26951, 26997));
            this.Add(new VerseList(44, 3, 26998, 27023));
            this.Add(new VerseList(44, 4, 27024, 27060));
            this.Add(new VerseList(44, 5, 27061, 27102));
            this.Add(new VerseList(44, 6, 27103, 27117));
            this.Add(new VerseList(44, 7, 27118, 27177));
            this.Add(new VerseList(44, 8, 27178, 27217));
            this.Add(new VerseList(44, 9, 27218, 27260));
            this.Add(new VerseList(44, 10, 27261, 27308));
            this.Add(new VerseList(44, 11, 27309, 27338));
            this.Add(new VerseList(44, 12, 27339, 27363));
            this.Add(new VerseList(44, 13, 27364, 27415));
            this.Add(new VerseList(44, 14, 27416, 27443));
            this.Add(new VerseList(44, 15, 27444, 27484));
            this.Add(new VerseList(44, 16, 27485, 27524));
            this.Add(new VerseList(44, 17, 27525, 27558));
            this.Add(new VerseList(44, 18, 27559, 27586));
            this.Add(new VerseList(44, 19, 27587, 27627));
            this.Add(new VerseList(44, 20, 27628, 27665));
            this.Add(new VerseList(44, 21, 27666, 27705));
            this.Add(new VerseList(44, 22, 27706, 27735));
            this.Add(new VerseList(44, 23, 27736, 27770));
            this.Add(new VerseList(44, 24, 27771, 27797));
            this.Add(new VerseList(44, 25, 27798, 27824));
            this.Add(new VerseList(44, 26, 27825, 27856));
            this.Add(new VerseList(44, 27, 27857, 27900));
            this.Add(new VerseList(44, 28, 27901, 27931));
            this.Add(new VerseList(45, 1, 27932, 27963));
            this.Add(new VerseList(45, 2, 27964, 27992));
            this.Add(new VerseList(45, 3, 27993, 28023));
            this.Add(new VerseList(45, 4, 28024, 28048));
            this.Add(new VerseList(45, 5, 28049, 28069));
            this.Add(new VerseList(45, 6, 28070, 28092));
            this.Add(new VerseList(45, 7, 28093, 28117));
            this.Add(new VerseList(45, 8, 28118, 28156));
            this.Add(new VerseList(45, 9, 28157, 28189));
            this.Add(new VerseList(45, 10, 28190, 28210));
            this.Add(new VerseList(45, 11, 28211, 28246));
            this.Add(new VerseList(45, 12, 28247, 28267));
            this.Add(new VerseList(45, 13, 28268, 28281));
            this.Add(new VerseList(45, 14, 28282, 28304));
            this.Add(new VerseList(45, 15, 28305, 28337));
            this.Add(new VerseList(45, 16, 28338, 28364));
            this.Add(new VerseList(46, 1, 28365, 28395));
            this.Add(new VerseList(46, 2, 28396, 28411));
            this.Add(new VerseList(46, 3, 28412, 28434));
            this.Add(new VerseList(46, 4, 28435, 28455));
            this.Add(new VerseList(46, 5, 28456, 28468));
            this.Add(new VerseList(46, 6, 28469, 28488));
            this.Add(new VerseList(46, 7, 28489, 28528));
            this.Add(new VerseList(46, 8, 28529, 28541));
            this.Add(new VerseList(46, 9, 28542, 28568));
            this.Add(new VerseList(46, 10, 28569, 28601));
            this.Add(new VerseList(46, 11, 28602, 28635));
            this.Add(new VerseList(46, 12, 28636, 28666));
            this.Add(new VerseList(46, 13, 28667, 28679));
            this.Add(new VerseList(46, 14, 28680, 28719));
            this.Add(new VerseList(46, 15, 28720, 28777));
            this.Add(new VerseList(46, 16, 28778, 28801));
            this.Add(new VerseList(47, 1, 28802, 28825));
            this.Add(new VerseList(47, 2, 28826, 28842));
            this.Add(new VerseList(47, 3, 28843, 28860));
            this.Add(new VerseList(47, 4, 28861, 28878));
            this.Add(new VerseList(47, 5, 28879, 28899));
            this.Add(new VerseList(47, 6, 28900, 28917));
            this.Add(new VerseList(47, 7, 28918, 28933));
            this.Add(new VerseList(47, 8, 28934, 28957));
            this.Add(new VerseList(47, 9, 28958, 28972));
            this.Add(new VerseList(47, 10, 28973, 28990));
            this.Add(new VerseList(47, 11, 28991, 29023));
            this.Add(new VerseList(47, 12, 29024, 29044));
            this.Add(new VerseList(47, 13, 29045, 29058));
            this.Add(new VerseList(48, 1, 29059, 29082));
            this.Add(new VerseList(48, 2, 29083, 29103));
            this.Add(new VerseList(48, 3, 29104, 29132));
            this.Add(new VerseList(48, 4, 29133, 29163));
            this.Add(new VerseList(48, 5, 29164, 29189));
            this.Add(new VerseList(48, 6, 29190, 29207));
            this.Add(new VerseList(49, 1, 29208, 29230));
            this.Add(new VerseList(49, 2, 29231, 29252));
            this.Add(new VerseList(49, 3, 29253, 29273));
            this.Add(new VerseList(49, 4, 29274, 29305));
            this.Add(new VerseList(49, 5, 29306, 29338));
            this.Add(new VerseList(49, 6, 29339, 29362));
            this.Add(new VerseList(50, 1, 29363, 29392));
            this.Add(new VerseList(50, 2, 29393, 29422));
            this.Add(new VerseList(50, 3, 29423, 29443));
            this.Add(new VerseList(50, 4, 29444, 29466));
            this.Add(new VerseList(51, 1, 29467, 29495));
            this.Add(new VerseList(51, 2, 29496, 29518));
            this.Add(new VerseList(51, 3, 29519, 29543));
            this.Add(new VerseList(51, 4, 29544, 29561));
            this.Add(new VerseList(52, 1, 29562, 29571));
            this.Add(new VerseList(52, 2, 29572, 29591));
            this.Add(new VerseList(52, 3, 29592, 29604));
            this.Add(new VerseList(52, 4, 29605, 29622));
            this.Add(new VerseList(52, 5, 29623, 29650));
            this.Add(new VerseList(53, 1, 29651, 29662));
            this.Add(new VerseList(53, 2, 29663, 29679));
            this.Add(new VerseList(53, 3, 29680, 29697));
            this.Add(new VerseList(54, 1, 29698, 29717));
            this.Add(new VerseList(54, 2, 29718, 29732));
            this.Add(new VerseList(54, 3, 29733, 29748));
            this.Add(new VerseList(54, 4, 29749, 29764));
            this.Add(new VerseList(54, 5, 29765, 29789));
            this.Add(new VerseList(54, 6, 29790, 29810));
            this.Add(new VerseList(55, 1, 29811, 29828));
            this.Add(new VerseList(55, 2, 29829, 29854));
            this.Add(new VerseList(55, 3, 29855, 29871));
            this.Add(new VerseList(55, 4, 29872, 29893));
            this.Add(new VerseList(56, 1, 29894, 29909));
            this.Add(new VerseList(56, 2, 29910, 29924));
            this.Add(new VerseList(56, 3, 29925, 29939));
            this.Add(new VerseList(57, 1, 29940, 29964));
            this.Add(new VerseList(58, 1, 29965, 29978));
            this.Add(new VerseList(58, 2, 29979, 29996));
            this.Add(new VerseList(58, 3, 29997, 30015));
            this.Add(new VerseList(58, 4, 30016, 30031));
            this.Add(new VerseList(58, 5, 30032, 30045));
            this.Add(new VerseList(58, 6, 30046, 30065));
            this.Add(new VerseList(58, 7, 30066, 30093));
            this.Add(new VerseList(58, 8, 30094, 30106));
            this.Add(new VerseList(58, 9, 30107, 30134));
            this.Add(new VerseList(58, 10, 30135, 30173));
            this.Add(new VerseList(58, 11, 30174, 30213));
            this.Add(new VerseList(58, 12, 30214, 30242));
            this.Add(new VerseList(58, 13, 30243, 30267));
            this.Add(new VerseList(59, 1, 30268, 30294));
            this.Add(new VerseList(59, 2, 30295, 30320));
            this.Add(new VerseList(59, 3, 30321, 30338));
            this.Add(new VerseList(59, 4, 30339, 30355));
            this.Add(new VerseList(59, 5, 30356, 30375));
            this.Add(new VerseList(60, 1, 30376, 30400));
            this.Add(new VerseList(60, 2, 30401, 30425));
            this.Add(new VerseList(60, 3, 30426, 30447));
            this.Add(new VerseList(60, 4, 30448, 30466));
            this.Add(new VerseList(60, 5, 30467, 30480));
            this.Add(new VerseList(61, 1, 30481, 30501));
            this.Add(new VerseList(61, 2, 30502, 30523));
            this.Add(new VerseList(61, 3, 30524, 30541));
            this.Add(new VerseList(62, 1, 30542, 30551));
            this.Add(new VerseList(62, 2, 30552, 30580));
            this.Add(new VerseList(62, 3, 30581, 30604));
            this.Add(new VerseList(62, 4, 30605, 30625));
            this.Add(new VerseList(62, 5, 30626, 30646));
            this.Add(new VerseList(63, 1, 30647, 30659));
            this.Add(new VerseList(64, 1, 30660, 30673));
            this.Add(new VerseList(65, 1, 30674, 30698));
            this.Add(new VerseList(66, 1, 30699, 30718));
            this.Add(new VerseList(66, 2, 30719, 30747));
            this.Add(new VerseList(66, 3, 30748, 30769));
            this.Add(new VerseList(66, 4, 30770, 30780));
            this.Add(new VerseList(66, 5, 30781, 30794));
            this.Add(new VerseList(66, 6, 30795, 30811));
            this.Add(new VerseList(66, 7, 30812, 30828));
            this.Add(new VerseList(66, 8, 30829, 30841));
            this.Add(new VerseList(66, 9, 30842, 30862));
            this.Add(new VerseList(66, 10, 30863, 30873));
            this.Add(new VerseList(66, 11, 30874, 30892));
            this.Add(new VerseList(66, 12, 30893, 30909));
            this.Add(new VerseList(66, 13, 30910, 30927));
            this.Add(new VerseList(66, 14, 30928, 30947));
            this.Add(new VerseList(66, 15, 30948, 30955));
            this.Add(new VerseList(66, 16, 30956, 30976));
            this.Add(new VerseList(66, 17, 30977, 30994));
            this.Add(new VerseList(66, 18, 30995, 31018));
            this.Add(new VerseList(66, 19, 31019, 31039));
            this.Add(new VerseList(66, 20, 31040, 31054));
            this.Add(new VerseList(66, 21, 31055, 31081));
            this.Add(new VerseList(66, 22, 31082, 31102));
            /* Apocrypha begins here */
            this.Add(new VerseList(67, 1, 31103, 31124));
            this.Add(new VerseList(67, 2, 31125, 31138));
            this.Add(new VerseList(67, 3, 31139, 31155));
            this.Add(new VerseList(67, 4, 31156, 31176));
            this.Add(new VerseList(67, 5, 31177, 31197));
            this.Add(new VerseList(67, 6, 31198, 31214));
            this.Add(new VerseList(67, 7, 31215, 31232));
            this.Add(new VerseList(67, 8, 31233, 31253));
            this.Add(new VerseList(67, 9, 31254, 31259));
            this.Add(new VerseList(67, 10, 31260, 31271));
            this.Add(new VerseList(67, 11, 31272, 31290));
            this.Add(new VerseList(67, 12, 31291, 31312));
            this.Add(new VerseList(67, 13, 31313, 31330));
            this.Add(new VerseList(67, 14, 31331, 31345));
            this.Add(new VerseList(68, 1, 31346, 31361));
            this.Add(new VerseList(68, 2, 31362, 31389));
            this.Add(new VerseList(68, 3, 31390, 31399));
            this.Add(new VerseList(68, 4, 31400, 31414));
            this.Add(new VerseList(68, 5, 31415, 31438));
            this.Add(new VerseList(68, 6, 31439, 31459));
            this.Add(new VerseList(68, 7, 31460, 31491));
            this.Add(new VerseList(68, 8, 31492, 31527));
            this.Add(new VerseList(68, 9, 31528, 31541));
            this.Add(new VerseList(68, 10, 31542, 31564));
            this.Add(new VerseList(68, 11, 31565, 31587));
            this.Add(new VerseList(68, 12, 31588, 31607));
            this.Add(new VerseList(68, 13, 31608, 31623));
            this.Add(new VerseList(68, 14, 31624, 31642));
            this.Add(new VerseList(68, 15, 31643, 31656));
            this.Add(new VerseList(68, 16, 31657, 31681));
            this.Add(new VerseList(69, 1, 31682, 31697));
            this.Add(new VerseList(69, 2, 31698, 31721));
            this.Add(new VerseList(69, 3, 31722, 31740));
            this.Add(new VerseList(69, 4, 31741, 31760));
            this.Add(new VerseList(69, 5, 31761, 31783));
            this.Add(new VerseList(69, 6, 31784, 31808));
            this.Add(new VerseList(69, 7, 31809, 31838));
            this.Add(new VerseList(69, 8, 31839, 31859));
            this.Add(new VerseList(69, 9, 31860, 31877));
            this.Add(new VerseList(69, 10, 31878, 31898));
            this.Add(new VerseList(69, 11, 31899, 31924));
            this.Add(new VerseList(69, 12, 31925, 31951));
            this.Add(new VerseList(69, 13, 31952, 31970));
            this.Add(new VerseList(69, 14, 31971, 32001));
            this.Add(new VerseList(69, 15, 32002, 32020));
            this.Add(new VerseList(69, 16, 32021, 32049));
            this.Add(new VerseList(69, 17, 32050, 32070));
            this.Add(new VerseList(69, 18, 32071, 32095));
            this.Add(new VerseList(69, 19, 32096, 32117));
            this.Add(new VerseList(70, 1, 32118, 32147));
            this.Add(new VerseList(70, 2, 32148, 32165));
            this.Add(new VerseList(70, 3, 32166, 32196));
            this.Add(new VerseList(70, 4, 32197, 32227));
            this.Add(new VerseList(70, 5, 32228, 32242));
            this.Add(new VerseList(70, 6, 32243, 32279));
            this.Add(new VerseList(70, 7, 32280, 32315));
            this.Add(new VerseList(70, 8, 32316, 32334));
            this.Add(new VerseList(70, 9, 32335, 32352));
            this.Add(new VerseList(70, 10, 32353, 32383));
            this.Add(new VerseList(70, 11, 32384, 32417));
            this.Add(new VerseList(70, 12, 32418, 32435));
            this.Add(new VerseList(70, 13, 32436, 32461));
            this.Add(new VerseList(70, 14, 32462, 32488));
            this.Add(new VerseList(70, 15, 32489, 32508));
            this.Add(new VerseList(70, 16, 32509, 32538));
            this.Add(new VerseList(70, 17, 32539, 32570));
            this.Add(new VerseList(70, 18, 32571, 32603));
            this.Add(new VerseList(70, 19, 32604, 32633));
            this.Add(new VerseList(70, 20, 32634, 32665));
            this.Add(new VerseList(70, 21, 32666, 32693));
            this.Add(new VerseList(70, 22, 32694, 32720));
            this.Add(new VerseList(70, 23, 32721, 32747));
            this.Add(new VerseList(70, 24, 32748, 32781));
            this.Add(new VerseList(70, 25, 32782, 32807));
            this.Add(new VerseList(70, 26, 32808, 32836));
            this.Add(new VerseList(70, 27, 32837, 32866));
            this.Add(new VerseList(70, 28, 32867, 32892));
            this.Add(new VerseList(70, 29, 32893, 32920));
            this.Add(new VerseList(70, 30, 32921, 32945));
            this.Add(new VerseList(70, 31, 32946, 32976));
            this.Add(new VerseList(70, 32, 32977, 33000));
            this.Add(new VerseList(70, 33, 33001, 33031));
            this.Add(new VerseList(70, 34, 33032, 33057));
            this.Add(new VerseList(70, 35, 33058, 33077));
            this.Add(new VerseList(70, 36, 33078, 33103));
            this.Add(new VerseList(70, 37, 33104, 33134));
            this.Add(new VerseList(70, 38, 33135, 33168));
            this.Add(new VerseList(70, 39, 33169, 33203));
            this.Add(new VerseList(70, 40, 33204, 33233));
            this.Add(new VerseList(70, 41, 33234, 33256));
            this.Add(new VerseList(70, 42, 33257, 33281));
            this.Add(new VerseList(70, 43, 33282, 33314));
            this.Add(new VerseList(70, 44, 33315, 33337));
            this.Add(new VerseList(70, 45, 33338, 33363));
            this.Add(new VerseList(70, 46, 33364, 33383));
            this.Add(new VerseList(70, 47, 33384, 33408));
            this.Add(new VerseList(70, 48, 33409, 33433));
            this.Add(new VerseList(70, 49, 33434, 33449));
            this.Add(new VerseList(70, 50, 33450, 33478));
            this.Add(new VerseList(70, 51, 33479, 33508));
            this.Add(new VerseList(71, 1, 33509, 33530));
            this.Add(new VerseList(71, 2, 33531, 33565));
            this.Add(new VerseList(71, 3, 33566, 33602));
            this.Add(new VerseList(71, 4, 33603, 33639));
            this.Add(new VerseList(71, 5, 33640, 33648));
            this.Add(new VerseList(71, 6, 33649, 33720));
            this.Add(new VerseList(72, 1, 33721, 33784));
            this.Add(new VerseList(72, 2, 33785, 33854));
            this.Add(new VerseList(72, 3, 33855, 33914));
            this.Add(new VerseList(72, 4, 33915, 33975));
            this.Add(new VerseList(72, 5, 33976, 34043));
            this.Add(new VerseList(72, 6, 34044, 34106));
            this.Add(new VerseList(72, 7, 34107, 34156));
            this.Add(new VerseList(72, 8, 34157, 34188));
            this.Add(new VerseList(72, 9, 34189, 34261));
            this.Add(new VerseList(72, 10, 34262, 34350));
            this.Add(new VerseList(72, 11, 34351, 34424));
            this.Add(new VerseList(72, 12, 34425, 34477));
            this.Add(new VerseList(72, 13, 34478, 34530));
            this.Add(new VerseList(72, 14, 34531, 34579));
            this.Add(new VerseList(72, 15, 34580, 34620));
            this.Add(new VerseList(72, 16, 34621, 34644));
            this.Add(new VerseList(73, 1, 34645, 34680));
            this.Add(new VerseList(73, 2, 34681, 34712));
            this.Add(new VerseList(73, 3, 34713, 34752));
            this.Add(new VerseList(73, 4, 34753, 34802));
            this.Add(new VerseList(73, 5, 34803, 34829));
            this.Add(new VerseList(73, 6, 34830, 34860));
            this.Add(new VerseList(73, 7, 34861, 34902));
            this.Add(new VerseList(73, 8, 34903, 34938));
            this.Add(new VerseList(73, 9, 34939, 34967));
            this.Add(new VerseList(73, 10, 34968, 35005));
            this.Add(new VerseList(73, 11, 35006, 35043));
            this.Add(new VerseList(73, 12, 35044, 35088));
            this.Add(new VerseList(73, 13, 35089, 35114));
            this.Add(new VerseList(73, 14, 35115, 35160));
            this.Add(new VerseList(73, 15, 35161, 35199));
        }
    }
    #endregion
}