using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public class GetCampaignsTest
        : CampaignsRepositoryTests
    {
        [TestMethod]
        public void GetTest()
        {
            // Create lots of campaigns.

            var campaigns = CreateCampaigns(0, 101);

            // Get combinations.

            GetTest(campaigns, null);
        }

        [TestMethod]
        public void GetCategoryFilterTest()
        {
            // Create lots of campaigns.

            var memberCampaigns = CreateCampaigns(0, 101, CampaignCategory.Member);
            var employerCampaigns = CreateCampaigns(101, 23, CampaignCategory.Employer);

            // Get combinations.

            GetTest(employerCampaigns.Concat(memberCampaigns), null);
            GetTest(memberCampaigns, CampaignCategory.Member);
            GetTest(employerCampaigns, CampaignCategory.Employer);
        }

        private void GetTest(IEnumerable<Campaign> campaigns, CampaignCategory? category)
        {
            AssertCampaigns(campaigns, _repository.GetCampaigns(category, new Range()).RangeItems);
            AssertCampaigns(campaigns.Skip(2).Take(3), _repository.GetCampaigns(category, new Range(2, 3)).RangeItems);
            AssertCampaigns(campaigns.Skip(20).Take(30), _repository.GetCampaigns(category, new Range(20, 30)).RangeItems);
            AssertCampaigns(campaigns.Skip(4), _repository.GetCampaigns(category, new Range(4)).RangeItems);
            AssertCampaigns(campaigns.Take(4), _repository.GetCampaigns(category, new Range(0, 4)).RangeItems);
            AssertCampaigns(campaigns.Skip(20).Take(300), _repository.GetCampaigns(category, new Range(20, 300)).RangeItems);
        }

        private static void AssertCampaigns(IEnumerable<Campaign> expectedCampaigns, IList<Campaign> campaigns)
        {
            Assert.AreEqual(expectedCampaigns.Count(), campaigns.Count);
            for (var index = 0; index < expectedCampaigns.Count(); ++index)
                AssertCampaign(expectedCampaigns.ElementAt(index), campaigns[index]);
        }

        private IList<Campaign> CreateCampaigns(int start, int count, CampaignCategory category)
        {
            var campaigns = new List<Campaign>();
            for (var index = start; index < start + count; ++index)
            {
                Campaign campaign;
                Template template;
                CreateTestCampaign(index, category, CampaignStatus.Draft, out campaign, out template);
                campaigns.Insert(0, campaign);
            }
            return campaigns;
        }

        private IList<Campaign> CreateCampaigns(int start, int count)
        {
            return CreateCampaigns(start, count, CampaignCategory.Employer);
        }
    }
}
