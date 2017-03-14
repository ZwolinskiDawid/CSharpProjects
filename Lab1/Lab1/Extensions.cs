using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    static class Extensions
    {
        public static DateTime getTheOldestDate(this DirectoryInfo di)
        {
            DateTime theOdlestDate = di.CreationTime;

            foreach (FileSystemInfo fsi in di.GetFileSystemInfos())
            {
                DateTime tmp;

                if (fsi is DirectoryInfo)
                {
                    tmp = ((DirectoryInfo)fsi).getTheOldestDate();
                }
                else
                {
                    tmp = fsi.CreationTime;
                }

                if (theOdlestDate.CompareTo(tmp) > 0)
                {
                    theOdlestDate = tmp;
                }
            }

            return theOdlestDate;
        }

        public static string getAttributes(this FileSystemInfo fsi)
        {
            string attr = "";

            attr += ((fsi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) ? "r" : "-";
            attr += ((fsi.Attributes & FileAttributes.Archive) == FileAttributes.Archive) ? "a" : "-";
            attr += ((fsi.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden) ? "h" : "-";
            attr += ((fsi.Attributes & FileAttributes.System) == FileAttributes.System) ? "s" : "-";

            return attr;
        }
    }
}
