using System;
using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Instrumentation.Adaptors;
using LinkMe.Framework.Utility.Unity;
using NServiceBus;

namespace LinkMe.Apps.Search.Members
{
    public class EndpointConfig
        : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization, IWantCustomLogging
    {
        public void Init()
        {
            // Configure NServiceBus.

            var logAppender = new Log4NetAppender(typeof(EndpointConfig));

            NServiceBus.SetLoggingLibrary.Log4Net(null, logAppender);

            NServiceBus.Configure.With()
                .Log4Net(logAppender)
                .UnityBuilder(Container.Current)
                .BinarySerializer();

            // Create and initialize WCF host.

            var host = Container.Current.Resolve<WcfTcpHost>();
            Container.Current.RegisterInstance(host); // force instance to be a singleton
            host.Open();
        }
    }
}
