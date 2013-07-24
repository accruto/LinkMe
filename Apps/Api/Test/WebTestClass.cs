using System.Net;
using LinkMe.Apps.Agents.Test.Users;
using LinkMe.Apps.Api.Areas.Accounts.Models;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test
{
    [TestClass]
    public abstract class WebTestClass
        : Asp.Test.WebTestClass
    {
        private ReadOnlyUrl _loginUrl;
        private ReadOnlyUrl _logoutUrl;

        protected static T Resolve<T>()
        {
            TestAssembly.InitialiseContainer();
            return Container.Current.Resolve<T>();
        }

        [TestInitialize]
        public void WebTestClassInitialize()
        {
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