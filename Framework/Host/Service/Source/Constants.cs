namespace LinkMe.Framework.Host.Service.Constants
{
    internal static class Config
    {
        internal const string UtilSection = LinkMe.Framework.Configuration.Constants.Config.SectionElement + "/host.service";

        internal static class Options
        {
            internal const string ServiceName = "serviceName";
            internal const string ApplicationRootFolder = "rootFolder";
            internal const string ConfigurationFile = "configurationFile";
        }
    }
}
