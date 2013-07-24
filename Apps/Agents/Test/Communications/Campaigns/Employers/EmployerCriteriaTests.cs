using System;
using System.Collections.Generic;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Employers
{
    [TestClass]
    public class EmployerCriteriaTests
        : CategoryCriteriaTests
    {
        [TestMethod]
        public void UpdateEmptyWithEmptyCriteriaTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            AssertCriterias(campaign);

            // Update with empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should still be empty.

            AssertCriterias(campaign);
        }

        [TestMethod]
        public void UpdateEmptyWithNonEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            AssertCriterias(campaign);

            // Update with non-empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            newCriteria.IndustryIds.Add(Guid.NewGuid());
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            var criteria = _campaignsQuery.GetCriteria(campaign.Id);
            Assert.AreEqual(newCriteria, criteria);
        }

        [TestMethod]
        public void UpdateNonEmptyWithEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);

            // Update with non-empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            newCriteria.IndustryIds.Add(Guid.NewGuid());
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Update with empty criteria.

            newCriteria = new OrganisationEmployerSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            var criteria = _campaignsQuery.GetCriteria(campaign.Id);
            Assert.AreEqual(new OrganisationEmployerSearchCriteria(), criteria);
        }

        [TestMethod]
        public void UpdateIndustriesTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);

            // 1 industry.

            var industries = new[] { Guid.NewGuid() };
            var newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            var criteria = (OrganisationEmployerSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            AssertIndustries(industries, criteria.IndustryIds);

            // 2 industries.

            industries = new[] { Guid.NewGuid(), Guid.NewGuid() };
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            criteria = (OrganisationEmployerSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            AssertIndustries(industries, criteria.IndustryIds);

            // 0 industries.

            industries = new Guid[0];
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            Assert.IsInstanceOfType(_campaignsQuery.GetCriteria(campaign.Id), typeof(OrganisationEmployerSearchCriteria));

            // Lots of industries.

            industries = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            criteria = (OrganisationEmployerSearchCriteria)_campaignsQuery.GetCriteria(campaign.Id);
            AssertIndustries(industries, criteria.IndustryIds);
        }

        private static void AssertIndustries(ICollection<Guid> expected, ICollection<Guid> industries)
        {
            Assert.AreEqual(expected.Count, industries.Count);
            var expectedEnum = expected.GetEnumerator();
            var industryEnum = industries.GetEnumerator();
            while (expectedEnum.MoveNext() && industryEnum.MoveNext())
                Assert.AreEqual(expectedEnum.Current, industryEnum.Current);
        }
    }
}