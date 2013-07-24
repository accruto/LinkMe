using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Update
{
    [TestClass]
    public class ContactDetailsUpdateJobAdsTests
        : UpdateJobAdsTests
    {
        private const string FirstNameFormat = "Monty{0}";
        private const string LastNameFormat = "Burns{0}";
        private const string EmailAddressFormat = "mburns{0}@test.linkme.net.au";
        private const string CompanyFormat = "Acme{0}";
        private const string TitleFormat = "This is the title {0}";
        private const string ContentFormat = "This is the content {0}";

        [TestMethod]
        public void TestAddContactDetails()
        {
            var employer = CreateEmployer();

            // No contact details.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                Description = { Content = string.Format(ContentFormat, 1) }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);

            // Add contact details.

            jobAd.ContactDetails = new ContactDetails
            {
                FirstName = string.Format(FirstNameFormat, 1),
                LastName = string.Format(LastNameFormat, 1),
                EmailAddress = string.Format(EmailAddressFormat, 1),
                CompanyName = string.Format(CompanyFormat, 1),
            };
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);
        }

        [TestMethod]
        public void TestRemoveContactDetails()
        {
            var employer = CreateEmployer();

            // Create with contact details.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                ContactDetails = new ContactDetails
                {
                    FirstName = string.Format(FirstNameFormat, 0),
                    LastName = string.Format(LastNameFormat, 0),
                    EmailAddress = string.Format(EmailAddressFormat, 0),
                    CompanyName = string.Format(CompanyFormat, 0),
                },
                Description = { Content = string.Format(ContentFormat, 1) }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);

            // Remvoe it.

            jobAd.ContactDetails = null;
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);
        }

        [TestMethod]
        public void TestUpdateContactDetails()
        {
            var employer = CreateEmployer();

            // Create with contact details.

            var jobAd = new JobAd
            {
                PosterId = employer.Id,
                Title = string.Format(TitleFormat, 0),
                ContactDetails = new ContactDetails
                {
                    FirstName = string.Format(FirstNameFormat, 0),
                    LastName = string.Format(LastNameFormat, 0),
                    EmailAddress = string.Format(EmailAddressFormat, 0),
                    CompanyName = string.Format(CompanyFormat, 0),
                },
                Description = { Content = string.Format(ContentFormat, 1) }
            };
            _jobAdsCommand.CreateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);

            // Update it.

            jobAd.ContactDetails = new ContactDetails
            {
                FirstName = string.Format(FirstNameFormat, 1),
                LastName = string.Format(LastNameFormat, 1),
                EmailAddress = string.Format(EmailAddressFormat, 1),
                CompanyName = string.Format(CompanyFormat, 1),
            };
            _jobAdsCommand.UpdateJobAd(jobAd);

            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAdEntry>(jobAd.Id).ContactDetails);
            AssertContactDetails(jobAd.ContactDetails, _jobAdsQuery.GetJobAd<JobAd>(jobAd.Id).ContactDetails);
        }

        private static void AssertContactDetails(ContactDetails expectedContactDetails, ContactDetails contactDetails)
        {
            if (expectedContactDetails == null)
            {
                Assert.IsNull(contactDetails);
            }
            else
            {
                Assert.IsNotNull(contactDetails);
                Assert.AreEqual(expectedContactDetails.Id, contactDetails.Id);
                Assert.AreEqual(expectedContactDetails.FirstName, contactDetails.FirstName);
                Assert.AreEqual(expectedContactDetails.LastName, contactDetails.LastName);
                Assert.AreEqual(expectedContactDetails.CompanyName, contactDetails.CompanyName);
                Assert.AreEqual(expectedContactDetails.EmailAddress, contactDetails.EmailAddress);
                Assert.AreEqual(expectedContactDetails.SecondaryEmailAddresses, contactDetails.SecondaryEmailAddresses);
                Assert.AreEqual(expectedContactDetails.FaxNumber, contactDetails.FaxNumber);
                Assert.AreEqual(expectedContactDetails.PhoneNumber, contactDetails.PhoneNumber);
            }
        }
    }
}