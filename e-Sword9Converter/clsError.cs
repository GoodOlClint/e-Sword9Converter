using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace e_Sword9Converter
{
    public class Error
    {
        private static object threadLock = new object();
        public static void Record(object sender, Exception ex)
        {
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter("e-Sword9Converter.log"))
                {
                    try
                    { sw.WriteLine(string.Format("Error: {0} {1}", ((Database)sender).DestDB, ex.Message)); }
                    catch
                    { sw.WriteLine("Error: " + ex.Message); }
                }
            }
        }
        public static void Log(string message)
        {
            lock (threadLock)
            {
                using (StreamWriter sw = new StreamWriter("e-Sword9Converter.log"))
                { sw.WriteLine("Warning: " + message); }
            }
        }
    }
}
