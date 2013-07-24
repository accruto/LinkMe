using System.Collections.Generic;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navs
{
    [TestClass]
    public class AnonymousMemberNavTests
        : NavTests
    {
        private static ReadOnlyApplicationUrl _homeUrl;

        private static ReadOnlyApplicationUrl _profileUrl;
        private const string ProfileText = "Profile";
        private static ReadOnlyApplicationUrl _friendsUrl;
        private const string FriendsText = "Friends";

        private static ReadOnlyApplicationUrl _jobsUrl;
        private const string JobsText = "Jobs";

        private static ReadOnlyApplicationUrl _resourcesUrl;
        private const string ResourcesText = "Resources";

        private static ReadOnlyApplicationUrl _employerUrl;

        private static ReadOnlyApplicationUrl _loginUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _homeUrl = new ReadOnlyApplicationUrl("~/");

            _profileUrl = new ReadOnlyApplicationUrl("~/guests/profile");
            _friendsUrl = new ReadOnlyApplicationUrl("~/guests/Friends.aspx");

            _jobsUrl = new ReadOnlyApplicationUrl("~/search/jobs");

            _resourcesUrl = new ReadOnlyApplicationUrl("~/members/resources");

            _employerUrl = new ReadOnlyApplicationUrl(true, "~/employers", new ReadOnlyQueryString("ignorePreferred", "true"));

            _loginUrl = new ReadOnlyApplicationUrl(true, "~/login");
        }

        [TestMethod]
        public void TestNavs()
        {
            Browser.UseMobileUserAgent = false;
            TestNavs(null);
        }

        [TestMethod]
        public void TestOverrideMobileNavs()
        {
            // Override the browser.

            Browser.UseMobileUserAgent = true;
            SwitchBrowser(false);

            TestNavs(null);
        }

        [TestMethod]
        public void TestVerticalNavs()
        {
            Browser.UseMobileUserAgent = false;
            TestNavs(TestCommunity.Scouts);
        }

        private void TestNavs(TestCommunity? testCommunity)
        {
            Set(testCommunity);
            var navs = GetNavs();
            SetNavs(_homeUrl, navs);
            TestNavs(_homeUrl);
            TestNavs(navs);
        }

        private static IList<Nav> GetNavs()
        {
            return new List<Nav>
            {
                new Nav
                {
                    Url = _profileUrl,
                    Text = ProfileText,
                },
                new Nav
                {
                    Url = _jobsUrl,
                    Text = JobsText,
                },
                new Nav
                {
                    Url = _friendsUrl,
                    Text = FriendsText,
                },
                new Nav
                {
                    Url = _resourcesUrl,
                    Text = ResourcesText,
                },
            };
        }

        private void Set(TestCommunity? testCommunity)
        {
            if (testCommunity == null)
                return;

            var community = testCommunity.Value.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            Get(new ReadOnlyApplicationUrl("~/" + _verticalsQuery.GetVertical(community.Id).Url));
        }

        protected override void AssertHeaderNavs()
        {
            // Second row.

            AssertEmployerSwitchNav(_employerUrl);

            // Third row.

            if (Browser.CurrentUrl.PathAndQuery == _homeUrl.PathAndQuery)
                AssertNoLoginNav();
            else
                AssertLoginNav(_loginUrl);
            AssertNoSettingsNav();
            AssertNoLogoutNav();
        }
    }
}