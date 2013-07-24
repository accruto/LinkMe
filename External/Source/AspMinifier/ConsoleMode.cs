using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AspMinifier
{
    class ConsoleMode
    {
        private string _dir;
        private int _saved = 0;
        public ConsoleMode(string dir)
        {
            _dir = dir;
            if (!Directory.Exists(dir))
            {
                Console.WriteLine("Directory not found: {0}", dir);
                return;
            }

//            execute_compression("*.js");
//            execute_compression("*.css");
            execute_compression("*.aspx", "*.master", "*.ascx", "*.html", "*.htm");

            Console.WriteLine();
            Console.WriteLine("All done! You saved {0:0,0} bytes.", _saved);
        }

        void execute_compression(params string[] extensions)
        {
            List<String> files = new List<string>();

            Console.WriteLine("Collecting all " + String.Join(" ", extensions) + " files");

            foreach (string ext in extensions)
                Utilities.getFiles(_dir, files, ext);

            Console.WriteLine("Minifying " + files.Count + " " + String.Join(" ", extensions) + " files");

            Minifier mini = new Minifier() { Backup = false };
            int count = 0;
            foreach (string file in files)
            {
                _saved += mini.Minify(file);
                Console.WriteLine("Minifying {0}/{1}...", ++count, files.Count);
            }
            Console.WriteLine("Finished Minifying all " + String.Join(" ", extensions) + " files");
        }
    }
}
