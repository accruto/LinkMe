using System;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Communications.Campaigns.Data;
using LinkMe.Domain.Roles.Communications.Campaigns.Queries;
using LinkMe.Domain.Roles.Test.Communications.Campaigns.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns
{
    [TestClass]
    public abstract class CampaignsCommandTests
        : TestClass
    {
        protected const string CampaignNameFormat = "My new campaign{0}";
        protected const string TemplateSubjectFormat = "The subject{0} of the template";
        protected const string TemplateBodyFormat = "The body{0} of the template";

        protected ICampaignsRepository _repository = new CampaignsRepository(Resolve<IDataContextFactory>(), new MockCriteriaPersister());
        protected ICampaignsCommand _campaignsCommand;
        protected ICampaignsQuery _campaignsQuery;

        [TestInitialize]
        public void CampaignsCommandTestsInitialize()
        {
            _campaignsCommand = new CampaignsCommand(_repository);
            _campaignsQuery = new CampaignsQuery(_repository);

            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void CreateTestCampaign(int index, out Campaign campaign, out Template template)
        {
            campaign = new Campaign
            {
                Name = string.Format(CampaignNameFormat, index),
                CreatedBy = Guid.NewGuid(),
            };

            _campaignsCommand.CreateCampaign(campaign);

            template = new Template
            {
                Subject = string.Format(TemplateSubjectFormat, index),
                Body = string.Format(TemplateBodyFormat, index)
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);
        }

        protected void AssertCampaign(Campaign expectedCampaign, Template expectedTemplate)
        {
            AssertCampaigns(new[]{expectedCampaign}, new[]{expectedTemplate});
        }

        protected void AssertCampaigns(Campaign[] expectedCampaigns, Template[] expectedTemplates)
        {
            // Get each individually.

            for (var index = 0; index < expectedCampaigns.Length; ++index)
            {
                Assert.AreNotEqual(DateTime.MinValue, expectedCampaigns[index].CreatedTime);
                AssertCampaign(expectedCampaigns[index], _campaignsQuery.GetCampaign(expectedCampaigns[index].Id));
                AssertTemplate(expectedTemplates[index], _campaignsQuery.GetTemplate(expectedCampaigns[index].Id));
            }

            // Get all.

            var campaigns = _campaignsQuery.GetCampaigns(null, new Range());
            Assert.AreEqual(expectedCampaigns.Length, campaigns.RangeItems.Count);
            for (var index = 0; index < expectedCampaigns.Length; ++index)
                AssertCampaign(expectedCampaigns[index], campaigns.RangeItems[index]);
        }

        protected static void AssertCampaign(Campaign expectedCampaign, Campaign campaign)
        {
            Assert.AreEqual(expectedCampaign.Id, campaign.Id);
            Assert.AreEqual(expectedCampaign.Name, campaign.Name);
            Assert.AreEqual(expectedCampaign.Status, campaign.Status);
            Assert.AreEqual(expectedCampaign.Category, campaign.Category);
            Assert.AreEqual(expectedCampaign.CommunicationCategoryId, campaign.CommunicationCategoryId);
            Assert.AreEqual(expectedCampaign.CommunicationDefinitionId, campaign.CommunicationDefinitionId);
            Assert.AreEqual(expectedCampaign.Query, campaign.Query);
            Assert.AreEqual(expectedCampaign.CreatedBy, campaign.CreatedBy);
        }

        protected static void AssertTemplate(Template expectedTemplate, Template template)
        {
            Assert.AreEqual(expectedTemplate.Body, template.Body);
            Assert.AreEqual(expectedTemplate.Subject, template.Subject);
        }

        protected static void GetCriterias(ICampaignsQuery campaignsQuery, params Campaign[] campaigns)
        {
            // Get a corresponding criteria which should simply be equivalent to an empty criteria.

            foreach (var campaign in campaigns)
            {
                var criteria = campaignsQuery.GetCriteria(campaign.Id);
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
