using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AspMinifier
{
    class Utilities
    {
        public static void getFiles(string Path, List<string> files, string type)
        {
            string[] filenames = System.IO.Directory.GetFiles(Path, type, SearchOption.AllDirectories);
            foreach (string file in filenames)
                files.Add(file);
        }
    }
}
