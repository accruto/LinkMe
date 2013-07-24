using System.Diagnostics;
using LinkMe.Environment.CommandLines;

namespace LinkMe.Framework.Host.Service.Commands
{
    public abstract class RunCommand
        : Command
    {
        public override void Execute()
        {
            try
            {
                string serviceName = null;
                string applicationRootFolder = null;
                string configurationFile = null;

                var option = Options[Constants.Config.Options.ServiceName];
                if (option != null && option.IsValueSupplied)
                    serviceName = option.Values[0];

                option = Options[Constants.Config.Options.ApplicationRootFolder];
                if (option != null && option.IsValueSupplied)
                    applicationRootFolder = option.Values[0];

                option = Options[Constants.Config.Options.ConfigurationFile];
                if (option != null && option.IsValueSupplied)
                    configurationFile = option.Values[0];

                // Run now.

                Run(serviceName, applicationRootFolder, configurationFile);
            }
            catch (System.Exception ex)
            {
                EventLog.WriteEntry("LinkMe.Framework.Host.Service", string.Format("Unable to start the service. {0}", ex.Message), EventLogEntryType.Error);
            }
        }

        protected abstract void Run(string serviceName, string applicationRootFolder, string configurationFile);
    }
}
