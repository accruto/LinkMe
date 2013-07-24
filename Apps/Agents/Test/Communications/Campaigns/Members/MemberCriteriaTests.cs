using System;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Members
{
    [TestClass]
    public class MemberCriteriaTests
        : CategoryCriteriaTests
    {
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void UpdateEmptyWithEmptyCriteriaTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            AssertCriterias(campaign);

            // Update with empty criteria.

            var newCriteria = new MemberSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should still be empty.

            AssertCriterias(campaign);
        }

        [TestMethod]
        public void UpdateEmptyWithNonEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            AssertCriterias(campaign);

            // Update with non-empty criteria.

            var newCriteria = new MemberSearchCriteria();
            newCriteria.IndustryIds.Add(_industriesQuery.GetIndustries()[0].Id);
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            var criteria = _campaignsQuery.GetCriteria(campaign.Id);
            Assert.AreEqual(newCriteria, criteria);
        }

        [TestMethod]
        public void UpdateNonEmptyWithEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);

            // Update with non-empty criteria.

            var newCriteria = new MemberSearchCriteria();
            newCriteria.IndustryIds.Add(Guid.NewGuid());
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Update with empty criteria.

            newCriteria = new MemberSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            var criteria = _campaignsQuery.GetCriteria(campaign.Id);
            Assert.AreEqual(new MemberSearchCriteria(), criteria);
        }

        [TestMethod]
        public void UpdateIndustriesTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);

            // 1 industry.

            var industries = new[] { _industriesQuery.GetIndustries()[0].Id };
            var newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            var criteria = (MemberSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            Assert.IsTrue(industries.NullableCollectionEqual(criteria.IndustryIds));

            // 2 industries.

            industries = new[] { _industriesQuery.GetIndustries()[1].Id, _industriesQuery.GetIndustries()[2].Id };
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            criteria = (MemberSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            Assert.IsTrue(industries.NullableCollectionEqual(criteria.IndustryIds));

            // 0 industries.

            industries = new Guid[0];
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            Assert.IsInstanceOfType(_campaignsQuery.GetCriteria(campaign.Id), typeof(MemberSearchCriteria));

            // Lots of industries.

            industries = new[] { _industriesQuery.GetIndustries()[0].Id, _industriesQuery.GetIndustries()[1].Id, _industriesQuery.GetIndustries()[2].Id, _industriesQuery.GetIndustries()[3].Id, _industriesQuery.GetIndustries()[4].Id };
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            criteria = (MemberSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            Assert.IsTrue(industries.NullableCollectionEqual(criteria.IndustryIds));
        }
    }
}