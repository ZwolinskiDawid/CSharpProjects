using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    static class Extensions
    {
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
