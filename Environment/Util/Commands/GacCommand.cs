using System;

namespace LinkMe.Environment.Util.Commands
{
    public class GacCommand
        : UtilCommand
    {
        public override void Execute()
        {
            var assembly = Options["i"].Values[0];

            // Iterate across all assemblies.

            using (GacUtil gac = new GacUtil())
            {
                if (gac.InstallAssemblyFile(assembly))
                    Console.WriteLine("Installed '" + assembly + "' into the GAC.");
            }
        }
    }
}