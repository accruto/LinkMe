using System;

namespace LinkMe.Environment.Util.Commands
{
    public class UngacCommand
        : UtilCommand
    {
        public override void Execute()
        {
            var assembly = Options["i"].Values[0];

            // Iterate across all assemblies.

            using (GacUtil gac = new GacUtil())
            {
                if (gac.UninstallAssemblyFile(assembly))
                    Console.WriteLine("Uninstalled '" + assembly + "' from the GAC.");
            }
        }
    }
}