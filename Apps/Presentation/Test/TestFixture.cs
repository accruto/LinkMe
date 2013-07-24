using LinkMe.Analyse.Engine.Unity;
using LinkMe.Analyse.Unity;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Search.Engine.Unity;
using NUnit.Framework;

namespace LinkMe.Apps.Presentation.Test
{
    public abstract class TestFixture
    {
        private static bool _initialised;

        [SetUp]
        public virtual void SetUp()
        {
        }

        protected static T Resolve<T>()
        {
            if (!_initialised)
            {
                InitialiseContainer();
                _initialised = true;
            }
            return Container.Current.Resolve<T>();
        }

        protected static T Resolve<T>(string name)
        {
            if (!_initialised)
            {
                InitialiseContainer();
                _initialised = true;
            }
            return Container.Current.Resolve<T>(name);
        }

        private static void InitialiseContainer()
        {
            new ContainerConfigurer()
                .Add(new DomainConfigurator())
                .Add(new RolesConfigurator())
                .Add(new UsersConfigurator())
                .Add(new QueryEngineConfigurator())
                .Add(new AnalyseConfigurator())
                .Add(new AnalysisEngineConfigurator())
                .Add(new PresentationConfigurator())
                .Add("linkme.resources.container")
                .Configure(Container.Current);
        }
    }
}