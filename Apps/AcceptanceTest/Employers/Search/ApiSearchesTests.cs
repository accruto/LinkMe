using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Alerts;
using LinkMe.Apps.Agents.Communications.Alerts.Commands;
using LinkMe.Apps.Agents.Communications.Alerts.Queries;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class ApiSearchesTests
        : SearchTests
    {
        private readonly IMemberSearchesQuery _memberSearchesQuery = Resolve<IMemberSearchesQuery>();
        private readonly IMemberSearchAlertsCommand _memberSearchAlertsCommand = Resolve<IMemberSearchAlertsCommand>();
        private readonly IMemberSearchAlertsQuery _memberSearchAlertsQuery = Resolve<IMemberSearchAlertsQuery>();

        private ReadOnlyUrl _saveSearchUrl;
        private const string NameFormat = "My new search {0}";
        private const string KeywordsFormat = "business analyst {0}";

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();

            _saveSearchUrl = new ReadOnlyApplicationUrl("~/employers/searches/api/save");
        }

        [TestMethod]
        public void TestSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            // New search.

            var keywords1 = Search(1);
            var name1 = string.Format(NameFormat, 1);
            var model = SaveSearch(name1, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name1, keywords1 } }, new Dictionary<string, string>());

            // Another search.

            var keywords2 = Search(2);
            var name2 = string.Format(NameFormat, 2);
            model = SaveSearch(name2, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name1, keywords1 }, { name2, keywords2 } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestAlert()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            // New alert.

            var keywords1 = Search(1);
            var name1 = string.Format(NameFormat, 1);
            var model = SaveSearch(name1, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name1, keywords1 } });

            // Another alert.

            var keywords2 = Search(2);
            var name2 = string.Format(NameFormat, 2);
            model = SaveSearch(name2, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name1, keywords1 }, { name2, keywords2} });
        }

        [TestMethod]
        public void TestSearchSameNameSameSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            var keywords1 = Search(1);

            // New search.

            var name = string.Format(NameFormat, 1);
            var model = SaveSearch(name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords1 } }, new Dictionary<string, string>());

            // Save again.

            var keywords2 = string.Format(KeywordsFormat, 2);
            Get(GetPartialSearchUrl(keywords2));
            model = SaveSearch(name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords2 } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestConvertSearchSameNameSameSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            var keywords1 = Search(1);

            // New search.

            var name = string.Format(NameFormat, 1);
            var model = SaveSearch(name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords1 } }, new Dictionary<string, string>());

            // Save again as an alert.

            var keywords2 = string.Format(KeywordsFormat, 2);
            Get(GetPartialSearchUrl(keywords2));
            model = SaveSearch(name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name, keywords2 } });
        }

        [TestMethod]
        public void TestAlertSameNameSameSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            var keywords1 = Search(1);

            // New alert.

            var name = string.Format(NameFormat, 1);
            var model = SaveSearch(name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name, keywords1 } });

            // Save again.

            var keywords2 = string.Format(KeywordsFormat, 2);
            Get(GetPartialSearchUrl(keywords2));
            model = SaveSearch(name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name, keywords2 } });
        }

        [TestMethod]
        public void TestConvertAlertSameNameSameSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            var keywords1 = Search(1);

            // New alert.

            var name = string.Format(NameFormat, 1);
            var model = SaveSearch(name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { name, keywords1 } });

            // Save not as alert.

            var keywords2 = string.Format(KeywordsFormat, 2);
            Get(GetPartialSearchUrl(keywords2));
            model = SaveSearch(name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords2 } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestSearchSameNameDifferentSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);
            var keywords0 = Search(0);

            // New search.

            var name = string.Format(NameFormat, 1);
            var model = SaveSearch(name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords0 } }, new Dictionary<string, string>());

            // Save again after new search.

            Search(1);
            model = SaveSearch(name, false);
            AssertJsonError(model, "Name", "The name is already being used.");
            AssertSearches(employer.Id, new Dictionary<string, string> { { name, keywords0 } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestSavedSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var search = CreateSearch(employer, false, 1);

            // Should be able to save from the saved search page.

            Get(GetSavedSearchUrl(search.Id));
            var model = SaveSearch(search.Name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { search.Name, search.Criteria.GetKeywords() } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestSavedAlert()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var search = CreateSearch(employer, true, 1);

            // Should be able to save from the saved search page.

            Get(GetSavedSearchUrl(search.Id));
            var model = SaveSearch(search.Name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { search.Name, search.Criteria.GetKeywords() } });
        }

        [TestMethod]
        public void TestConvertSavedSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var search = CreateSearch(employer, false, 1);

            // Should be able to save from the saved search page.

            Get(GetSavedSearchUrl(search.Id));
            var model = SaveSearch(search.Name, true);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string>(), new Dictionary<string, string> { { search.Name, search.Criteria.GetKeywords() } });
        }

        [TestMethod]
        public void TestConvertSavedAlert()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var search = CreateSearch(employer, true, 1);

            // Should be able to save from the saved search page.

            Get(GetSavedSearchUrl(search.Id));
            var model = SaveSearch(search.Name, false);
            AssertJsonSuccess(model);
            AssertSearches(employer.Id, new Dictionary<string, string> { { search.Name, search.Criteria.GetKeywords() } }, new Dictionary<string, string>());
        }

        [TestMethod]
        public void TestSavedSearchSameNameDifferentSavedSearch()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var search1 = CreateSearch(employer, false, 1);
            var search2 = CreateSearch(employer, false, 2);

            // Save again with same name.

            Get(GetSavedSearchUrl(search1.Id));
            var model = SaveSearch(search2.Name, false);
            AssertJsonError(model, "Name", "The name is already being used.");
            AssertSearches(
                employer.Id,
                new Dictionary<string, string>
                    {
                        { search1.Name, search1.Criteria.GetKeywords() },
                        { search2.Name, search2.Criteria.GetKeywords() }
                    },
                new Dictionary<string, string>());
        }

        private string Search(int index)
        {
            var keywords = string.Format(KeywordsFormat, index);

            // Do a search.

            Get(GetSearchUrl());
            _keywordsTextBox.Text = keywords;
            _searchButton.Click();

            return keywords;
        }

        private MemberSearch CreateSearch(IUser employer, bool isAlert, int index)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(string.Format(KeywordsFormat, index));
            var search = new MemberSearch
            {
                Name = string.Format(NameFormat, index),
                Criteria = criteria,
            };

            if (isAlert)
                _memberSearchAlertsCommand.CreateMemberSearchAlert(employer, search, AlertType.Email);
            else
                _memberSearchAlertsCommand.CreateMemberSearch(employer, search);
            return search;
        }

        private void AssertSearches(Guid employerId, ICollection<KeyValuePair<string, string>> areNotAlerts, ICollection<KeyValuePair<string, string>> areAlerts)
        {
            var searches = _memberSearchesQuery.GetMemberSearches(employerId);
            Assert.AreEqual(areNotAlerts.Count + areAlerts.Count, searches.Count);

            var alerts = _memberSearchAlertsQuery.GetMemberSearchAlerts(from s in searches select s.Id, null);
            Assert.AreEqual(areAlerts.Count, alerts.Count);

            foreach (var expectedSearch in areNotAlerts)
            {
                // Must be a search.

                var name = expectedSearch.Key;
                var search = (from s in searches where s.Name == name select s).SingleOrDefault();
                Assert.IsNotNull(search);
                Assert.AreEqual(expectedSearch.Value, search.Criteria.GetKeywords());

                // Must not be an alert.

                var alert = (from a in alerts where a.MemberSearchId == search.Id select a).SingleOrDefault();
                Assert.IsNull(alert);
            }

            foreach (var expectedSearch in areAlerts)
            {
                // Must be a search.

                var name = expectedSearch.Key;
                var search = (from s in searches where s.Name == name select s).SingleOrDefault();
                Assert.IsNotNull(search);
                Assert.AreEqual(expectedSearch.Value, search.Criteria.GetKeywords());

                // Must be an alert.

                var alert = (from a in alerts where a.MemberSearchId == search.Id select a).SingleOrDefault();
                Assert.IsNotNull(alert);
            }
        }

        private JsonResponseModel SaveSearch(string name, bool isAlert)
        {
            var url = _saveSearchUrl.AsNonReadOnly();
            url.QueryString.Add("name", name);
            url.QueryString.Add("isAlert", isAlert.ToString());

            return Deserialize<JsonResponseModel>(Post(url));
        }
    }
}
