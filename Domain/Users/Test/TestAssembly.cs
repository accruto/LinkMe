using LinkMe.Domain.Roles.Orders.Commands;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Orders;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds.Queries;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test
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
                    .Add("linkme.resources.container")
                    .RegisterType<IJobAdProcessingQuery, MockJobAdProcessingQuery>(new ContainerControlledLifetimeManager())
                    .RegisterType<IParseResumeDataCommand, MockLensParseResumeDataCommand>(new ContainerControlledLifetimeManager())
                    .RegisterType<IPurchasesCommand, MockPurchasesCommand>(new ContainerControlledLifetimeManager())
                    .Configure(Container.Current, new ContainerEventSource());

                _initialised = true;
            }
        }
    }
}
