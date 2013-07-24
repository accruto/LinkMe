using LinkMe.Framework.Utility.Wcf;

namespace LinkMe.Framework.Host.Wcf
{
    public class WcfTcpSource
        : WcfSource<TcpBindingFactory>
    {
        public WcfTcpSource(ServiceDefinition[] serviceDefinitions)
            : base(serviceDefinitions)
        {
        }
    }
}
