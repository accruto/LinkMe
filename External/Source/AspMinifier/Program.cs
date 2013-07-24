using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace AspMinifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length == 1)
            {
                Console.WriteLine("Running windows form.");
                Console.WriteLine("To executing with command prompt, pass the folder path as an argument.");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else if (args.Length == 2)
            {
                new ConsoleMode(args[1]);
            }
        }
    }
}
