using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Anonymous.Commands;
using LinkMe.Domain.Users.Anonymous.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Anonymous
{
    [TestClass]
    public class AnonymousContactsTests
        : TestClass
    {
        private readonly IAnonymousUsersCommand _anonymousUsersCommand = Resolve<IAnonymousUsersCommand>();
        private readonly IAnonymousUsersQuery _anonymousUsersQuery = Resolve<IAnonymousUsersQuery>();

        private const string EmailAddressFormat = "homer{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Homer{0}";
        private const string LastNameFormat = "Simpson{0}";

        [TestInitialize]
        public void AnonymousContactsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateContact()
        {
            var user = new AnonymousUser();
            var contactDetails = CreateContactDetails(0);

            var contact = _anonymousUsersCommand.CreateContact(user, contactDetails);
            Assert.AreNotEqual(Guid.Empty, user.Id);
            Assert.AreNotEqual(Guid.Empty, contact.Id);
            AssertContact(contact.Id, contactDetails, contact);
            AssertContact(contact.Id, contactDetails, _anonymousUsersQuery.GetContact(contact.Id));
        }

        [TestMethod]
        public void TestCreateSameContact()
        {
            var user = new AnonymousUser();
            var contactDetails = CreateContactDetails(0);

            var contact1 = _anonymousUsersCommand.CreateContact(user, contactDetails);
            AssertContact(contact1.Id, contactDetails, contact1);
            AssertContact(contact1.Id, contactDetails, _anonymousUsersQuery.GetContact(contact1.Id));
            var contact2 = _anonymousUsersCommand.CreateContact(user, contactDetails);
            AssertContact(contact1.Id, contactDetails, contact2);
            AssertContact(contact1.Id, contactDetails, _anonymousUsersQuery.GetContact(contact2.Id));
        }

        [TestMethod]
        public void TestCreateOtherContact()
        {
            var user = new AnonymousUser();
            var contactDetails1 = CreateContactDetails(0);
            var contactDetails2 = CreateContactDetails(1);

            var contact1 = _anonymousUsersCommand.CreateContact(user, contactDetails1);
            AssertContact(contact1.Id, contactDetails1, contact1);
            AssertContact(contact1.Id, contactDetails1, _anonymousUsersQuery.GetContact(contact1.Id));

            var contact2 = _anonymousUsersCommand.CreateContact(user, contactDetails2);
            AssertContact(contact2.Id, contactDetails2, contact2);
            AssertContact(contact2.Id, contactDetails2, _anonymousUsersQuery.GetContact(contact2.Id));

            Assert.AreNotEqual(contact1.Id, contact2.Id);

            var contact = _anonymousUsersCommand.CreateContact(user, contactDetails1);
            AssertContact(contact1.Id, contactDetails1, contact);
            AssertContact(contact1.Id, contactDetails1, _anonymousUsersQuery.GetContact(contact1.Id));
            contact = _anonymousUsersCommand.CreateContact(user, contactDetails2);
            AssertContact(contact2.Id, contactDetails2, contact);
            AssertContact(contact2.Id, contactDetails2, _anonymousUsersQuery.GetContact(contact2.Id));
        }

        [TestMethod]
        public void TestEmailAddressErrors()
        {
            var user = new AnonymousUser();
            var contactDetails = CreateContactDetails(0);

            contactDetails.EmailAddress = null;
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RequiredValidationError' error has occurred for the EmailAddress property.");

            contactDetails.EmailAddress = "a";
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.EmailAddressValidationError' error has occurred for the EmailAddress property.");

            contactDetails.EmailAddress = "abademailaddress";
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.EmailAddressValidationError' error has occurred for the EmailAddress property.");
        }

        [TestMethod]
        public void TestFirstNameErrors()
        {
            var user = new AnonymousUser();
            var contactDetails = CreateContactDetails(0);

            contactDetails.FirstName = null;
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RequiredValidationError' error has occurred for the FirstName property.");

            contactDetails.FirstName = "a";
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the FirstName property.");

            contactDetails.FirstName = new string('a', 500);
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the FirstName property.");
        }

        [TestMethod]
        public void TestLastNameErrors()
        {
            var user = new AnonymousUser();
            var contactDetails = CreateContactDetails(0);

            contactDetails.LastName = null;
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RequiredValidationError' error has occurred for the LastName property.");

            contactDetails.LastName = "a";
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the LastName property.");

            contactDetails.LastName = new string('a', 500);
            AssertException.Thrown<ValidationErrorsException>(() => _anonymousUsersCommand.CreateContact(user, contactDetails), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the LastName property.");
        }

        private static void AssertContact(Guid id, ContactDetails contactDetails, ICommunicationRecipient contact)
        {
            Assert.AreEqual(id, contact.Id);
            Assert.AreEqual(contactDetails.EmailAddress, contact.EmailAddress);
            Assert.AreEqual(contactDetails.FirstName, contact.FirstName);
            Assert.AreEqual(contactDetails.LastName, contact.LastName);
        }

        private static ContactDetails CreateContactDetails(int index)
        {
            return new ContactDetails
            {
                EmailAddress = string.Format(EmailAddressFormat, index),
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
        }
    }
}
