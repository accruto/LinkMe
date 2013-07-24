using System;
using System.Net;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Api.Areas.Employers.Models.Search;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Mvc.Models.JavaScriptConverters;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Searches
{
    [TestClass]
    public class EditSearchTests
        : SearchesTests
    {
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();

        [TestMethod]
        public void TestEditSearch()
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

            // Update the name.

            search.Name = string.Format(NameFormat, 1);
            AssertJsonSuccess(EditSearch(search, false, "BogusDeviceToken2"));
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken2", MakeTupleTuple(search, AlertType.AppleDevice, false));

            // Update the criteria.

            criteria.SetKeywords(NewKeywords);
            search.Criteria = criteria;
            AssertJsonSuccess(EditSearch(search, false, "BogusDeviceToken2"));
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken2", MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestEditSearchAlert()
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

            // Turn into an alert.

            AssertJsonSuccess(EditSearch(search, true, "BogusDeviceToken4"));
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, true));
            AssertSearches(employer, "BogusDeviceToken4", MakeTupleTuple(search, AlertType.AppleDevice, true));

            // Delete alert.

            AssertJsonSuccess(EditSearch(search, false, "BogusDeviceToken"));
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestUnknownSearch()
        {
            var employer = CreateEmployer(0);
            LogIn(employer);
            AssertSearches(Searches());
            AssertSearches(employer, null);

            // Update the name.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Id = Guid.NewGuid(), Name = string.Format(NameFormat, 0), Criteria = criteria };
            AssertJsonError(EditSearch(HttpStatusCode.NotFound, search, false, string.Empty), null, "400", "The search cannot be found.");
            AssertSearches(Searches());
            AssertSearches(employer, null);
        }

        [TestMethod]
        public void TestNoName()
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

            var originalName = search.Name;
            search.Name = null;
            AssertJsonError(EditSearch(search, false, "BogusDeviceToken"), "Name", "The name is required.");

            search.Name = originalName;
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestSameName()
        {
            var employer = CreateEmployer(0);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search0 = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria.Clone() };
            _memberSearchAlertsCommand.CreateMemberSearch(employer, search0);
            CreateDevice(employer.Id, "BogusDeviceToken");

            var search1 = new MemberSearch { Name = string.Format(NameFormat, 1), Criteria = criteria.Clone() };
            _memberSearchAlertsCommand.CreateMemberSearch(employer, search1);

            LogIn(employer);
            AssertSearches(Searches(), MakeTupleTuple(search0, AlertType.AppleDevice, false), MakeTupleTuple(search1, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search0, AlertType.AppleDevice, false), MakeTupleTuple(search1, AlertType.AppleDevice, false));

            // Update it to another.

            var originalName = search1.Name;
            search1.Name = search0.Name;
            AssertJsonError(EditSearch(search1, false, "BogusDeviceToken"), "Name", "The name is already being used.");

            search1.Name = originalName;
            AssertSearches(Searches(), MakeTupleTuple(search0, AlertType.AppleDevice, false), MakeTupleTuple(search1, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search0, AlertType.AppleDevice, false), MakeTupleTuple(search1, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestNoCriteria()
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

            search.Criteria = null;
            AssertJsonError(EditSearch(search, false, "BogusDeviceToken"), "Criteria", "The criteria cannot be empty.");

            search.Criteria = criteria;
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestEmptyCriteria()
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

            search.Criteria = new MemberSearchCriteria();
            AssertJsonError(EditSearch(search, false, "BogusDeviceToken"), "Criteria", "The criteria cannot be empty.");

            search.Criteria = criteria;
            AssertSearches(Searches(), MakeTupleTuple(search, AlertType.AppleDevice, false));
            AssertSearches(employer, "BogusDeviceToken", MakeTupleTuple(search, AlertType.AppleDevice, false));
        }

        [TestMethod]
        public void TestCannotEditOtherSearch()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(Keywords);
            var search = new MemberSearch { Name = string.Format(NameFormat, 0), Criteria = criteria };
            _memberSearchAlertsCommand.CreateMemberSearch(employer0, search);
            CreateDevice(employer0.Id, "BogusDeviceToken");

            LogIn(employer1);
            AssertSearches(Searches());
            AssertSearches(employer1, string.Empty);

            // Try to update the name.

            search.Name = string.Format(NameFormat, 1);
            AssertJsonError(EditSearch(search, false, string.Empty), null, "301", "You do not have sufficient permissions.");
        }

        private JsonResponseModel EditSearch(HttpStatusCode expectedStatusCode, MemberSearch search, bool isAlert, string deviceToken)
        {
            var model = new MemberSearchRequestModel
            {
                Name = search.Name,
                IsAlert = isAlert,
                Criteria = search.Criteria,
                DeviceToken = deviceToken,
            };
            return Deserialize<JsonResponseModel>(Put(expectedStatusCode, GetSearchUrl(search.Id), JsonContentType, Serialize(model, new MemberSearchModelJavaScriptConverter())));
        }

        private JsonResponseModel EditSearch(MemberSearch search, bool isAlert, string deviceToken)
        {
            var model = new MemberSearchRequestModel
            {
                Name = search.Name,
                IsAlert = isAlert,
                Criteria = search.Criteria,
                DeviceToken = deviceToken,
            };

            var converters = new JavaScriptConverter[]
            {
                new MemberSearchModelJavaScriptConverter(),
                new MemberSearchCriteriaJavaScriptConverter(_locationQuery, _industriesQuery),
            };
            return Deserialize<JsonResponseModel>(Put(GetSearchUrl(search.Id), JsonContentType, Serialize(model, converters)));
        }
    }
}
