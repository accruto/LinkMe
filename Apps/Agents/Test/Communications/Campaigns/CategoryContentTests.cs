using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns
{
    public abstract class CategoryContentTests
        : CategoryTests
    {
        protected const string EmailAddress = "test@test.linkme.net.au";
        protected const string FirstName = "Homer";
        protected const string LastName = "Simpson";

        private static readonly EmailRecipient Return = new EmailRecipient("do_not_reply@test.linkme.net.au", "LinkMe");

        protected void TestContent(string templateSubject, string templateBody)
        {
            TestContent(templateSubject, templateBody, templateSubject, templateBody);
        }

        protected void TestContent(string templateSubject, string templateBody, string expectedBody)
        {
            TestContent(templateSubject, templateBody, templateSubject, expectedBody);
        }

        protected void TestContent(string templateSubject, string templateBody, string expectedSubject, string expectedBody)
        {
            var campaignEmailsCommand = CreateCampaignEmailsCommand();

            // Create the campaign.

            var campaign = CreateTestCampaign();
            var template = new Template
            {
                Subject = templateSubject,
                Body = templateBody
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);

            // Send it.

            var user = new EmailUser(EmailAddress, FirstName, LastName);
            var campaignEmail = CreateEmail(campaign, user);
            campaignEmailsCommand.Send(new[] { campaignEmail }, campaign.Id, true);

            // Assert.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, user);
            email.AssertSubject(expectedSubject);
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(campaignEmail, expectedBody));
        }
    }
}