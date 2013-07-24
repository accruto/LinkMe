using System;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Searches.Mobile
{
    [TestClass]
    public class SaveSearchesTests
        : SearchesTests
    {
        private ReadOnlyUrl _resultsUrl;
        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;
        private HtmlTextBoxTester _nameTextBox;
        private HtmlCheckBoxTester _createAlertCheckBox;
        private HtmlButtonTester _saveButton;
        private const string Keywords = "Archeologist";
        private const string SearchName = "My saved search";

        [TestInitialize]
        public void TestInitialize()
        {
            _resultsUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs/results");
            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "SEARCH");
            _nameTextBox = new HtmlTextBoxTester(Browser, "name");
            _createAlertCheckBox = new HtmlCheckBoxTester(Browser, "createAlert");
            _saveButton = new HtmlButtonTester(Browser, "SAVE");
        }

        [TestMethod]
        public void TestSaveSearch()
        {
            var member = CreateMember(0);
            LogIn(member);

            // Do a search.

            _keywordsTextBox.Text = Keywords;
            _searchButton.Click();
            AssertUrl(_resultsUrl);

            // Save the search.

            Get(_saveSearchesUrl);
            AssertUrl(_saveSearchesUrl);

            _nameTextBox.Text = SearchName;
            _createAlertCheckBox.IsChecked = false;
            _saveButton.Click();

            AssertUrlWithoutQuery(_resultsUrl);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null);
            criteria.Distance = 0;
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            Get(_savedSearchesUrl);
            AssertSavedSearches(Tuple.Create(search, false));

            // Check the database.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(search.Name, searches[0].Name);
            Assert.AreEqual(search.Criteria, searches[0].Criteria);

            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts(new[] {searches[0].Id});
            Assert.AreEqual(0, alerts.Count);
        }

        [TestMethod]
        public void TestSaveAlert()
        {
            var member = CreateMember(0);
            LogIn(member);

            // Do a search.

            _keywordsTextBox.Text = Keywords;
            _searchButton.Click();
            AssertUrl(_resultsUrl);

            // Save the search.

            Get(_saveSearchesUrl);
            AssertUrl(_saveSearchesUrl);

            _nameTextBox.Text = SearchName;
            _createAlertCheckBox.IsChecked = true;
            _saveButton.Click();

            AssertUrlWithoutQuery(_resultsUrl);

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords(Keywords);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null);
            criteria.Distance = 0;
            var search = new JobAdSearch { Criteria = criteria, Name = SearchName };
            Get(_savedSearchesUrl);
            AssertSavedSearches(Tuple.Create(search, true));

            // Check the database.

            var searches = _jobAdSearchesQuery.GetJobAdSearches(member.Id);
            Assert.AreEqual(1, searches.Count);
            Assert.AreEqual(search.Name, searches[0].Name);
            Assert.AreEqual(search.Criteria, searches[0].Criteria);

            var alerts = _jobAdSearchAlertsQuery.GetJobAdSearchAlerts(new[] { searches[0].Id });
            Assert.AreEqual(1, alerts.Count);
            Assert.AreEqual(searches[0].Id, alerts[0].JobAdSearchId);
        }
    }
}