using LinkMe.Analyse.Engine.Unity;
using LinkMe.Analyse.Unity;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using NUnit.Framework;

namespace LinkMe.Workflow.Test
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

        private static void InitialiseContainer()
        {
            new ContainerConfigurer()
                .Add(new DomainConfigurator())
                .Add(new RolesConfigurator())
                .Add(new UsersConfigurator())
                .Add(new QueryConfigurator())
                .Add(new SearchConfigurator())
                .Add(new QueryEngineConfigurator())
                .Add(new AnalyseConfigurator())
                .Add(new AnalysisEngineConfigurator())
                .Add(new ReportsConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new ServicesConfigurator())
                .Add(new AspConfigurator())
                .Add("linkme.resources.container")
                .Configure(Container.Current);
        }
    }
}