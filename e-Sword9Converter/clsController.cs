using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using eSword9Converter.Globalization;
namespace eSword9Converter
{
    public static class Controller
    {
        public static ThreadSafeCollection<FileConversionInfo> FileNames;
        public static bool AutomaticallyOverwrite { get; set; }
        public static bool SkipPassword { get; set; }
        public static Database DB { get; set; }
        public static Form CurrentForm { get; set; }

        #region Private Members
        private static ThreadSafeCollection<string> passwords;
        private static object threadLock;
        private static SynchronizationContext sync;
        private static frmMain MainForm;
        private static frmAdvanced AdvancedForm;
        private static frmPassword passwordForm;
        private static bool Stop;
        #endregion

        #region Events
        public delegate void LanguageChangedEventHandler();
        public static event LanguageChangedEventHandler LanguageChangedEvent;
        public static void RaiseLanguageChanged()
        {
            Debug.WriteLine("RaiseLanguageChanged called");
            if (LanguageChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { LanguageChangedEvent(); }), null); }
        }

        public delegate void StatusChangedEventHandler(object sender, updateStatus status);
        public static event StatusChangedEventHandler StatusChangedEvent;
        public static void RaiseStatusChanged(object sender, updateStatus status)
        {
            Debug.WriteLine(string.Format("RaiseStatusChanged({0}, {1}) called", sender.ToString(), status.ToString()));
            if (StatusChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { StatusChangedEvent(sender, status); }), null); }
        }

        public delegate void ProgressChangedEventHandler(object sender, int count);
        public static event ProgressChangedEventHandler ProgressChangedEvent;
        public static void RaiseProgressChanged(object sender, int count)
        {
            //Debug.WriteLine("RaiseProgressChanged called");
            if (ProgressChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { ProgressChangedEvent(sender, count); }), null); }
        }

        public delegate void MaxValueChangedEventHandler(object sender, int value);
        public static event MaxValueChangedEventHandler MaxValueChangedEvent;
        public static void RaiseMaxValueChanged(object sender, int value)
        {
            Debug.WriteLine(string.Format("RaiseMaxValueChanged({0}, {1}) called", sender.ToString(), value));
            if (MaxValueChangedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { MaxValueChangedEvent(sender, value); }), null); }
        }

        //public delegate void LogMessageEventHandler(object sender, messageType mesageType, string message);
        //public static event LogMessageEventHandler LogMessageEvent;
        //public static void RaiseLogMessage(object sender, messageType type, string message)
        //{
        //    Debug.WriteLine(string.Format("RaiseLogMessage({0}, {1}, {2}) called", sender.ToString(), type.ToString(), message));
        //    if (LogMessageEvent != null)
        //    { sync.Send(new SendOrPostCallback(delegate { LogMessageEvent(sender, type, message); }), null); }
        //}

        public delegate void ShowPasswordEventHandler(object sender, string path, bool tried, out string pass);
        public static event ShowPasswordEventHandler ShowPasswordBoxEvent;
        public static void RaiseShowPasswordBox(object sender, string path, bool tried, out string pass)
        {
            Debug.WriteLine(string.Format("RaiseShowPasswordBox({0}, {1}, {2}) called", sender.ToString(), path, tried));
            if (ShowPasswordBoxEvent != null)
            {
                string p = "";
                sync.Send(new SendOrPostCallback(delegate { ShowPasswordBoxEvent(sender, path, tried, out p); }), null);
                pass = p;
            }
            else
            { Trace.WriteLine("No Subscribers to GetPasswordEvent"); pass = ""; }
        }

        public delegate void ConversionFinishedEventHandler();
        public static event ConversionFinishedEventHandler ConversionFinishedEvent;
        public static void RaiseConversionFinished()
        {
            Debug.WriteLine("RaiseConversionFinished called");
            if (ConversionFinishedEvent != null)
            { sync.Send(new SendOrPostCallback(delegate { ConversionFinishedEvent(); }), null); }
        }
        #endregion

        #region Event Handlers

        static void SubForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Debug.WriteLine(CurrentForm.GetType().ToString() + " Closed");
            if (DB != null)
                DB.Stop();
            if (!switching)
            { Stop = true; Application.Exit(); }
        }

        private static void Controller_GetPasswordEvent(object sender, string path, bool tried, out string pass)
        { ShowPasswordBox(sender, path, tried, out pass); }

        private static void ShowPasswordBox(object sender, string Path, bool tried, out string pass)
        {
            Controller.CurrentForm.AddOwnedForm(passwordForm);
            passwordForm.FileName = Path;
            if (tried)
            { passwordForm.Text = Globalization.CurrentLanguage.InvalidPassword; }
            else { passwordForm.Text = Globalization.CurrentLanguage.Password; }
            passwordForm.Activate();
            if (passwordForm.ShowDialog() == DialogResult.OK)
            {
                pass = passwordForm.Password;
            }
            else
            {
                Trace.WriteLine("Password dialog box closed");
                pass = "";
            }
        }
        #endregion

        public static void Initalize()
        {
            try
            {
                CurrentLanguage.Initalize();
                Debug.WriteLine("Initalizing Controller");
                FileNames = new ThreadSafeCollection<FileConversionInfo>();
                passwords = new ThreadSafeCollection<string>();
                passwordForm = new frmPassword();
                MainForm = new frmMain();
                threadLock = new object();
                sync = SynchronizationContext.Current;
                if (System.IO.File.Exists("Passwords.txt"))
                {
                    Debug.WriteLine("Passwords.txt found, loading passwords");
                    StreamReader SR = new StreamReader("Passwords.txt", Encoding.Default);
                    while (!SR.EndOfStream)
                    { passwords.Add(SR.ReadLine()); }
                    Debug.WriteLine("Loaded " + passwords.Count + " passwords");
                }
                ShowPasswordBoxEvent += new ShowPasswordEventHandler(Controller_GetPasswordEvent);
                MainForm.FormClosed += new FormClosedEventHandler(SubForm_FormClosed);
                MainForm.Show();
                CurrentForm = MainForm;
                Debug.WriteLine("Initalizing Controller Finished");
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }

        private static bool switching;
        public static void SwitchForms()
        {
            Debug.WriteLine("Switching forms");
            switching = true;
            try
            {
                if (CurrentForm == MainForm)
                {
                    Debug.WriteLine("Switching from MainForm to AdvancedForm");
                    AdvancedForm = new frmAdvanced();
                    AdvancedForm.FormClosed += new FormClosedEventHandler(SubForm_FormClosed);
                    MainForm.Close();
                    AdvancedForm.Show();
                    CurrentForm = AdvancedForm;
                }
                else
                {
                    Debug.WriteLine("Switching from AdvancedForm to MainForm");
                    MainForm = new frmMain();
                    MainForm.FormClosed += new FormClosedEventHandler(SubForm_FormClosed);
                    AdvancedForm.Close();
                    MainForm.Show();
                    CurrentForm = MainForm;
                }
            }
            finally { switching = false; }
        }

        public static DialogResult ShowMessageBox(string Message, string Title, MessageBoxButtons Buttons, MessageBoxIcon Icon)
        { return MessageBox.Show(Message, Title, Buttons, Icon); }

        public static void SetMaxValue(object sender, int count) { RaiseMaxValueChanged(sender, count); }

        public static bool NeedPassword(string filePath)
        {
            Debug.WriteLine("Checking if " + filePath + " needs a password");
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
                { Trace.WriteLine(ex); return false; }
            }
        }

        public static string GetPassword(string path) { return GetPassword(path, false); }
        private static string GetPassword(string path, bool tried) { return GetPassword(path, tried, 0); }
        private static string GetPassword(string path, bool tried, int passCount) { return GetPassword(path, tried, passCount, ""); }
        private static string GetPassword(string path, bool tried, int passCount, string password)
        {
            if (SkipPassword) { return ""; }
            if (ValidPassword(path, password))
            {
                Trace.WriteLine(string.Format(Globalization.CurrentLanguage.PasswordFound, path, password));
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
                return GetPassword(path, true, passCount + 1, pass);
            }
            catch (Exception ex) { Trace.WriteLine(ex); return password; }
        }

        public static void Begin()
        {
            try
            {
                Thread T = new Thread(new ThreadStart(Controller.Process));
                T.Start();
            }
            catch (Exception ex) { Trace.WriteLine(ex); }
        }


        public static void Process()
        {
            Debug.WriteLine("Controller.Process begining");
            try
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
                            Trace.WriteLine(new InvalidDataException("Invalid filetype " + fci.OldExtension + " added to collection"));
                            return;
                    }
                    Debug.WriteLine(string.Format("{0} selected", DB.ToString()));
                    DB.SourceDB = fci.OldFullPath;
                    Debug.WriteLine(string.Format("Source File is {0}", DB.SourceDB));
                    bool needPassword = NeedPassword(fci.OldFullPath);
                    Debug.WriteLine(string.Format("Source File Needs Password: {0}", needPassword));
                    if ((needPassword && !SkipPassword) || !needPassword)
                    {
                        Debug.WriteLine(string.Format("Need Password: {0} SkipPassword: {1}", needPassword, SkipPassword));
                        bool exist = File.Exists(fci.NewFullPath);
                        Debug.WriteLine("Destination file already exists: " + exist);
                        if ((!exist) || (exist && AutomaticallyOverwrite) || (exist && (ShowMessageBox(string.Format(Globalization.CurrentLanguage.Overwrite, fci.NewFullPath), Globalization.CurrentLanguage.FileExists, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)))
                        {
                            DB.DestDB = fci.NewFullPath;
                            Debug.WriteLine("Destination File is " + DB.DestDB);
                            Debug.WriteLine("Begining conversion");
                            DB.ConvertFormat();
                            Debug.WriteLine("Conversion finished");
                            DB.Clear();
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Skipping file " + DB.SourceDB);
                    }
                    if (Stop)
                    {
                        Debug.WriteLine("Exiting batch conversion");
                        break;
                    }
                }
                Controller.FileNames.Clear();
                RaiseConversionFinished();
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
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
            try
            {
                Debug.WriteLine("Creating new FileConversionInfo object");
                this.OldFullPath = oldPath;
                this.NewFullPath = newPath;
                string[] oldpath = oldPath.Split('\\');
                string[] newpath = newPath.Split('\\');
                this.OldFileName = oldpath[oldpath.Length - 1];
                this.NewFileName = newpath[newpath.Length - 1];
                for (int i = 0; i <= oldpath.Length - 2; i++)
                { this.OldDirectory += oldpath[i] + @"\"; }
                for (int i = 0; i <= newpath.Length - 2; i++)
                { this.NewDirectory += newpath[i] + @"\"; }
                oldpath = oldPath.Split('.');
                newpath = newPath.Split('.');
                this.OldExtension = oldpath[oldpath.Length - 1];
                this.NewExtension = newpath[newpath.Length - 1];
                Debug.Write(this);
                Debug.WriteLine("Creating new FileConversionInfo object Finished");
            }
            catch (Exception ex)
            { Trace.WriteLine(ex); }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().ToString());
            sb.AppendLine("\t OldFullPath: " + this.OldFullPath);
            sb.AppendLine("\t NewFullPath: " + this.NewFullPath);
            sb.AppendLine("\t OldFileName: " + this.OldFileName);
            sb.AppendLine("\t NewFileName: " + this.NewFileName);
            sb.AppendLine("\t OldDirectory: " + this.OldDirectory);
            sb.AppendLine("\t NewDirectory: " + this.NewDirectory);
            sb.AppendLine("\t OldExtension: " + this.OldExtension);
            sb.AppendLine("\t NewExtension: " + this.NewExtension);
            return sb.ToString();
        }
    }
}