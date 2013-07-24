using System.Collections.Generic;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.MemberToMemberNotifications;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.MemberToMemberNotifications
{
    [TestClass]
    public class SendJobToFriendEmailTests
        : EmailTests
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();

        private const string SenderAddress = "linkme1@test.linkme.net.au";
        private const string SenderDisplayName = "Borat";
        private const string RecipientAddress = "linkme2@test.linkme.net.au";
        private const string RecipientDisplayName = "Azhamat";
        private const string MessageText = "My name a Borat, you see my movie-film or I get execute.";

        public override TemplateEmail GeneratePreview(Community community)
        {
            var adToSend = CreateTestEmployerAndJobAd();

            // With user entered message.

            return new SendJobToFriendEmail(RecipientAddress, RecipientDisplayName, SenderAddress, SenderDisplayName, adToSend, MessageText);
        }

        [TestMethod]
        public void TestSendJobToFriendEmail()
        {
            var adToSend = CreateTestEmployerAndJobAd();

            // With user entered message.

            var templateEmail = new SendJobToFriendEmail(RecipientAddress, RecipientDisplayName, SenderAddress, SenderDisplayName, adToSend, MessageText);
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(SenderAddress, SenderDisplayName), Return, new EmailRecipient(RecipientAddress, RecipientDisplayName));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member {EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = RecipientAddress }}}, GetContent(templateEmail, MessageText, adToSend)));
            email.AssertNoAttachments();

            // Without user entered message.

            templateEmail = new SendJobToFriendEmail(RecipientAddress, RecipientDisplayName, SenderAddress, SenderDisplayName, adToSend, string.Empty);
            _emailsCommand.TrySend(templateEmail);

            email = _emailServer.AssertEmailSent();
            email.AssertAddresses(new EmailRecipient(SenderAddress, SenderDisplayName), Return, new EmailRecipient(RecipientAddress, RecipientDisplayName));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, new Member { EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = RecipientAddress } } }, GetContent(templateEmail, string.Empty, adToSend)));
            email.AssertNoAttachments();
        }

        private JobAd CreateTestEmployerAndJobAd()
        {
            Employer jobPoster = _employerAccountsCommand.CreateTestEmployer("employer", _organisationsCommand.CreateTestOrganisation(0));
            return _jobAdsCommand.PostTestJobAd(jobPoster);
        }

        private string GetContent(TemplateEmail templateEmail, string messageText, JobAdEntry ad)
        {
            var builder = new StringBuilder();
            builder.AppendLine("<p>Hi " + RecipientDisplayName +"</p>");
            builder.AppendLine("<p>");
            builder.AppendLine("  " + SenderDisplayName + " has found an interesting job ");
            builder.AppendLine("  opportunity and thought that you should see it.");
            builder.AppendLine("</p>");
            builder.AppendLine();

            if(!string.IsNullOrEmpty(messageText))
            {
                builder.AppendLine("<p>");
                builder.AppendLine("  " + SenderDisplayName + "'s personal message:");
                builder.AppendLine("  <br />");
                builder.AppendLine("  " + messageText);
                builder.AppendLine("</p>");
                builder.AppendLine();
            }

            builder.AppendLine("<p>");
            builder.AppendLine("  View the ");
            builder.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, false, "~/jobs/" + ad.Id) + "\">" + ad.Title + "</a>");
            builder.AppendLine("  job and apply now.");
            builder.AppendLine("</p>");
            return builder.ToString();
        }
    }
}