using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class CandidateResumesEmailTests
        : EmailTests
    {
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerResumeFilesQuery _employerResumeFilesQuery = Resolve<IEmployerResumeFilesQuery>();

        public override TemplateEmail GeneratePreview(Community community)
        {
            if (community != null)
                return null;

            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member1 = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member1.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var member2 = CreateOtherMember();
            candidate = _candidatesCommand.GetCandidate(member2.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new [] {member1, member2});
            return new CandidateResumesEmail(employer, views);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member1 = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member1.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var member2 = CreateOtherMember();
            candidate = _candidatesCommand.GetCandidate(member2.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] { member1, member2 });
            var templateEmail = new CandidateResumesEmail(employer, views);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject());
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, new[]{member1, member2})));
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestAttachment()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member1 = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member1.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var member2 = CreateOtherMember();
            candidate = _candidatesCommand.GetCandidate(member2.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var views = _employerMemberViewsQuery.GetEmployerMemberViews(employer, new[] { member1, member2 });
            var templateEmail = new CandidateResumesEmail(employer, views);
            templateEmail.AddAttachments(new[] { GetResumeFile(views) });
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAttachment("Resumes.zip", "application/zip");
        }

        private static string GetSubject()
        {
            return "Resumes";
        }

        private string GetContent(TemplateEmail templateEmail, IEnumerable<Member> members)
        {
            var ids = (from m in members select "candidateId=" + m.Id).ToArray();

            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  Please find attached the resumes as requested.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine();
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/candidates?" + string.Join("&", ids)) + "\">View these resumes</a>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/search/candidates") + "\">Search for more candidates</a>");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private ContentAttachment GetResumeFile(IEnumerable<EmployerMemberView> views)
        {
            var resumeFile = _employerResumeFilesQuery.GetResumeFile(views);
            var fileName = resumeFile.Name;

            // Save the contents of the zip file into a stream.

            var stream = new MemoryStream();
            resumeFile.Save(stream);
            stream.Position = 0;
            return new ContentAttachment(stream, fileName, MediaType.GetMediaTypeFromExtension(Path.GetExtension(fileName), MediaType.Text));
        }
    }
}