using System;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Users.Members.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds
{
    [TestClass]
    public class SubmitWithLastUsedResumeTests
        : SubmitTests
    {
        [TestMethod]
        public void TestSubmitProfileFile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            var resume = AddResume(member.Id, fileReference);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var applicationId = _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check the application.

            var applications = _memberApplicationsQuery.GetApplications(member.Id);
            Assert.AreEqual(1, applications.Count);
            Assert.AreEqual(applicationId, applications[0].Id);
            AssertApplication(member.Id, jobAd.Id, fileReference.Id, applications[0]);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
        }

        [TestMethod]
        public void TestSubmitNonProfileFile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var resume = AddResume(member.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);

            var fileReference = GetResumeFile();
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id, fileReference.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var applicationId = _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check the application.

            var applications = _memberApplicationsQuery.GetApplications(member.Id);
            Assert.AreEqual(1, applications.Count);
            Assert.AreEqual(applicationId, applications[0].Id);
            AssertApplication(member.Id, jobAd.Id, fileReference.Id, applications[0]);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
        }

        [TestMethod]
        public void TestProfileLastUpdatedTime()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);

            // Update the last used time for some time in the past.

            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date);
            UpdateLastUsedTime(member.Id, fileReference.Id, DateTime.Now.AddDays(-2));
            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date.AddDays(-2));

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check that the last used time was updated.

            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date);
        }

        [TestMethod]
        public void TestNonProfileLastUpdatedTime()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            AddResume(member.Id);

            var fileReference = GetResumeFile();
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference.Id });

            // Update the last used time for some time in the past.

            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date);
            UpdateLastUsedTime(member.Id, fileReference.Id, DateTime.Now.AddDays(-2));
            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date.AddDays(-2));

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check that the last used time was updated.

            AssertLastUsedTime(member.Id, fileReference.Id, DateTime.Now.Date);
        }

        [TestMethod]
        public void TestMultipleFiles()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference1 = GetResumeFile(TestResume.Complete);
            var resume = AddResume(member.Id, fileReference1);
            UpdateLastUsedTime(member.Id, fileReference1.Id, DateTime.Now.AddDays(-2));
            AssertResumeFile(member.Id, resume.Id, fileReference1.Id);
            AssertResumeFiles(member.Id, fileReference1.Id);

            var fileReference2 = GetResumeFile(TestResume.NoPhoneNumber);
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference2.Id });
            UpdateLastUsedTime(member.Id, fileReference2.Id, DateTime.Now.AddDays(-1));
            AssertResumeFile(member.Id, resume.Id, fileReference1.Id);
            AssertResumeFiles(member.Id, fileReference1.Id, fileReference2.Id);

            var fileReference3 = GetResumeFile(TestResume.NoName);
            _candidateResumeFilesCommand.CreateResumeFile(member.Id, new ResumeFileReference { FileReferenceId = fileReference3.Id });
            UpdateLastUsedTime(member.Id, fileReference3.Id, DateTime.Now.AddDays(-3));
            AssertResumeFile(member.Id, resume.Id, fileReference1.Id);
            AssertResumeFiles(member.Id, fileReference1.Id, fileReference2.Id, fileReference3.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var applicationId = _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Check application.

            var applications = _memberApplicationsQuery.GetApplications(member.Id);
            Assert.AreEqual(1, applications.Count);
            Assert.AreEqual(applicationId, applications[0].Id);
            AssertApplication(member.Id, jobAd.Id, fileReference2.Id, applications[0]);

            AssertLastUsedTime(member.Id, fileReference1.Id, DateTime.Now.Date.AddDays(-2));
            AssertLastUsedTime(member.Id, fileReference2.Id, DateTime.Now.Date);
            AssertLastUsedTime(member.Id, fileReference3.Id, DateTime.Now.Date.AddDays(-3));
            AssertResumeFile(member.Id, resume.Id, fileReference1.Id);
            AssertResumeFiles(member.Id, fileReference1.Id, fileReference2.Id, fileReference3.Id);
        }

        [TestMethod, ExpectedException(typeof(AlreadyAppliedException))]
        public void TestAlreadyApplied()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);

            // Submit again.

            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);
        }

        [TestMethod, ExpectedException(typeof(NoResumeFileException))]
        public void TestSubmitNoFile()
        {
            // Create a member.

            var member = CreateMember();
            AddResume(member.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            _internalApplicationsCommand.SubmitApplicationWithLastUsedResume(member, jobAd, null);
        }

        private void AssertApplication(Guid applicantId, Guid jobAdId, Guid resumeFileId, Application application)
        {
            Assert.IsInstanceOfType(application, typeof(InternalApplication));
            AssertApplication(applicantId, jobAdId, application);
            Assert.IsNull(((InternalApplication)application).ResumeId);
            Assert.AreEqual(resumeFileId, ((InternalApplication)application).ResumeFileId);
        }
    }
}
