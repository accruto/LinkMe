using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Anonymous.Data
{
    internal static class Mappings
    {
        public static AnonymousUserEntity Map(this IAnonymousUser user)
        {
            return new AnonymousUserEntity { id = user.Id };
        }

        public static AnonymousContact Map(this AnonymousContactEntity entity, ContactDetailEntity contactDetailsEntity)
        {
            return new AnonymousContact
            {
                Id = entity.contactDetailsId,
                EmailAddress = contactDetailsEntity.email,
                FirstName = contactDetailsEntity.firstName,
                LastName = contactDetailsEntity.lastName,
            };
        }

        public static AnonymousContactEntity Map(this AnonymousContact contact, Guid userId)
        {
            return new AnonymousContactEntity
            {
                userId = userId,
                ContactDetailEntity = new ContactDetailEntity
                {
                    id = contact.Id,
                    email = contact.EmailAddress,
                    firstName = contact.FirstName,
                    lastName = contact.LastName
                }
            };
        }
    }
}
