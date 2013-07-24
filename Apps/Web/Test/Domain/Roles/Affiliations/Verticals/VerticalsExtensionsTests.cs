using LinkMe.Apps.Agents.Applications;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Web.Domain.Roles.Affiliations.Verticals;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Domain.Roles.Affiliations.Verticals
{
    [TestClass]
    public class VerticalsExtensionsTests
    {
        private const string Host = "careernetwork.hrcareers.com.au";
        private const string SecondaryHost = "ahricareers.linkme.com.au";
        private const string TertiaryHost = "something.linkme.com.au";
        private const string NonVerticalHost = "www.linkme.com.au";

        [TestInitialize]
        public void TestInitialize()
        {
            ApplicationSetUp.SetCurrentApplication(WebSite.LinkMe);
        }

        [TestMethod]
        public void TestDeletedRedirectUrl()
        {
            // Not deleted, no secondary host.

            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host }, Host, null);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host }, "aaa." + Host, null);

            // Deleted, no secondary host.

            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host }, Host, NonVerticalHost);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host }, "aaa." + Host, NonVerticalHost);

            // Not deleted, secondary host.

            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host, SecondaryHost = SecondaryHost }, Host, null);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host, SecondaryHost = SecondaryHost }, "aaa." + Host, null);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host, SecondaryHost = SecondaryHost }, SecondaryHost, null);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = false, Host = Host, SecondaryHost = SecondaryHost }, "aaa." + SecondaryHost, null);

            // Deleted, secondary host.

            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host, SecondaryHost = SecondaryHost }, Host, NonVerticalHost);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host, SecondaryHost = SecondaryHost }, "aaa." + Host, NonVerticalHost);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host, SecondaryHost = SecondaryHost }, SecondaryHost, NonVerticalHost);
            TestDeletedRedirectUrl(new Vertical { IsDeleted = true, Host = Host, SecondaryHost = SecondaryHost }, "aaa." + SecondaryHost, NonVerticalHost);
        }

        [TestMethod]
        public void TestSecondaryHostRedirectUrl()
        {
            // No secondary host.

            TestSecondaryHostRedirectUrl(new Vertical { Host = Host }, Host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = Host }, "aaa." + Host, null);

            // Secondary host.

            TestSecondaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost }, Host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost }, "aaa." + Host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost }, SecondaryHost, Host);
            TestSecondaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost }, "aaa." + SecondaryHost, "aaa." + Host);
        }

        [TestMethod]
        public void TestTertiaryHostRedirectUrl()
        {
            // No secondary host.

            TestTertiaryHostRedirectUrl(new Vertical { Host = Host }, Host, null);
            TestTertiaryHostRedirectUrl(new Vertical { Host = Host }, "aaa." + Host, null);

            // Tertiary host.

            TestTertiaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost, TertiaryHost = TertiaryHost }, Host, null);
            TestTertiaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost, TertiaryHost = TertiaryHost }, "aaa." + Host, null);
            TestTertiaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost, TertiaryHost = TertiaryHost }, TertiaryHost, Host);
            TestTertiaryHostRedirectUrl(new Vertical { Host = Host, SecondaryHost = SecondaryHost, TertiaryHost = TertiaryHost }, "aaa." + TertiaryHost, "aaa." + Host);
        }

        [TestMethod]
        public void TestNursingCentreSecondaryHostRedirectUrl()
        {
            const string host = "careers.thenursingcentre.com.au";
            const string secondaryHost = "thenursingcentre.com.au";

            // No secondary host.

            TestSecondaryHostRedirectUrl(new Vertical { Host = host }, host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = host }, "aaa." + host, null);

            // Secondary host.

            TestSecondaryHostRedirectUrl(new Vertical { Host = host, SecondaryHost = secondaryHost }, host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = host, SecondaryHost = secondaryHost }, "aaa." + host, null);
            TestSecondaryHostRedirectUrl(new Vertical { Host = host, SecondaryHost = secondaryHost }, secondaryHost, host);
            TestSecondaryHostRedirectUrl(new Vertical { Host = host, SecondaryHost = secondaryHost }, "aaa." + secondaryHost, "aaa." + host);
        }

        private static void TestDeletedRedirectUrl(Vertical vertical, string host, string expectedHost)
        {
            var url = new ApplicationUrl("~/something/here") {Host = host};
            var redirectUrl = vertical.GetDeletedRedirectUrl(url, NonVerticalHost);

            if (!vertical.IsDeleted)
            {
                Assert.IsNull(redirectUrl);
                Assert.IsNull(expectedHost);
            }
            else
            {
                Assert.AreEqual(expectedHost, redirectUrl.Host);
            }
        }

        private static void TestSecondaryHostRedirectUrl(Vertical vertical, string host, string expectedHost)
        {
            var url = new ApplicationUrl("~/something/here") { Host = host };
            var redirectUrl = vertical.GetAlternativeHostRedirectUrl(url);

            if (expectedHost == null)
                Assert.IsNull(redirectUrl);
            else
                Assert.AreEqual(expectedHost, redirectUrl.Host);
        }

        private static void TestTertiaryHostRedirectUrl(Vertical vertical, string host, string expectedHost)
        {
            var url = new ApplicationUrl("~/something/here") { Host = host };
            var redirectUrl = vertical.GetAlternativeHostRedirectUrl(url);

            if (expectedHost == null)
                Assert.IsNull(redirectUrl);
            else
                Assert.AreEqual(expectedHost, redirectUrl.Host);
        }
    }
}
