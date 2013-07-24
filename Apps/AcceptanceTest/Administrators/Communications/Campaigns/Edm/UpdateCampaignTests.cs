using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns.Edm
{
    [TestClass]
    public class UpdateCampaignTests
        : EdmCampaignsTests
    {
        private const string UpdatedCampaignName = "My updated campaign";

        [TestMethod]
        public void UpdateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(administrator);

            var definitionId = _settingsQuery.GetDefinition("EdmEmail").Id;
            var categoryId = _settingsQuery.GetCategory("Campaign").Id;
            AssertCampaign(CampaignCategory.Member, definitionId, categoryId, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(definitionId.ToString(), _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(categoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = UpdatedCampaignName;
            _saveButton.Click();
            Assert.AreEqual(UpdatedCampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(definitionId.ToString(), _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(categoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            Assert.IsNull(_campaignsQuery.GetCampaign(campaign.Name));
            AssertCampaign(CampaignCategory.Member, definitionId, categoryId, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, definitionId, categoryId, null, _campaignsQuery.GetCampaign(UpdatedCampaignName));
        }

        private static void AssertCampaign(CampaignCategory category, Guid? communicationDefinitionId, Guid? communicationCategoryId, string query, Campaign campaign)
        {
            Assert.AreEqual(category, campaign.Category);
            Assert.AreEqual(communicationDefinitionId, campaign.CommunicationDefinitionId);
            Assert.AreEqual(communicationCategoryId, campaign.CommunicationCategoryId);
            Assert.AreEqual(query, campaign.Query);
        }
    }
}