using System;
using System.Net;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateCampaignTests
        : CampaignsTests
    {
        private const string UpdatedCampaignName = "My updated campaign";
        private const string Query = "SELECT id FROM Member";

        [TestMethod]
        public void UpdateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = UpdatedCampaignName;
            _saveButton.Click();
            Assert.AreEqual(UpdatedCampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            Assert.IsNull(_campaignsQuery.GetCampaign(campaign.Name));
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(UpdatedCampaignName));
        }

        [TestMethod]
        public void UpdateCategoryTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _categoryDropDownList.SelectedValue = CampaignCategory.Employer.ToString();
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Employer.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Employer, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Employer, null, null, null, _campaignsQuery.GetCampaign(campaign.Name));
        }

        [TestMethod]
        public void UpdateCommunicationDefinitionTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);

            // Set it and save.

            var communicationDefinitionId = _settingsQuery.GetDefinitions(UserType.Member)[1].Id;

            _communicationDefinitionDropDownList.SelectedValue = communicationDefinitionId.ToString();
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationDefinitionId.ToString(), _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, communicationDefinitionId, null, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, communicationDefinitionId, null, null, _campaignsQuery.GetCampaign(campaign.Name));

            // Reset it.

            _communicationDefinitionDropDownList.SelectedValue = string.Empty;
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Name));
        }

        [TestMethod]
        public void UpdateCommunicationCategoryTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);

            // Set it and save.

            var communicationCategoryId = _settingsQuery.GetCategories(UserType.Member)[1].Id;

            _communicationCategoryDropDownList.SelectedValue = communicationCategoryId.ToString();
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, communicationCategoryId, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, null, communicationCategoryId, null, _campaignsQuery.GetCampaign(campaign.Name));

            // Reset it.

            _communicationCategoryDropDownList.SelectedValue = string.Empty;
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Name));
        }

        [TestMethod]
        public void UpdateQueryTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);
            AssertCampaign(CampaignCategory.Member, null, null, null, _campaignsQuery.GetCampaign(campaign.Id));

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign.Id));
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _queryTextArea.Text = Query;
            _saveButton.Click();
            Assert.AreEqual(campaign.Name, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(Query, _queryTextArea.Text.Trim().Trim());

            AssertCampaign(CampaignCategory.Member, null, null, Query, _campaignsQuery.GetCampaign(campaign.Id));
            AssertCampaign(CampaignCategory.Member, null, null, Query, _campaignsQuery.GetCampaign(campaign.Name));
        }

        [TestMethod]
        public void NotFoundTest()
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            var id = Guid.NewGuid();
            Get(HttpStatusCode.NotFound, GetEditCampaignUrl(id));
            AssertPageContains("The campaign with id '" + id + "' cannot be found.");
        }

        [TestMethod]
        public void NameErrorsTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);

            // Login as administrator.

            LogIn(administrator);

            // No name.

            Get(GetEditCampaignUrl(campaign.Id));
            _nameTextBox.Text = string.Empty;
            _saveButton.Click();
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            AssertPageContains("The name is required.");

            // Long name.

            Get(GetEditCampaignUrl(campaign.Id));
            var longName = new string('a', 200);
            _nameTextBox.Text = longName;
            _saveButton.Click();
            Assert.AreEqual(longName, _nameTextBox.Text);
            AssertPageContains("The name must be no more than 100 characters in length.");
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign1 = CreateCampaign(1, administrator);
            var campaign2 = CreateCampaign(2, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditCampaignUrl(campaign1.Id));
            _nameTextBox.Text = campaign2.Name;
            _saveButton.Click();
            Assert.AreEqual(campaign2.Name, _nameTextBox.Text);
            AssertPageContains("The name is already being used.");
        }

        [TestMethod]
        public void NameHtmlTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Put some html in the name which should be stripped out.

            NameHtmlTest(campaign, "<div>Some html1</div>", "Some html1");
            NameHtmlTest(campaign, "<div>Some html2", "Some html2");
            NameHtmlTest(campaign, "<script>Some html3</script>", "Some html3");
            NameHtmlTest(campaign, "<script>Some </script>html4", "Some html4");
            NameHtmlTest(campaign, "<script>Some</script>html5", "Somehtml5");
        }

        private static void AssertCampaign(CampaignCategory category, Guid? communicationDefinitionId, Guid? communicationCategoryId, string query, Campaign campaign)
        {
            Assert.AreEqual(category, campaign.Category);
            Assert.AreEqual(communicationDefinitionId, campaign.CommunicationDefinitionId);
            Assert.AreEqual(communicationCategoryId, campaign.CommunicationCategoryId);
            Assert.AreEqual(query, campaign.Query);
        }

        private void NameHtmlTest(Campaign campaign, string withHtml, string withoutHtml)
        {
            Get(GetEditCampaignUrl(campaign.Id));
            _nameTextBox.Text = withHtml;
            _saveButton.Click();

            // Check.

            Assert.AreEqual(withoutHtml, _nameTextBox.Text);
        }
    }
}
