using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Apps.Presentation.Domain.Roles.Candidates;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Integration;
using LinkMe.Domain.Roles.Integration.Commands;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Integration;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds
{
    [TestClass]
    public class ResumeTests
        : IntegrationTests
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IIntegrationCommand _integrationCommand = Resolve<IIntegrationCommand>();
        private readonly IResumeFilesQuery _resumeFilesQuery = Resolve<IResumeFilesQuery>();

        private const string OtherUserName = "otherintegrator";
        private const string OtherPassword = "secretpass";

        [TestMethod]
        public void TestSuccess()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            var resume = _candidateResumesCommand.AddTestResume(candidate);
            var view = new EmployerMemberView(member, candidate, resume, null, ProfessionalContactDegree.Applicant, false, false);
            var resumeFile = _resumeFilesQuery.GetResumeFile(view, resume);

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            Assert.AreEqual(resumeFile.Contents, Get(GetResumeUrl(candidate.Id), integratorUser, Password, true));
        }

        [TestMethod]
        public void TestSecurity()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // Incorrect password.

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var response = Get(GetResumeUrl(candidate.Id), integratorUser, "badpass", true);
            Assert.AreEqual("Error: Web service authorization failed: the password for user '" + integratorUser.LoginId + "' is incorrect.", response);

            // Integrator with no access to resumes.

            var otherIntegratorUser = _integrationCommand.CreateTestIntegratorUser(OtherUserName, OtherPassword, IntegratorPermissions.GetJobAds | IntegratorPermissions.PostJobAds);
            response = Get(GetResumeUrl(candidate.Id), otherIntegratorUser, OtherPassword, true);
            Assert.AreEqual("Error: Web service authorization failed: user 'otherintegrator' does not have permission to access the requested service.", response);
        }

        [TestMethod, ExpectedException(typeof(NotFoundException))]
        public void TestErrors()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            // No such resume.

            var integratorUser = _integrationCommand.CreateTestIntegratorUser();
            var badId = Guid.NewGuid();
            Get(GetResumeUrl(badId), integratorUser, Password, true);
        }

        private static ReadOnlyUrl GetResumeUrl(Guid memberId)
        {
            return new ReadOnlyApplicationUrl(true, "~/resume/" + memberId.ToString("n") + "/file/rtf");
        }
    }
}