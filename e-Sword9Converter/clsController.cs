using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Threading;

namespace eSword9Converter
{
    public static class Controller
    {
        public static ThreadSafeCollection<FileConversionInfo> FileNames;

        #region Private Members
        private static ThreadSafeCollection<string> passwords;
        private static frmPassword passwordForm;
        private static object threadLock;
        #endregion

        #region Constructor
        static Controller()
        {
            FileNames = new ThreadSafeCollection<FileConversionInfo>();
            passwords = new ThreadSafeCollection<string>();
            passwordForm = new frmPassword();
            threadLock = new object();
            ShowPasswordBoxEvent += new ShowPasswordEventHandler(Controller_GetPasswordEvent);
            if (System.IO.File.Exists("Passwords.txt"))
            {
                StreamReader SR = new StreamReader("Passwords.txt", Encoding.Default);
                List<string> passList = new List<string>();
                while (!SR.EndOfStream)
                {
                    passwords.Add(SR.ReadLine());
                }
            }
        }
        #endregion

        #region Events
        public delegate void StatusChangedEventHandler(updateStatus status);
        public static event StatusChangedEventHandler StatusChangedEvent;
        public static void RaiseStatusChanged(updateStatus status) { if (StatusChangedEvent != null) { StatusChangedEvent(status); } SetMaxValue(100); }

        public delegate void ProgressChangedEventHandler(int count);
        public static event ProgressChangedEventHandler ProgressChangedEvent;
        public static void RaiseProgressChanged(int count) { if (ProgressChangedEvent != null) { ProgressChangedEvent(count); } }

        public delegate void MaxValueChangedEventHandler(int value);
        public static event MaxValueChangedEventHandler MaxValueChangedEvent;
        public static void RaiseMaxValueChanged(int value) { if (MaxValueChangedEvent != null) { MaxValueChangedEvent(value); } }

        public delegate void LogMessageEventHandler(messageType mesageType, string message);
        public static event LogMessageEventHandler LogMessageEvent;
        public static void RaiseLogMessage(messageType type, string message) { if (LogMessageEvent != null) { LogMessageEvent(type, message); } }

        public delegate string ShowPasswordEventHandler(string path, bool tried);
        public static event ShowPasswordEventHandler ShowPasswordBoxEvent;
        public static string RaiseShowPasswordBox(string path, bool tried) { if (ShowPasswordBoxEvent != null) { return ShowPasswordBoxEvent(path, tried); } else { RaiseLogMessage(messageType.Error, "No Subscribers to GetPasswordEvent"); return ""; } }
        #endregion

        #region Event Handlers
        private static string Controller_GetPasswordEvent(string path, bool tried)
        {
            if (passwordForm.InvokeRequired)
            {
                return (string)passwordForm.Invoke(new Controller.ShowPasswordEventHandler(ShowPasswordBox), new object[] { path, tried });
            }
            else
            {
                return ShowPasswordBox(path, tried);
            }
        }

        private static string ShowPasswordBox(string Path, bool tried)
        {
            passwordForm.FileName = Path;
            if (tried)
            { passwordForm.Text = Globalization.CurrentLanguage.InvalidPassword; }
            else { passwordForm.Text = Globalization.CurrentLanguage.Password; }
            passwordForm.TopMost = true;
            passwordForm.StartPosition = FormStartPosition.CenterScreen;
            passwordForm.Activate();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                return passwordForm.Password;
            }
            else
            {
                RaiseLogMessage(messageType.Warning, "Password dialog box closed");
                return "";
            }
        }
        #endregion

        public static void SetMaxValue(int count) { RaiseMaxValueChanged(count); }

        public static bool NeedPassword(string filePath)
        {
            using (OleDbConnection odbcCon = new OleDbConnection())
            {
                try
                {
                    string str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};".Replace("{file}", filePath);
                    odbcCon.ConnectionString = str;
                    odbcCon.Open();
                    odbcCon.Close();
                    return false;
                }
                catch { return true; }
                finally { odbcCon.Close(); }
            }
        }
        private static bool ValidPassword(string filePath, string password)
        {
            using (OleDbConnection odbcCon = new OleDbConnection())
            {
                try
                {
                    string str = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={file};".Replace("{file}", filePath);
                    str = str + "Jet OLEDB:Database Password=\"" + password + "\";";
                    odbcCon.ConnectionString = str;
                    odbcCon.Open();
                    odbcCon.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    RaiseLogMessage(messageType.Information, ex.ToString());
                    return false;
                }
            }
        }

        public static string GetPassword(string path) { return GetPassword(path, false); }
        private static string GetPassword(string path, bool tried) { return GetPassword(path, tried, 0); }
        private static string GetPassword(string path, bool tried, int passCount) { return GetPassword(path, tried, passCount, ""); }
        private static string GetPassword(string path, bool tried, int passCount, string password)
        {
            if (ValidPassword(path, password))
            {
                Error.Log(string.Format(Globalization.CurrentLanguage.PasswordFound, path, password));
                return password;
            }
            try
            {
                string pass = "Password";
                if (passwords.Count == 0)
                { pass = RaiseShowPasswordBox(path, tried); }
                else { pass = passwords[passCount]; }
                return GetPassword(path, tried, passCount + 1, pass);
            }
            catch (Exception ex) { Error.Record("Converter.GetPassword", ex); return password; }
        }

        public static void Begin()
        {
            foreach (FileConversionInfo fci in Controller.FileNames)
            {
                Database DB;
                switch (fci.OldExtension)
                {
                    case "bbl":
                        DB = new Tables.Bible();
                        break;
                    case "brp":
                        DB = new Tables.BibleReadingPlan();
                        break;
                    case "cmt":
                        DB = new Tables.Commentary();
                        break;
                    case "dct":
                        DB = new Tables.Dictionary();
                        break;
                    case "dev":
                        DB = new Tables.Devotion();
                        break;
                    case "map":
                        DB = new Tables.Graphic();
                        break;
                    case "har":
                        DB = new Tables.Harmony();
                        break;
                    case "not":
                        DB = new Tables.Notes();
                        break;
                    case "mem":
                        DB = new Tables.Memory();
                        break;
                    case "ovl":
                        DB = new Tables.Overlay();
                        break;
                    case "prl":
                        DB = new Tables.PrayerRequests();
                        break;
                    case "top":
                        DB = new Tables.Topic();
                        break;
                    case "lst":
                        DB = new Tables.VerseList();
                        break;
                    default:
                        Error.Record("Error!", new Exception());
                        return;
                }
                DB.SourceDB = fci.OldFullPath;
                DB.DestDB = fci.NewFullPath;
                Thread t = new Thread(new ThreadStart(DB.ConvertFormat));
                t.Start();
                while (DB.Running)
                { Application.DoEvents(); }
            }
        }
    }

    public class FileConversionInfo
    {
        public string OldDirectory { get; set; }
        public string NewDirectory { get; set; }
        public string OldFileName { get; set; }
        public string NewFileName { get; set; }
        public string NewFullPath { get; set; }
        public string OldFullPath { get; set; }
        public string OldExtension { get; set; }
        public string NewExtension { get; set; }
        public FileConversionInfo(string oldPath, string newPath)
        {
            this.OldFullPath = oldPath;
            this.NewFullPath = newPath;
            string[] oldpath = oldPath.Split('\\');
            string[] newpath = newPath.Split('\\');
            this.OldFileName = oldpath[oldpath.Length - 1];
            this.NewFileName = newpath[oldpath.Length - 1];
            for (int i = 0; i <= oldpath.Length - 2; i++)
            { this.OldDirectory += oldpath[i] + @"\"; }
            for (int i = 0; i <= newpath.Length - 2; i++)
            { this.NewDirectory += newpath[i] + @"\"; }
            oldpath = oldPath.Split('.');
            newpath = newPath.Split('.');
            this.OldExtension = oldpath[oldpath.Length - 1];
            this.NewExtension = newpath[newpath.Length - 1];

        }
    }
}