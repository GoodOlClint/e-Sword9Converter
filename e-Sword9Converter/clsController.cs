﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;

namespace eSword9Converter
{
    public static class Controller
    {
        public static ThreadSafeCollection<FileConversionInfo> FileNames;
        public static bool AutomaticallyOverwrite { get; set; }

        #region Private Members
        private static ThreadSafeCollection<string> passwords;
        private static frmPassword passwordForm;
        private static object threadLock;
        private static SynchronizationContext sync;
        public static Database DB;
        public static Form CurrentForm;
        #endregion

        #region Constructor
        static Controller()
        {
            sync = SynchronizationContext.Current;
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
        public delegate void StatusChangedEventHandler(object sender, updateStatus status);
        public static event StatusChangedEventHandler StatusChangedEvent;
        public static void RaiseStatusChanged(object sender, updateStatus status)
        {
            if (StatusChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { StatusChangedEvent(sender, status); }), null); }
            SetMaxValue("Controller.RaiseStatusChangedEvent", 100);
        }

        public delegate void ProgressChangedEventHandler(object sender, int count);
        public static event ProgressChangedEventHandler ProgressChangedEvent;
        public static void RaiseProgressChanged(object sender, int count)
        {
            if (ProgressChangedEvent != null)
            { sync.Post(new SendOrPostCallback(delegate { ProgressChangedEvent(sender, count); }), null); }
        }

        public delegate void MaxValueChangedEventHandler(object sender, int value);
        public static event MaxValueChangedEventHandler MaxValueChangedEvent;
        public static void RaiseMaxValueChanged(object sender, int value)
        {
            if (MaxValueChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { MaxValueChangedEvent(sender, value); }), null); }
        }

        public delegate void LogMessageEventHandler(object sender, messageType mesageType, string message);
        public static event LogMessageEventHandler LogMessageEvent;
        public static void RaiseLogMessage(object sender, messageType type, string message)
        {
            if (LogMessageEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { LogMessageEvent(sender, type, message); }), null); }
        }

        public delegate void ShowPasswordEventHandler(object sender, string path, bool tried, out string pass);
        public static event ShowPasswordEventHandler ShowPasswordBoxEvent;
        public static void RaiseShowPasswordBox(object sender, string path, bool tried, out string pass)
        {
            if (ShowPasswordBoxEvent != null)
            {
                string p = "";
                sync.Send(new SendOrPostCallback(delegate { ShowPasswordBoxEvent(sender, path, tried, out p); }), null);
                pass = p;
            }
            else
            { RaiseLogMessage(sender, messageType.Error, "No Subscribers to GetPasswordEvent"); pass = ""; }
        }
        #endregion

        #region Event Handlers
        private static void Controller_GetPasswordEvent(object sender, string path, bool tried, out string pass)
        { ShowPasswordBox(sender, path, tried, out pass); }

        private static void ShowPasswordBox(object sender, string Path, bool tried, out string pass)
        {
            //passwordForm.Parent = Controller.CurrentForm;
            Controller.CurrentForm.AddOwnedForm(passwordForm);
            passwordForm.FileName = Path;
            if (tried)
            { passwordForm.Text = Globalization.CurrentLanguage.InvalidPassword; }
            else { passwordForm.Text = Globalization.CurrentLanguage.Password; }
            //passwordForm.TopMost = true;
            //passwordForm.StartPosition = FormStartPosition.CenterScreen;
            passwordForm.Activate();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                pass = passwordForm.Password;
            }
            else
            {
                RaiseLogMessage(sender, messageType.Warning, "Password dialog box closed");
                pass = "";
            }
        }
        #endregion

        public static DialogResult ShowMessageBox(string Message, string Title, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        {
            //DialogResult results = DialogResult.None;
            //SendOrPostCallback spc = new SendOrPostCallback(delegate {results = MessageBox.Show(Message, Title, Buttons, Icon); });
            //object state = new object();
            //sync.Send(spc, state);
            //return results;
            return MessageBox.Show(Message, Title, Buttons, Icon);
        }

        public static void SetMaxValue(object sender, int count) { RaiseMaxValueChanged(sender, count); }

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
                    RaiseLogMessage("Controller.ValidPassword", messageType.Information, ex.ToString());
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
                RaiseLogMessage("Controller.GetPassword", messageType.Information, string.Format(Globalization.CurrentLanguage.PasswordFound, path, password));
                return password;
            }
            try
            {
                string pass = "Password";
                if (passwords.Count == 0)
                {
                    RaiseShowPasswordBox("Controller.GetPassword", path, tried, out pass);
                    if (pass == "") { DB.Stop(); return ""; }
                }
                else { pass = passwords[passCount]; }
                return GetPassword(path, tried, passCount + 1, pass);
            }
            catch (Exception ex) { RaiseLogMessage("Controller.GetPassword", messageType.Error, ex.Message); return password; }
        }

        public static void Begin()
        {
            Thread T = new Thread(new ThreadStart(Controller.Process));
            T.Start();
        }


        public static void Process()
        {
            foreach (FileConversionInfo fci in Controller.FileNames)
            {
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
                        RaiseLogMessage("Controller.Begin", messageType.Error, new InvalidDataException("Invalid filetype added to collection").Message);
                        return;
                }
                DB.SourceDB = fci.OldFullPath;
                if (File.Exists(fci.NewFullPath) && !AutomaticallyOverwrite)
                {
                    if (ShowMessageBox(Globalization.CurrentLanguage.FileExists, "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        break;
                    }
                }
                DB.DestDB = fci.NewFullPath;
                Thread t = new Thread(new ThreadStart(DB.ConvertFormat));
                t.Start();
                while (DB.Running)
                { }//Application.DoEvents(); }
            }
            Controller.FileNames.Clear();
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