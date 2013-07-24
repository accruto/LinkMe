using System;
using System.Net;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Test.Candidates.Mocks;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class LoggedInResumeTests
        : JobG8Tests
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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id);
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

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
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
        public void TestSubmitProfileFileUseForProfile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var fileReference = GetResumeFile();
            var resume = AddResume(member.Id, fileReference);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, true, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id);
            AssertResumeFile(member.Id, _candidatesQuery.GetCandidate(member.Id).ResumeId.Value, fileReference.Id);
            AssertResumeFiles(member.Id, fileReference.Id);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(member, jobAd, application, fileReference);
        }

        [TestMethod]
        public void TestSubmitNonProfileFileUseForProfile()
        {
            // Create a member with a resume profile file.

            var member = CreateMember();
            var resume = AddResume(member.Id);
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);

            var fileReference = GetResumeFile();

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, true, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Check the application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference.Id);
            AssertResumeFile(member.Id, resume.Id, fileReference.Id);
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
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText));
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
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
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
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference2.Id, false, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
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
        public void TestMultipleFilesUseForProfile()
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
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference2.Id, true, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference2.FileName);

            // Check application.

            var application = AssertResumeFileApplication(applicationId, jobAd, member.Id, CoverLetterText, fileReference2.Id);

            AssertLastUsedTime(member.Id, fileReference1.Id, DateTime.Now.Date.AddDays(-2));
            AssertLastUsedTime(member.Id, fileReference2.Id, DateTime.Now.Date);
            AssertLastUsedTime(member.Id, fileReference3.Id, DateTime.Now.Date.AddDays(-3));
            AssertResumeFile(member.Id, _candidatesQuery.GetCandidate(member.Id).ResumeId.Value, fileReference2.Id);
            AssertResumeFiles(member.Id, fileReference1.Id, fileReference2.Id, fileReference3.Id);
            _emailServer.AssertNoEmailSent();
            AssertJobG8Request(member, jobAd, application, fileReference2);
        }

        [TestMethod]
        public void TestUnknownFile()
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
            AssertJsonError(ApiApplyWithUploadedResume(HttpStatusCode.NotFound, jobAd.Id, Guid.NewGuid(), false, null), null, "400", "The file cannot be found.");
            AssertStatus(jobAd, member.Id, false, true, fileReference.FileName);
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
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);

            // Apply again.

            AssertJsonError(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, CoverLetterText), null, "300", "This job has already been applied for.");
            GetAppliedUrl(employer, jobAd);
            AssertStatus(jobAd, member.Id, true, true, fileReference.FileName);
        }
    }
}