using System;
using System.Net;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    [TestClass]
    public class DeleteSearchTests
        : SearchesTests
    {
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();

        [TestMethod]
        public void TestDeleteSearch()
        {
            var employer = CreateEmployer(0);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            _memberSearchAlertsCommand.CreateMemberSearch(employer, search);
            CreateDevice(employer.Id, "BogusDeviceToken");

            LogIn(employer);
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, false));

            // Delete it.

            AssertJsonSuccess(DeleteSearch(search));
            AssertSearches(Searches());
            AssertSearches(employer, "BogusDeviceToken");
        }

        [TestMethod]
        public void TestDeleteSearchAlert()
        {
            var employer = CreateEmployer(0);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            _memberSearchAlertsCommand.CreateMemberSearchAlert(employer, search, AlertType.AppleDevice);

            LogIn(employer);
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, true));
            AssertSearches(employer, string.Empty, MakeTupleTuple(search, AlertType.AppleDevice, true));

            // Delete it.

            AssertJsonSuccess(DeleteSearch(search));
            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);
        }

        [TestMethod]
        public void TestUnknownSearch()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);

            // Try to delete it.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Id = Guid.NewGuid(), Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonError(DeleteSearch(HttpStatusCode.NotFound, search), null, "400", "The search cannot be found.");
            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);
        }

        [TestMethod]
        public void TestCannotDeleteOtherSearch()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            _memberSearchAlertsCommand.CreateMemberSearch(employer0, search);

            LogIn(employer1);
            AssertSearches(Searches());
            AssertSearches(employer1, string.Empty);

            // Try to delete it.

            AssertJsonError(DeleteSearch(search), null, "301", "You do not have sufficient permissions.");
        }

        private JsonResponseModel DeleteSearch(HttpStatusCode expectedStatusCode, MemberSearch search)
        {
            return Deserialize<JsonResponseModel>(Delete(expectedStatusCode, GetSearchUrl(search.Id)));
        }

        private JsonResponseModel DeleteSearch(MemberSearch search)
        {
            return Deserialize<JsonResponseModel>(Delete(GetSearchUrl(search.Id)));
        }
    }
}
