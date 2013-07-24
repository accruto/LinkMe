using System.Text;
using System.Web;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class JobApplicationEmailTests
        : EmailTests
    {
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();

        private const string ExternalReferenceId = "ABC123";
        private const string SecondaryEmail = "linkme2@test.linkme.net.au";
        private const string TertiaryEmail = "linkme3@test.linkme.net.au";

        private const string CoverLetter = @"Dear Sir or Madam,

I would like to apply for the position you advertised on LinkMe.

I can be contacted at member0@test.linkme.com.au at your convenience.

Thanks,

Paul Hodgman

";

        private const string RequiresEncodingCoverLetter = @"Give & it
to me";

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            return new JobApplicationEmail(member, jobApplication, jobAd, null, null);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(jobAd.ContactDetails.EmailAddress));
            email.AssertSubject(GetSubject(jobAd));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new UnregisteredEmployer { EmailAddress = new EmailAddress { Address = jobAd.ContactDetails.EmailAddress } }, GetContent(communication, member, jobAd, CoverLetter)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailExternalReferenceId()
        {
            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            jobAd.Integration.ExternalReferenceId = ExternalReferenceId;
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(jobAd.ContactDetails.EmailAddress));
            email.AssertSubject(GetSubject(jobAd));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new UnregisteredEmployer { EmailAddress = new EmailAddress { Address = jobAd.ContactDetails.EmailAddress } }, GetContent(communication, member, jobAd, CoverLetter)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestSingleSecondaryEmail()
        {
            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            jobAd.ContactDetails.SecondaryEmailAddresses = SecondaryEmail;
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(jobAd.ContactDetails.EmailAddress), new[] { new EmailRecipient(SecondaryEmail) }, null);
            email.AssertSubject(GetSubject(jobAd));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new UnregisteredEmployer { EmailAddress = new EmailAddress { Address = jobAd.ContactDetails.EmailAddress } }, GetContent(communication, member, jobAd, CoverLetter)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestMultipleSecondaryEmail()
        {
            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            jobAd.ContactDetails.SecondaryEmailAddresses = SecondaryEmail + "," + TertiaryEmail;
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = CoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(jobAd.ContactDetails.EmailAddress), new[] { new EmailRecipient(SecondaryEmail), new EmailRecipient(TertiaryEmail) }, null);
            email.AssertSubject(GetSubject(jobAd));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new UnregisteredEmployer { EmailAddress = new EmailAddress { Address = jobAd.ContactDetails.EmailAddress } }, GetContent(communication, member, jobAd, CoverLetter)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestEmailRequiresEncodingContents()
        {
            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);

            // Create an employer.

            var employer = CreateEmployer();
            var jobAd = employer.CreateTestJobAd();
            var jobApplication = new InternalApplication { PositionId = jobAd.Id, ApplicantId = candidate.Id, CoverLetterText = RequiresEncodingCoverLetter };

            // Send the email.

            var communication = new JobApplicationEmail(member, jobApplication, jobAd, null, null);
            _emailsCommand.TrySend(communication);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(member, Return, new EmailRecipient(jobAd.ContactDetails.EmailAddress));
            email.AssertSubject(GetSubject(jobAd));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(communication, new UnregisteredEmployer { EmailAddress = new EmailAddress { Address = jobAd.ContactDetails.EmailAddress } }, GetContent(communication, member, jobAd, RequiresEncodingCoverLetter)));
            email.AssertNoAttachments();
            AssertCompatibleAddresses(email);
        }

        private string GetContent(TemplateEmail templateEmail, IRegisteredUser member, JobAdEntry jobAd, string coverLetter)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine();
            sb.AppendLine("  Hi,");
            sb.AppendLine();
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  We wanted to let you know that " + member.FullName + " has applied for the");
            sb.AppendLine("  " + jobAd.Title + " job " + (jobAd.Integration.ExternalReferenceId == null ? "" : " (ref# " + jobAd.Integration.ExternalReferenceId + ")"));
            sb.AppendLine("  on <a href=\"" + GetTinyUrl(templateEmail, false, "~/employers") + "\">LinkMe.com.au</a>.");
            sb.AppendLine("  You can also see their online resume");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/candidates", "candidateId", member.Id.ToString()) + "\">here</a>.");
            sb.AppendLine("</p>");
            sb.AppendLine();
            sb.AppendLine("<p>");
            sb.AppendLine("  " + member.FirstName + " attached a cover letter:");
            sb.AppendLine("</p>");
            sb.AppendLine("<p style=\"padding-left:40px\">");
            sb.AppendLine(HtmlUtil.LineBreaksToHtml(HttpUtility.HtmlEncode(coverLetter)));
            sb.AppendLine("</p>");
            sb.AppendLine();
            sb.AppendLine("<br />");
            return sb.ToString();
        }

        private static string GetSubject(JobAdEntry jobAd)
        {
            var externalReferenceId = jobAd.Integration.ExternalReferenceId;
            return (string.IsNullOrEmpty(externalReferenceId) ? "" : "[" + externalReferenceId + "] ")
                + string.Format("Application for {0}{1}", jobAd.Title, string.IsNullOrEmpty(externalReferenceId) ? string.Empty : " (ref# " + externalReferenceId + ")");
        }
    }
}