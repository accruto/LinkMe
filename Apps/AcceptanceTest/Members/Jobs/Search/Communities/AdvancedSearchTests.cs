using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Communities
{
    [TestClass]
    public abstract class AdvancedSearchTests
        : SearchTests
    {
        private ReadOnlyUrl _advancedSearchUrl;
        private ReadOnlyUrl _searchResultsUrl;
        private HtmlTextBoxTester _allWordsTextBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            _advancedSearchUrl = new ReadOnlyApplicationUrl("~/search/jobs/advanced");
            _searchResultsUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            _allWordsTextBox = new HtmlTextBoxTester(Browser, "KeywordsAdvanced");
            _searchButton = new HtmlButtonTester(Browser, "searchAdvanced");
        }

        protected override void Search(string jobTitle, bool? onlyCommunityJobAds)
        {
            Get(_advancedSearchUrl);
            _allWordsTextBox.Text = jobTitle;

            _searchButton.Click();
            AssertPath(_searchResultsUrl);
        }
    }
}