using System;
using System.Collections.Specialized;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches
{
    [TestClass]
    public class ApiDeleteTests
        : SearchTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IJobAdSearchAlertsCommand _jobAdSearchAlertsCommand = Resolve<IJobAdSearchAlertsCommand>();
        private readonly IJobAdSearchAlertsQuery _jobAdSearchAlertsQuery = Resolve<IJobAdSearchAlertsQuery>();

        private const string Keywords = "Archeologist";
        private const string SearchName = "My search";

        private ReadOnlyUrl _apiDeleteSearchUrl;
        private ReadOnlyUrl _apiDeleteSearchAlertUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _apiDeleteSearchUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/api/delete");
            _apiDeleteSearchAlertUrl = new ReadOnlyApplicationUrl(true, "~/members/alerts/api/delete");
        }

        [TestMethod]
        public void TestDeleteSearch()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Name = SearchName, Criteria = criteria };
            _jobAdSearchAlertsCommand.CreateJobAdSearch(member.Id, search);

            var gotSearch = _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id);
            Assert.AreEqual(SearchName, gotSearch.Name);
            Assert.AreEqual(criteria, gotSearch.Criteria);
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));

            // Delete.

            AssertJsonSuccess(DeleteSearch(search.Id));

            // Check.

            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestDeleteSearchAndAlert()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now);

            var gotSearch = _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id);
            Assert.AreEqual(SearchName, gotSearch.Name);
            Assert.AreEqual(criteria, gotSearch.Criteria);
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));

            // Delete.

            AssertJsonSuccess(DeleteSearch(search.Id));

            // Check.

            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearch(search.Id));
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        [TestMethod]
        public void TestDeleteAlert()
        {
            var member = CreateMember();
            LogIn(member);

            // Save a search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            _jobAdSearchAlertsCommand.CreateJobAdSearchAlert(member.Id, search, DateTime.Now);

            var gotSearch = _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id);
            Assert.AreEqual(SearchName, gotSearch.Name);
            Assert.AreEqual(criteria, gotSearch.Criteria);
            Assert.IsNotNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));

            // Delete.

            AssertJsonSuccess(DeleteSearchAlert(search.Id));

            // Check.

            gotSearch = _jobAdSearchAlertsQuery.GetJobAdSearch(search.Id);
            Assert.IsNotNull(gotSearch);
            Assert.AreEqual(search.Name, gotSearch.Name);
            Assert.AreEqual(search.Criteria, gotSearch.Criteria);
            Assert.IsNull(_jobAdSearchAlertsQuery.GetJobAdSearchAlert(search.Id));
        }

        private JsonResponseModel DeleteSearch(Guid searchId)
        {
            return Deserialize<JsonResponseModel>(Post(_apiDeleteSearchUrl, GetParameters(searchId)));
        }

        private JsonResponseModel DeleteSearchAlert(Guid searchId)
        {
            return Deserialize<JsonResponseModel>(Post(_apiDeleteSearchAlertUrl, GetParameters(searchId)));
        }

        private static NameValueCollection GetParameters(Guid searchId)
        {
            return new NameValueCollection
            {
                {"searchId", searchId.ToString()},
            };
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }
    }
}
