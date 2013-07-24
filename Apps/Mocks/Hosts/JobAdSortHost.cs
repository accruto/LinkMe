using LinkMe.Framework.Host.Wcf;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Sort;

namespace LinkMe.Apps.Mocks.Hosts
{
    public static class JobAdSortHost
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

            var service = Container.Current.Resolve<JobAdSortService>();
            service.InitialiseIndex = initialiseIndex;
            Container.Current.RegisterInstance<IJobAdSortService>(service);

            var serviceDef = new ServiceDefinition
                 {
                     Service = service,
                     Address = Container.Current.Resolve<string>("linkme.sort.jobads.tcpAddress"),
                     BindingName = "linkme.sort.jobads.tcp",
                 };

            _host = new WcfTcpHost { ServiceDefinitions = new []{ serviceDef } };
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
            ((IJobAdSortService)_host.ServiceDefinitions[0].Service).Clear();
        }
    }
}
