using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class LoggedInTests
        : JobG8Tests
    {
        [TestMethod]
        public void TestLoggedIn()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Create a member and log in.

            var member = CreateMember(true);
            LogIn(member);

            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply for the job.

            View(jobAd.Id, () => AssertLoggedInView(true, true, true));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertView(jobAd.Id, member.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
            AssertPreviousApplication(jobAd, application);
        }
    }
}