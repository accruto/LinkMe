using LinkMe.Domain.Roles.Test.Communications.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    [TestClass]
    public class LoggedInTests
        : AppliedForExternallyTests
    {
        [TestMethod]
        public void TestApply()
        {
            var employer = CreateEmployer();
            var jobAd = CreateJobAd(employer);
            var member = CreateMember();

            AssertNoView(jobAd.Id, member.Id);
            AssertNoExternalApplication(member.Id, jobAd.Id);

            // Apply.

            LogIn(member);
            View(jobAd.Id, AssertView);
            Apply(jobAd);

            // Assert.

            AssertView(jobAd.Id, member.Id);
            var application = AssertExternalApplication(member.Id, jobAd.Id);
            _emailServer.AssertNoEmailSent();

            AssertPreviousApplication(jobAd, application);
        }
    }
}
