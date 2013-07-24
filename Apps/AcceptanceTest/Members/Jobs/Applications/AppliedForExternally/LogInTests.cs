using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    [TestClass]
    public class LogInTests
        : AppliedForExternallyTests
    {
        [TestMethod]
        public void TestApply()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var member = CreateMember();

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();

            AssertNoView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);
            AssertNoExternalApplication(anonymousId, jobAd.Id);
            AssertNoExternalApplication(member.Id, jobAd.Id);

            // Apply.

            View(jobAd.Id, AssertView);
            ApiLogIn(member);
            Apply(jobAd);

            // The job was viewed anonymously.

            AssertView(jobAd.Id, anonymousId);
            AssertNoView(jobAd.Id, member.Id);

            // The application came from the member.

            AssertNoExternalApplication(anonymousId, jobAd.Id);
            AssertExternalApplication(member.Id, jobAd.Id);
            _emailServer.AssertNoEmailSent();
        }
    }
}
