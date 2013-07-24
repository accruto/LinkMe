using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class CreateCampaignTests
        : CampaignsTests
    {
        private const string CampaignName = "My new campaign";
        private const string Query = "SELECT id FROM Member";

        [TestMethod]
        public void CreateTest()
        {
            var communicationCategoryId = _settingsQuery.GetCategory("Campaign").Id;

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = CampaignName;
            _saveButton.Click();
            AssertPageContains(CampaignName);

            // Check the details have been set.

            Get(GetEditCampaignUrl(GetCurrentCampaignId()));
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, communicationCategoryId, null, _campaignsQuery.GetCampaign(CampaignName));
        }

        [TestMethod]
        public void CreateCategoryTest()
        {
            var communicationCategoryId = _settingsQuery.GetCategory("Campaign").Id;

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = CampaignName;
            _categoryDropDownList.SelectedValue = CampaignCategory.Employer.ToString();
            _saveButton.Click();
            AssertPageContains(CampaignName);

            // Check the details have been set.

            Get(GetEditCampaignUrl(GetCurrentCampaignId()));
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Employer.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Employer, null, communicationCategoryId, null, _campaignsQuery.GetCampaign(CampaignName));
        }

        [TestMethod]
        public void CreateCommunicationCategoryTest()
        {
            var communicationCategoryId = _settingsQuery.GetCategory("Campaign").Id;

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            communicationCategoryId = _settingsQuery.GetCategories(UserType.Member)[1].Id;

            _nameTextBox.Text = CampaignName;
            _communicationCategoryDropDownList.SelectedValue = communicationCategoryId.ToString();
            _saveButton.Click();
            AssertPageContains(CampaignName);

            // Check the details have been set.

            Get(GetEditCampaignUrl(GetCurrentCampaignId()));
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, communicationCategoryId, null, _campaignsQuery.GetCampaign(CampaignName));
        }

        [TestMethod]
        public void CreateCommunicationDefinitionTest()
        {
            var communicationCategoryId = _settingsQuery.GetCategory("Campaign").Id;
            var communicationDefinitionId = _settingsQuery.GetDefinition("IosLaunchEmail").Id;

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = CampaignName;
            _communicationDefinitionDropDownList.SelectedValue = communicationDefinitionId.ToString();
            _saveButton.Click();
            AssertPageContains(CampaignName);

            // Check the details have been set.

            Get(GetEditCampaignUrl(GetCurrentCampaignId()));
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationDefinitionId.ToString(), _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, communicationDefinitionId, communicationCategoryId, null, _campaignsQuery.GetCampaign(CampaignName));
        }

        [TestMethod]
        public void CreateQueryTest()
        {
            var communicationCategoryId = _settingsQuery.GetCategory("Campaign").Id;

            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _queryTextArea.Text.Trim());

            // Set it and save.

            _nameTextBox.Text = CampaignName;
            _queryTextArea.Text = Query;
            _saveButton.Click();
            AssertPageContains(CampaignName);

            // Check the details have been set.

            Get(GetEditCampaignUrl(GetCurrentCampaignId()));
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            Assert.AreEqual(CampaignCategory.Member.ToString(), _categoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(string.Empty, _communicationDefinitionDropDownList.SelectedItem.Value);
            Assert.AreEqual(communicationCategoryId.ToString(), _communicationCategoryDropDownList.SelectedItem.Value);
            Assert.AreEqual(Query, _queryTextArea.Text.Trim());

            AssertCampaign(CampaignCategory.Member, null, communicationCategoryId, Query, _campaignsQuery.GetCampaign(CampaignName));
        }

        [TestMethod]
        public void NameErrorsTest()
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // No name.

            Get(_newCampaignUrl);
            _saveButton.Click();
            Assert.AreEqual(string.Empty, _nameTextBox.Text);
            AssertPageContains("The name is required.");

            // Long name.

            Get(_newCampaignUrl);
            var longName = new string('a', 200);
            _nameTextBox.Text = longName;
            _saveButton.Click();
            Assert.AreEqual(longName, _nameTextBox.Text);
            AssertPageContains("The name must be no more than 100 characters in length.");
        }

        [TestMethod]
        public void DuplicateNameTest()
        {
            // Login as administrator.

          var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Navigate to page.

            Get(_newCampaignUrl);
            _nameTextBox.Text = CampaignName;
            _saveButton.Click();

            // Create another with the same name.

            Get(_newCampaignUrl);
            _nameTextBox.Text = CampaignName;
            _saveButton.Click();
            Assert.AreEqual(CampaignName, _nameTextBox.Text);
            AssertPageContains("The name is already being used.");
        }

        [TestMethod]
        public void NameHtmlTest()
        {
            // Login as administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            LogIn(administrator);

            // Put some html in the name which should be stripped out.

            NameHtmlTest("<div>Some html1</div>", "Some html1");
            NameHtmlTest("<div>Some html2", "Some html2");
            NameHtmlTest("<script>Some html3</script>", "Some html3");
            NameHtmlTest("<script>Some </script>html4", "Some html4");
            NameHtmlTest("<script>Some</script>html5", "Somehtml5");
        }

        private static void AssertCampaign(CampaignCategory category, Guid? communicationDefinitionId, Guid? communicationCategoryId, string query, Campaign campaign)
        {
            Assert.AreEqual(category, campaign.Category);
            Assert.AreEqual(communicationDefinitionId, campaign.CommunicationDefinitionId);
            Assert.AreEqual(communicationCategoryId, campaign.CommunicationCategoryId);
            Assert.AreEqual(query, campaign.Query);
        }

        private void NameHtmlTest(string withHtml, string withoutHtml)
        {
            Get(_newCampaignUrl);
            _nameTextBox.Text = withHtml;
            _saveButton.Click();

            // Check.

            AssertPageContains(withoutHtml);
        }
    }
}
