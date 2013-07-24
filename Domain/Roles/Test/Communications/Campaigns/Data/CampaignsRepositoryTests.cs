using System;
using LinkMe.Domain.Data;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Data;
using LinkMe.Domain.Roles.Test.Communications.Campaigns.Mocks;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public abstract class CampaignsRepositoryTests
        : TestClass
    {
        private const string CampaignNameFormat = "My new campaign{0}";
        private const string TemplateSubjectFormat = "The subject{0} of the template";
        private const string TemplateBodyFormat = "The body{0} of the template";

        protected ICampaignsRepository _repository = new CampaignsRepository(Resolve<IDataContextFactory>(), new MockCriteriaPersister());

        [TestInitialize]
        public void CampaignsRepositoryTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected void CreateTestCampaign(int index, CampaignCategory category, CampaignStatus status, out Campaign campaign, out Template template)
        {
            campaign = new Campaign
            {
                Id = Guid.NewGuid(),
                Name = Format(CampaignNameFormat, index),
                CreatedTime = DateTime.Now.AddMinutes(index),
                Category = category,
                Query = null,
                Status = status,
            };

            _repository.CreateCampaign(campaign);
            template = new Template
            {
                Subject = Format(TemplateSubjectFormat, index),
                Body = Format(TemplateBodyFormat, index)
            };

            _repository.CreateTemplate(campaign.Id, template);
        }

        protected void CreateTestCampaign(int index, out Campaign campaign, out Template template)
        {
            CreateTestCampaign(index, CampaignCategory.Employer, CampaignStatus.Draft, out campaign, out template);
        }

        protected void CreateTestCampaign(int index, CampaignCategory category, out Campaign campaign, out Template template)
        {
            CreateTestCampaign(index, category, CampaignStatus.Draft, out campaign, out template);
        }

        private static string Format(string format, int index)
        {
            return string.Format(format, index < 10 ? "00" + index : (index < 100 ? "0" + index : index.ToString()));
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
                AssertCampaign(expectedCampaigns[index], _repository.GetCampaign(expectedCampaigns[index].Id));
                AssertTemplate(expectedTemplates[index], _repository.GetTemplate(expectedCampaigns[index].Id));
            }

            // Get all.

            var campaigns = _repository.GetCampaigns(null, new Range());
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
    }
}
