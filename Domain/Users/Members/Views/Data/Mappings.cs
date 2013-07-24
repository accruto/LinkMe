using System;
using LinkMe.Domain.Contacts.Data;
using LinkMe.Domain.Data;
using LinkMe.Domain.Location.Data;
using LinkMe.Domain.Users.Members.Data;

namespace LinkMe.Domain.Users.Members.Views.Data
{
    internal enum UserFlags
    {
        None = 0x00,
        MustChangePassword = 0x02,
        Disabled = 0x04,
        Activated = 0x20
    }

    internal partial class LocationReferenceEntity
        : ILocationReferenceEntity
    {
    }

    internal partial class AddressEntity
        : IAddressEntity<LocationReferenceEntity>
    {
        LocationReferenceEntity IAddressEntity<LocationReferenceEntity>.LocationReferenceEntity
        {
            get { return LocationReferenceEntity; }
            set { LocationReferenceEntity = value; }
        }
    }

    internal partial class MemberEntity
        : IMemberEntity<AddressEntity, LocationReferenceEntity>
    {
        DateTime? IHavePartialDateEntity.date
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }

        byte? IHavePartialDateEntity.dateParts
        {
            get { return dateOfBirthParts; }
            set { dateOfBirthParts = value; }
        }
    }

    internal partial class RegisteredUserEntity
        : IRegisteredUserEntity, IHaveEmailAddressesEntity
    {
        string IHaveEmailAddressesEntity.primaryEmailAddress
        {
            get { return emailAddress; }
            set { emailAddress = value; }
        }

        bool IHaveEmailAddressesEntity.primaryEmailAddressVerified
        {
            get { return emailAddressVerified; }
            set { emailAddressVerified = value; }
        }

        string IHaveEmailAddressesEntity.secondaryEmailAddress
        {
            get { return secondaryEmailAddress; }
            set { secondaryEmailAddress = value; }
        }

        bool? IHaveEmailAddressesEntity.secondaryEmailAddressVerified
        {
            get { return secondaryEmailAddressVerified; }
            set { secondaryEmailAddressVerified = value; }
        }
    }

    internal partial class CommunityMemberEntity
        : ICommunityMemberEntity
    {
    }
}
