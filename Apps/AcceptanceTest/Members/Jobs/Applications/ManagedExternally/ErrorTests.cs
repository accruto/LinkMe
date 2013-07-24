using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Applications.ManagedExternally
{
    [TestClass]
    public class ErrorTests
        : ManagedExternallyTests
    {
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";

        [TestMethod]
        public void TestAnonymousErrors()
        {
            var jobAd = CreateJobAd(CreateEmployer());

            // Unknown file.

            AssertJsonError(ApiApply(HttpStatusCode.NotFound, jobAd.Id, Guid.NewGuid(), EmailAddress, FirstName, LastName), null, "400", "The file cannot be found.");

            // Email address.

            var fileReference = GetResumeFile();
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, null, FirstName, LastName), "EmailAddress", "300", "The email address is required.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, "bademail", FirstName, LastName), "EmailAddress", "300", "The email address must be valid and have less than 320 characters.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, new string('a', 500), FirstName, LastName), "EmailAddress", "300", "The email address must be valid and have less than 320 characters.");

            // First name.

            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, null, LastName), "FirstName", "300", "The first name is required.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, "x", LastName), "FirstName", "300", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, new string('x', 500), LastName), "FirstName", "300", "The first name must be between 2 and 30 characters in length and not have any invalid characters.");

            // Last name.

            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, FirstName, null), "LastName", "300", "The last name is required.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, FirstName, "x"), "LastName", "300", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
            AssertJsonError(ApiApply(jobAd.Id, fileReference.Id, EmailAddress, FirstName, new string('x', 500)), "LastName", "300", "The last name must be between 2 and 30 characters in length and not have any invalid characters.");
        }
    }
}