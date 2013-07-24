using System;
using System.Collections.Generic;
using System.Data.Linq;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Contacts.Data
{
    internal enum UserFlags
    {
        None = 0x00,
        MustChangePassword = 0x02,
        Disabled = 0x04,
        Activated = 0x20,
    }

    public interface IRegisteredUserEntity
    {
        Guid id { get; set; }
        DateTime createdTime { get; set; }
        short flags { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
    }

    public interface IHaveEmailAddressEntity
    {
        string emailAddress { get; set; }
        bool emailAddressVerified { get; set; }
    }

    public interface IHaveEmailAddressesEntity
    {
        string primaryEmailAddress { get; set; }
        bool primaryEmailAddressVerified { get; set; }
        string secondaryEmailAddress { get; set; }
        bool? secondaryEmailAddressVerified { get; set; }
    }

    public interface IHavePhoneNumberEntity
    {
        string phoneNumber { get; set; }
        byte? phoneNumberType { get; set; }
    }

    public interface IHavePhoneNumbersEntity
    {
        string primaryPhoneNumber { get; set; }
        byte? primaryPhoneNumberType { get; set; }
        string secondaryPhoneNumber { get; set; }
        byte? secondaryPhoneNumberType { get; set; }
        string tertiaryPhoneNumber { get; set; }
        byte? tertiaryPhoneNumberType { get; set; }
    }

    public interface IContactDetailsEntity
    {
        Guid id { get; set; }
        string firstName { get; set; }
        string lastName { get; set; }
        string email { get; set; }
        string secondaryEmails { get; set; }
        string faxNumber { get; set; }
        string phoneNumber { get; set; }
    }

    public interface IHaveContactDetailsEntity<TContactDetailsEntity>
        where TContactDetailsEntity : class, IContactDetailsEntity
    {
        TContactDetailsEntity ContactDetailsEntity { get; set; }
        string companyName { get; set; }
    }

    public interface IHaveContactDetailsEntities<TContactDetailsEntity>
        where TContactDetailsEntity : class
    {
        Table<TContactDetailsEntity> ContactDetailsEntities { get; }
    }

    public static class Mappings
    {
        public static TUser MapTo<TUser>(this IRegisteredUserEntity entity)
            where TUser : RegisteredUser, new()
        {
            return new TUser
            {
                Id = entity.id,
                CreatedTime = entity.createdTime,
                IsEnabled = (entity.flags & (short)UserFlags.Disabled) != (short)UserFlags.Disabled,
                IsActivated = ((UserFlags)entity.flags).IsFlagSet(UserFlags.Activated),
                FirstName = entity.firstName,
                LastName = entity.lastName,
            };
        }

        public static void MapTo<TUser>(this TUser user, IRegisteredUserEntity entity)
            where TUser : RegisteredUser
        {
            var flags = new UserFlags();
            if (!user.IsEnabled)
                flags = flags.SetFlag(UserFlags.Disabled);
            if (user.IsActivated)
                flags = flags.SetFlag(UserFlags.Activated);

            entity.flags = (short)flags;
            entity.createdTime = user.CreatedTime;
            entity.firstName = user.FirstName;
            entity.lastName = user.LastName;
        }

        public static void MapTo(this IHaveEmailAddressEntity entity, IHaveEmailAddress haveEmailAddress)
        {
            haveEmailAddress.EmailAddress = string.IsNullOrEmpty(entity.emailAddress)
                ? null
                : new EmailAddress { Address = entity.emailAddress, IsVerified = entity.emailAddressVerified };
        }

        public static void MapTo(this IHaveEmailAddress haveEmailAddress, IHaveEmailAddressEntity entity)
        {
            if (haveEmailAddress == null || haveEmailAddress.EmailAddress == null)
            {
                entity.emailAddress = null;
                entity.emailAddressVerified = true;
            }
            else
            {
                entity.emailAddress = haveEmailAddress.EmailAddress.Address;
                entity.emailAddressVerified = haveEmailAddress.EmailAddress.IsVerified;
            }
        }

        public static void MapTo(this IHaveEmailAddressesEntity entity, IHaveEmailAddresses haveEmailAddresses)
        {
            if (string.IsNullOrEmpty(entity.primaryEmailAddress))
            {
                haveEmailAddresses.EmailAddresses = null;
                return;
            }

            haveEmailAddresses.EmailAddresses = new List<EmailAddress>
            {
                new EmailAddress { Address = entity.primaryEmailAddress, IsVerified = entity.primaryEmailAddressVerified }
            };

            if (string.IsNullOrEmpty(entity.secondaryEmailAddress) || entity.secondaryEmailAddressVerified == null)
                return;
            haveEmailAddresses.EmailAddresses.Add(new EmailAddress { Address = entity.secondaryEmailAddress, IsVerified = entity.secondaryEmailAddressVerified.Value });
        }

        public static void MapTo(this IHaveEmailAddresses haveEmailAddresses, IHaveEmailAddressesEntity entity)
        {
            entity.primaryEmailAddress = null;
            entity.primaryEmailAddressVerified = false;
            entity.secondaryEmailAddress = null;
            entity.secondaryEmailAddressVerified = false;
            if (haveEmailAddresses == null || haveEmailAddresses.EmailAddresses == null || haveEmailAddresses.EmailAddresses.Count == 0)
                return;

            entity.primaryEmailAddress = haveEmailAddresses.EmailAddresses[0].Address;
            entity.primaryEmailAddressVerified = haveEmailAddresses.EmailAddresses[0].IsVerified;
            if (haveEmailAddresses.EmailAddresses.Count == 1)
                return;

            entity.secondaryEmailAddress = haveEmailAddresses.EmailAddresses[1].Address;
            entity.secondaryEmailAddressVerified = haveEmailAddresses.EmailAddresses[1].IsVerified;
        }

        public static void MapTo(this IHavePhoneNumberEntity entity, IHavePhoneNumber havePhoneNumber)
        {
            havePhoneNumber.PhoneNumber = string.IsNullOrEmpty(entity.phoneNumber) || entity.phoneNumberType == null
                ? null
                : new PhoneNumber { Number = entity.phoneNumber, Type = (PhoneNumberType) entity.phoneNumberType };
        }

        public static void MapTo(this IHavePhoneNumber havePhoneNumber, IHavePhoneNumberEntity entity)
        {
            entity.phoneNumber = null;
            entity.phoneNumberType = null;
            if (havePhoneNumber == null || havePhoneNumber.PhoneNumber == null || string.IsNullOrEmpty(havePhoneNumber.PhoneNumber.Number))
                return;

            entity.phoneNumber = havePhoneNumber.PhoneNumber.Number;
            entity.phoneNumberType = (byte?)havePhoneNumber.PhoneNumber.Type;
        }

        public static void MapTo(this IHavePhoneNumbersEntity entity, IHavePhoneNumbers havePhoneNumbers)
        {
            if (string.IsNullOrEmpty(entity.primaryPhoneNumber) || entity.primaryPhoneNumberType == null)
            {
                havePhoneNumbers.PhoneNumbers = null;
                return;
            }

            havePhoneNumbers.PhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber { Number = entity.primaryPhoneNumber, Type = (PhoneNumberType) entity.primaryPhoneNumberType }
            };

            if (string.IsNullOrEmpty(entity.secondaryPhoneNumber) || entity.secondaryPhoneNumberType == null)
                return;
            havePhoneNumbers.PhoneNumbers.Add(new PhoneNumber { Number = entity.secondaryPhoneNumber, Type = (PhoneNumberType)entity.secondaryPhoneNumberType });

            if (string.IsNullOrEmpty(entity.tertiaryPhoneNumber) || entity.tertiaryPhoneNumberType == null)
                return;
            havePhoneNumbers.PhoneNumbers.Add(new PhoneNumber { Number = entity.tertiaryPhoneNumber, Type = (PhoneNumberType)entity.tertiaryPhoneNumberType });
        }

        public static void MapTo(this IHavePhoneNumbers havePhoneNumbers, IHavePhoneNumbersEntity entity)
        {
            entity.primaryPhoneNumber = null;
            entity.primaryPhoneNumberType = null;
            entity.secondaryPhoneNumber = null;
            entity.secondaryPhoneNumberType = null;
            entity.tertiaryPhoneNumber = null;
            entity.tertiaryPhoneNumberType = null;
            if (havePhoneNumbers == null || havePhoneNumbers.PhoneNumbers == null || havePhoneNumbers.PhoneNumbers.Count == 0)
                return;

            entity.primaryPhoneNumber = havePhoneNumbers.PhoneNumbers[0].Number;
            entity.primaryPhoneNumberType = (byte?)havePhoneNumbers.PhoneNumbers[0].Type;
            if (havePhoneNumbers.PhoneNumbers.Count == 1)
                return;

            entity.secondaryPhoneNumber = havePhoneNumbers.PhoneNumbers[1].Number;
            entity.secondaryPhoneNumberType = (byte?)havePhoneNumbers.PhoneNumbers[1].Type;

            if (havePhoneNumbers.PhoneNumbers.Count == 2)
                return;

            entity.tertiaryPhoneNumber = havePhoneNumbers.PhoneNumbers[2].Number;
            entity.tertiaryPhoneNumberType = (byte?)havePhoneNumbers.PhoneNumbers[2].Type;
        }

        public static void CheckDeleteContactDetails<TContactDetailsEntity>(this IHaveContactDetailsEntities<TContactDetailsEntity> haveContactDetailsEntities, IHaveContactDetails haveContactDetails, IHaveContactDetailsEntity<TContactDetailsEntity> entity)
            where TContactDetailsEntity : class, IContactDetailsEntity
        {
            // If there are now no contact details but there were some before then delete.

            if (!HasContactDetails(haveContactDetails) && HasContactDetails(entity))
            {
                haveContactDetailsEntities.ContactDetailsEntities.DeleteOnSubmit(entity.ContactDetailsEntity);
                entity.ContactDetailsEntity = null;
            }
        }

        public static void MapTo<TContactDetailsEntity>(this IHaveContactDetails haveContactDetails, IHaveContactDetailsEntity<TContactDetailsEntity> entity)
            where TContactDetailsEntity : class, IContactDetailsEntity, new()
        {
            // Update the contact details in place if able.

            if (HasContactDetails(haveContactDetails))
            {
                if (entity.ContactDetailsEntity == null)
                    entity.ContactDetailsEntity = haveContactDetails.ContactDetails.MapTo<TContactDetailsEntity>();
                else
                    haveContactDetails.ContactDetails.MapTo(entity.ContactDetailsEntity);

                entity.companyName = haveContactDetails.ContactDetails.CompanyName;
            }
        }

        public static ContactDetails Map<TContactDetailsEntity>(this IHaveContactDetailsEntity<TContactDetailsEntity> entity)
            where TContactDetailsEntity : class, IContactDetailsEntity
        {
            if (entity.ContactDetailsEntity == null)
                return null;

            return new ContactDetails
            {
                Id = entity.ContactDetailsEntity.id,
                FirstName = entity.ContactDetailsEntity.firstName,
                LastName = entity.ContactDetailsEntity.lastName,
                EmailAddress = entity.ContactDetailsEntity.email,
                SecondaryEmailAddresses = entity.ContactDetailsEntity.secondaryEmails,
                FaxNumber = entity.ContactDetailsEntity.faxNumber,
                PhoneNumber = entity.ContactDetailsEntity.phoneNumber,
                CompanyName = entity.companyName,
            };
        }

        private static bool HasContactDetails<TContactDetailsEntity>(IHaveContactDetailsEntity<TContactDetailsEntity> entity)
            where TContactDetailsEntity : class, IContactDetailsEntity
        {
            return entity.ContactDetailsEntity != null;
        }

        private static bool HasContactDetails(IHaveContactDetails haveContactDetails)
        {
            return haveContactDetails.ContactDetails != null && !haveContactDetails.ContactDetails.IsEmpty;
        }

        private static T MapTo<T>(this ContactDetails contactDetails)
            where T : IContactDetailsEntity, new()
        {
            var t = new T { id = contactDetails.Id };
            contactDetails.MapTo(t);
            return t;
        }

        private static void MapTo(this ContactDetails contactDetails, IContactDetailsEntity entity)
        {
            // Need to ensure that objects match what is in the database.

            if (entity.id != Guid.Empty)
                contactDetails.Id = entity.id;

            entity.firstName = contactDetails.FirstName;
            entity.lastName = contactDetails.LastName;
            entity.email = contactDetails.EmailAddress;
            entity.secondaryEmails = contactDetails.SecondaryEmailAddresses;
            entity.faxNumber = contactDetails.FaxNumber;
            entity.phoneNumber = contactDetails.PhoneNumber;
        }
    }
}
