using System;
using System.IO;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Mocks.Hosts;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers
{
    [TestClass]
    public class JobAdsTests
        : TestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IInternalApplicationsCommand _internalApplicationsCommand = Resolve<IInternalApplicationsCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();
        private IMockEmailServer _emailServer;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
            _emailServer = EmailHost.Start();
            _emailServer.ClearEmails();
        }

        [TestMethod]
        public void TestSubmitWithProfile()
        {
            var member = CreateMember();
            AddResume(member.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplication(member, jobAd, null);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            email.AssertAttachment(member.FullName + ".doc", "application/msword");
        }

        [TestMethod]
        public void TestSubmitWithLastUsedFile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            AddResume(member.Id);

            var fileReference = GetResumeFile();
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            email.AssertAttachment(fileReference.FileName, "application/msword");
        }

        [TestMethod]
        public void TestSubmitWithResume()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            AddResume(member.Id);

            var fileReference = GetResumeFile();

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithResume(member, jobAd, fileReference.Id, false, null);

            // Check the email.

            var email = _emailServer.AssertEmailSent();
            email.AssertAttachment(fileReference.FileName, "application/msword");
        }

        private Member CreateMember()
        {
            return _memberAccountsCommand.CreateTestMember(0);
        }

        protected Employer CreateEmployer()
        {
            return _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected JobAd CreateJobAd(IEmployer employer)
        {
            return _jobAdsCommand.PostTestJobAd(employer);
        }

        private void AddResume(Guid memberId)
        {
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume);
        }

        protected FileReference GetResumeFile()
        {
            const string fileName = "resume.doc";
            var data = TestResume.Complete.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }
    }
}
