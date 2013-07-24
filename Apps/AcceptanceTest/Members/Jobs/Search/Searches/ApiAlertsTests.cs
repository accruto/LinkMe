using System;
using System.Collections.Specialized;
using System.Net;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public abstract class ApiAlertsTests
        : SearchTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        protected readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        protected readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();
        protected readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();

        private ReadOnlyUrl _apiCreateFromSavedUrl;
        private ReadOnlyUrl _apiCreateFromPreviousUrl;
        private ReadOnlyUrl _apiCreateFromCurrentUrl;

        [TestInitialize]
        public void ApiAlertsTestsInitialize()
        {
            _apiCreateFromSavedUrl = new ReadOnlyApplicationUrl(true, "~/members/alerts/api/createFromSaved");
            _apiCreateFromPreviousUrl = new ReadOnlyApplicationUrl(true, "~/members/alerts/api/createFromPrevious");
            _apiCreateFromCurrentUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/api/createFromCurrent");
        }

        protected JsonResponseModel CreateFromCurrent(string name, bool createAlert)
        {
            return Deserialize<JsonResponseModel>(Post(_apiCreateFromCurrentUrl, GetParameters(name, createAlert)));
        }

        protected JsonResponseModel CreateFromCurrent(HttpStatusCode expectedStatusCode, string name, bool createAlert)
        {
            return Deserialize<JsonResponseModel>(Post(expectedStatusCode, _apiCreateFromCurrentUrl, GetParameters(name, createAlert)));
        }

        protected JsonResponseModel CreateFromPrevious(Guid previousSearchId)
        {
            return Deserialize<JsonResponseModel>(Post(_apiCreateFromPreviousUrl, GetParameters(previousSearchId)));
        }

        protected JsonResponseModel CreateFromPrevious(HttpStatusCode expectedStatusCode, Guid previousSearchId)
        {
            return Deserialize<JsonResponseModel>(Post(expectedStatusCode, _apiCreateFromPreviousUrl, GetParameters(previousSearchId)));
        }

        protected JsonResponseModel CreateFromSaved(Guid searchId)
        {
            return Deserialize<JsonResponseModel>(Post(_apiCreateFromSavedUrl, GetParameters(searchId)));
        }

        protected JsonResponseModel CreateFromSaved(HttpStatusCode expectedStatusCode, Guid searchId)
        {
            return Deserialize<JsonResponseModel>(Post(expectedStatusCode, _apiCreateFromSavedUrl, GetParameters(searchId)));
        }

        private static NameValueCollection GetParameters(Guid id)
        {
            return new NameValueCollection
            {
                {"id", id.ToString()},
            };
        }

        private static NameValueCollection GetParameters(string name, bool createAlert)
        {
            var parameters = new NameValueCollection
            {
                {"createAlert", createAlert.ToString().ToLower()},
            };

            if (!string.IsNullOrEmpty(name))
                parameters["name"] = name;
            return parameters;
        }

        protected Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }
    }
}
