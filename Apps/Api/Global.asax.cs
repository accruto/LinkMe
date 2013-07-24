using System;
using System.Web;
using System.Web.Mvc;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Api.Areas.Employers.Models.Accounts;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
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
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Api
{
    public class Global
        : HttpApplication
    {
        private readonly EventSource _eventSource = new EventSource(typeof(Global));

        protected void Application_Start()
        {
            const string method = "Application_Start";

            ApplicationContext.SetupApplications(WebSite.Api);

            AreaRegistration.RegisterAllAreas();

            try
            {
                InitialiseContainer();

                var locationQuery = Container.Current.Resolve<ILocationQuery>();
                var industriesQuery = Container.Current.Resolve<IIndustriesQuery>();
                var channelsQuery = Container.Current.Resolve<IChannelsQuery>();
                
                InitialiseContext(channelsQuery);
                InitialiseMvc(locationQuery, industriesQuery);
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, "An exception was thrown in Application_Start.", ex, new StandardErrorHandler());
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            const string method = "Application_Error";

            var lastError = Server.GetLastError();

            Response.Clear();

            if (lastError == null)
                _eventSource.Raise(Event.Error, method, "Application_Error event fired, but Server.GetLastError() returned null.");

            LogError(lastError);

            var isNotFoundError = lastError.IsNotFoundError();
            if (isNotFoundError)
                HandleNotFoundError();
            else
                HandleServerError();
        }

        private void HandleNotFoundError()
        {
            string requestedUrl = null;
            var clientUrl = Context.GetClientUrl();
            if (clientUrl != null)
                requestedUrl = clientUrl.PathAndQuery;

            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            HttpContext.Current.ExecuteController<ErrorsApiController, string>(c => c.NotFound, requestedUrl);
        }

        private void HandleServerError()
        {
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            HttpContext.Current.ExecuteController<ErrorsApiController>(c => c.ServerError);
        }

        private void LogError(Exception exception)
        {
            const string method = "LogError";

            try
            {
                ExceptionManager.HandleException(exception, new StandardErrorHandler());
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.CriticalError, method, "Fatal error occurred trying to send unhandled exception email!", ex, new StandardErrorHandler());
            }
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

        private static void InitialiseContext(IChannelsQuery channelsQuery)
        {
            ActivityContext.Current.Register(new ChannelContext(channelsQuery.GetChannelApp(channelsQuery.GetChannel("API").Id, "iOS")));
        }

        private static void InitialiseMvc(ILocationQuery locationQuery, IIndustriesQuery industriesQuery)
        {
            // Controllers.

            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(Container.Current));

            // Binders.

            ModelBinders.Binders.Add(typeof(MemberSearchCriteria), new ModelBinder(new MemberSearchCriteriaConverter(locationQuery, industriesQuery), new StandardErrorHandler()));
            ModelBinders.Binders.Add(typeof(EmployerJoinModel), new ModelBinder(new EmployerJoinModelConverter(), new StandardErrorHandler()));

            // ValueProviders.

            ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());
        }
    }
}