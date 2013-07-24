using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Communities
{
    [TestClass]
    public abstract class SimpleSearchTests
        : SearchTests
    {
        private ReadOnlyUrl _searchUrl;
        private ReadOnlyUrl _searchResultsUrl;
        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _searchUrl = new ReadOnlyApplicationUrl("~/search/jobs");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        protected override void Search(string jobTitle, bool? onlyCommunityJobAds)
        {
            Get(_searchUrl);
            _keywordsTextBox.Text = jobTitle;

            _searchButton.Click();
            AssertPath(_searchResultsUrl);
        }
    }
}