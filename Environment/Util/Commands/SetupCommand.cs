using System;

namespace LinkMe.Environment.Util.Commands
{
    public class SetupCommand
        : UtilCommand
    {
        public override void Execute()
        {
            // Equivalent to:
            //   LinkMe.Framework.Util.exe /gacall
            //   LinkMe.Framework.Util.exe /regall
            //   LinkMe.Framework.Util.exe /setprodinstallfolder

            Console.WriteLine("Putting all assemblies into the GAC.");
            GacAllCommand.GacAll(GetAssemblyFolder());

            Console.WriteLine("Registering all dlls.");
            RegAllCommand.RegAll(GetAssemblyFolder());

            Console.WriteLine("Setting the install folder.");
            SetInstallFolderCommand.SetInstallFolder(GetInstallFolder(), EnvironmentType.Production);
        }
    }
}