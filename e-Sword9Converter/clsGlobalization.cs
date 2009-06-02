using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Diagnostics;

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
        public static string InvalidPassword { get { return Strings["InvalidPassword"]; } }
        public static string Loading { get { return Strings["Loading"]; } }
        public static string MainTitle { get { return Strings["MainTitle"]; } }
        public static string MemoryVerses { get { return Strings["MemoryVerses"]; } }
        public static string Message { get { return Strings["Message"]; } }
        public static string Normal { get { return Strings["Normal"]; } }
        public static string Notes { get { return Strings["Notes"]; } }
        public static string Ok { get { return Strings["Ok"]; } }
        public static string Overlay { get { return Strings["Overlay"]; } }
        public static string Overwrite { get { return Strings["Overwrite"]; } }
        public static string Password { get { return Strings["Password"]; } }
        public static string PasswordBlank { get { return Strings["PasswordBlank"]; } }
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
            string fileStream = "eSword9Converter.Strings." + CultureInfo.CurrentCulture.TwoLetterISOLanguageName + ".txt";
            Debug.WriteLine("Attempting to load localized strings from " + fileStream);
            Stream stream = typeof(CurrentLanguage).Assembly.GetManifestResourceStream(fileStream);

            if (stream == null)
            {
                stream = typeof(CurrentLanguage).Assembly.GetManifestResourceStream("eSword9Converter.Strings.en.txt");
                Debug.WriteLine("Loading of " + fileStream + "failed. Falling back on eSword9Converter.Strings.en.txt");
            }

            LoadLanguage(stream);
            stream.Close();
            stream.Dispose();
        }

        static void LoadLanguage(Stream FileStream)
        {
            using (StreamReader SR = new StreamReader(FileStream, System.Text.Encoding.Default))
            {
                Debug.WriteLine("Reading FileStream one line at a time");
                while (!SR.EndOfStream)
                {
                    string[] dct = Regex.Split(SR.ReadLine(), ":=");
                    Strings[dct[0]] = dct[1];

                }
                Debug.WriteLine("Finished");
                SR.Close();
            }
            Regex searchTerm = new Regex(@"\\r\\n");
            var query = from KeyValuePair<string, string> kvp in Strings.ToArray()
                        where searchTerm.Matches(kvp.Value).Count > 0
                        select kvp;
            Debug.WriteLineIf(query.Count() > 0, "Creating " + query.Count() + " new lines");
            foreach (KeyValuePair<string, string> kvp in query)
            {
                Strings[kvp.Key] = searchTerm.Replace(kvp.Value, "\r\n");
            }
            Debug.WriteLineIf(query.Count() > 0, "Finished");
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
            Debug.WriteLineIf(query.Count() > 0, "Finished");
            Controller.RaiseLanguageChanged();
        }

        static string ParseString(string inString)
        {
            Regex searchTerm = new Regex(@"\$\{(.+)\}");
            Match m = searchTerm.Match(inString);
            if (m.Length > 0)
            {
                string outString = inString.Replace(m.Groups[0].Value, Strings[m.Groups[1].Value]);
                return ParseString(outString);
            }
            else
            { return inString; }
        }
    }
}