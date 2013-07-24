using System;

namespace LinkMe.Environment.Util.Commands
{
    public class UngacNameCommand
        : UtilCommand
    {
        public override void Execute()
        {
            var assemblyName = Options["n"].Values[0];

            using (GacUtil gac = new GacUtil())
            {
                if (gac.UninstallAssembly(assemblyName))
                    Console.WriteLine("Uninstalled '" + assemblyName + "' from the GAC.");
            }
        }
    }
}