using LinkMe.Framework.Host.Wcf;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Engine.Resources;
using Container = LinkMe.Framework.Utility.Unity.Container;

namespace LinkMe.Apps.Mocks.Hosts
{
    public static class ResourceSearchHost
    {
        private static WcfTcpHost _host;

        public static void Start()
        {
            // Given that the resources are static initialise from the database.

            Start(true, true);
        }

        public static void Start(bool initialiseIndex, bool rebuildIndex)
        {
            if (_host != null)
                return;

            var service = Container.Current.Resolve<ResourceSearchService>();
            service.InitialiseIndex = initialiseIndex;
            service.RebuildIndex = rebuildIndex;
            Container.Current.RegisterInstance<IResourceSearchService>(service);

            var serviceDef = new ServiceDefinition
            {
                Service = service,
                Address = Container.Current.Resolve<string>("linkme.search.resources.tcpAddress"),
                BindingName = "linkme.search.resources.tcp",
            };

            _host = new WcfTcpHost { ServiceDefinitions = new[] { serviceDef } };
            _host.Open();
            _host.Start();
        }

        public static void Stop()
        {
            if (_host == null)
                return;

            _host.Stop();
            _host.Close();

            _host = null;
        }

        public static void ClearIndex()
        {
            ((IResourceSearchService)_host.ServiceDefinitions[0].Service).Clear();
        }

    }
}
