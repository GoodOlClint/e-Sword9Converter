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
        private static TextWriter Writer = new StreamWriter("error.log");
        public static void Record(object sender, Exception ex)
        {
            lock (threadLock)
            {
                try
                {
                    Database db = (Database)sender;
                    Writer.WriteLine(string.Format("{0} {1}", db.DestDB, ex.Message));
                    Writer.Flush();
                }
                catch
                {
                    try
                    {
                        ITable table = (ITable)sender;
                        Writer.WriteLine(string.Format("{0} {1}", table.TableName, ex.Message));
                        Writer.Flush();
                    }
                    catch
                    {
                        Writer.WriteLine(ex.Message);
                        Writer.Flush();
                    }
                }
            }
        }
    }
}
