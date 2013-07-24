using System;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.InternalEmails;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.InternalEmails
{
    [TestClass]
    public class AdministratorEmployerEnquiryEmailTests
        : EmailTests
    {
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();

        private const string SubjectTemplate = "Employer Access Request to the '{0}' Community";
        private const string LoginId = "administrator";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community == null)
                community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(LoginId, community.Id);
            var enquiry = CreateEnquiry();
            return new AdministratorEmployerEnquiryEmail(community, new[] { custodian }, enquiry);
        }

        [TestMethod]
        public void TestMailContents()
        {
            // Send.

            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(LoginId, community.Id);

            var enquiry = CreateEnquiry();
            var communication = new AdministratorEmployerEnquiryEmail(community, new [] { custodian }, enquiry);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(System, Return, ClientServicesInbox, new [] { new EmailRecipient(custodian.EmailAddress.Address, custodian.FullName, custodian.FirstName, custodian.LastName) }, null);
            email.AssertSubject(GetSubject(community));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, GetContent(enquiry)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(Community community)
        {
            return string.Format(SubjectTemplate, community.Name);
        }

        private static string GetContent(AffiliationEnquiry enquiry)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Access Request Submitted</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Id: " + enquiry.Id + "<br />");
            sb.AppendLine("  Company Name: " + enquiry.CompanyName + "<br />");
            sb.AppendLine("  Email Address: " + enquiry.EmailAddress + "<br />");
            sb.AppendLine("  First Name: " + enquiry.FirstName + "<br />");
            sb.AppendLine("  Last Name: " + enquiry.LastName + "<br />");
            sb.AppendLine("  Job Title: " + enquiry.JobTitle + "<br />");
            sb.AppendLine("  Phone Number: " + enquiry.PhoneNumber + "<br />");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private static AffiliationEnquiry CreateEnquiry()
        {
            return new AffiliationEnquiry
                       {
                           Id = Guid.NewGuid(),
                           CompanyName = "Test company",
                           CreatedTime = DateTime.Now,
                           EmailAddress = "test@test.linkme.net.au",
                           FirstName = "Barney",
                           LastName = "Gumble",
                           JobTitle = "CEO",
                           PhoneNumber = "99999999",
                       };
        }
    }
}