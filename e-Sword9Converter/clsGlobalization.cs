using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace eSword9Converter.Globalization
{
    public static class CurrentLanguage
    {
        private static ThreadSafeDictionary<string, string> Strings;

        public static string AdvancedTitle { get { return Strings["AdvancedTitle"]; } }
        public static string AlleSwordModules { get { return Strings["AlleSwordModules"]; } }
        public static string AutomaticallyOverwrite { get { return Strings["AutomaticallyOverwrite"]; } }
        public static string BatchMode { get { return Strings["BatchMode"]; } }
        public static string Bible { get { return Strings["Bible"]; } }
        public static string BibleReadingPlan { get { return Strings["BibleReadingPlan"]; } }
        public static string Bibles { get { return Strings["Bibles"]; } }
        public static string Cancel { get { return Strings["Cancel"]; } }
        public static string Column { get { return Strings["Column"]; } }
        public static string Commentaries { get { return Strings["Commentaries"]; } }
        public static string Commentary { get { return Strings["Commentary"]; } }
        public static string Completed { get { return Strings["Completed"]; } }
        public static string Convert { get { return Strings["Convert"]; } }
        public static string ConvertedFile { get { return Strings["ConvertedFile"]; } }
        public static string Converting { get { return Strings["Converting"]; } }
        public static string Destination { get { return Strings["Destination"]; } }
        public static string DestinationDirectory { get { return Strings["DestinationDirectory"]; } }
        public static string Devotional { get { return Strings["Devotional"]; } }
        public static string Dictionaries { get { return Strings["Dictionaries"]; } }
        public static string Dictionary { get { return Strings["Dictionary"]; } }
        public static string Error { get { return Strings["Error"]; } }
        public static string eSword { get { return Strings["e-Sword"]; } }
        public static string FileExists { get { return Strings["FileExists"]; } }
        public static string FileToConvert { get { return Strings["FileToConvert"]; } }
        public static string Finished { get { return Strings["Finished"]; } }
        public static string FinishedConverting { get { return Strings["FinishedConverting"]; } }
        public static string Graphics { get { return Strings["Graphics"]; } }
        public static string Harmonies { get { return Strings["Harmonies"]; } }
        public static string Harmony { get { return Strings["Harmony"]; } }
        public static string Illustrations { get { return Strings["Illustrations"]; } }
        public static string IncludeSubdirectories { get { return Strings["IncludeSubdirectories"]; } }
        public static string InvalidFileType { get { return Strings["InvalidFileType"]; } }
        public static string InvalidPassword { get { return Strings["InvalidPassword"]; } }
        public static string InvalidString { get { return Strings["InvalidString"]; } }
        public static string Loading { get { return Strings["Loading"]; } }
        public static string MainTitle { get { return Strings["MainTitle"]; } }
        public static string MemoryVerses { get { return Strings["MemoryVerses"]; } }
        public static string Message { get { return Strings["Message"]; } }
        public static string NoGetPasswordEventSubscribers { get { return Strings["NoGetPasswordEventSubscribers"]; } }
        public static string Normal { get { return Strings["Normal"]; } }
        public static string Notes { get { return Strings["Notes"]; } }
        public static string Ok { get { return Strings["Ok"]; } }
        public static string Optimizing { get { return Strings["Optimizing"]; } }
        public static string Overlay { get { return Strings["Overlay"]; } }
        public static string Overwrite { get { return Strings["Overwrite"]; } }
        public static string Password { get { return Strings["Password"]; } }
        public static string PasswordBlank { get { return Strings["PasswordBlank"]; } }
        public static string PasswordBoxClosed { get { return Strings["PasswordBoxClosed"]; } }
        public static string PasswordFound { get { return Strings["PasswordFound"]; } }
        public static string PrayerRequests { get { return Strings["PrayerRequests"]; } }
        public static string ProgressExceededMax { get { return Strings["ProgressExceededMax"]; } }
        public static string Row { get { return Strings["Row"]; } }
        public static string Saving { get { return Strings["Saving"]; } }
        public static string SkipPasswordProtectedFiles { get { return Strings["SkipPasswordProtectedFiles"]; } }
        public static string Source { get { return Strings["Source"]; } }
        public static string SourceDirectory { get { return Strings["SourceDirectory"]; } }
        public static string SourceFileInvalid { get { return Strings["SourceFileInvalid"]; } }
        public static string SourceFileNotExist { get { return Strings["SourceFileNotExist"]; } }
        public static string TopicNotes { get { return Strings["TopicNotes"]; } }
        public static string Type { get { return Strings["Type"]; } }
        public static string Value { get { return Strings["Value"]; } }
        public static string VerseLists { get { return Strings["VerseLists"]; } }
        public static string Warning { get { return Strings["Warning"]; } }

        public static string SaveFileList { get { return Bible + "|*.bblx|" + BibleReadingPlan + "|*.brpx|" + Commentary + "|*.cmtx|" + Dictionary + "|*.dctx|" + Harmony + "|*.harx|" + PrayerRequests + "|*.prlx" + TopicNotes + "|*.topx|" + VerseLists + "|*.lstx|" + Graphics + "|*.mapx|" + Notes + "|*.notx"; } }
        public static string OpenFileList { get { return AlleSwordModules + "*.bbl;*.brp;*.cmt;*.dct;*.dev;*.map;*.har;*.not;*.mem;*.ovl;*.prl;*.top;*.lst|" + Bibles + "|*.bbl|" + Commentaries + "|*.cmt|" + Dictionaries + "|*.dct|" + Harmonies + "|*.har|" + TopicNotes + "|*.top|" + VerseLists + "|*.lst|" + Graphics + "|*.map|" + Notes + "|*.not"; } }
        public static string SqlErrorString { get { return "{0}\t{1}\t" + Row + ":{2}\t" + Column + ":{3}\t" + Type + ":{4}\t" + Value + ":{5}\t" + Message + ":{6}"; } }


        public static void Initalize()
        {
            Debug.WriteLine("Globalization initalization started");
            Strings = new ThreadSafeDictionary<string, string>();
            LoadLanguage(CultureInfo.CurrentCulture.TwoLetterISOLanguageName);
            Debug.WriteLine("Globalization initalization finished");
        }

        /// <summary>
        /// Attempts to load <paramref name="ISOLanguageName"/> localized strings from an embeded resource
        /// </summary>
        /// <param name="ISOLanguageName">A two letter ISO Language code to try to load</param>
        /// <remarks>If <paramref name="ISOLanguageName"/> cannot be loaded, it loads English as the default</remarks>
        static void LoadLanguage(string ISOLanguageName)
        {
            string fileStream = "eSword9Converter.Strings." + ISOLanguageName + ".txt";
            Debug.WriteLine("Attempting to load localized strings from " + fileStream);
            Stream stream = typeof(CurrentLanguage).Assembly.GetManifestResourceStream(fileStream);
            /* If the stream is null, that language isn't supported load english instead */
            if (stream == null)
            {
                stream = typeof(CurrentLanguage).Assembly.GetManifestResourceStream("eSword9Converter.Strings.en.txt");
                Debug.WriteLine("Loading of " + fileStream + "failed. Falling back on eSword9Converter.Strings.en.txt");
            }
            using (StreamReader SR = new StreamReader(stream, System.Text.Encoding.Default))
            {
                Debug.WriteLine("Reading FileStream one line at a time");
                while (!SR.EndOfStream)
                {
                    string[] dct = Regex.Split(SR.ReadLine(), ":=");
                    Strings[dct[0]] = dct[1];

                }
                Debug.WriteLine("Finished loading localized strings");
                SR.Close();
            }
            stream.Close();
            stream.Dispose();
            /* \r\n is treated as a string litteral when it is loaded as a stream */
            /* We need to transform it to a new line */
            Regex searchTerm = new Regex(@"\\r\\n");
            var query = from KeyValuePair<string, string> kvp in Strings.ToArray()
                        where searchTerm.Matches(kvp.Value).Count > 0
                        select kvp;
            Debug.WriteLineIf(query.Count() > 0, "Creating " + query.Count() + " new lines");
            foreach (KeyValuePair<string, string> kvp in query)
            {
                Strings[kvp.Key] = searchTerm.Replace(kvp.Value, "\r\n");
            }
            Debug.WriteLineIf(query.Count() > 0, "Finished splitting newlines");

            /* Macros can be embeded in the localization file to reduce chances of something strange popping up in translation */
            searchTerm = new Regex(@"\$\{(.+)\}");
            query = from KeyValuePair<string, string> kvp in Strings.ToArray()
                    where searchTerm.Matches(kvp.Value).Count > 0
                    select kvp;
            Debug.WriteLineIf(query.Count() > 0, "Transforming " + query.Count() + " strings");
            foreach (KeyValuePair<string, string> kvp in query)
            {
                Match match = searchTerm.Match(kvp.Value);
                Strings[kvp.Key] = ParseString(kvp.Value);
            }
            Debug.WriteLineIf(query.Count() > 0, "Finished transforming strings");

            /* Finally, let any subscribers know we've changed languages */
            Controller.RaiseLanguageChanged();
        }

        /// <summary>
        /// Takes a string from a globalization file and parses the macros in it
        /// </summary>
        /// <param name="inString">A string containing a globalization macro</param>
        /// <returns>A fully localized string in the currently selected language</returns>
        /// <remarks>A globalization macro can be created by adding $(<value>PropertyName</value>) to a string</remarks>
        static string ParseString(string inString)
        {
            Regex searchTerm = new Regex(@"\$\{(.+)\}");
            MatchCollection mCol = searchTerm.Matches(inString);
            string outString = inString;
            foreach (Match m in mCol)
            {
                outString = outString.Replace(m.Groups[0].Value, Strings[m.Groups[1].Value]);
                outString = ParseString(outString);
            }
            return outString;
        }
    }
}