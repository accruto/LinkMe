using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.AppliedForExternally
{
    [TestClass]
    public class ErrorTests
        : AppliedForExternallyTests
    {
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestMethod]
        public void TestWrongApply()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Cannot apply directly for this ad at all.

            AssertJsonError(ApiApply(HttpStatusCode.NotFound, jobAd.Id, Guid.NewGuid(), EmailAddress, FirstName, LastName), null, "400", "The job ad cannot be found.");

            var member = CreateMember();
            LogIn(member);

            AssertJsonError(ApiApplyWithLastUsedResume(HttpStatusCode.NotFound, jobAd.Id, null), null, "400", "The job ad cannot be found.");
            AssertJsonError(ApiApplyWithUploadedResume(HttpStatusCode.NotFound, jobAd.Id, Guid.NewGuid(), false, null), null, "400", "The job ad cannot be found.");
            AssertJsonError(ApiApplyWithProfile(HttpStatusCode.NotFound, jobAd.Id, null), null, "400", "The job ad cannot be found.");
        }
    }
}
