using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Queries
{
    public interface IUsersQuery
    {
        RegisteredUser GetUser(Guid id);
    }
}