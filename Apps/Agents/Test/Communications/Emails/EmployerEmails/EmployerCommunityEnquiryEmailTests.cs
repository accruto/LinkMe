using System;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class EmployerCommunityEnquiryEmailTests
        : EmailTests
    {
        private const string SubjectTemplate = "Thank you for registering your interest with {0}";

        public override TemplateEmail GeneratePreview(Community community)
        {
            // Only valid for Monash Business and Economics.

            if (community == null)
                community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var enquiry = CreateEnquiry();
            return new EmployerCommunityEnquiryEmail(community, enquiry);
        }

        [TestMethod]
        public void TestMailContents()
        {
            // Send.

            var community = TestCommunity.MonashGsb.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
            var enquiry = CreateEnquiry();
            var communication = new EmployerCommunityEnquiryEmail(community, enquiry);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, new EmailRecipient(enquiry.EmailAddress, enquiry.FirstName.CombineLastName(enquiry.LastName), enquiry.FirstName, enquiry.LastName));
            email.AssertSubject(GetSubject(community));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new Employer(), GetContent(community, enquiry)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private static string GetSubject(Community community)
        {
            return string.Format(SubjectTemplate, community.Name);
        }

        private static string GetContent(Community community, AffiliationEnquiry enquiry)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>Hi " + enquiry.FirstName + "</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  Thank you for registering your interest in becoming");
            sb.AppendLine("  a Preferred Employment Partner through");
            sb.AppendLine("  the Monash Business and Economics career and networking portal.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  You will be contacted shortly to organise your access");
            sb.AppendLine("  to search, review and contact");
            sb.AppendLine("  " + community.Name + " students and graduates");
            sb.AppendLine("  for employment opportunities.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  We look forward to working with you.");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        protected override void GetClientSignature(TemplateEmail email, StringBuilder sb)
        {
            sb.AppendLine("        <p>Kind regards,</p>");
            sb.AppendLine("        <p>Monash Business and Economics Career Services.</p>");
            sb.AppendLine("        <p>");
            sb.AppendLine("          <img src=\"" + InsecureRootPath + "themes/communities/monash/gsb/img/email-logo.jpg\" alt=\"Monash Business and Economics\" />");
            sb.AppendLine("        </p>");
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