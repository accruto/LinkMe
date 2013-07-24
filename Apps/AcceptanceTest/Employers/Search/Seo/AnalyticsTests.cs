using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Seo
{
    [TestClass]
    public class AnalyticsTests
        : SearchTests
    {
        [TestMethod]
        public void TestGclid()
        {
            // Search.

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords("mentor");

            var searchUrl = GetSearchUrl(criteria).AsNonReadOnly();
            var currentUrl = GetResultsUrl().AsNonReadOnly();
            currentUrl.Scheme = "https";

            Get(searchUrl);
            AssertUrl(currentUrl);

            // Attach google gclid parameter.

            searchUrl.QueryString["gclid"] = "ThisIsATest";
            currentUrl.QueryString["gclid"] = "ThisIsATest";

            Get(searchUrl);
            AssertUrl(currentUrl);
        }
    }
}
