using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    [TestClass]
    public class AnonymousTests
        : AppliedForExternallyTests
    {
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
            Apply(jobAd);

            // Assert.

            AssertView(jobAd.Id, anonymousId);
            AssertExternalApplication(anonymousId, jobAd.Id);
            _emailServer.AssertNoEmailSent();
        }
    }
}
