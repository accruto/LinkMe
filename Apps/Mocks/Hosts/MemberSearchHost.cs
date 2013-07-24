using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;

namespace LinkMe.Apps.Mocks.Hosts
{
    public static class MemberSearchHost
    {
        private static WcfTcpHost _host;

        public static void Start()
        {
            Start(false, true);
        }

        public static void Start(bool initialiseIndex, bool rebuildIndex)
        {
            if (_host != null)
                return;

            var service = Container.Current.Resolve<MemberSearchService>();
            service.InitialiseIndex = initialiseIndex;
            service.RebuildIndex = rebuildIndex;
            Container.Current.RegisterInstance<IMemberSearchService>(service);

            var serviceDefinition = new ServiceDefinition
            {
                Service = service,
                Address = Container.Current.Resolve<string>("linkme.search.members.tcpAddress"),
                BindingName = "linkme.search.members.tcp",
            };

            _host = new WcfTcpHost { ServiceDefinitions = new[] { serviceDefinition } };
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
            ((IMemberSearchService)_host.ServiceDefinitions[0].Service).Clear();
        }
    }
}
