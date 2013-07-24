using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Seo
{
    [TestClass]
    public class AnalyticsTests
        : SearchTests
    {
        [TestMethod]
        public void TestGclid()
        {
            // Search.

            var criteria = new JobAdSearchCriteria();
            criteria.SetKeywords("mentor");

            var searchUrl = GetSearchUrl(criteria).AsNonReadOnly();
            var currentUrl = GetResultsUrl().AsNonReadOnly();

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