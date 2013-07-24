using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.WebPages;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Context;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Asp;
using LinkMe.Apps.Asp.Caches;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Apps.Asp.Mvc.Controllers;
using LinkMe.Apps.Asp.Mvc.Models.Binders;
using LinkMe.Apps.Asp.Routing;
using LinkMe.Apps.Asp.Security;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Asp.Urls;
using LinkMe.Apps.Presentation.Converters;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Products;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Domain.Validation;
using LinkMe.Environment;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Employers;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Resources;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using LinkMe.Utility;
using LinkMe.Utility.Configuration;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Areas.Accounts;
using LinkMe.Web.Areas.Administrators;
using LinkMe.Web.Areas.Api;
using LinkMe.Web.Areas.Custodians;
using LinkMe.Web.Areas.Employers;
using LinkMe.Web.Areas.Employers.Models;
using LinkMe.Web.Areas.Employers.Models.JobAds;
using LinkMe.Web.Areas.Employers.Routes;
using LinkMe.Web.Areas.Errors;
using LinkMe.Web.Areas.Errors.Controllers;
using LinkMe.Web.Areas.Errors.Models.Errors;
using LinkMe.Web.Areas.Errors.Routes;
using LinkMe.Web.Areas.Integration;
using LinkMe.Web.Areas.Integration.Controllers;
using LinkMe.Web.Areas.Landing;
using LinkMe.Web.Areas.Landing.Models.JobAds;
using LinkMe.Web.Areas.Members;
using LinkMe.Web.Areas.Members.Models;
using LinkMe.Web.Areas.Members.Models.Profiles;
using LinkMe.Web.Areas.Members.Models.Resources;
using LinkMe.Web.Areas.Public;
using LinkMe.Web.Areas.Public.Models.Join;
using LinkMe.Web.Areas.Shared;
using LinkMe.Web.Areas.Technical;
using LinkMe.Web.Areas.Verticals;
using LinkMe.Web.Content;
using LinkMe.Web.Context;
using LinkMe.Web.Helper;
using LinkMe.Web.Manager.Errors;
using LinkMe.Web.UI;
using LinkMe.Web.Views;
using LinkMe.WebControls;
using LinkMe.Apps.Asp.Exceptions;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Framework.Instrumentation;
using LinkMe.Domain.Location.Queries;
using Microsoft.Practices.Unity;
using Container=LinkMe.Framework.Utility.Unity.Container;
using ErrorReport=LinkMe.Web.Areas.Errors.Models.Errors.ErrorReport;

namespace LinkMe.Web
{
	public class Global : HttpApplication
	{
        // Same as for AuthenticationModule, EventSource is instance here instead of static to allow
        // more meaningful errors.
		private readonly EventSource _eventSource = new EventSource(typeof(Global));

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			// Avoid error messages caused by VS.NET's request for a non-existent file.

			if (Request.Url.AbsolutePath.EndsWith("get_aspx_ver.aspx"))
			{
				Response.End();
				return;
			}

            if (!NavigationManager.IsExcluded(Request.Url.AbsolutePath))
			{
                if (!Context.Items.Contains(LinkMeRequestValidator.INVALID_DANGEROUS_REQUEST))
				{
					// On a POST validate the form data, otherwise validate the query string.

					var request = Context.Request;
					var toValidate = (request.HttpMethod == "POST" ? request.Form : request.QueryString);
					var requestErrors = FieldInputHelper.ValidateNameValueCollection(toValidate);
					if(requestErrors != null)
					{
						Context.Items.Add(LinkMeRequestValidator.INVALID_DANGEROUS_REQUEST, requestErrors);
					}
				}
			}
		}

	    protected void Application_Error(object sender, EventArgs e)
	    {
	        const string method = "Application_Error";

	        var lastError = Server.GetLastError();

	        Response.Clear();

            if (lastError == null)
                _eventSource.Raise(Event.Error, method, "Application_Error event fired, but Server.GetLastError() returned null.");
	        else if (ProcessMaxRequestExceededError(lastError))
                return;

			var isNotFoundError = lastError.IsNotFoundError();
            LogError(lastError, isNotFoundError);

            if (isNotFoundError)
			    HandleNotFoundError();
			else if (string.Compare(Context.GetClientUrl().Path, ErrorsRoutes.ServerError.GenerateUrl().Path, StringComparison.InvariantCultureIgnoreCase) != 0)
			    HandleServerError(lastError);
		}

        private void HandleNotFoundError()
	    {
	        string requestedUrl = null;
	        var clientUrl = Context.GetClientUrl();
            if (clientUrl != null)
                requestedUrl = clientUrl.PathAndQuery;
	        var referrerUrl = Request.Headers["Referer"];

            Server.ClearError();
	        Response.TrySkipIisCustomErrors = true;

            if (Context.Request.AcceptTypes != null && Context.Request.AcceptTypes.Contains(ContentTypes.Json))
                HttpContext.Current.ExecuteController<ErrorsApiController, string>(c => c.NotFound, requestedUrl);
            else
                HttpContext.Current.ExecuteController<ErrorsController, string, string>(c => c.NotFound, requestedUrl, referrerUrl);
	    }

        private void HandleServerError(Exception exception)
        {
            var report = Context.GenerateErrorReport(exception);

            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            if (Context.Request.AcceptTypes != null && Context.Request.AcceptTypes.Contains(ContentTypes.Json))
                HttpContext.Current.ExecuteController<ErrorsApiController>(c => c.ServerError);
            else
                HttpContext.Current.ExecuteController<ErrorsController, ErrorReport, bool?>(c => c.ServerError, report, null);
        }

        private void LogError(Exception exception, bool isError404)
		{
		    const string method = "LogError";

            // Don't send alerts for 404 errors if the referrers is external, ie. other sites have links that are wrong.

            if (isError404 && Request.UrlReferrer != null && !NavigationManager.IsInternalUrl(new ApplicationUrl(Request.UrlReferrer.AbsoluteUri)))
                return;

            if (exception is HttpException)
            {
                // Bug 5721 - bots cause this when they send garbage ViewState data.

                if (exception.InnerException is ViewStateException)
                    return;

                // Ignore errors caused by bots using the wrong hostname - can't fix this.

                if (exception.InnerException is LicenseException && HttpHelper.IsBotUserAgent(Request.UserAgent))
                    return;
            }

            try
            {
                ExceptionManager.HandleException(exception, new StandardErrorHandler());
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.CriticalError, method, "Fatal error occurred trying to send unhandled exception email!", ex, new StandardErrorHandler());
            }
		}

		private bool ProcessMaxRequestExceededError(Exception exception)
		{
			var httpEx = exception as HttpException;
			if (httpEx == null || httpEx.Message != "Maximum request length exceeded.")
				return false;

			// Redirect the "Maximum request exceeded" error to a custom error page. Don't log an exception
			// in this case.

			Server.ClearError();

            // If this is a request to the attach API then need to send back an appropriate JSON error message.

		    var clientUrl = Context.GetClientUrl();
		    var attachUrl = CandidatesRoutes.ApiAttach.GenerateUrl();
            if (string.Compare(clientUrl.Path, attachUrl.Path, StringComparison.InvariantCultureIgnoreCase) == 0)
                return ProcessAttachMaxFileSizeError();

            // Everything else rediect to an appropriate page.

			NavigationManager.Redirect<UploadTooLargeError>();
			return true;
		}

	    private bool ProcessAttachMaxFileSizeError()
	    {
            // Need to send back the equivalent of a JSON error with content type text/plain.
            // See CandidatesApiController.Attach.

            var exception = new FileTooLargeException { MaxFileSize = LinkMe.Domain.Users.Employers.Contacts.Constants.MaxAttachmentFileSize };
	        var formatter = (IErrorHandler)new StandardErrorHandler();
	        var message = formatter.FormatErrorMessage(exception);
           
            var model = new JsonResponseModel
            {
                Success = false,
                Errors = new List<JsonError>
                {
                    new JsonError {Key = "", Code = ErrorCodes.GetDefaultErrorCode(ErrorCodeClass.Validation), Message = message}
                }
            };

            // Serialize it out and send back.

            Response.ContentType = MediaType.Text;
            Response.Write(new JavaScriptSerializer().Serialize(model));
	        Response.End();

	        return true;
	    }

	    protected void Application_Start(Object sender, EventArgs e)
		{
            const string method = "Application_Start";
            
            _eventSource.Raise(Event.Information, method, "Application_Start event fired.");

			try
			{
                ApplicationSetUp.RegisterPath();
                ApplicationContext.SetupApplications(WebSite.LinkMe);

                _eventSource.Raise(Event.Information, method, "Initializing container...");

			    InitialiseContainer();

                var locationQuery = Container.Current.Resolve<ILocationQuery>();
                var industriesQuery = Container.Current.Resolve<IIndustriesQuery>();
                var channelsQuery = Container.Current.Resolve<IChannelsQuery>();

                InitialiseTraceListeners();
                InitialiseContext(locationQuery, channelsQuery);
                InitialiseReferences();

                ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(Container.Current));

			    var errorHandler = new StandardErrorHandler();

                ModelBinders.Binders.Add(typeof(Guid[]), new GuidArrayBinder());
                ModelBinders.Binders.Add(typeof(int[]), new IntArrayBinder());
                ModelBinders.Binders.Add(typeof(CheckBoxValue), new CheckBoxBinder());
                ModelBinders.Binders.Add(typeof(CreditCard), new CreditCardBinder());
                ModelBinders.Binders.Add(typeof(MemberSearchCriteria), new ModelBinder(new MemberSearchCriteriaConverter(locationQuery, industriesQuery), errorHandler));
                ModelBinders.Binders.Add(typeof(MemberSearchSortCriteria), new ModelBinder(new MemberSearchSortCriteriaConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(JobAdSearchCriteria), new ModelBinder(new JobAdSearchCriteriaConverter(locationQuery, industriesQuery), errorHandler));
                ModelBinders.Binders.Add(typeof(JobAdSearchSortCriteria), new ModelBinder(new JobAdSearchSortCriteriaConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(OrganisationEmployerSearchCriteria), new ModelBinder(new EmployerSearchCriteriaConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(CandidatesPresentationModel), new ModelBinder(new CandidatesPresentationModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(JobAdsPresentationModel), new ModelBinder(new JobAdsPresentationModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(PersonalDetailsMemberModel), new ModelBinder(new PersonalDetailsMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(JobDetailsMemberModel), new ModelBinder(new JobDetailsMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(ContactDetailsMemberModel), new ModelBinder(new ContactDetailsMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(DesiredJobMemberModel), new ModelBinder(new DesiredJobMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(CareerObjectivesMemberModel), new ModelBinder(new CareerObjectivesMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(SchoolModel), new ModelBinder(new EducationUpdateModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(OtherMemberModel), new ModelBinder(new OtherMemberModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(VisibilityModel), new ModelBinder(new VisibilityModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(EmploymentHistoryUpdateModel), new ModelBinder(new EmploymentHistoryUpdateModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(EducationUpdateModel), new ModelBinder(new EducationUpdateModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(ResourceSearchCriteria), new ModelBinder(new ResourceSearchCriteriaConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(ResourceSearchSortCriteria), new ModelBinder(new ResourceSearchSortCriteriaConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(ResourcesPresentationModel), new ModelBinder(new ResourcesPresentationModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(SearchModel), new ModelBinder(new SearchModelConverter(), errorHandler));
                ModelBinders.Binders.Add(typeof(JobAdModel), new ModelBinder(new JobAdModelConverter(locationQuery), errorHandler));

                // This effectively turns off the ASP.NET MVC built-in validation.  May re-consider at a later time on best way to use it.

                ModelValidatorProviders.Providers.Clear();
                ModelValidatorProviders.Providers.Add(new NullModelValidatorProvider());

                ValueProviderFactories.Factories.Add(new JsonValueProviderFactory());

                RegisterRoutes(RouteTable.Routes);
                RegisterBundles(BundleTable.Bundles);

                DisplayModeProvider.Instance.Modes.Insert(0, new DefaultDisplayMode("iPhone")
                {
                    ContextCondition = c =>
                    {
                        var userAgent = c.GetOverriddenUserAgent();
                        return !string.IsNullOrEmpty(userAgent) && userAgent.IndexOf("iPhone", StringComparison.OrdinalIgnoreCase) >= 0;
                    }
                });

                var verticalsQuery = Container.Current.Resolve<IVerticalsQuery>();
			    var viewEngine = new VerticalViewEngine(Container.Current, verticalsQuery); // new DeviceViewEngine(Container.Current, verticalsQuery);
                ViewEngines.Engines.Clear();
                ViewEngines.Engines.Add(viewEngine);

                _eventSource.Raise(Event.Information, method, "Application_Start completed.");
                //RouteDebug.RouteDebugger.RewriteRoutesForTesting(RouteTable.Routes);
            }
			catch (Exception ex)
			{
                _eventSource.Raise(Event.Error, method, "An exception was thrown in Application_Start.", ex, new StandardErrorHandler());
                ExceptionManager.HandleException(ex, new StandardErrorHandler());
                throw;
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
                .Add(new AgentsConfigurator())
                .Add(new AspConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new ServicesConfigurator())

                .Add("linkme.resources.container")
                .Add("linkme.environment.container")

                .RegisterType<IRefreshCacheManager, HomeRefreshCacheManager>(
                    "cache.home",
                    new ContainerControlledLifetimeManager())

                .RegisterType<ICacheManager, CacheManager>(
                    "cache.home",
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        new ResolvedParameter<IRefreshCacheManager>("cache.home"),
                        new TimeSpan(0, 30, 0)))

                .RegisterType<ICacheManager, CacheManager>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionConstructor(
                        new ResolvedParameter<IRefreshCacheManager>("cache.home"),
                        new TimeSpan(0, 30, 0)))

                .RegisterType<Areas.Public.Controllers.Home.HomeController>(
                    new InjectionConstructor(
                        new ResolvedParameter<IAccountsManager>(),
                        new ResolvedParameter<ILoginCredentialsQuery>(),
                        new ResolvedParameter<IFaqsQuery>(),
                        new ResolvedParameter<IIndustriesQuery>(),
                        new ResolvedParameter<ICacheManager>("cache.home"),
                        10,
                        new ResolvedParameter<ILocationQuery>(),
                        new ResolvedParameter<IResourcesQuery>()))

                .RegisterType<Areas.Employers.Controllers.Home.HomeController>(
                    new InjectionConstructor(
                        new ResolvedParameter<IAccountsManager>(),
                        new ResolvedParameter<ILoginCredentialsQuery>(),
                        new ResolvedParameter<IFaqsQuery>(),
                        new ResolvedParameter<ILocationQuery>()))

                .RegisterType<IJobAdFeedsManager, JobAdFeedsManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IJobAdPostsManager, JobAdPostsManager>(new ContainerControlledLifetimeManager())
                .RegisterType<IJobAdApplicationsManager, JobAdApplicationsManager>(new ContainerControlledLifetimeManager())

                .Configure(Container.Current, new ContainerEventSource());
        }

	    protected void Application_End(Object sender, EventArgs e)
		{
            const string method = "Application_End";

            _eventSource.Raise(Event.Information, method, "Application_End event fired.");
		}

        public void Session_OnEnd()
        {
            const string method = "Session_OnEnd";

            // Just log if this fails.

            try
            {
                Container.Current.Resolve<IAuthenticationManager>().EndSession(Session);
            }
            catch (Exception ex)
            {
                _eventSource.Raise(Event.Error, method, "Cannot end the session.", ex, new StandardErrorHandler());
            }
        }

        [Conditional("DEBUG")]
		private void InitialiseTraceListeners()
		{
            const string method = "InitialiseTraceListeners";
            _eventSource.Raise(Event.Information, method, "Initializing trace listeners...");
            Debug.Listeners.Add(new BreakOnFailListener());
			Debug.Listeners.Add(new WriteToResponseListener());
		}

        private void InitialiseContext(ILocationQuery locationQuery, IChannelsQuery channelsQuery)
        {
            _eventSource.Raise(Event.Information, "InitialiseContext", "Initializing context...");
            ActivityContext.Current.Register(new SessionVerticalContext());
            ActivityContext.Current.Register(new SessionCommunityContext());
            ActivityContext.Current.Register(new HttpContextLocationContext(locationQuery.GetCountry("Australia")));
            ActivityContext.Current.Register(new ChannelContext(channelsQuery.GetChannelApp(channelsQuery.GetChannel("Web").Id, "Web")));
        }

        private void InitialiseReferences()
        {
            _eventSource.Raise(Event.Information, "InitialiseReferences", "Initializing references...");
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.UniversalLayout);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.HeaderAndNav);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.Fonts);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.TextLinksHeadings);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.WidgetsAndLists);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.Sidebar);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.Forms);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.Forms2);
            Apps.Asp.UI.MasterPage.AddStandardStyleSheetReference(StyleSheets.Messaging);

            Apps.Asp.UI.MasterPage.AddStandardJavaScriptReference(JavaScripts.Prototype);
            Apps.Asp.UI.MasterPage.AddStandardJavaScriptReference(JavaScripts.Scriptaculous);
            Apps.Asp.UI.MasterPage.AddStandardJavaScriptReference(JavaScripts.TooltipBehaviour);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            RouteTable.Routes.RegisterArea<AccountsAreaRegistration>();
            RouteTable.Routes.RegisterArea<AdministratorsAreaRegistration>();
            RouteTable.Routes.RegisterArea<CustodiansAreaRegistration>();
            RouteTable.Routes.RegisterArea<ApiAreaRegistration>();
            RouteTable.Routes.RegisterArea<EmployersAreaRegistration>();
            RouteTable.Routes.RegisterArea<ErrorsAreaRegistration>();
            RouteTable.Routes.RegisterArea<IntegrationAreaRegistration>();
            RouteTable.Routes.RegisterArea<MembersAreaRegistration>();
            RouteTable.Routes.RegisterArea<PublicAreaRegistration>();
            RouteTable.Routes.RegisterArea<VerticalsAreaRegistration>();
            RouteTable.Routes.RegisterArea<LandingAreaRegistration>();
            RouteTable.Routes.RegisterArea<TechnicalAreaRegistration>();

            // Error routes.

            routes.MapRoute("ErrorNotFound", "error/notfound", new { controller = "Error", action = "NotFound" });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }

        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleResolver.Current = new VersionBundleResolver();
            ScriptBundles.RegisterBundles(bundles);
            StyleBundles.RegisterBundles(bundles);

            BundleTable.EnableOptimizations = RuntimeEnvironment.Environment != ApplicationEnvironment.Dev;
        }
    }
}
