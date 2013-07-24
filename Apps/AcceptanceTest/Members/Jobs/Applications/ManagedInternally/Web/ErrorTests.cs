using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedInternally.Web
{
    [TestClass]
    public class ErrorTests
        : ManagedInternallyTests
    {
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestMethod]
        public void TestWrongApply()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Cannot apply anonymously for this ad.

            AssertJsonError(ApiApply(HttpStatusCode.NotFound, jobAd.Id, Guid.NewGuid(), EmailAddress, FirstName, LastName), null, "400", "The job ad cannot be found.");
        }
    }
}