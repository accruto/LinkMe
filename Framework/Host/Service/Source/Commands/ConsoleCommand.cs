namespace LinkMe.Framework.Host.Service.Commands
{
    public class ConsoleCommand
        : RunCommand
    {
        protected override void Run(string serviceName, string applicationRootFolder, string configurationFile)
        {
            // Only the first container is used.

            var form = new ServiceForm(new ServiceParameters(serviceName, applicationRootFolder, configurationFile));
            form.ShowDialog();
        }
    }
}
