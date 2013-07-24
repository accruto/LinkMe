using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using LinkMe.Utility.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test
{
    [TestClass]
    public class TestAssembly
    {
        private static bool _initialised;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            InitialiseContainer();
            ApplicationContext.SetupApplications(WebSite.Api);
            MemberSearchHost.Start();
            JobAdSearchHost.Start();
            JobAdSortHost.Start();
        }

        public static void InitialiseContainer()
        {
            if (!_initialised)
            {
                new ContainerConfigurer()
                    .Add(new DomainConfigurator())
                    .Add(new RolesConfigurator())
                    .Add(new UsersConfigurator())
                    .Add(new QueryConfigurator())
                    .Add(new SearchConfigurator())
                    .Add(new ReportsConfigurator())
                    .Add(new QueryEngineConfigurator())
                    .Add(new PresentationConfigurator())
                    .Add(new AgentsConfigurator())
                    .Add(new AspConfigurator())
                    .Add(new ServicesConfigurator())
                    .Add("linkme.resources.container")
                    .Add("linkme.environment.container")
                    .RegisterType<IMemberSearchService, MemberSearchService>(new ContainerControlledLifetimeManager())
                    .RegisterType<IJobAdSearchService, JobAdSearchService>(new ContainerControlledLifetimeManager())
                    .Configure(Container.Current, new ContainerEventSource());

                _initialised = true;
            }
        }
    }
}