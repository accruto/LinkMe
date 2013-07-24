using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Domain.Roles.Communications.Campaigns;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns
{
    public abstract class CategoryPreviewTests
        : CategoryTests
    {
        protected void TestPreview(string subject, string body)
        {
            // Create the campaign.

            var campaign = CreateTestCampaign();
            var template = new Template
            {
                Subject = subject,
                Body = body
            };
            _campaignsCommand.CreateTemplate(campaign.Id, template);

            // Get the preview.

            var email = CreateEmail(campaign, new EmailUser("test@test.linkme.net.au"));
            var command = CreateCampaignEmailsCommand();
            var communication = command.GeneratePreview(email, campaign.Id);

            AssertCommunication(email, subject, body, communication);
        }
    }
}