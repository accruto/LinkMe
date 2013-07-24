using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members
{
    [TestClass]
    public class MembersCommandTests
        : TestClass
    {
        private const string EmailAddress = "member@test.linkme.net.au";

        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";
        private const string EmailAddressFormat = "test{0}@test.linkme.net.au";

        [TestInitialize]
        public void MembersCommandTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestCreateDuplicates()
        {
            _membersCommand.CreateTestMember(EmailAddress);
            _membersCommand.CreateTestMember(EmailAddress);
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestUpdateDuplicates()
        {
            _membersCommand.CreateTestMember(EmailAddress);
            var member = _membersCommand.CreateTestMember(1);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress } };
            _membersCommand.UpdateMember(member);
        }

        [TestMethod]
        public void TestUpdateFirstName()
        {
            var member = _membersCommand.CreateTestMember(1);
            AssertMember(member, _membersQuery.GetMember(member.Id));

            member.FirstName = "Changed";
            _membersCommand.UpdateMember(member);
            AssertMember(member, _membersQuery.GetMember(member.Id));
        }

        [TestMethod]
        public void TestCreateNullEmailAddresses()
        {
            var member = CreateMember(0);
            Assert.IsNull(member.EmailAddresses);
            TestCreateEmailAddresses(member, typeof(RequiredValidationError));
        }

        [TestMethod]
        public void TestCreateEmptyEmailAddresses()
        {
            var member = CreateMember(0);
            member.EmailAddresses = new List<EmailAddress>();
            TestCreateEmailAddresses(member, typeof(LengthRangeValidationError));
        }

        [TestMethod]
        public void TestCreateTooManyEmailAddresses()
        {
            var member = CreateMember(0);
            var address0 = string.Format(EmailAddressFormat, 0);
            var address1 = string.Format(EmailAddressFormat, 1);
            var address2 = string.Format(EmailAddressFormat, 2);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address0 }, new EmailAddress { Address = address1 }, new EmailAddress { Address = address2 } };
            TestCreateEmailAddresses(member, typeof(LengthRangeValidationError));
        }

        [TestMethod]
        public void TestCreateTooLongEmailAddress()
        {
            var member = CreateMember(0);
            var address = new string('a', 321);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address } };
            TestCreateEmailAddresses(member, typeof(EmailAddressValidationError));
        }

        [TestMethod]
        public void TestCreateEmailAddress()
        {
            var member = CreateMember(0);
            var address = string.Format(EmailAddressFormat, 0);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address } };
            TestCreateEmailAddresses(member, address);
        }

        [TestMethod]
        public void TestCreateEmailAddresses()
        {
            var member = CreateMember(0);
            var address0 = string.Format(EmailAddressFormat, 0);
            var address1 = string.Format(EmailAddressFormat, 1);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address0 }, new EmailAddress { Address = address1 } };
            TestCreateEmailAddresses(member, address0, address1);
        }

        [TestMethod]
        public void TestCreateDuplicateEmailAddresses()
        {
            var member = CreateMember(0);
            var address = string.Format(EmailAddressFormat, 0);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address }, new EmailAddress { Address = address } };
            TestCreateEmailAddresses(member, address);
        }

        [TestMethod]
        public void TestCreateDuplicateDifferentCaseEmailAddresses()
        {
            var member = CreateMember(0);
            var address = string.Format(EmailAddressFormat, 0);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = address.ToLower() }, new EmailAddress { Address = address.ToUpper() } };
            TestCreateEmailAddresses(member, address);
        }

        private void TestCreateEmailAddresses(Member member, Type expectedType)
        {
            try
            {
                _membersCommand.CreateMember(member);
                Assert.Fail();
            }
            catch (ValidationErrorsException ex)
            {
                Assert.AreEqual(1, ex.Errors.Count);
                Assert.AreEqual("EmailAddresses", ex.Errors[0].Name);
                Assert.IsInstanceOfType(ex.Errors[0], expectedType);
            }
        }

        private void TestCreateEmailAddresses(Member member, params string[] addresses)
        {
            _membersCommand.CreateMember(member);
            var emailAddresses = _membersQuery.GetMember(member.Id).EmailAddresses;
            Assert.AreEqual(addresses.Length, emailAddresses.Count);
            foreach (var address in addresses)
                Assert.IsTrue((from a in emailAddresses where a.Address == address select a).Any());
        }

        private Member CreateMember(int index)
        {
            return new Member
            {
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
                VisibilitySettings = new VisibilitySettings(),
                Address = new Address { Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), null) },
            };
        }

        private static void AssertMember(IHasId<Guid> expectedMember, IHasId<Guid> member)
        {
            Assert.AreEqual(expectedMember.Id, member.Id);
            Assert.AreEqual(expectedMember, member);
        }
    }
}
