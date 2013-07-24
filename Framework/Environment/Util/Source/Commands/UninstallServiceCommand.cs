using LinkMe.Environment.CommandLines;

namespace LinkMe.Framework.Environment.Util.Commands
{
    public class UninstallServiceCommand
        : Command
    {
        public override void Execute()
        {
            var serviceName = Options["serviceName"].Values[0];

            // Uninstall.

            ServiceInstallation.Uninstall(serviceName);
        }
    }
}
