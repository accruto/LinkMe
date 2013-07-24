using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Roles.Communications.Campaigns;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communications.Campaigns
{
    [TestClass]
    public class PreviewTests
        : CampaignsTests
    {
        private const string Subject = "My updated subject";
        private const string Body = "My updated body";

        private HtmlTextBoxTester _subjectTextBox;
        private HtmlTextAreaTester _bodyTextArea;

        [TestInitialize]
        public void TestInitialize()
        {
            _subjectTextBox = new HtmlTextBoxTester(Browser, "Subject");
            _bodyTextArea = new HtmlTextAreaTester(Browser, "Body");
        }

        [TestMethod]
        public void MemberPreviewTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Member, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Preview.

            Get(GetPreviewUrl(campaign.Id));
            AssertPageContains(Body);
        }

        [TestMethod]
        public void EmployerPreviewTest()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var campaign = CreateCampaign(1, CampaignCategory.Employer, administrator);

            // Login as administrator.

            LogIn(administrator);

            // Navigate to page and set the template.

            Get(GetEditTemplateUrl(campaign.Id));
            _subjectTextBox.Text = Subject;
            _bodyTextArea.Text = Body;
            _saveButton.Click();

            // Preview.

            Get(GetPreviewUrl(campaign.Id));
            AssertPageContains(Body);
        }
    }
}
