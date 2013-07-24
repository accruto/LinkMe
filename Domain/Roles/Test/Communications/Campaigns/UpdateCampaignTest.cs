using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Communications.Campaigns
{
    [TestClass]
    public class UpdateCampaignTest
        : CampaignsCommandTests
    {
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private const string UpdatedName = "My new campaign updated";

        [TestMethod]
        public void UpdateTest()
        {
            // Create campaigns.

            Campaign campaign1;
            Template template1;
            CreateTestCampaign(1, out campaign1, out template1);

            Campaign campaign2;
            Template template2;
            CreateTestCampaign(2, out campaign2, out template2);

            // Update one of them.

            campaign1.Name = UpdatedName;
            _campaignsCommand.UpdateCampaign(campaign1);

            // Get them.

            AssertCampaigns(new[]{campaign2, campaign1}, new[]{template2, template1});
        }

        [TestMethod]
        public void UpdateNullNameTest()
        {
            UpdateNameTest(null, new RequiredValidationError("Name"));
        }

        [TestMethod]
        public void UpdateEmptyNameTest()
        {
            UpdateNameTest(string.Empty, new RequiredValidationError("Name"));
        }

        [TestMethod]
        public void UpdateLongNameTest()
        {
            UpdateNameTest(new string('a', 200), new MaximumLengthValidationError("Name", 100));
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

            // Try to update one of them.

            UpdateNameTest(campaign1, campaign2.Name, new DuplicateValidationError("Name"));
        }

        [TestMethod]
        public void UpdateNameHtmlTest()
        {
            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);

            // Put some html in the name which should be stripped out.

            NameHtmlTest(campaign, "<div>Some html</div>", "Some html");
            NameHtmlTest(campaign, "<div>Some html", "Some html");
            NameHtmlTest(campaign, "<script>Some html</script>", "Some html");
            NameHtmlTest(campaign, "<script>Some </script>html", "Some html");
            NameHtmlTest(campaign, "<script>Some</script>html", "Somehtml");
        }

        [TestMethod]
        public void UpdateCommunicationCategoryTest()
        {
            // Create campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);
            Assert.IsNull(campaign.CommunicationCategoryId);

            // Update.

            campaign.CommunicationCategoryId = _settingsQuery.GetCategories(UserType.Member)[1].Id;
            _campaignsCommand.UpdateCampaign(campaign);

            // Get them.

            AssertCampaign(campaign, template);
        }

        [TestMethod]
        public void UpdateCommunicationDefinitionTest()
        {
            // Create campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);
            Assert.IsNull(campaign.CommunicationDefinitionId);

            // Update.

            campaign.CommunicationDefinitionId = _settingsQuery.GetDefinitions(UserType.Member)[1].Id;
            _campaignsCommand.UpdateCampaign(campaign);

            // Get them.

            AssertCampaign(campaign, template);
        }

        private void UpdateNameTest(string name, ValidationError expectedError)
        {
            // Create a campaign.

            Campaign campaign;
            Template template;
            CreateTestCampaign(1, out campaign, out template);

            // Update the name.

            UpdateNameTest(campaign, name, expectedError);
        }

        private void UpdateNameTest(Campaign campaign, string name, ValidationError expectedError)
        {
            // Update the name.

            campaign.Name = name;
            try
            {
                _campaignsCommand.UpdateCampaign(campaign);
                Assert.Fail("ValidationException should have been thrown.");
            }
            catch (ValidationErrorsException ex)
            {
                AssertErrors(ex, expectedError);
            }
        }

        private void NameHtmlTest(Campaign campaign, string withHtml, string withoutHtml)
        {
            campaign.Name = withHtml;
            _campaignsCommand.UpdateCampaign(campaign);

            // It should have been stripped out.

            campaign.Name = withoutHtml;
            AssertCampaign(campaign, _campaignsQuery.GetCampaign(campaign.Id));
        }
    }
}
