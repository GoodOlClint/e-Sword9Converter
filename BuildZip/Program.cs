using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace BuildZip
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string fileName = args[0];
                List<string> filesToZip = new List<string>();
                for (int i = 1; i <= args.Length - 1; i++)
                { filesToZip.Add(args[i]); }

                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(filesToZip[0]);
                string version = "{0}.{1}.{2}.{3}";
                version = string.Format(version, fvi.FileMajorPart, fvi.FileMinorPart, fvi.FileBuildPart, fvi.FilePrivatePart);
                fileName = fileName + "-" + version + ".zip";

                // 'using' statements gaurantee the stream is closed properly which is a big source
                // of problems otherwise.  Its exception safe as well which is great.
                using (ZipOutputStream s = new ZipOutputStream(File.Create(fileName)))
                {

                    s.SetLevel(9); // 0 - store only to 9 - means best compression

                    byte[] buffer = new byte[4096];

                    foreach (string file in filesToZip)
                    {
                        // Using GetFileName makes the result compatible with XP
                        // as the resulting path is not absolute.
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

                        // Setup the entry data as required.

                        // Crc and size are handled by the library for seakable streams
                        // so no need to do them here.

                        // Could also use the last write time or similar for the file.
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            // Using a fixed size buffer here makes no noticeable difference for output
                            // but keeps a lid on memory usage.
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    // Finish/Close arent needed strictly as the using statement does this automatically

                    // Finish is important to ensure trailing information for a Zip file is appended.  Without this
                    // the created file would be invalid.
                    s.Finish();

                    // Close is important to wrap things up and unlock the file.
                    s.Close();
                }
                Console.WriteLine("Created zip {0}", fileName);
            }
            catch (Exception ex)
            { Console.WriteLine(ex.Message); }
        }
    }
}
