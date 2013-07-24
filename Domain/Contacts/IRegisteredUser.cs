using System;
using LinkMe.Domain.Accounts;

namespace LinkMe.Domain.Contacts
{
    public interface IRegisteredUser
        : IUserAccount, IUser
    {
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        Guid? AffiliateId { get; }
    }
}
