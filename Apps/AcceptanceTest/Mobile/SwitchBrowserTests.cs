using System;
using System.Web;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Mobile
{
    [TestClass]
    public class SwitchBrowserTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();

        private ReadOnlyUrl _switchBrowserUrl;
        private ReadOnlyUrl _membersHomeUrl;
        private ReadOnlyUrl _membersJobsUrl;
        private ReadOnlyUrl _searchJobsUrl;
        private ReadOnlyUrl _membersApplicationsUrl;
        private ReadOnlyUrl _previousApplicationsUrl;
        private ReadOnlyUrl _suggestedJobsUrl;
        private ReadOnlyUrl _mobileFolderUrl;
        private ReadOnlyUrl _searchesUrl;
        private ReadOnlyUrl _recentSearchesUrl;
        private ReadOnlyUrl _savedSearchesUrl;

        private const string WebHtml = "<div class=\"nav-menu\">";
        private const string MobileHtml = "<div class=\"wrapper logo\">";

        [TestInitialize]
        public void TestInitialize()
        {
            _switchBrowserUrl = new ReadOnlyApplicationUrl("~/browser/switch");
            _membersHomeUrl = new ReadOnlyApplicationUrl(true, "~/members/home");
            _membersJobsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs");
            _searchJobsUrl = new ReadOnlyApplicationUrl(true, "~/search/jobs");
            _membersApplicationsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/applications");
            _previousApplicationsUrl = new ReadOnlyApplicationUrl(true, "~/ui/registered/networkers/previousapplications.aspx");
            _suggestedJobsUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/suggested");
            _mobileFolderUrl = new ReadOnlyApplicationUrl(true, "~/members/jobs/folders/mobile");
            _searchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches");
            _recentSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/recent");
            _savedSearchesUrl = new ReadOnlyApplicationUrl(true, "~/members/searches/saved");
        }

        [TestMethod]
        public void TestHome()
        {
            Get(HomeUrl);
            AssertUrl(HomeUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(HomeUrl);
            AssertUrl(HomeUrl);
            
            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, HomeUrl));
            AssertUrl(HomeUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);
            AssertSwitchLink(true);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, HomeUrl));
            AssertUrl(HomeUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);
        }

        [TestMethod]
        public void TestMembersHome()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_membersHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_membersHomeUrl);
            AssertUrl(_membersHomeUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _membersHomeUrl));
            AssertUrl(LoggedInMemberHomeUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, LoggedInMemberHomeUrl));
            AssertUrl(LoggedInMemberHomeUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);
        }

        [TestMethod]
        public void TestMembersJobs()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_membersJobsUrl);
            AssertUrl(_searchJobsUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_membersJobsUrl);
            AssertUrl(_membersJobsUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _membersJobsUrl));
            AssertUrl(_searchJobsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _searchJobsUrl));
            AssertUrl(_searchJobsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);
        }

        [TestMethod]
        public void TestMembersApplications()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_membersApplicationsUrl);
            AssertUrl(_previousApplicationsUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_membersApplicationsUrl);
            AssertUrl(_membersApplicationsUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _membersApplicationsUrl));
            AssertUrl(_previousApplicationsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _previousApplicationsUrl));
            AssertUrl(_previousApplicationsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);
        }

        [TestMethod]
        public void TestSuggestedJobs()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_suggestedJobsUrl);
            AssertUrl(_suggestedJobsUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_suggestedJobsUrl);
            AssertUrl(_suggestedJobsUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _suggestedJobsUrl));
            AssertUrl(_suggestedJobsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _suggestedJobsUrl));
            AssertUrl(_suggestedJobsUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);
        }

        [TestMethod]
        public void TestMobileFolder()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_mobileFolderUrl);
            AssertUrl(_searchJobsUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_mobileFolderUrl);
            AssertUrl(_mobileFolderUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _mobileFolderUrl));
            AssertUrl(_searchJobsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _searchJobsUrl));
            AssertUrl(_searchJobsUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);
        }

        [TestMethod]
        public void TestSearches()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_searchesUrl);
            AssertUrl(_recentSearchesUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_searchesUrl);
            AssertUrl(_searchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _searchesUrl));
            AssertUrl(_recentSearchesUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _recentSearchesUrl));
            AssertUrl(_recentSearchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);
        }

        [TestMethod]
        public void TestRecentSearches()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_recentSearchesUrl);
            AssertUrl(_recentSearchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _recentSearchesUrl));
            AssertUrl(_recentSearchesUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _recentSearchesUrl));
            AssertUrl(_recentSearchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);
        }

        [TestMethod]
        public void TestSavedSearches()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);

            // Web specific HTML.

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Mobile specific HTML.

            Browser.UseMobileUserAgent = true;

            Get(_savedSearchesUrl);
            AssertUrl(_savedSearchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);

            // Switch back to web.

            Get(GetSwitchBrowserUrl(false, _savedSearchesUrl));
            AssertUrl(_savedSearchesUrl);

            AssertPageContains(WebHtml);
            AssertPageDoesNotContain(MobileHtml);

            // Switch back to mobile.

            Get(GetSwitchBrowserUrl(true, _savedSearchesUrl));
            AssertUrl(_savedSearchesUrl);

            AssertPageDoesNotContain(WebHtml);
            AssertPageContains(MobileHtml);
        }

        private ReadOnlyUrl GetSwitchBrowserUrl(bool mobile, ReadOnlyUrl returnUrl)
        {
            var url = _switchBrowserUrl.AsNonReadOnly();
            url.QueryString["mobile"] = mobile.ToString();
            url.QueryString["returnUrl"] = returnUrl.PathAndQuery;
            return url;
        }

        private void AssertSwitchLink(bool mobile)
        {
            var url = GetSwitchBrowserUrl(mobile, new ReadOnlyUrl(Browser.CurrentUrl));
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("//div[@id='footer']//div[@class='right-section']//div[@class='footer-link']/a[.='Mobile site']");
            Assert.IsNotNull(node);
            Assert.IsNotNull(node.Attributes);
            Assert.IsTrue(string.Compare(url.PathAndQuery, HttpUtility.HtmlDecode(node.Attributes["href"].Value), StringComparison.InvariantCultureIgnoreCase) == 0);
        }
    }
}
