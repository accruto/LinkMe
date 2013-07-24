using System;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Communities
{
    [TestClass]
    public class RedirectTests
        : CommunityTests
    {
        [TestMethod]
        public void RedirectPathTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            RedirectPathTest(community, "", false);
            RedirectPathTest(community, "join.aspx", true);
            RedirectPathTest(community, "login.aspx", true);
        }

        [TestMethod]
        public void RedirectSecondaryHostTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            RedirectHostTest(community, "", GetCommunitySecondaryHostUrl, false);
            RedirectHostTest(community, "join.aspx", GetCommunitySecondaryHostUrl, true);
            RedirectHostTest(community, "login.aspx", GetCommunitySecondaryHostUrl, true);
        }

        [TestMethod]
        public void RedirectTertiaryHostTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);

            RedirectHostTest(community, "", GetCommunityTertiaryHostUrl, false);
            RedirectHostTest(community, "join.aspx", GetCommunityTertiaryHostUrl, true);
            RedirectHostTest(community, "login.aspx", GetCommunityTertiaryHostUrl, true);
        }

        [TestMethod]
        public void RedirectDeletedCommunityPathTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var vertical = _verticalsCommand.GetVertical(community.Id);
            vertical.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical);

            RedirectDeletedCommunityPathTest(community, "", HomeUrl, false);
            RedirectDeletedCommunityPathTest(community, "join.aspx", new ReadOnlyApplicationUrl(true, "~/join"), true);
            RedirectDeletedCommunityPathTest(community, "login.aspx", new ReadOnlyApplicationUrl(true, "~/login"), true);
        }

        [TestMethod]
        public void RedirectDeletedCommunitySecondaryHostTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var vertical = _verticalsCommand.GetVertical(community.Id);
            vertical.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical);

            RedirectDeletedCommunityHostTest(community, "", GetCommunitySecondaryHostUrl, HomeUrl, false);
            RedirectDeletedCommunityHostTest(community, "join.aspx", GetCommunitySecondaryHostUrl, new ReadOnlyApplicationUrl(true, "~/join"), true);
            RedirectDeletedCommunityHostTest(community, "login.aspx", GetCommunitySecondaryHostUrl, new ReadOnlyApplicationUrl(true, "~/login"), true);
        }

        [TestMethod]
        public void RedirectDeletedCommunityTertiaryHostTest()
        {
            var community = TestCommunity.Crossroads.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var vertical = _verticalsCommand.GetVertical(community.Id);
            vertical.IsDeleted = true;
            _verticalsCommand.UpdateVertical(vertical);

            RedirectDeletedCommunityHostTest(community, "", GetCommunityTertiaryHostUrl, HomeUrl, false);
            RedirectDeletedCommunityHostTest(community, "join.aspx", GetCommunityTertiaryHostUrl, new ReadOnlyApplicationUrl(true, "~/join"), true);
            RedirectDeletedCommunityHostTest(community, "login.aspx", GetCommunityTertiaryHostUrl, new ReadOnlyApplicationUrl(true, "~/login"), true);
        }

        private void RedirectPathTest(Community community, string page, bool secure)
        {
            // Hit the landing page.

            var url = GetCommunityPathUrl(community, page);
            Get(url);

            // Check the redirection.

            var hostUrl = GetCommunityHostUrl(community, page);
            AssertUrl(hostUrl, Browser.CurrentUrl);

            // Check https.

            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = "https";
            Get(secureUrl);

            var secureHostUrl = hostUrl.AsNonReadOnly();
            secureHostUrl.Scheme = secure ? "https" : "http";
            AssertUrl(secureHostUrl, Browser.CurrentUrl);
        }

        private void RedirectDeletedCommunityPathTest(Community community, string page, ReadOnlyUrl expectedUrl, bool secure)
        {
            // Hit the landing page.

            var url = GetCommunityPathUrl(community, page);
            Get(url);

            // Should simply be redirected to the non-community page.

            AssertUrl(expectedUrl, Browser.CurrentUrl);

            // Check https.

            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = "https";
            Get(secureUrl);

            var secureExpectedUrl = expectedUrl.AsNonReadOnly();
            secureExpectedUrl.Scheme = secure ? "https" : "http";
            AssertUrl(secureExpectedUrl, Browser.CurrentUrl);
        }

        private void RedirectHostTest(Community community, string page, Func<Community, string, ReadOnlyUrl> getcommunityHostUrl, bool secure)
        {
            // Hit the landing page.

            var url = getcommunityHostUrl(community, page);
            Get(url);

            // Check the redirection.

            var hostUrl = GetCommunityHostUrl(community, page);
            AssertUrl(hostUrl, Browser.CurrentUrl);

            // Check https.

            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = "https";
            Get(secureUrl);

            var secureHostUrl = hostUrl.AsNonReadOnly();
            secureHostUrl.Scheme = secure ? "https" : "http";
            AssertUrl(secureHostUrl, Browser.CurrentUrl);
        }

        private void RedirectDeletedCommunityHostTest(Community community, string page, Func<Community, string, ReadOnlyUrl> getcommunityHostUrl, ReadOnlyUrl expectedUrl, bool secure)
        {
            // Hit the landing page.

            var url = getcommunityHostUrl(community, page);
            Get(url);

            // Should simply be redirected to the non-community page.

            AssertUrl(expectedUrl, Browser.CurrentUrl);

            // Check https.

            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = "https";
            Get(secureUrl);

            var secureExpectedUrl = expectedUrl.AsNonReadOnly();
            secureExpectedUrl.Scheme = secure ? "https" : "http";
            AssertUrl(secureExpectedUrl, Browser.CurrentUrl);
        }

        private static void AssertUrl(ReadOnlyUrl url, Uri currentUrl)
        {
            // Check that they are the same.

            if (url.AbsoluteUri.Equals(currentUrl.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
                return;

            // A change from http to https is ok.

            var secureUrl = url.AsNonReadOnly();
            secureUrl.Scheme = "https";
            Assert.IsTrue(secureUrl.AbsoluteUri.Equals(currentUrl.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
