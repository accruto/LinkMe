using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns
{
    [TestClass]
    public class UpdateCampaignStatusTest
        : CampaignsCommandTests
    {
        [TestMethod]
        public void UpdateActivatedTest()
        {
            UpdateTest(0, CampaignStatus.Activated, CampaignStatus.Activated, true);
            UpdateTest(1, CampaignStatus.Activated, CampaignStatus.Deleted, true);
            UpdateTest(2, CampaignStatus.Activated, CampaignStatus.Draft, false);
            UpdateTest(3, CampaignStatus.Activated, CampaignStatus.Running, true);
            UpdateTest(4, CampaignStatus.Activated, CampaignStatus.Stopped, true);
        }

        [TestMethod]
        public void UpdateDeletedTest()
        {
            UpdateTest(0, CampaignStatus.Deleted, CampaignStatus.Activated, false);
            UpdateTest(1, CampaignStatus.Deleted, CampaignStatus.Deleted, true);
            UpdateTest(2, CampaignStatus.Deleted, CampaignStatus.Draft, false);
            UpdateTest(3, CampaignStatus.Deleted, CampaignStatus.Running, false);
            UpdateTest(4, CampaignStatus.Deleted, CampaignStatus.Stopped, false);
        }

        [TestMethod]
        public void UpdateDraftTest()
        {
            UpdateTest(0, CampaignStatus.Draft, CampaignStatus.Activated, true);
            UpdateTest(1, CampaignStatus.Draft, CampaignStatus.Deleted, true);
            UpdateTest(2, CampaignStatus.Draft, CampaignStatus.Draft, true);
            UpdateTest(3, CampaignStatus.Draft, CampaignStatus.Running, false);
            UpdateTest(4, CampaignStatus.Draft, CampaignStatus.Stopped, false);
        }

        [TestMethod]
        public void UpdateRunningTest()
        {
            UpdateTest(0, CampaignStatus.Running, CampaignStatus.Activated, false);
            UpdateTest(1, CampaignStatus.Running, CampaignStatus.Deleted, false);
            UpdateTest(2, CampaignStatus.Running, CampaignStatus.Draft, false);
            UpdateTest(3, CampaignStatus.Running, CampaignStatus.Running, true);
            UpdateTest(4, CampaignStatus.Running, CampaignStatus.Stopped, true);
        }

        [TestMethod]
        public void UpdateStoppedTest()
        {
            UpdateTest(0, CampaignStatus.Stopped, CampaignStatus.Activated, true);
            UpdateTest(1, CampaignStatus.Stopped, CampaignStatus.Deleted, true);
            UpdateTest(2, CampaignStatus.Stopped, CampaignStatus.Draft, false);
            UpdateTest(3, CampaignStatus.Stopped, CampaignStatus.Running, false);
            UpdateTest(4, CampaignStatus.Stopped, CampaignStatus.Stopped, true);
        }

        private void UpdateTest(int index, CampaignStatus from, CampaignStatus to, bool expectedToSucceed)
        {
            // Create the campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(index, out campaign, out template);

            // Put it into the initial state.

            _repository.UpdateStatus(campaign.Id, from);
            campaign = _repository.GetCampaign(campaign.Id);
            Assert.AreEqual(from, campaign.Status);

            try
            {
                _campaignsCommand.UpdateStatus(campaign, to);
                if (!expectedToSucceed)
                    Assert.Fail("Should not be able to change status from " + from + " to " + to + ".");
                Assert.AreEqual(to, campaign.Status);
            }
            catch (ValidationErrorsException ex)
            {
                if (expectedToSucceed)
                    Assert.Fail("Should be able to change status from " + from + " to " + to + ".");
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.AreEqual(new ChangedValidationError("Status", from.ToString(), to.ToString()), ex.Errors[0]);
            }
        }
    }
}