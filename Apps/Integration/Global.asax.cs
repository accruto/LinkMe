using System;
using System.Web;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;

namespace LinkMe.Apps.Integration
{
    public class Global
        : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            InitialiseContainer();
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
                .Add(new ReportsConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new AspConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new ServicesConfigurator())
                .Add("linkme.resources.container")
                .Add("linkme.environment.container")
                .Configure(Container.Current, new ContainerEventSource());
        }
    }
}