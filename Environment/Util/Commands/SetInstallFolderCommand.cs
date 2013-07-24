using System;

namespace LinkMe.Environment.Util.Commands
{
    public abstract class SetInstallFolderCommand
        : UtilCommand
    {
        protected abstract EnvironmentType EnvironmentType { get; }

        public override void Execute()
        {
            // Use either the value passed in or determine it based on this assembly's location.

            string installFolder;
            var options = Options["if"];
            if (options != null)
                installFolder = options.Values[0];
            else
                installFolder = GetInstallFolder();

            SetInstallFolder(installFolder, EnvironmentType);
        }

        public static void SetInstallFolder(string installFolder, EnvironmentType environmentType)
        {
            // Set the environment values for the product itself.

            StaticEnvironment.SetInstallFolder(installFolder);
            StaticEnvironment.SetInstallType(environmentType);

            // Set the environment values for the SDK as well.

            StaticEnvironment.SetInstallFolder(Environment.Constants.Product.Sdk, installFolder);
            StaticEnvironment.SetInstallType(Environment.Constants.Product.Sdk, environmentType);

            Console.WriteLine("Install folder set to '" + installFolder + "'.");
        }
    }
}