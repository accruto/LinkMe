using System.ServiceProcess;
using System.Text;
using LinkMe.Environment;
using LinkMe.Framework.Host;

namespace LinkMe.Framework.Environment.Util.Commands
{
    internal static class ServiceInstallation
    {
        public static void Uninstall(string serviceName)
        {
            ServiceUtil.RemoveService(".", serviceName);
        }

        public static void Install(string serviceName, string displayName, string rootFolder, string configurationFile, string account, string password)
        {
            // Determine path to executable.

            var servicePath = EnvironmentFileManager.GetBinFile("LinkMe.Framework.Host.Service.exe", "Framework\\Host");

            // Build the command line common for the service.
            // The format is:
            // "<servicePath>" /service /rootFolder <rootFolder> /configurationFile <configurationFile>

            var commandLine = new StringBuilder();
            commandLine.Append("\"").Append(servicePath).Append("\"");

            commandLine.Append(" /service");
            commandLine.Append(" /serviceName ").Append(serviceName);
            commandLine.Append(" /rootFolder ").Append(rootFolder);
            commandLine.Append(" /configurationFile ").Append(configurationFile);

            var path = commandLine.ToString();

            // Create service.

            ServiceUtil.CreateService(".", serviceName, displayName, "", false, ServiceStartMode.Automatic, path, null, account, password);
        }
    }
}
