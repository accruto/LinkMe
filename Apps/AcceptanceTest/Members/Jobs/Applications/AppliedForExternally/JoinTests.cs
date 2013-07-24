using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    [TestClass]
    public class JoinTests
        : AppliedForExternallyTests
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
            AssertNoExternalApplication(anonymousId, jobAd.Id);

            // Apply.

            View(jobAd.Id, AssertView);
            AssertJsonSuccess(ApiJoin(FirstName, LastName, EmailAddress, Password));
            Apply(jobAd);

            // The job was viewed anonymously.

            var member = _membersQuery.GetMember(EmailAddress);
            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the new member.

            AssertNoExternalApplication(anonymousId, jobAd.Id);
            AssertExternalApplication(member.Id, jobAd.Id);
            _emailServer.AssertNoEmailSent();
        }
    }
}
