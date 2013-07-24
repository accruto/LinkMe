using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Affiliations.Verticals;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Navigation
{
    [TestClass]
    public class SeoTests
        : WebTestClass
    {
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        private readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        private readonly IContentEngine _contentEngine = Resolve<IContentEngine>();

        [TestMethod]
        public void TestSiteMapValid()
        {
            // Get the site map file.

            var url = GetSiteMapUrl();
            Get(url);
            AssertUrl(url);

            // Parse the file and validate each url.

            foreach (var loc in Browser.CurrentHtml.DocumentNode.SelectNodes("/urlset/url/loc"))
                Validate(new ReadOnlyApplicationUrl(loc.InnerText));
        }

        [TestMethod]
        public void TestBingAuthentication()
        {
            var url = new ReadOnlyApplicationUrl("~/BingSiteAuth.xml");
            Get(url);
            AssertUrl(url);

            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("/users/user");
            Assert.AreEqual("7FD72FBD406C9BDB6A7B1FF994F35B0F", node.InnerText);
        }

        [TestMethod]
        public void TestCanonicalLink()
        {
            TestCanonicalLink("~/");
            TestCanonicalLink("~/jobs");
            TestCanonicalLink("~/guests/Friends.aspx");
            TestCanonicalLink("~/members/resources");
        }

        [TestMethod]
        public void TestVerticalCanonicalLink()
        {
            var community = TestCommunity.Gta.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var vertical = _verticalsCommand.GetVertical(community);

            TestCanonicalLink(vertical, "");
            TestCanonicalLink(vertical, "jobs");
            TestCanonicalLink(vertical, "guests/Friends.aspx");
            TestCanonicalLink(vertical, "members/resources");
        }

        private void TestCanonicalLink(string path)
        {
            var url = new ApplicationUrl(path);
            Get(url);
            AssertCanonicalLink(url);

            url.QueryString["aaa"] = "bbb";
            url.QueryString["ccc"] = "ddd";
            Get(url);
            AssertCanonicalLink(url);
        }

        private void TestCanonicalLink(Vertical vertical, string path)
        {
            var url = vertical.GetVerticalHostUrl(path).AsNonReadOnly();
            var nonVerticalUrl = new ReadOnlyApplicationUrl("~/" + path);

            Get(url);
            AssertCanonicalLink(nonVerticalUrl);

            url.QueryString["aaa"] = "bbb";
            url.QueryString["ccc"] = "ddd";
            Get(url);
            AssertCanonicalLink(nonVerticalUrl);
        }

        private void AssertCanonicalLink(ReadOnlyUrl url)
        {
            var node = Browser.CurrentHtml.DocumentNode.SelectSingleNode("/html/head/link[@rel='canonical']");
            Assert.AreEqual(url.AbsolutePath, node.Attributes["href"].Value);
        }

        private void Validate(ReadOnlyUrl url)
        {
            // Get the url.

            try
            {
                Get(url);
            }
            catch (BadStatusException e)
            {
                Assert.Fail("Status code '" + e.Status + "' returned when trying to get '" + url + "'.");
            }

            // Check that there was no redirect.

            Assert.AreEqual(url.ToString().ToLower(), Browser.CurrentUrl.AbsoluteUri.ToLower());
        }

        private static ReadOnlyUrl GetSiteMapUrl()
        {
            return new ReadOnlyApplicationUrl("~/sitemap.xml");
        }
    }
}

