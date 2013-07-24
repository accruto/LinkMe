using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Create
{
    [TestClass]
    public class ContactDetailsCreateJobAdTests
        : CreateJobAdTests
    {
        private const string FirstName = "Paul";
        private const string LastName = "Hodgman";
        private const string EmailAddress = "paul@test.linkme.net.au";
        private const string SecondaryEmailAddresses = "ph@gmail.com";
        private const string FaxNumber = "88888888";
        private const string PhoneNumber = "99999999";
        private const string CompanyName = "Acme";

        [TestMethod]
        public void TestNullContactDetails()
        {
            TestContactDetails(null, false, FirstName, LastName, CompanyName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestEmptyContactDetails()
        {
            TestContactDetails(new ContactDetails(), false, FirstName, LastName, CompanyName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                CompanyName = CompanyName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            TestContactDetails(contactDetails, true, FirstName, LastName, CompanyName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        private void TestContactDetails(ContactDetails contactDetails, bool expectContactDetails, string expectedFirstName, string expectedLastName, string expectedCompany, string expectedEmailAddress, string expectedSecondaryEmailAddresses, string expectedFaxNumber, string expectedPhoneNumber)
        {
            var poster = CreateJobPoster();
            var jobAd = CreateJobAd(poster.Id, 0);
            jobAd.ContactDetails = contactDetails;
            _jobAdsCommand.CreateJobAd(jobAd);
            AssertContactDetails(jobAd.Id, expectContactDetails, expectedFirstName, expectedLastName, expectedCompany, expectedEmailAddress, expectedSecondaryEmailAddresses, expectedFaxNumber, expectedPhoneNumber);
        }

        private void AssertContactDetails(Guid jobAdId, bool expectContactDetails, string expectedFirstName, string expectedLastName, string expectedCompany, string expectedEmailAddress, string expectedSecondaryEmailAddresses, string expectedFaxNumber, string expectedPhoneNumber)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);

            if (expectContactDetails)
            {
                Assert.IsNotNull(jobAd.ContactDetails);
                Assert.AreEqual(expectedFirstName, jobAd.ContactDetails.FirstName);
                Assert.AreEqual(expectedLastName, jobAd.ContactDetails.LastName);
                Assert.AreEqual(expectedCompany, jobAd.ContactDetails.CompanyName);
                Assert.AreEqual(expectedEmailAddress, jobAd.ContactDetails.EmailAddress);
                Assert.AreEqual(expectedSecondaryEmailAddresses, jobAd.ContactDetails.SecondaryEmailAddresses);
                Assert.AreEqual(expectedFaxNumber, jobAd.ContactDetails.FaxNumber);
                Assert.AreEqual(expectedPhoneNumber, jobAd.ContactDetails.PhoneNumber);
            }
            else
            {
                Assert.IsNull(jobAd.ContactDetails);
            }
        }
    }
}