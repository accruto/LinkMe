using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class LoggedInProfileTests
        : JobG8Tests
    {
        [TestMethod]
        public void TestApply()
        {
            var member = CreateMember();
            var resume = AddResume(member.Id);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertStatus(jobAd, member.Id, true, true, null);

            // Check the application.

            var application = AssertResumeApplication(applicationId, jobAd, member.Id, CoverLetterText, GetResumeId(member.Id));
            AssertNoResumeFile(member.Id, resume.Id);
            AssertResumeFiles(member.Id);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }

        [TestMethod]
        public void TestAlreadyApplied()
        {
            var member = CreateMember();
            AddResume(member.Id);

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, true, null);

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            // Apply again.

            AssertStatus(jobAd, member.Id, true, true, null);
            AssertJsonError(ApiApplyWithProfile(jobAd.Id, CoverLetterText), null, "300", "This job has already been applied for.");
            AssertStatus(jobAd, member.Id, true, true, null);
        }

        [TestMethod]
        public void TestSubmitWithNoResume()
        {
            var member = CreateMember();
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            // Apply.

            LogIn(member);
            AssertStatus(jobAd, member.Id, false, false, null);
            AssertJsonError(ApiApplyWithProfile(jobAd.Id, CoverLetterText), null, "300", "The resume cannot be found.");
            AssertStatus(jobAd, member.Id, false, false, null);
        }

        private Guid GetResumeId(Guid memberId)
        {
            return _candidatesQuery.GetCandidate(memberId).ResumeId.Value;
        }
    }
}