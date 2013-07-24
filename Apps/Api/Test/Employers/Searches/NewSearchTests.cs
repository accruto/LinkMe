using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    [TestClass]
    public class NewSearchTests
        : SearchesTests
    {
        [TestMethod]
        public void TestNewSearch()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonSuccess(NewSearch(search, false, string.Empty));

            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false) );
            AssertSearches(employer, string.Empty, MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestNewSearchAlert()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonSuccess(NewSearch(search, true, "BogusDeviceToken"));

            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, true));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, true));
        }

        [TestMethod]
        public void TestNoName()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Criteria = criteria };
            AssertJsonError(NewSearch(search, false, string.Empty), "Name", "The name is required.");

            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);
        }

        [TestMethod]
        public void TestSameName()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search0 = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonSuccess(NewSearch(search0, false, string.Empty));

            // Create another.

            var search1 = new MemberSearch { Name = search0.Name, Criteria = criteria };
            AssertJsonError(NewSearch(search1, false, string.Empty), "Name", "The name is already being used.");

            AssertSearches(Searches(), MakeTupleTuple(search0, AlertType.AppleDevice, false));
            AssertSearches(employer, string.Empty, MakeTupleTuple(search0, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestNoCriteria()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var search = new MemberSearch { Name = string.Format(NameFormat, 0) };
            AssertJsonError(NewSearch(search, false, string.Empty), "Criteria", "The criteria cannot be empty.");

            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);
        }

        [TestMethod]
        public void TestEmptyCriteria()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonError(NewSearch(search, false, string.Empty), "Criteria", "The criteria cannot be empty.");

            AssertSearches(Searches());
            AssertSearches(employer, string.Empty);
        }

        private JsonResponseModel NewSearch(MemberSearch search, bool isAlert, string deviceToken)
        {
            var model = new MemberSearchRequestModel
            {
                Name = search.Name,
                IsAlert = isAlert,
                Criteria = search.Criteria,
                DeviceToken = deviceToken,
            };
            return Deserialize<JsonResponseModel>(Post(GetSearchesUrl(deviceToken), JsonContentType, Serialize(model, new MemberSearchCriteriaJavaScriptConverter(_locationQuery, _industriesQuery))));
        }
    }
}
