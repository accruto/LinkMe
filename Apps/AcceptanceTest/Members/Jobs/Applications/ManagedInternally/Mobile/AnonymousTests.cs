using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Mobile
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

            View(jobAd.Id, () => AssertView(jobAd.Id));

            // Need to be logged in to apply.

            Get(GetApplyUrl(jobAd.Id));

            var loginUrl = GetLoginUrl(GetApplyUrl(jobAd.Id)).AsNonReadOnly();
            loginUrl.QueryString["reason"] = "Apply";
            AssertUrl(loginUrl);
        }
    }
}