using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Anonymous
{
    public interface IAnonymousRepository
    {
        void CreateContact(IAnonymousUser user, AnonymousContact contact);
        AnonymousContact GetContact(Guid id);
        AnonymousContact GetContact(IAnonymousUser user, ContactDetails contactDetails);
    }
}
