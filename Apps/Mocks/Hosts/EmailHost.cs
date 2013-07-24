using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Mocks.Hosts
{
    public static class EmailHost
    {
        private static WcfTcpHost _host;

        public static IMockEmailServer Start()
        {
            if (_host != null)
                return ((MockEmailService)_host.ServiceDefinitions[0].Service).EmailServer;

            var service = Container.Current.Resolve<MockEmailService>();
            service.EmailServer = new MockEmailServer();
            Container.Current.RegisterInstance<IMockEmailService>(service);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Container.Current.Resolve<string>("linkme.email.tcpAddress"),
                BindingName = "linkme.email.tcp",
            };

            _host = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host.Open();
            _host.Start();

            return service.EmailServer;
        }

        public static void Stop()
        {
            if (_host == null)
                return;

            _host.Stop();
            _host.Close();

            _host = null;
        }
    }
}
