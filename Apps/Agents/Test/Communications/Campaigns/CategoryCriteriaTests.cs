using System;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns
{
    [TestClass]
    public abstract class CategoryCriteriaTests
        : TestClass
    {
        private const string CampaignNameFormat = "My new campaign{0}";

        protected ICampaignsCommand _campaignsCommand = Resolve<ICampaignsCommand>();
        protected ICampaignsQuery _campaignsQuery = Resolve<ICampaignsQuery>();

        [TestInitialize]
        public void CategoryCriteriaTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Campaign CreateTestCampaign(int index, CampaignCategory category)
        {
            var campaign = new Campaign
                               {
                                   Name = string.Format(CampaignNameFormat, index),
                                   CreatedBy = Guid.NewGuid(),
                                   Category = category,
                                   Query = null,
                               };

            _campaignsCommand.CreateCampaign(campaign);
            return campaign;
        }

        protected void AssertCampaigns(params Campaign[] expectedCampaigns)
        {
            // Get each individually.

            foreach (var expectedCampaign in expectedCampaigns)
            {
                Assert.AreNotEqual(DateTime.MinValue, expectedCampaign.CreatedTime);
                Assert.AreEqual(expectedCampaign, _campaignsQuery.GetCampaign(expectedCampaign.Id));
            }

            // Get all.

            var campaigns = _campaignsQuery.GetCampaigns(null, new Range());
            Assert.AreEqual(expectedCampaigns.Length, campaigns.RangeItems.Count);
            for (var index = 0; index < expectedCampaigns.Length; ++index)
                Assert.AreEqual(expectedCampaigns[index], campaigns.RangeItems[index]);
        }

        protected void AssertCriterias(params Campaign[] campaigns)
        {
            // Get a corresponding criteria which should simply be equivalent to an empty criteria.

            foreach (var campaign in campaigns)
            {
                var criteria = _campaignsQuery.GetCriteria(campaign.Id);
                Assert.IsNotNull(criteria);
            }
        }

        protected static void AssertErrors(ValidationErrorsException ex, params ValidationError[] expectedErrors)
        {
            Assert.AreEqual(expectedErrors.Length, ex.Errors.Count);
            for (var index = 0; index < expectedErrors.Length; ++index)
            {
                var expectedError = expectedErrors[index];
                var error = ex.Errors[index];
                Assert.AreEqual(expectedError.Name, error.Name);
                Assert.AreEqual(expectedError.Message, error.Message);
            }
        }
    }
}