using System;
using System.IO;

namespace LinkMe.Environment.Util.Commands
{
    public class GacAllCommand
        : UtilCommand
    {
        public override void Execute()
        {
            var folder = Options["f"].Values[0];
            GacAll(folder);
        }

        public static void GacAll(string folder)
        {
            // Iterate across all assemblies.

            using (GacUtil gac = new GacUtil())
            {
                foreach (string file in Directory.GetFiles(folder))
                {
                    string fileName = Path.GetFileName(file);
                    if (!string.Equals(fileName, EntryAssemblyFileName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Don't attempt to put Sdk or VisualStudio assemblies in the GAC.

                        if (!fileName.Contains("wix") && !fileName.Contains(".Sdk.") && !fileName.Contains(".VisualStudio."))
                        {
                            if (gac.InstallAssemblyFile(file))
                                Console.WriteLine("Installed '" + file + "' into the GAC.");
                        }
                    }
                }
            }
        }
    }
}