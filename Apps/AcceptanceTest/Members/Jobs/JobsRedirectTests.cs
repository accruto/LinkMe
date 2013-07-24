using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs
{
    [TestClass]
    public class JobsRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestJobs()
        {
            // Get the job page.

            var url = new ReadOnlyApplicationUrl("~/jobs");
            AssertNoRedirect(url);

            // Check that query strings are maintained.

            url = new ReadOnlyApplicationUrl("~/jobs/?name=value");
            var redirectUrl = new ReadOnlyApplicationUrl("~/jobs?name=value");
            AssertRedirect(url, redirectUrl, redirectUrl);

            url = new ReadOnlyApplicationUrl("~/jobs?name=value");
            AssertNoRedirect(url);
        }

        [TestMethod]
        public void TestOldUrls()
        {
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/SendJobToFriendPopupContents.aspx");
            var redirectUrl = new ReadOnlyApplicationUrl("~/jobs");
            AssertRedirect(url, redirectUrl, redirectUrl);
        }

        [TestMethod]
        public void TestBigpondLinks()
        {
            // bigpond.com

            const string queryString = "?Keywords=Blah&performSearch=True";
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString);
            var redirectUrl = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);
            var finalRedirectUrl = new ReadOnlyApplicationUrl("~/search/jobs/results");
            AssertRedirect(url, redirectUrl, finalRedirectUrl);
        }

        [TestMethod]
        public void TestTradingPostLinks()
        {
            // AdvancedSearch

            var queryString = "?performSearch=True&amp;Industries=fa9b69c7-4a3f-498c-a2c4-42addfb08f7d";
            var url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchAdvancedForm.aspx" + queryString + "&pcode=TPLNCH1");

            var redirectUrl1 = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchAdvancedForm.aspx" + queryString);
            var redirectUrl2 = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);

            AssertRedirect(url, redirectUrl1, redirectUrl2);
            AssertRedirect(redirectUrl1, redirectUrl2, redirectUrl2);

            // Search

            queryString = "?gglsrc=NavJobs";
            url = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString + "&pcode=TPLNCH1");

            redirectUrl1 = new ReadOnlyApplicationUrl("~/ui/unregistered/JobSearchForm.aspx" + queryString);
            redirectUrl2 = new ReadOnlyApplicationUrl("~/search/jobs" + queryString);

            AssertRedirect(url, redirectUrl1, redirectUrl2);
            AssertRedirect(redirectUrl1, redirectUrl2, redirectUrl2);
        }
    }
}
