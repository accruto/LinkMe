using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Apps.Mocks.Services.JobG8
{
    public static class JobG8Host
    {
        private static WcfTcpHost _host;

        public static IMockJobG8Server Start()
        {
            if (_host != null)
                return ((MockJobG8Service)_host.ServiceDefinitions[0].Service).JobG8Server;

            var service = Container.Current.Resolve<MockJobG8Service>();
            service.JobG8Server = new MockJobG8Server();
            Container.Current.RegisterInstance<IMockJobG8Service>(service);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Container.Current.Resolve<string>("linkme.jobg8.tcpAddress"),
                BindingName = "linkme.jobg8.tcp",
            };

            _host = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
            _host.Open();
            _host.Start();

            return service.JobG8Server;
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