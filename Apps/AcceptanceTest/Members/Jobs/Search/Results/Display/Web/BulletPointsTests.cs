using System.Collections.Generic;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class BulletPointsTests
        : DisplayTests
    {
        private static readonly string[] BulletPoints = new [] { "One", "two", "Three" };

        [TestMethod]
        public void TestBulletPoints()
        {
            TestBulletPoints(BulletPoints);
        }

        [TestMethod]
        public void TestNoBulletPoints()
        {
            TestBulletPoints(null);
        }

        private void TestBulletPoints(IList<string> bulletPoints)
        {
            var jobAd = CreateJobAd(bulletPoints);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//ul[@class='bulletpoints']");
            Assert.IsNotNull(node);
            var nodes = node.SelectNodes("./li");
            if (bulletPoints.IsNullOrEmpty())
            {
                Assert.IsTrue(nodes.IsNullOrEmpty());
            }
            else
            {
                Assert.AreEqual(bulletPoints.Count, nodes.Count);
                for (var index = 0; index < nodes.Count; ++index)
                    Assert.AreEqual(bulletPoints[index], nodes[index].InnerText);
            }

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue(bulletPoints.NullableSequenceEqual(model.JobAds[0].BulletPoints));
        }

        private JobAd CreateJobAd(IList<string> bulletPoints)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Description.BulletPoints = bulletPoints;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
