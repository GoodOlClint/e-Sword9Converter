using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace e_Sword9Converter
{
    public class Error
    {
        public static void Record(object sender, Exception ex)
        {
            System.Diagnostics.Trace.WriteLine(string.Format("{0} sent error {1}", sender.ToString(), ex.Message));
        }
    }
}
