using LinkMe.Framework.Host.Wcf;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using Container = LinkMe.Framework.Utility.Unity.Container;

namespace LinkMe.Apps.Mocks.Hosts
{
    public static class JobAdSearchHost
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

            var service = Container.Current.Resolve<JobAdSearchService>();
            service.InitialiseIndex = initialiseIndex;
            service.RebuildIndex = rebuildIndex;
            Container.Current.RegisterInstance<IJobAdSearchService>(service);

            var serviceDef = new ServiceDefinition
            {
                Service = service,
                Address = Container.Current.Resolve<string>("linkme.search.jobads.tcpAddress"),
                BindingName = "linkme.search.jobads.tcp",
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
            ((IJobAdSearchService)_host.ServiceDefinitions[0].Service).Clear();
        }

    }
}
