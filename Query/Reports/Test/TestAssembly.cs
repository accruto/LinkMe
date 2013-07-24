using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test
{
    [TestClass]
    public class TestAssembly
    {
        private static bool _initialised;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            InitialiseContainer();
        }

        public static void InitialiseContainer()
        {
            if (!_initialised)
            {
                new ContainerConfigurer()
                    .Add(new DomainConfigurator())
                    .Add(new RolesConfigurator())
                    .Add(new UsersConfigurator())
                    .Add(new SearchConfigurator())
                    .Add(new ReportsConfigurator())
                    .RegisterType<IJobAdProcessingQuery, MockJobAdProcessingQuery>(new ContainerControlledLifetimeManager())
                    .Add("linkme.resources.container")
                    .Configure(Container.Current, new ContainerEventSource());

                _initialised = true;
            }
        }
    }
}