using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Search.Results.Display.Web
{
    [TestClass]
    public class IndustriesTests
        : DisplayTests
    {
        [TestMethod]
        public void TestOneIndustry()
        {
            TestIndustries(_industriesQuery.GetIndustries()[3]);
        }

        [TestMethod]
        public void TestMultipleIndustry()
        {
            TestIndustries(_industriesQuery.GetIndustries()[3], _industriesQuery.GetIndustries()[5], _industriesQuery.GetIndustries()[8]);
        }

        [TestMethod]
        public void TestNoIndustry()
        {
            TestIndustries(new Industry[0]);
        }

        private void TestIndustries(params Industry[] industries)
        {
            var jobAd = CreateJobAd(industries);
            Search(Keywords);
            var node = GetResult(jobAd.Id);

            node = node.SelectSingleNode(".//div[@class='industry']/div[@class='desc']");
            Assert.IsNotNull(node);
            if (industries.IsNullOrEmpty())
                Assert.AreEqual("", node.InnerText);
            else
                Assert.IsTrue((from i in industries select i.Name).CollectionEqual(node.InnerText.Split(new[] {", "}, StringSplitOptions.RemoveEmptyEntries)));

            var model = ApiSearch(Keywords);
            Assert.AreEqual(1, model.JobAds.Count);
            Assert.IsTrue((from i in industries select i.Id).NullableCollectionEqual(model.JobAds[0].Industries));
        }

        private JobAd CreateJobAd(IList<Industry> industries)
        {
            var employer = CreateEmployer(0);
            var jobAd = employer.CreateTestJobAd(Keywords);
            jobAd.Description.Industries = industries;
            _jobAdsCommand.CreateJobAd(jobAd);
            _jobAdsCommand.OpenJobAd(jobAd);
            return jobAd;
        }
    }
}
