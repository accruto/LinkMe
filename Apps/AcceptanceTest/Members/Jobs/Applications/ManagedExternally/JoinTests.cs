using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class JoinTests
        : ManagedExternallyTests
    {
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string Password = "password";

        [TestMethod]
        public void TestApply()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Apply.

            var fileReference = GetResumeFile();
            View(jobAd.Id, () => AssertNotLoggedInView(false));
            AssertJsonSuccess(ApiJoin(FirstName, LastName, EmailAddress, Password));
            var applicationId = AssertApply(ApiApplyWithUploadedResume(jobAd.Id, fileReference.Id, false, null));
            GetAppliedUrl(employer, jobAd);

            // The job was viewed anonymously.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the new member.

            AssertNoApplication(anonymousId, jobAd.Id);
            AssertApplication(applicationId, jobAd, member.Id, null);
            _emailServer.AssertNoEmailSent();
            AssertRedirectToExternal(true, applicationId, jobAd);
        }
    }
}