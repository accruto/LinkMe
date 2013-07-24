using LinkMe.AcceptanceTest.Navigation;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class SearchRedirectTests
        : RedirectTests
    {
        [TestMethod]
        public void TestRedirects()
        {
            var redirectedUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");

            var url = new ReadOnlyApplicationUrl("~/search/resumes/SimpleSearch.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            url = new ReadOnlyApplicationUrl("~/search/resumes/AdvancedSearch.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            redirectedUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates/results");
            url = new ReadOnlyApplicationUrl(true, "~/search/candidates/current");
            AssertRedirect(url, redirectedUrl, redirectedUrl);

            redirectedUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates/tips");
            url = new ReadOnlyApplicationUrl(true, "~/search/resumes/Tips.aspx");
            AssertRedirect(url, redirectedUrl, redirectedUrl);
        }
    }
}
