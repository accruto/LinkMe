using LinkMe.Domain.Industries.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Seo
{
    [TestClass]
    public class JobsTests
        : SeoTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void TestJobAspx()
        {
            // No job ad id should simply be redirected.

            TestUrl(new ReadOnlyApplicationUrl("~/jobs"), new ReadOnlyApplicationUrl("~/jobs/Job.aspx"));
            TestUrl(new ReadOnlyApplicationUrl(true, "~/jobs"), new ReadOnlyApplicationUrl(true, "~/jobs/Job.aspx"));
        }

        [TestMethod]
        public void TestJobs()
        {
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs");
            TestUrl(expectedUrl);
            TestUrl(expectedUrl, new ReadOnlyApplicationUrl("~/jobs/"));
        }

        [TestMethod]
        public void TestIndustryJobs()
        {
            // Choose an industry that has a url alias.

            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlName + "-jobs");

            TestUrl(expectedUrl);
            TestUrls(
                expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlName + "-jobs/"),
                new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlName),
                new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlAliases[0] + "-jobs"),
                new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlAliases[0]));
        }

        [TestMethod]
        public void TestUnknownIndustryJobs()
        {
            // Unknown industries should be redirected back to jobs.

            const string industryUrlName = "none";
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs");

            TestUrls(
                expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/-/" + industryUrlName + "-jobs"),
                new ReadOnlyApplicationUrl("~/jobs/-/" + industryUrlName));
        }

        [TestMethod]
        public void TestLocationJobs()
        {
            var location = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs");

            TestUrl(expectedUrl);
            TestUrls(expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs/"),
                new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName));
        }

        [TestMethod]
        public void TestUnknownLocationJobs()
        {
            // Unknown locations should be redirected back to jobs.

            const string locationUrlName = "unknown";
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs");

            TestUrls(expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/" + locationUrlName + "-jobs"),
                new ReadOnlyApplicationUrl("~/jobs/" + locationUrlName));
        }

        [TestMethod]
        public void TestLocationIndustryJobs()
        {
            var location = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");
            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs/" + industry.UrlName + "-jobs");

            var locationSegments = new[]
            {
                location.UrlName + "-jobs",
                location.UrlName
            };
            var industrySegments = new[]
            {
                industry.UrlName + "-jobs",
                industry.UrlName,
                industry.UrlAliases[0] + "-jobs",
                industry.UrlAliases[0],
            };

            TestUrl(expectedUrl);

            foreach (var locationSegment in locationSegments)
            {
                foreach (var industrySegment in industrySegments)
                {
                    var url = new ReadOnlyApplicationUrl("~/jobs/" + locationSegment + "/" + industrySegment);
                    if (expectedUrl.Path != url.Path)
                        TestUrl(expectedUrl, url);
                }
            }
        }

        [TestMethod]
        public void TestLocationUnknownIndustryJobs()
        {
            const string industryUrlName = "none";
            var location = _locationQuery.GetRegion(_locationQuery.GetCountry("Australia"), "Melbourne");
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs");

            TestUrls(
                expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs/" + industryUrlName + "-jobs"),
                new ReadOnlyApplicationUrl("~/jobs/" + location.UrlName + "-jobs/" + industryUrlName));
        }

        [TestMethod]
        public void TestUnknownLocationIndustryJobs()
        {
            // Unknown locations should be redirected back to jobs.

            const string locationUrlName = "unknown";
            var industry = _industriesQuery.GetIndustry("Healthcare, Medical & Pharmaceutical");
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs/-/" + industry.UrlName + "-jobs");

            TestUrls(expectedUrl,
                new ReadOnlyApplicationUrl("~/jobs/" + locationUrlName + "-jobs/" + industry.UrlName + "-jobs"),
                new ReadOnlyApplicationUrl("~/jobs/" + locationUrlName + "/" + industry.UrlName + "-jobs"));
        }

        [TestMethod]
        public void TestUnknownLocationUnknownIndustryJobs()
        {
            const string locationUrlName = "unknown";
            const string industryUrlName = "none";
            var expectedUrl = new ReadOnlyApplicationUrl("~/jobs");

            var locationSegments = new[]
            {
                locationUrlName + "-jobs",
                locationUrlName
            };
            var industrySegments = new[]
            {
                industryUrlName + "-jobs",
                industryUrlName,
            };

            foreach (var locationSegment in locationSegments)
            {
                foreach (var industrySegment in industrySegments)
                    TestUrls(expectedUrl, new ReadOnlyApplicationUrl("~/jobs/" + locationSegment + "/" + industrySegment));
            }
        }
    }
}
