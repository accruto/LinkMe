using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class UpdateTemplateTests
        : CampaignsTests
    {
        private const string UpdatedSubject = "My updated subject";
        private const string UpdatedBody = "My updated body";

        private HtmlTextBoxTester _subjectTextBox;
        private HtmlTextAreaTester _bodyTextArea;

        [TestInitialize]
        public void TestInitialize()
        {
            _subjectTextBox = new HtmlTextBoxTester(Browser, "Subject");
            _bodyTextArea = new HtmlTextAreaTester(Browser, "Body");
        }

        [TestMethod]
        public void UpdateTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page.

            Get(GetEditTemplateUrl(campaign.Id));
            Assert.AreEqual(string.Empty, _subjectTextBox.Text);
            Assert.AreEqual(string.Empty, _bodyTextArea.Text.Trim());

            // Set it and save.

            _subjectTextBox.Text = UpdatedSubject;
            _bodyTextArea.Text = UpdatedBody;
            _saveButton.Click();
            Assert.AreEqual(UpdatedSubject, _subjectTextBox.Text);
            Assert.AreEqual(global::System.Environment.NewLine + UpdatedBody, _bodyTextArea.Text);
        }
    }
}
