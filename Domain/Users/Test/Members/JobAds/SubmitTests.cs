using System;
using System.IO;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Files;
using LinkMe.Domain.Files.Commands;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Commands;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Employers.JobAds;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds
{
    [TestClass]
    public abstract class SubmitTests
        : TestClass
    {
        protected readonly IInternalApplicationsCommand _internalApplicationsCommand = Resolve<IInternalApplicationsCommand>();
        protected readonly IMemberApplicationsQuery _memberApplicationsQuery = Resolve<IMemberApplicationsQuery>();
        private readonly IJobAdsCommand _jobAdsCommand = Resolve<IJobAdsCommand>();
        private readonly IJobPostersCommand _jobPostersCommand = Resolve<IJobPostersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        protected readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        protected readonly ICandidateResumeFilesCommand _candidateResumeFilesCommand = Resolve<ICandidateResumeFilesCommand>();
        protected readonly ICandidateResumeFilesQuery _candidateResumeFilesQuery = Resolve<ICandidateResumeFilesQuery>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IFilesCommand _filesCommand = Resolve<IFilesCommand>();
        private readonly IParseResumesCommand _parseResumesCommand = Resolve<IParseResumesCommand>();

        [TestInitialize]
        public void SubmitTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Member CreateMember()
        {
            var member = _membersCommand.CreateTestMember(0);
            var candidate = new Candidate { Id = member.Id };
            _candidatesCommand.CreateCandidate(candidate);
            return member;
        }

        protected Resume AddResume(Guid memberId)
        {
            var fileReference = GetResumeFile();
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume);
            return resume;
        }

        protected Resume AddResume(Guid memberId, FileReference fileReference)
        {
            var resume = _parseResumesCommand.ParseResume(fileReference).Resume;
            var candidate = _candidatesQuery.GetCandidate(memberId);
            _candidateResumesCommand.CreateResume(candidate, resume, fileReference);
            return resume;
        }

        protected FileReference GetResumeFile()
        {
            return GetResumeFile(TestResume.Complete);
        }

        protected FileReference GetResumeFile(TestResume testResume)
        {
            const string fileName = "resume.doc";
            var data = testResume.GetData();
            using (var stream = new MemoryStream(data))
            {
                return _filesCommand.SaveFile(FileType.Resume, new StreamFileContents(stream), fileName);
            }
        }

        protected Employer CreateEmployer()
        {
            var employer = _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            _jobPostersCommand.CreateJobPoster(new JobPoster { Id = employer.Id });
            return employer;
        }

        protected JobAd CreateJobAd(IEmployer employer)
        {
            return _jobAdsCommand.PostTestJobAd(employer);
        }

        protected void AssertApplication(Guid applicantId, Guid jobAdId, Application application)
        {
            Assert.AreEqual(applicantId, application.ApplicantId);
            Assert.AreEqual(jobAdId, application.PositionId);
        }

        protected void AssertNoResumeFile(Guid memberId, Guid resumeId)
        {
            Assert.IsNull(_candidateResumeFilesQuery.GetResumeFile(resumeId));
        }

        protected void AssertResumeFile(Guid memberId, Guid resumeId, Guid fileReferenceId)
        {
            var resumeFile = _candidateResumeFilesQuery.GetResumeFile(resumeId);
            Assert.AreEqual(fileReferenceId, resumeFile.FileReferenceId);
        }

        protected void AssertResumeFiles(Guid memberId, params Guid[] fileReferenceIds)
        {
            var resumeFiles = _candidateResumeFilesQuery.GetResumeFiles(memberId);
            Assert.AreEqual(fileReferenceIds.Length, resumeFiles.Count);
            foreach (var fileReferenceId in fileReferenceIds)
                Assert.IsTrue((from r in resumeFiles where r.FileReferenceId == fileReferenceId select r).Any());

            foreach (var fileReferenceId in fileReferenceIds)
            {
                var resumeFile = _candidateResumeFilesQuery.GetResumeFile(memberId, fileReferenceId);
                Assert.AreEqual(fileReferenceId, resumeFile.FileReferenceId);
            }
        }

        protected void UpdateLastUsedTime(Guid memberId, Guid fileReferenceId, DateTime time)
        {
            var resumeFile = _candidatesRepository.GetResumeFile(memberId, fileReferenceId);
            resumeFile.LastUsedTime = time;
            _candidatesRepository.UpdateResumeFile(resumeFile);
        }

        protected void AssertLastUsedTime(Guid memberId, Guid fileReferenceId, DateTime date)
        {
            var resumeFile = _candidatesRepository.GetResumeFile(memberId, fileReferenceId);
            Assert.AreEqual(date, resumeFile.LastUsedTime.Date);
        }
    }
}
