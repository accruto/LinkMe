using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns.Data
{
    [TestClass]
    public class CreateCampaignTest
        : CampaignsRepositoryTests
    {
        [TestMethod]
        public void CreateTest()
        {
            // Create it.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);

            // Get it.

            AssertCampaign(campaign, template);
        }

        [TestMethod]
        public void CreateMultipleTest()
        {
            // Create them.

            const int count = 5;
            var campaigns = new Campaign[count];
            var templates = new Template[count];
            for (var index = 0; index < count; ++index)
            {
                Campaign campaign;
                Template template;
                CreateTestCampaign(index, out campaign, out template);
                campaigns[count - index - 1] = campaign;
                templates[count - index - 1] = template;
            }

            // Get them.

            AssertCampaigns(campaigns, templates);
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            // Create one campaign.

            Campaign campaign1;
            Template template1;
            CreateTestCampaign(1, out campaign1, out template1);

            // Try to create a campaign with the same name.

            try
            {
                Campaign campaign2;
                Template template2;
                CreateTestCampaign(1, out campaign2, out template2);
                Assert.Fail("Expected an exception");
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.IsInstanceOfType(ex.Errors[0], typeof(DuplicateValidationError));
            }
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            // Create some campaigns.

            Campaign campaign1;
            Template template1;
            CreateTestCampaign(1, out campaign1, out template1);

            Campaign campaign2;
            Template template2;
            CreateTestCampaign(2, CampaignCategory.Member, out campaign2, out template2);

            Campaign campaign3;
            Template template3;
            CreateTestCampaign(3, CampaignCategory.Employer, out campaign3, out template3);

            // Get it.

            AssertCampaigns(new[]{campaign3, campaign2, campaign1}, new[]{template3, template2, template1});
        }
    }
}
