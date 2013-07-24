using LinkMe.Apps.Presentation.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Engine.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test
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
                    .Add(new QueryEngineConfigurator())
                    .Add(new PresentationConfigurator())
                    .Add("linkme.resources.container")
                    .Configure(Container.Current, new ContainerEventSource());

                _initialised = true;
            }
        }
    }
}