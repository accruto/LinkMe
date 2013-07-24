using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Landing
{
    [TestClass]
    public class JobSearchTests
        : WebTestClass
    {
        private HtmlTextBoxTester _txtKeywords;
        private HtmlTextBoxTester _txtLocation;
        private HtmlButtonTester _btnSearch;

        private HtmlTextBoxTester _txtSampleKeywords;
        private HtmlTextBoxTester _txtSampleLocation;
        private HtmlButtonTester _btnSampleSearch;

        private const string Keywords = "Architect";
        private const string Location = "Melbourne VIC 3000";

        private ReadOnlyUrl _landingSearchUrl;
        private ReadOnlyUrl _landingSimpleSearchUrl;
        private ReadOnlyUrl _landingSampleSearchUrl;
        private ReadOnlyUrl _searchResultsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            _txtKeywords = new HtmlTextBoxTester(Browser, "ctl00_Body_ucJobSearch_txtKeywords");
            _txtLocation = new HtmlTextBoxTester(Browser, "ctl00_Body_ucJobSearch_txtLocation");
            _btnSearch = new HtmlButtonTester(Browser, "ctl00_Body_btnSearch");

            _txtSampleKeywords = new HtmlTextBoxTester(Browser, "LinkMeKeywords");
            _txtSampleLocation = new HtmlTextBoxTester(Browser, "LinkMeLocation");
            _btnSampleSearch = new HtmlButtonTester(Browser, "LinkMeJobSearchSubmit");

            _landingSearchUrl = new ReadOnlyApplicationUrl("~/landing/search/jobs");
            _landingSimpleSearchUrl = new ReadOnlyApplicationUrl("~/landing/search/jobs/SimpleSearch.aspx");
            _landingSampleSearchUrl = new ReadOnlyApplicationUrl("~/landing/search/jobs/Sample.aspx");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
        }

        [TestMethod]
        public void TestDefaults()
        {
            Get(_landingSearchUrl);
            AssertUrl(_landingSearchUrl);
            AssertDefaults();

            Get(_landingSimpleSearchUrl);
            AssertUrl(_landingSearchUrl);
            AssertDefaults();
        }

        [TestMethod]
        public void TestSearch()
        {
            Get(_landingSearchUrl);
            Search(Keywords, Location);
            AssertSearchResults(Keywords, Location);
        }

        [TestMethod]
        public void TestSampleSearch()
        {
            Get(_landingSampleSearchUrl);
            SampleSearch(Keywords, Location);
            AssertSearchResults(Keywords, Location);
        }

        private void AssertDefaults()
        {
            Assert.IsTrue(_txtKeywords.IsVisible);
            Assert.AreEqual(string.Empty, _txtKeywords.Text);
            Assert.IsTrue(_txtLocation.IsVisible);
            Assert.AreEqual(string.Empty, _txtLocation.Text);
            Assert.IsTrue(_btnSearch.IsVisible);
        }

        private void Search(string keywords, string location)
        {
            _txtKeywords.Text = keywords;
            _txtLocation.Text = location;
            _btnSearch.Click();
        }

        private void SampleSearch(string keywords, string location)
        {
            _txtSampleKeywords.Text = keywords;
            _txtSampleLocation.Text = location;
            _btnSampleSearch.Click();
        }

        private void AssertSearchResults(string keywords, string location)
        {
            AssertUrl(_searchResultsUrl);
            AssertPageContains(keywords);
            AssertPageContains(location);
        }
    }
}
