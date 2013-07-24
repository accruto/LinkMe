using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Web
{
    [TestClass]
    public class AnonymousTests
        : ManagedInternallyTests
    {
        [TestMethod]
        public void TestAnonymous()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);

            Get(HomeUrl);
            var anonymousId = GetAnonymousId();
            AssertNoView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);

            // Try to apply.

            View(jobAd.Id, AssertView);
            AssertJsonError(Apply(employer, jobAd, false, CoverLetterText), null, "100", "The user is not logged in.");

            // Assert no application saved.

            AssertView(jobAd.Id, anonymousId);
            AssertNoApplication(anonymousId, jobAd.Id);
            _emailServer.AssertNoEmailSent();
        }
    }
}