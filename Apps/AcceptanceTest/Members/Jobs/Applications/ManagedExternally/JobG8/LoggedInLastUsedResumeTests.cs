using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class LoggedInLastUsedResumeTests
        : JobG8Tests
    {
        [TestMethod]
        public void TestSubmitProfileFile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            var resume = AddResume(member.Id, fileReference);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));

            AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id, false);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
            _emailServer.AssertNoEmailSent();
            AssertNoJobG8Request();

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            // Check the application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id, true);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application, fileReference);
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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application, fileReference);
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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference2.FileName);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AssertStatus(jobAd, member.Id, false, true, fileReference2.FileName);

            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertStatus(jobAd, member.Id, true, true, fileReference2.FileName);

            // Check application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference2.Id);

            AssertLastUsedTime(member.Id, fileReference1.Id, DateTime.Now.Date.AddDays(-2));
            AssertLastUsedTime(member.Id, fileReference2.Id, DateTime.Now.Date);
            AssertLastUsedTime(member.Id, fileReference3.Id, DateTime.Now.Date.AddDays(-3));
            AssertResumeFile(member.Id, resume.Id, fileReference1.Id);
            AssertResumeFiles(member.Id, fileReference1.Id, fileReference2.Id, fileReference3.Id);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application, fileReference2);
        }

        [TestMethod]
        public void TestAlreadyApplied()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            AddResume(member.Id, fileReference);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithLastUsedResume(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);

            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Apply again.

            AssertJsonError(ApiApplyWithLastUsedResume(jobAd.Id, null), null, "300", "This job has already been applied for.");
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);
        }

        [TestMethod]
        public void TestSubmitNoFile()
        {
            // Create a member.

            var member = CreateMember();
            AddResume(member.Id);

            // Submit.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            //AssertJsonError(ApplyWithLastUsedResume(jobAd.Id), null, "300", "The resume file cannot be found.");
            AssertStatus(jobAd, member.Id, false, true, null);
        }
    }
}