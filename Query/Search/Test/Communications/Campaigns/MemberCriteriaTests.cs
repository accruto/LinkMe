using System;
using LinkMe.Domain;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Communications.Campaigns
{
    [TestClass]
    public class MemberCriteriaTests
        : CriteriaTests
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestMethod]
        public void UpdateEmptyWithEmptyCriteriaTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new MemberSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with empty criteria.

            var newCriteria = new MemberSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should still be empty.

            AssertCriteria(new MemberSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateEmptyWithNonEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new MemberSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with non-empty criteria.

            var newCriteria = new MemberSearchCriteria();
            newCriteria.IndustryIds.Add(_industriesQuery.GetIndustries()[0].Id);
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateNonEmptyWithEmptyCriteriaSet()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            _campaignsCommand.CreateCampaign(campaign);

            // Update with non-empty criteria.

            var newCriteria = new MemberSearchCriteria();
            newCriteria.IndustryIds.Add(_industriesQuery.GetIndustries()[0].Id);
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // Update with empty criteria.

            newCriteria = new MemberSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(new MemberSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateAllTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            _campaignsCommand.CreateCampaign(campaign);
            AssertCriteria(new MemberSearchCriteria(), _campaignsQuery.GetCriteria(campaign.Id));

            // Update with non-empty criteria.

            var newCriteria = new MemberSearchCriteria
            {
                CandidateStatusFlags = CandidateStatusFlags.OpenToOffers,
                CommunityId = Guid.NewGuid(),
                CompanyKeywords = "aaa",
                DesiredJobTitle = "bbb",
                Distance = 42,
                EthnicStatus = EthnicStatus.Aboriginal,
                JobTypes = JobTypes.FullTime,
                IncludeRelocating = true,
                JobTitlesToSearch = JobsToSearch.LastJob,
                JobTitle = "ccc",
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Armadale VIC 3143"),
                Salary = new Salary { LowerBound = 10000, UpperBound = 20000, Rate = SalaryRate.Hour, Currency = Currency.AUD },
                SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated },
            };
            newCriteria.SetKeywords("ddd", "eee", "fff", "ggg");

            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);

            // Should not be empty.

            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        [TestMethod]
        public void UpdateIndustriesTest()
        {
            // Create empty criteria.

            var campaign = CreateTestCampaign(1, CampaignCategory.Member);
            _campaignsCommand.CreateCampaign(campaign);

            // 1 industry.

            var industries = new[] { _industriesQuery.GetIndustries()[0].Id };
            var newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // 2 industries.

            industries = new[] { _industriesQuery.GetIndustries()[1].Id, _industriesQuery.GetIndustries()[2].Id };
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // 0 industries.

            industries = new Guid[0];
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));

            // Lots of industries.

            industries = new[] { _industriesQuery.GetIndustries()[0].Id, _industriesQuery.GetIndustries()[1].Id, _industriesQuery.GetIndustries()[2].Id, _industriesQuery.GetIndustries()[3].Id, _industriesQuery.GetIndustries()[4].Id, _industriesQuery.GetIndustries()[5].Id, _industriesQuery.GetIndustries()[6].Id };
            newCriteria = new MemberSearchCriteria { IndustryIds = industries };
            _campaignsCommand.UpdateCriteria(campaign.Id, newCriteria);
            AssertCriteria(newCriteria, _campaignsQuery.GetCriteria(campaign.Id));
        }

        private static void AssertCriteria(MemberSearchCriteria expectedCriteria, Criteria criteria)
        {
            Assert.IsInstanceOfType(criteria, typeof(MemberSearchCriteria));
            var organisationCriteria = (MemberSearchCriteria)criteria;
            Assert.AreEqual(expectedCriteria.CandidateStatusFlags, organisationCriteria.CandidateStatusFlags);
            Assert.AreEqual(expectedCriteria.CommunityId, organisationCriteria.CommunityId);
            Assert.AreEqual(expectedCriteria.CompanyKeywords, organisationCriteria.CompanyKeywords);
            Assert.AreEqual(expectedCriteria.CompanyKeywordsExpression, organisationCriteria.CompanyKeywordsExpression);
            Assert.AreEqual(expectedCriteria.DesiredJobTitle, organisationCriteria.DesiredJobTitle);
            Assert.AreEqual(expectedCriteria.DesiredJobTitleExpression, organisationCriteria.DesiredJobTitleExpression);
            Assert.AreEqual(expectedCriteria.Distance, organisationCriteria.Distance);
            Assert.AreEqual(expectedCriteria.EthnicStatus, organisationCriteria.EthnicStatus);
            Assert.AreEqual(expectedCriteria.JobTypes, organisationCriteria.JobTypes);
            Assert.AreEqual(expectedCriteria.IncludeRelocating, organisationCriteria.IncludeRelocating);
            Assert.AreEqual(expectedCriteria.JobTitlesToSearch, organisationCriteria.JobTitlesToSearch);
            Assert.AreEqual(expectedCriteria.JobTitle, organisationCriteria.JobTitle);
            Assert.AreEqual(expectedCriteria.JobTitleExpression, organisationCriteria.JobTitleExpression);
            Assert.AreEqual(expectedCriteria.KeywordsExpression, organisationCriteria.KeywordsExpression);
            Assert.AreEqual(expectedCriteria.Location, organisationCriteria.Location);
            Assert.AreEqual(expectedCriteria.Salary, organisationCriteria.Salary);
            Assert.AreEqual(expectedCriteria.SortCriteria.SortOrder, organisationCriteria.SortCriteria.SortOrder);

            Assert.AreEqual(expectedCriteria.IndustryIds.Count, organisationCriteria.IndustryIds.Count);
            var expectedEnum = expectedCriteria.IndustryIds.GetEnumerator();
            var industryEnum = organisationCriteria.IndustryIds.GetEnumerator();
            while (expectedEnum.MoveNext() && industryEnum.MoveNext())
                Assert.AreEqual(expectedEnum.Current, industryEnum.Current);
        }
    }
}