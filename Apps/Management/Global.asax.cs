using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Management.Views;
using LinkMe.Apps.Presentation.Errors;
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
using LinkMe.Utility.Configuration;

namespace LinkMe.Apps.Management
{
    public class Global
        : HttpApplication
    {
        private readonly EventSource _eventSource = new EventSource<Global>();

        protected void Application_Start(object sender, EventArgs e)
        {
            ApplicationSetUp.RegisterPath();
            ApplicationContext.SetupApplications(WebSite.Management);

            InitialiseContainer();
            InitialiseMvc();
        }

        protected void Application_End(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            const string method = "Application_Error";

            var lastError = Server.GetLastError();

            if (lastError == null)
                _eventSource.Raise(Event.CriticalError, method, "Application_Error event fired, but Server.GetLastError() returned null.");
            else
                _eventSource.Raise(Event.CriticalError, method, "Application error has occured.", lastError, new StandardErrorHandler());
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
                .Add(new PresentationConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new ServicesConfigurator())
                .Add(new AspConfigurator())
                .Add("linkme.resources.container")
                .Add("linkme.environment.container")
                .Configure(Container.Current, new ContainerEventSource());
        }

        private static void InitialiseMvc()
        {
            // Controllers.

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(Container.Current));

            // Areas.

            AreaRegistration.RegisterAllAreas();

            // View engines.

            var viewEngine = new ContentTypeViewEngine(Container.Current);
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(viewEngine);
        }
    }
}