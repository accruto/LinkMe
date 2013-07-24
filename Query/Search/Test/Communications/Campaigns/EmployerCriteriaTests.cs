using System;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Search.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Communications.Campaigns
{
    [TestClass]
    public class EmployerCriteriaTests
        : CriteriaTests
    {
        [TestMethod]
        public void UpdateEmptyWithEmptyCriteriaTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new OrganisationEmployerSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should still be empty.

            AssertCriteria(new OrganisationEmployerSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateEmptyWithNonEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new OrganisationEmployerSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with non-empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            newCriteria.IndustryIds.Add(Guid.NewGuid());
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateNonEmptyWithEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            _campaignsCommand.CreateCampaign(campaign);

            // Update with non-empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria();
            newCriteria.IndustryIds.Add(Guid.NewGuid());
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // Update with empty criteria.

            newCriteria = new OrganisationEmployerSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(new OrganisationEmployerSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateAllTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new OrganisationEmployerSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with non-empty criteria.

            var newCriteria = new OrganisationEmployerSearchCriteria
            {
                Employers = false,
                Recruiters = false,
                VerifiedOrganisations = false,
                UnverifiedOrganisations = false,
                IndustryIds = new []{Guid.NewGuid()},
                MinimumLogins = 10,
                MaximumLogins = 20,
            };

            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateIndustriesTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Employer);
            _campaignsCommand.CreateCampaign(campaign);

            // 1 industry.

            var industries = new[] { Guid.NewGuid() };
            var newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // 2 industries.

            industries = new[] { Guid.NewGuid(), Guid.NewGuid() };
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // 0 industries.

            industries = new Guid[0];
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // Lots of industries.

            industries = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            newCriteria = new OrganisationEmployerSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        private static void AssertCriteria(OrganisationEmployerSearchCriteria expectedCriteria, Criteria criteria)
        {
            Assert.IsInstanceOfType(criteria, typeof(OrganisationEmployerSearchCriteria));
            var organisationCriteria = (OrganisationEmployerSearchCriteria) criteria;
            Assert.AreEqual(expectedCriteria.Employers, organisationCriteria.Employers);
            Assert.AreEqual(expectedCriteria.Recruiters, organisationCriteria.Recruiters);
            Assert.AreEqual(expectedCriteria.MaximumLogins, organisationCriteria.MaximumLogins);
            Assert.AreEqual(expectedCriteria.MinimumLogins, organisationCriteria.MinimumLogins);
            Assert.AreEqual(expectedCriteria.VerifiedOrganisations, organisationCriteria.VerifiedOrganisations);
            Assert.AreEqual(expectedCriteria.UnverifiedOrganisations, organisationCriteria.UnverifiedOrganisations);

            Assert.AreEqual(expectedCriteria.IndustryIds.Count, organisationCriteria.IndustryIds.Count);
            var expectedEnum = expectedCriteria.IndustryIds.GetEnumerator();
            var industryEnum = organisationCriteria.IndustryIds.GetEnumerator();
            while (expectedEnum.MoveNext() && industryEnum.MoveNext())
                Assert.AreEqual(expectedEnum.Current, industryEnum.Current);
        }
    }
}