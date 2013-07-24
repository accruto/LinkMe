using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Recruiters
{
    [TestClass]
    public class ContactDetailsTests
        : OrganisationsTests
    {
        private const string Name = "Organisation";
        private const string FirstName = "Paul";
        private const string LastName = "Hodgman";
        private const string EmailAddress = "paul@test.linkme.net.au";
        private const string SecondaryEmailAddresses = "ph@gmail.com";
        private const string FaxNumber = "88888888";
        private const string PhoneNumber = "99999999";

        [TestMethod]
        public void TestCreateNullContactDetails()
        {
            TestCreateContactDetails(null, false, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestCreateEmptyContactDetails()
        {
            TestCreateContactDetails(new ContactDetails(), false, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestCreateContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            TestCreateContactDetails(contactDetails, true, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestUpdateNullContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            TestUpdateContactDetails(null, contactDetails, true, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestUpdateEmptyContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            TestUpdateContactDetails(new ContactDetails(), contactDetails, true, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        [TestMethod]
        public void TestUpdateContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            var toContactDetails = new ContactDetails
            {
                FirstName = FirstName + 0,
                LastName = LastName + 0,
                EmailAddress = 0 + EmailAddress,
                SecondaryEmailAddresses = 0 + SecondaryEmailAddresses,
                FaxNumber = FaxNumber + 0,
                PhoneNumber = PhoneNumber + 0,
            };
            TestUpdateContactDetails(contactDetails, toContactDetails, true, FirstName + 0, LastName + 0, 0 + EmailAddress, 0 + SecondaryEmailAddresses, FaxNumber + 0, PhoneNumber + 0);
        }

        [TestMethod]
        public void TestRemoveContactDetails()
        {
            var contactDetails = new ContactDetails
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddress = EmailAddress,
                SecondaryEmailAddresses = SecondaryEmailAddresses,
                FaxNumber = FaxNumber,
                PhoneNumber = PhoneNumber,
            };
            TestUpdateContactDetails(contactDetails, null, false, FirstName, LastName, EmailAddress, SecondaryEmailAddresses, FaxNumber, PhoneNumber);
        }

        private void TestCreateContactDetails(ContactDetails contactDetails, bool expectContactDetails, string expectedFirstName, string expectedLastName, string expectedEmailAddress, string expectedSecondaryEmailAddresses, string expectedFaxNumber, string expectedPhoneNumber)
        {
            var organisation = new VerifiedOrganisation
            {
                Name = Name,
                ContactDetails = contactDetails,
            };
            _organisationsCommand.CreateOrganisation(organisation);
            AssertContactDetails(organisation.Id, expectContactDetails, expectedFirstName, expectedLastName, expectedEmailAddress, expectedSecondaryEmailAddresses, expectedFaxNumber, expectedPhoneNumber);
        }

        private void TestUpdateContactDetails(ContactDetails fromContactDetails, ContactDetails toContactDetails, bool expectContactDetails, string expectedFirstName, string expectedLastName, string expectedEmailAddress, string expectedSecondaryEmailAddresses, string expectedFaxNumber, string expectedPhoneNumber)
        {
            var organisation = new VerifiedOrganisation
            {
                Name = Name,
                ContactDetails = fromContactDetails,
            };
            _organisationsCommand.CreateOrganisation(organisation);

            organisation = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(organisation.Id);
            organisation.ContactDetails = toContactDetails;
            _organisationsCommand.UpdateOrganisation(organisation);

            AssertContactDetails(organisation.Id, expectContactDetails, expectedFirstName, expectedLastName, expectedEmailAddress, expectedSecondaryEmailAddresses, expectedFaxNumber, expectedPhoneNumber);
        }

        private void AssertContactDetails(Guid organisationId, bool expectContactDetails, string expectedFirstName, string expectedLastName, string expectedEmailAddress, string expectedSecondaryEmailAddresses, string expectedFaxNumber, string expectedPhoneNumber)
        {
            var organisation = (VerifiedOrganisation)_organisationsQuery.GetOrganisation(organisationId);

            if (expectContactDetails)
            {
                Assert.IsNotNull(organisation.ContactDetails);
                Assert.AreEqual(expectedFirstName, organisation.ContactDetails.FirstName);
                Assert.AreEqual(expectedLastName, organisation.ContactDetails.LastName);
                Assert.AreEqual(null, organisation.ContactDetails.CompanyName);
                Assert.AreEqual(expectedEmailAddress, organisation.ContactDetails.EmailAddress);
                Assert.AreEqual(expectedSecondaryEmailAddresses, organisation.ContactDetails.SecondaryEmailAddresses);
                Assert.AreEqual(expectedFaxNumber, organisation.ContactDetails.FaxNumber);
                Assert.AreEqual(expectedPhoneNumber, organisation.ContactDetails.PhoneNumber);
            }
            else
            {
                Assert.IsNull(organisation.ContactDetails);
            }
        }
    }
}
