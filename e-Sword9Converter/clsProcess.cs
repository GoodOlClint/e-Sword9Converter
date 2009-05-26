using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;
using System.Collections;
namespace e_Sword9Converter
{
    public static class Converter
    {
        public static ThreadSafeCollection<string> FileNames;
        private static frmPassword passwordForm;
        private static object threadLock;
        private static updateStatus status;

        static Converter()
        {
            FileNames = new ThreadSafeCollection<string>();
            passwordForm = new frmPassword();
            threadLock = new object();
        }

        public static updateStatus Status { get { lock (threadLock) { return status; } } set { lock (threadLock) { status = value; } } }

        private static bool NeedPassword(string filePath)
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
                    Error.Log(Globalization.CurrentLanguage.Password);
                    return false;
                }
            }
        }
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
                passwordForm.FileName = path;
                if (tried)
                { passwordForm.Text = Globalization.CurrentLanguage.InvalidPassword; }
                else { passwordForm.Text = Globalization.CurrentLanguage.Password; }
                string pass = "Password";
                if (!System.IO.File.Exists("Passwords.txt"))
                {
                    if (passwordForm.ShowDialog() == DialogResult.OK)
                    {
                        pass = passwordForm.Password;
                        tried = true;
                    }
                    else { return ""; }
                }
                else
                {
                    StreamReader SR = new StreamReader("Passwords.txt", Encoding.Default);
                    List<string> passList = new List<string>();
                    while (!SR.EndOfStream)
                    {
                        passList.Add(SR.ReadLine());
                    }
                    if (passCount >= passList.ToArray().Length)
                    {
                        if (passwordForm.ShowDialog() == DialogResult.OK)
                        {
                            pass = passwordForm.Password;
                            tried = true;
                        }
                        else { return ""; }
                    }
                    else
                    {
                        pass = passList[passCount];
                    }
                }
                return GetPassword(path, tried, passCount + 1, pass);
            }
            catch (Exception ex) { Error.Record("Converter.GetPassword", ex); return password; }
        }
    }
}