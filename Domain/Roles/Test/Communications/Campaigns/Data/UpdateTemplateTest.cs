using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public class UpdateTemplateTest
        : CampaignsRepositoryTests
    {
        private const string UpdatedSubject = "My updated subject";
        private const string UpdatedBody = "My updated body";

        [TestMethod]
        public void UpdateTest()
        {
            // Create some campaigns.

            Campaign campaign1;
            Campaign campaign2;
            Template template1;
            Template template2;

            CreateTestCampaign(1, out campaign1, out template1);
            CreateTestCampaign(2, out campaign2, out template2);

            // Update one of them.

            template1.Subject = UpdatedSubject;
            template1.Body = UpdatedBody;
            _repository.UpdateTemplate(campaign1.Id, template1);

            // Get them.

            AssertCampaigns(new[]{campaign2, campaign1}, new[]{template2, template1});
        }
    }
}
