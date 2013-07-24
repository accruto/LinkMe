using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class HomeRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestCidParameter()
        {
            var homeUrl = HomeUrl.AsNonReadOnly();
            homeUrl.QueryString["cid"] = "BP-BP.com-Head-Tabs-jobs";
            Get(homeUrl);

            // Make sure a redirect occurred stripping out the parameter.

            AssertUrl(HomeUrl);
        }

        [TestMethod]
        public void TestHomePageRedirect()
        {
            // Get the web site root.

            var url = new ReadOnlyApplicationUrl("~/");
            AssertNoRedirect(url);

            Get(url);
            AssertUrl(HomeUrl);

            // Get the default page which should be redirected.

            var defaultUrl = new ReadOnlyApplicationUrl("~/default.aspx");
            AssertRedirect(defaultUrl, url, url);

            // Check that query strings are maintained.

            url = new ReadOnlyApplicationUrl("~/?name=value");
            AssertNoRedirect(url);

            // Get the default page which should be redirected.

            defaultUrl = new ReadOnlyApplicationUrl("~/default.aspx?name=value");
            AssertRedirect(defaultUrl, url, url);
        }

        [TestMethod]
        public void TestRef()
        {
            // yellowpages.com.au: http://www.linkme.com.au/?ref=ypnav

            var queryString = "?ref=ypnav";
            var url = new ReadOnlyApplicationUrl("~/" + queryString);
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);

            // whitepages.com.au: http://www.linkme.com.au/?ref=wpnav

            queryString = "?ref=wpnav";
            url = new ReadOnlyApplicationUrl("~/" + queryString);
            redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestAssessMe()
        {
            var url = new ReadOnlyApplicationUrl("~/ui/registered/networkers/assessmeform.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestGuests()
        {
            var url = new ReadOnlyApplicationUrl("~/guests/Messages.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/guests/ReferEmployer.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestHomeAspx()
        {
            var url = new ReadOnlyApplicationUrl("~/ui/registered/Home.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestErrorPages()
        {
            var url = new ReadOnlyApplicationUrl("~/ui/FileNotFoundError.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/");
            AssertRedirect(url, redirectUrl, redirectUrl);
            
            url = new ReadOnlyApplicationUrl("~/ui/ServerError.aspx");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }
    }
}
