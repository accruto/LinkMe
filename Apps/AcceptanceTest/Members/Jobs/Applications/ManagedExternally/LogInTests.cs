using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class LogInTests
        : ManagedExternallyTests
    {
        [TestMethod]
        public void TestMember()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var member = CreateMember(true);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            AssertNoApplication(member.Id, jobAd.Id);

            // Apply.

            View(jobAd.Id, () => AssertNotLoggedInView(false));
            ApiLogIn(member);
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, null));
            GetAppliedUrl(employer, jobAd);

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            AssertApplication(applicationId, jobAd, member.Id, null);
            _emailServer.AssertNoEmailSent();
            AssertRedirectToExternal(true, applicationId, jobAd);
        }

        [TestMethod]
        public void TestDeactivatedMember()
        {
            // Create the job ad.

            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

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

            View(jobAd.Id, () => AssertNotLoggedInView(false));
            ApiLogIn(member);
            var applicationId = AssertApply(ApiApplyWithProfile(jobAd.Id, null));
            GetAppliedUrl(employer, jobAd);

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoApplication(anonymousId, jobAd.Id);
            AssertApplication(applicationId, jobAd, member.Id, null);
            _emailServer.AssertNoEmailSent();
            AssertRedirectToExternal(true, applicationId, jobAd);
        }
    }
}