using System.Collections.Generic;
using LinkMe.Apps.Presentation.Domain.Roles.JobAds;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Presentation.Test.Domain.Roles.JobAds
{
    [TestClass]
    public class SegmentTests
        : TestClass
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        private const int MaximumSegmentLength = 250;

        [TestMethod]
        public void TestIndustrySegments()
        {
            var employer = new Employer();
            var jobAd = employer.CreateTestJobAd();

            // No industries.

            jobAd.Description.Industries = null;
            var path = jobAd.GetJobRelativePath();
            AssertSegmentLength(path);
            AssertIndustrySegment(path, "-");

            // Add an industry.

            jobAd.Description.Industries = new List<Industry>();
            var industry = _industriesQuery.GetIndustries()[0];
            jobAd.Description.Industries.Add(industry);
            path = jobAd.GetJobRelativePath();
            AssertSegmentLength(path);
            AssertIndustrySegment(path, industry.UrlName);

            // Set a different industry.

            jobAd.Description.Industries = new List<Industry>();
            industry = _industriesQuery.GetIndustries()[1];
            jobAd.Description.Industries.Add(industry);
            path = jobAd.GetJobRelativePath();
            AssertSegmentLength(path);
            AssertIndustrySegment(path, industry.UrlName);

            // Set 2 industries.

            jobAd.Description.Industries = new List<Industry>();
            industry = _industriesQuery.GetIndustries()[0];
            jobAd.Description.Industries.Add(industry);
            industry = _industriesQuery.GetIndustries()[1];
            jobAd.Description.Industries.Add(industry);
            path = jobAd.GetJobRelativePath();
            AssertSegmentLength(path);
            AssertIndustrySegment(path, "-");

            // Add all industries.

            jobAd.Description.Industries = new List<Industry>();
            foreach (var allIndustry in _industriesQuery.GetIndustries())
                jobAd.Description.Industries.Add(allIndustry);
            path = jobAd.GetJobRelativePath();
            AssertSegmentLength(path);
            AssertIndustrySegment(path, "-");
        }

        private static void AssertIndustrySegment(string path, string expected)
        {
            // Industry is the second segment.

            var start = path.IndexOf('/') + 1;
            var end = path.IndexOf('/', start);
            var industry = path.Substring(start, end - start);
            Assert.AreEqual(expected, industry);
        }

        private static void AssertSegmentLength(string path)
        {
            var pos = path.IndexOf('/');
            while (pos != -1)
            {
                var segment = path.Substring(0, pos);
                Assert.IsTrue(segment.Length <= MaximumSegmentLength);
                path = path.Substring(pos + 1);
                pos = path.IndexOf('/');
            }

            Assert.IsTrue(path.Length <= MaximumSegmentLength);
        }
    }
}