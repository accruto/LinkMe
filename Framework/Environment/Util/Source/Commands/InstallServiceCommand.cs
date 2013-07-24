using LinkMe.Environment.CommandLines;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class InstallServiceCommand
        : Command
    {
        public override void Execute()
        {
            var serviceName = Options["serviceName"].Values[0];
            var displayName = Options["displayName"].Values[0];
            var rootFolder = Options["rootFolder"].Values[0];
            var configurationFile = Options["configurationFile"].Values[0];
            var account = Options["account"].Values.Count > 0 ? Options["account"].Values[0] : null;
            var password = Options["password"].Values.Count > 0 ? Options["password"].Values[0] : null;

            // Uninstall first.

            ServiceInstallation.Uninstall(serviceName);
            ServiceInstallation.Install(serviceName, displayName, rootFolder, configurationFile, account, password);
        }
    }
}
