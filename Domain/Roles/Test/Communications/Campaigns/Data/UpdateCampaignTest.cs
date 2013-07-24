using System;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public class UpdateCampaignTest
        : CampaignsRepositoryTests
    {
        private const string UpdatedName = "My updated campaign";

        [TestMethod]
        public void UpdateTest()
        {
            // Create some campaigns.

            Campaign campaign1;
            Template template1;
            CreateTestCampaign(1, out campaign1, out template1);

            Campaign campaign2;
            Template template2;
            CreateTestCampaign(2, out campaign2, out template2);

            // Update one of them.

            campaign1.Name = UpdatedName;
            campaign1.Category = CampaignCategory.Member;
            _repository.UpdateCampaign(campaign1);

            // Get them.

            AssertCampaigns(new[]{campaign2, campaign1}, new[]{template2, template1});
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            // Create two campaigns.

            Campaign campaign1;
            Template template1;
            CreateTestCampaign(1, out campaign1, out template1);

            Campaign campaign2;
            Template template2;
            CreateTestCampaign(2, out campaign2, out template2);

            // Update one campaign so the names are the same.

            campaign1.Name = campaign2.Name;

            try
            {
                _repository.UpdateCampaign(campaign1);
                Assert.Fail("Expected an exception");
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(DuplicateValidationError));
            }
        }

        [TestMethod]
        public void UpdateStatusTest()
        {
            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);

            Assert.AreEqual(CampaignStatus.Draft, _repository.GetCampaign(campaign.Id).Status);

            // Update it.

            _repository.UpdateStatus(campaign.Id, CampaignStatus.Running);
            Assert.AreEqual(CampaignStatus.Running, _repository.GetCampaign(campaign.Id).Status);

            _repository.UpdateStatus(campaign.Id, CampaignStatus.Stopped);
            Assert.AreEqual(CampaignStatus.Stopped, _repository.GetCampaign(campaign.Id).Status);

            _repository.UpdateStatus(campaign.Id, CampaignStatus.Deleted);
            Assert.AreEqual(CampaignStatus.Deleted, _repository.GetCampaign(campaign.Id).Status);
        }
    }
}