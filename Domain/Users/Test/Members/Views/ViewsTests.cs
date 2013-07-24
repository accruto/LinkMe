using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Views
{
    public abstract class ViewsTests
        : TestClass
    {
        private const string FirstName = "Homer";
        private const string LastName = "Simpson";
        private const string EmailAddress = "hsimpson@test.linkme.net.au";
        private static readonly Guid? AffiliateId = Guid.NewGuid();
        private static readonly Guid? PhotoId = Guid.NewGuid();
        private const string HomePhoneNumber = "77777777";
        private const string WorkPhoneNumber = "88888888";
        private const string MobilePhoneNumber = "99999999";
        private static readonly Gender Gender = Gender.Female;
        private static readonly PartialDate? DateOfBirth = new PartialDate(1970, 1, 12);
        private static readonly Address Address = new Address {Line1 = "12 Olympic Ave"};
        private static readonly EthnicStatus EthnicStatus = EthnicStatus.TorresIslander;

        protected Member CreateMember(bool isVisible)
        {
            var member = new Member
            {
                FirstName = FirstName,
                LastName = LastName,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress, IsVerified = true } },
                AffiliateId = AffiliateId,
                PhotoId = PhotoId,
                PhoneNumbers = new List<PhoneNumber>
                {
                    new PhoneNumber {Number = MobilePhoneNumber, Type = PhoneNumberType.Mobile},
                    new PhoneNumber {Number = HomePhoneNumber, Type = PhoneNumberType.Home},
                    new PhoneNumber {Number = WorkPhoneNumber, Type = PhoneNumberType.Work}
                },
                Gender = Gender,
                DateOfBirth = DateOfBirth,
                Address = Address,
                EthnicStatus = EthnicStatus,
                VisibilitySettings = new VisibilitySettings(),
                IsEnabled = true,
                IsActivated = true,
            };

            member.VisibilitySettings.Personal.FirstDegreeVisibility = isVisible ? PersonalVisibility.All : PersonalVisibility.None;

            // Referees is not on for a member.

            member.VisibilitySettings.Professional.EmploymentVisibility = isVisible ? ProfessionalVisibility.All : ProfessionalVisibility.None;
            member.VisibilitySettings.Professional.EmploymentVisibility = member.VisibilitySettings.Professional.EmploymentVisibility.ResetFlag(ProfessionalVisibility.Referees);
            return member;
        }

        protected static void AssertName(bool canAccess, IMember member)
        {
            // This interface should always give full access.

            var communicationRecipient = (ICommunicationRecipient) member;
            AssertName(true, communicationRecipient.FirstName, communicationRecipient.LastName, communicationRecipient.FullName);

            // This should also give full access.

            var recipient = (ICommunicationRecipient)member;
            AssertName(true, communicationRecipient.FirstName, recipient.LastName, recipient.FullName);

            // Convert to IRegisteredUser, this may not give full access.

            var user = (IRegisteredUser)member;
            AssertName(canAccess, user.FirstName, user.LastName, user.FullName);

            // Convert to IMember, this may not give full access.

            AssertName(canAccess, member.FirstName, member.LastName, member.FullName);
        }

        protected static void AssertEmailAddress(bool canAccess, IMember member)
        {
            // This interface should always give full access.

            var communicationRecipient = (ICommunicationRecipient) member;
            AssertEmailAddress(true, communicationRecipient.EmailAddress);

            // This should also give full access.

            var recipient = (ICommunicationRecipient)member;
            AssertEmailAddress(true, recipient.EmailAddress);

            // Convert to IMember, this may not give full access.

            var emailAddress = member.GetBestEmailAddress();
            AssertEmailAddress(canAccess, emailAddress == null ? null : emailAddress.Address);
        }

        protected void AssertAffiliateId(bool canAccess, IMember member)
        {
            // This interface should always give full access.

            var communicationUser = (ICommunicationUser)member;
            AssertAffiliateId(true, communicationUser.AffiliateId);

            // Convert to IRegisteredUser, this may not give full access.

            var user = (IRegisteredUser)member;
            AssertAffiliateId(canAccess, user.AffiliateId);

            // Convert to IMember, this may not give full access.

            AssertAffiliateId(canAccess, member.AffiliateId);
        }

        private static void AssertAffiliateId(bool canAccess, Guid? affiliateId)
        {
            if (canAccess)
                Assert.AreEqual(AffiliateId, affiliateId);
            else
                Assert.IsNull(affiliateId);
        }

        protected void AssertPhoto(bool canAccess, IMember member)
        {
            if (canAccess)
                Assert.AreEqual(PhotoId, member.PhotoId);
            else
                Assert.IsNull(member.PhotoId);
        }

        protected static void AssertPhoneNumbers(bool canAccess, IMember member)
        {
            if (canAccess)
            {
                Assert.AreEqual(3, member.PhoneNumbers.Count);
                Assert.AreEqual(MobilePhoneNumber, member.PhoneNumbers[0].Number);
                Assert.AreEqual(PhoneNumberType.Mobile, member.PhoneNumbers[0].Type);
                Assert.AreEqual(HomePhoneNumber, member.PhoneNumbers[1].Number);
                Assert.AreEqual(PhoneNumberType.Home, member.PhoneNumbers[1].Type);
                Assert.AreEqual(WorkPhoneNumber, member.PhoneNumbers[2].Number);
                Assert.AreEqual(PhoneNumberType.Work, member.PhoneNumbers[2].Type);
            }
            else
            {
                Assert.AreEqual(3, member.PhoneNumbers.Count);
                Assert.AreEqual(null, member.PhoneNumbers[0].Number);
                Assert.AreEqual(PhoneNumberType.Mobile, member.PhoneNumbers[0].Type);
                Assert.AreEqual(null, member.PhoneNumbers[1].Number);
                Assert.AreEqual(PhoneNumberType.Home, member.PhoneNumbers[1].Type);
                Assert.AreEqual(null, member.PhoneNumbers[2].Number);
                Assert.AreEqual(PhoneNumberType.Work, member.PhoneNumbers[2].Type);
            }
        }

        protected static void AssertGender(bool canAccess, IMember member)
        {
            if (canAccess)
                Assert.AreEqual(Gender, member.Gender);
            else
                Assert.AreEqual(Gender.Unspecified, member.Gender);
        }

        protected static void AssertDateOfBirth(bool canAccess, IMember member)
        {
            if (canAccess)
                Assert.AreEqual(DateOfBirth, member.DateOfBirth);
            else
                Assert.AreEqual(null, member.DateOfBirth);
        }

        protected static void AssertEthnicStatus(bool canAccess, IMember member)
        {
            if (canAccess)
                Assert.AreEqual(EthnicStatus, member.EthnicStatus);
            else
                Assert.AreEqual((EthnicStatus)0, member.EthnicStatus);
        }

        private static void AssertName(bool canAccess, string firstName, string lastName, string fullName)
        {
            if (canAccess)
            {
                Assert.AreEqual(FirstName, firstName);
                Assert.AreEqual(LastName, lastName);
                Assert.AreEqual(FirstName.CombineLastName(LastName), fullName);
            }
            else
            {
                Assert.IsNull(firstName);
                Assert.IsNull(lastName);
                Assert.IsNull(fullName);
            }
        }

        private static void AssertEmailAddress(bool canAccess, string emailAddress)
        {
            if (canAccess)
                Assert.AreEqual(EmailAddress, emailAddress);
            else
                Assert.IsNull(emailAddress);
        }
    }
}