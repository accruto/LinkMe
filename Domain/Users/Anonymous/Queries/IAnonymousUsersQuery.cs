using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Anonymous.Queries
{
    public interface IAnonymousUsersQuery
    {
        AnonymousContact GetContact(Guid id);
        AnonymousContact GetContact(AnonymousUser user, ContactDetails contactDetails);
    }
}
