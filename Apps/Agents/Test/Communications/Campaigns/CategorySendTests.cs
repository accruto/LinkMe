using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Test.Communications.Mocks;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns
{
    public abstract class CategorySendTests
        : CategoryTests
    {
        private const string TemplateSubject = "My subject";
        private const string TemplateBody = "My body";
        private const string EmailAddress = "test@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        protected void SendEmail()
        {
            // Create the campaign.

            var campaignEmailsCommand = CreateCampaignEmailsCommand();
            var campaign = CreateCampaign();

            // Send it.

            var email = CreateEmail(campaign, new EmailUser(EmailAddress));
            campaignEmailsCommand.Send(new[] { email }, campaign.Id, true);

            // Assert.

            _emailServer.AssertEmailSent();
        }

        protected void SendTestEmailTwice()
        {
            // Create the campaign.

            var campaignEmailsCommand = CreateCampaignEmailsCommand();
            var campaign = CreateCampaign();

            // Send it.

            var email = CreateEmail(campaign, new EmailUser(EmailAddress));
            campaignEmailsCommand.Send(new[] { email }, campaign.Id, true);

            // Assert.

            _emailServer.AssertEmailSent();

            // Send again.

            campaignEmailsCommand.Send(new[] { email }, campaign.Id, true);
            _emailServer.AssertEmailSent();
        }

        protected void SendNonTestEmailTwice()
        {
            // Create the campaign.

            var campaignEmailsCommand = CreateCampaignEmailsCommand();
            var campaign = CreateCampaign();

            // Send it.

            var contact = new EmailUser(EmailAddress, FirstName, LastName);
            var email = CreateEmail(campaign, contact);
            campaignEmailsCommand.Send(new[] { email }, campaign.Id, false);

            // Assert.

            _emailServer.AssertEmailSent();

            // Send again.

            campaignEmailsCommand.Send(new[] { email }, campaign.Id, false);
            _emailServer.AssertNoEmailSent();
        }

        private Campaign CreateCampaign()
        {
            var campaign = CreateTestCampaign();
            var template = new Template {Subject = TemplateSubject, Body = TemplateBody};
            _campaignsCommand.CreateTemplate(campaign.Id, template);
            return campaign;
        }
    }
}