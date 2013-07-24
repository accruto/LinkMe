using System.IO;
using System.Text;
using LinkMe.Apps.Agents.Communications.Emails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Files;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails.EmployerEmails
{
    [TestClass]
    public class CandidateResumeEmailTests
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

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            return new CandidateResumeEmail(employer, view);
        }

        [TestMethod]
        public void TestEmailContents()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var templateEmail = new CandidateResumeEmail(employer, view);
            _emailsCommand.TrySend(templateEmail);

            // Check.

            var email = _emailServer.AssertEmailSent();
            email.AssertAddresses(Return, Return, employer);
            email.AssertSubject(GetSubject(view.FullName ?? "[Name hidden]"));
            email.AssertHtmlViewChecks();
            email.AssertHtmlView(GetBody(templateEmail, employer, GetContent(templateEmail, member, candidate, employer)));
            AssertCompatibleAddresses(email);
        }

        [TestMethod]
        public void TestAttachment()
        {
            // Create an employer.

            var employer = CreateEmployer();

            // Create a member.

            var member = CreateMember();
            var candidate = _candidatesCommand.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Send the email.

            var view = _employerMemberViewsQuery.GetEmployerMemberView(employer, member);
            var templateEmail = new CandidateResumeEmail(employer, view);
            templateEmail.AddAttachments(new[] { GetResumeFile(view) });
            _emailsCommand.TrySend(templateEmail);

            var email = _emailServer.AssertEmailSent();
            email.AssertAttachment("Mentor Business Analyst Course, Melbourne VIC 3000.doc", "application/msword");
        }

        private static string GetSubject(string fullName)
        {
            return "The resume of " + fullName;
        }

        private string GetContent(TemplateEmail templateEmail, Member member, ICandidate candidate, IEmployer employer)
        {
            var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
            var sb = new StringBuilder();
            sb.AppendLine("<p>");
            sb.AppendLine("  Please find attached the resume of");
            sb.AppendLine("  <strong>" + (view.FullName ?? "[Name hidden]") + "</strong>");
            sb.AppendLine("  as requested.");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/employers/candidates", "candidateId", candidate.Id.ToString()) + "\">View this resume</a>");
            sb.AppendLine("</p>");
            sb.AppendLine("<p>");
            sb.AppendLine("  <a href=\"" + GetTinyUrl(templateEmail, true, "~/search/candidates") + "\">Search for more candidates</a>");
            sb.AppendLine("</p>");
            return sb.ToString();
        }

        private ContentAttachment GetResumeFile(EmployerMemberView view)
        {
            var resumeFile = _employerResumeFilesQuery.GetResumeFile(view);

            // Save the contents into a stream.

            var stream = new MemoryStream();
            stream.Write(resumeFile.Contents, 0, resumeFile.Contents.Length);
            stream.Position = 0;
            return new ContentAttachment(stream, resumeFile.Name, MediaType.GetMediaTypeFromExtension(Path.GetExtension(resumeFile.Name), MediaType.Text));
        }
    }
}