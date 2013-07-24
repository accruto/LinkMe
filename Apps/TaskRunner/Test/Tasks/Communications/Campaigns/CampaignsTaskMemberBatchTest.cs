using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Query.Search.Members;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskMemberBatchTest
        : CampaignsTaskTests
    {
        [TestMethod]
        public void TestBatch1()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            for (var run = 0; run < 6; ++run)
            {
                _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
                TestSearch(1, 1, 1);
            }

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 1, 0);
        }

        [TestMethod]
        public void TestBatch2()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 2, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 2, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 2, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 2, 0);
        }

        [TestMethod]
        public void TestBatch4()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 4, 4);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 4, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(1, 4, 0);
        }

        [TestMethod]
        public void TestRun2()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 1, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 1, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 1, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 1, 0);
        }

        [TestMethod]
        public void TestRun4()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(4, 1, 4);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(4, 1, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(4, 1, 0);
        }

        [TestMethod]
        public void TestRun2Batch2()
        {
            var campaign = CreateCampaign();

            // Create some members.

            CreateMembers(0, 6);

            // Default criteria should return everyone.

            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 2, 4);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 2, 2);
            _campaignsCommand.UpdateStatus(campaign, CampaignStatus.Activated);
            TestSearch(2, 2, 0);
        }

        private void TestSearch(int runs, int batch, int expected)
        {
            // Run the task.

            new CampaignsTask().ExecuteTask(new[] { runs.ToString(), batch.ToString(), 0.ToString() });
            _emailServer.AssertEmailsSent(expected);
        }

        private Campaign CreateCampaign()
        {
            Campaign campaign;
            Template template;
            CreateCampaign(0, CampaignCategory.Member, null, CampaignStatus.Activated, out campaign, out template);
            var criteria = new MemberSearchCriteria();
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);
            return campaign;
        }
    }
}