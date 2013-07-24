using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Framework.Host.Wcf
{
    public class WcfMsmqSource
        : WcfSource<MsmqBindingFactory>
    {
        public WcfMsmqSource(ServiceDefinition[] serviceDefinitions, IChannelAware application)
            : base(serviceDefinitions, application)
        {
        }

        public WcfMsmqSource(ServiceDefinition[] serviceDefinitions)
            : base(serviceDefinitions)
        {
        }
    }
}
