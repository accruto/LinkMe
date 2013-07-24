using LinkMe.Domain.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Utility.Test
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
                    .Add("linkme.resources.container")
                    .Configure(Container.Current, new ContainerEventSource());

                _initialised = true;
            }
        }
    }
}
