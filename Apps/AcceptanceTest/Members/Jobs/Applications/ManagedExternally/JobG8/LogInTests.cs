using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally.JobG8
{
    [TestClass]
    public abstract class LogInTests
        : JobG8Tests
    {
        [TestMethod]
        public void TestMember()
        {
            var jobAd = CreateJobAd(CreateEmployer());
            var member = CreateMember(true);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply.

            View(jobAd.Id, () => AssertNotLoggedInView(true));
            AssertJsonSuccess(ApiLogIn(member));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }

        [TestMethod]
        public void TestDeactivatedMember()
        {
            // Create the job ad.

            var jobAd = CreateJobAd(CreateEmployer());

            // Create a member and log in.

            var member = CreateMember(true);
            member.IsActivated = false;
            _memberAccountsCommand.UpdateMember(member);

            // Apply.  The fact that the user is deactivated should make no difference.

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            View(jobAd.Id, () => AssertNotLoggedInView(true));
            AssertJsonSuccess(ApiLogIn(member));
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, CoverLetterText));
            Get(GetQuestionsUrl(jobAd.Id, applicationId));
            AnswerQuestions();
            AssertAppliedUrl(jobAd, applicationId, member.GetBestEmailAddress().Address);

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            var application = AssertApplication(applicationId, jobAd, member.Id, CoverLetterText);
            _emailServer.AssertNoEmailSent();

            AssertJobG8Request(member, jobAd, application);
        }
    }
}