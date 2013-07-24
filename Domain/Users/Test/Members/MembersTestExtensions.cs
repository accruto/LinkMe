using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Domain.Users.Test.Members
{
    public static class MembersTestExtensions
    {
        private static readonly ILocationQuery _locationQuery = Container.Current.Resolve<ILocationQuery>();

        private const string EmailAddressFormat = "member{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Paul{0}";
        private const string LastNameFormat = "Hodgman{0}";
        private const string DefaultPhoneNumber = "0410635666";
        private static readonly Country DefaultCountry = _locationQuery.GetCountry("Australia");
        private const string DefaultLocation = "Melbourne VIC 3000";
        private const Gender DefaultGender = Gender.Male;
        private static readonly PartialDate DefaultDateOfBirth = new PartialDate(1970, 2, 3);

        public static Member CreateTestMember(this IMembersCommand membersCommand, int index)
        {
            return membersCommand.CreateTestMember(string.Format(EmailAddressFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index));
        }

        public static Member CreateTestMember(this IMembersCommand membersCommand, int index, DateTime createTime)
        {
            return membersCommand.CreateTestMember(string.Format(EmailAddressFormat, index), string.Format(FirstNameFormat, index), string.Format(LastNameFormat, index), createTime);
        }

        public static Member CreateTestMember(this IMembersCommand membersCommand, string emailAddress)
        {
            return membersCommand.CreateTestMember(emailAddress, string.Format(FirstNameFormat, 0), string.Format(LastNameFormat, 0));
        }

        public static Member CreateTestMember(this IMembersCommand membersCommand, string emailAddress, string firstName, string lastName)
        {
            return membersCommand.CreateTestMember(emailAddress, firstName, lastName, null);
        }

        public static Member CreateTestMember(this IMembersCommand membersCommand, string emailAddress, string firstName, string lastName, DateTime? createdTime)
        {
            var member = new Member
            {
                IsEnabled = true,
                IsActivated = true,
                EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress, IsVerified = true } },
                PhoneNumbers = new List<PhoneNumber> { new PhoneNumber { Number = DefaultPhoneNumber, Type = PhoneNumberType.Mobile } },
                FirstName = firstName,
                LastName = lastName,
                Gender = DefaultGender,
                DateOfBirth = DefaultDateOfBirth,
                VisibilitySettings = new VisibilitySettings(),
            };

            if (createdTime.HasValue)
            {
                member.CreatedTime = createdTime.Value;
                member.LastUpdatedTime = createdTime.Value;
            }

            // Deny public access to real name, because existing tests rely on this. Might need to change this later.

            member.VisibilitySettings.Personal.PublicVisibility &= ~PersonalVisibility.Name;

            member.Address = new Address { Location = new LocationReference() };
            _locationQuery.ResolvePostalSuburb(member.Address.Location, DefaultCountry, DefaultLocation);

            membersCommand.CreateMember(member);
            return member;
        }
    }
}