using System.Net;
using LinkMe.Analyse.Engine.Unity;
using LinkMe.Analyse.Unity;
using LinkMe.Apps.Agents.Applications;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Agents.Unity;
using LinkMe.Apps.Api.Areas.Accounts.Models;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Unity;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Apps.Presentation.Unity;
using LinkMe.Apps.Services.Unity;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Unity;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Unity;
using LinkMe.Domain.Users.Unity;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.JobAds;
using LinkMe.Query.Members;
using LinkMe.Query.Reports.Unity;
using LinkMe.Query.Search.Engine.JobAds;
using LinkMe.Query.Search.Engine.Members;
using LinkMe.Query.Search.Engine.Unity;
using LinkMe.Query.Search.Unity;
using LinkMe.Query.Unity;
using LinkMe.Utility.Configuration;
using Microsoft.Practices.Unity;

namespace LinkMe.Apps.Api.Test
{
    public class WebTestFixture
        : Asp.Test.WebTestFixture
    {
        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _logoutUrl;
        private static bool _initialised;

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
                .Add(new ReportsConfigurator())
                .Add(new QueryEngineConfigurator())
                .Add(new AnalyseConfigurator())
                .Add(new AnalysisEngineConfigurator())
                .Add(new PresentationConfigurator())
                .Add(new AgentsConfigurator())
                .Add(new AspConfigurator())
                .Add(new ServicesConfigurator())
                .Add("linkme.resources.container")
                .Add("linkme.environment.container")
                .RegisterType<IMemberSearchService, MemberSearchService>(new ContainerControlledLifetimeManager())
                .RegisterType<IJobAdSearchService, JobAdSearchService>(new ContainerControlledLifetimeManager())
                .Configure(Container.Current);
        }

        protected override void FixtureSetUp()
        {
            base.FixtureSetUp();
            ApplicationContext.SetupApplications(WebSite.Api);
            MemberSearchHost.Start();
            JobAdSearchHost.Start();
            JobAdSortHost.Start();
            JobAdSentimentAnalysisHost.Start();
        }

        protected override void SetUp()
        {
            base.SetUp();

            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _loginUrl = new ReadOnlyApplicationUrl("~/v1/login");
            _logoutUrl = new ReadOnlyApplicationUrl("~/v1/logout");
        }

        protected void ClearSearchIndexes()
        {
            MemberSearchHost.ClearIndex();
            JobAdSearchHost.ClearIndex();
            JobAdSortHost.ClearIndex();
        }

        protected JsonResponseModel LogIn(IUser user)
        {
            return LogIn(HttpStatusCode.OK, user.GetLoginId(), user.GetPassword());
        }

        protected JsonResponseModel LogIn(HttpStatusCode expectedStatusCode, string loginId, string password)
        {
            var model = new LoginModel { LoginId = loginId, Password = password };
            return Deserialize<JsonResponseModel>(Post(expectedStatusCode, _loginUrl, JsonContentType, Serialize(model)));
        }

        protected JsonResponseModel LogOut()
        {
            return Deserialize<JsonResponseModel>(Post(_logoutUrl));
        }
    }
}