using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public class GetCampaignsStatusFilterTest
        : CampaignsRepositoryTests
    {
        [TestMethod]
        public void GetTest()
        {
            // Create lots of campaigns.

            var campaigns = CreateCampaigns(0, 101, CampaignStatus.Draft);

            // Get combinations.

            var empty = new List<Campaign>();
            AssertCampaigns(campaigns, _repository.GetCampaigns(null, CampaignStatus.Draft));
            AssertCampaigns(empty, _repository.GetCampaigns(null, CampaignStatus.Activated));
            AssertCampaigns(empty, _repository.GetCampaigns(null, CampaignStatus.Deleted));
            AssertCampaigns(empty, _repository.GetCampaigns(null, CampaignStatus.Running));
            AssertCampaigns(empty, _repository.GetCampaigns(null, CampaignStatus.Stopped));
        }

        [TestMethod]
        public void GetStatusFilterTest()
        {
            // Create lots of campaigns.

            var draftCampaigns = CreateCampaigns(0, 101, CampaignStatus.Draft);
            var deletedCampaigns = CreateCampaigns(101, 23, CampaignStatus.Deleted);
            var activatedCampaigns = CreateCampaigns(101 + 23, 86, CampaignStatus.Activated);

            // Get combinations.

            AssertCampaigns(activatedCampaigns, _repository.GetCampaigns(null, CampaignStatus.Activated));
            AssertCampaigns(deletedCampaigns, _repository.GetCampaigns(null, CampaignStatus.Deleted));
            AssertCampaigns(draftCampaigns, _repository.GetCampaigns(null, CampaignStatus.Draft));
        }

        [TestMethod]
        public void GetCategoryFilterTest()
        {
            // Create lots of campaigns.

            var memberCampaigns = CreateCampaigns(0, 101, CampaignCategory.Member, CampaignStatus.Draft);
            var employerCampaigns = CreateCampaigns(101, 23, CampaignCategory.Employer, CampaignStatus.Draft);

            // Get combinations.

            AssertCampaigns(employerCampaigns.Concat(memberCampaigns), _repository.GetCampaigns(null, CampaignStatus.Draft));
            AssertCampaigns(memberCampaigns, _repository.GetCampaigns(CampaignCategory.Member, CampaignStatus.Draft));
            AssertCampaigns(employerCampaigns, _repository.GetCampaigns(CampaignCategory.Employer, CampaignStatus.Draft));
        }

        private static void AssertCampaigns(IEnumerable<Campaign> expectedCampaigns, IList<Campaign> campaigns)
        {
            Assert.AreEqual(expectedCampaigns.Count(), campaigns.Count);
            for (var index = 0; index < expectedCampaigns.Count(); ++index)
                AssertCampaign(expectedCampaigns.ElementAt(index), campaigns[index]);
        }

        private IList<Campaign> CreateCampaigns(int start, int count, CampaignCategory category, CampaignStatus status)
        {
            var campaigns = new List<Campaign>();
            for (var index = start; index < start + count; ++index)
            {
                Campaign campaign;
                Template template;
                CreateTestCampaign(index, category, status, out campaign, out template);
                campaigns.Insert(0, campaign);
            }
            return campaigns;
        }

        private IList<Campaign> CreateCampaigns(int start, int count, CampaignStatus status)
        {
            return CreateCampaigns(start, count, CampaignCategory.Employer, status);
        }
    }
}
