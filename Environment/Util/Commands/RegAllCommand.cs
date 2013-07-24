using System;
using System.IO;

namespace LinkMe.Environment.Util.Commands
{
    public class RegAllCommand
        : RegCommand
    {
        public override void Execute()
        {
            var folder = Options["f"].Values[0];
            RegAll(folder);
        }

        public static void RegAll(string folder)
        {
            // Iterate across all assemblies.

            foreach (string file in Directory.GetFiles(folder))
            {
                if (!string.Equals(Path.GetFileName(file), EntryAssemblyFileName, StringComparison.OrdinalIgnoreCase))
                {
                    if (Register(file))
                        Console.WriteLine("Registered '" + file + "'.");
                }
            }
        }
    }
}