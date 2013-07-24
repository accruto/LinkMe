using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Accounts
{
    public interface IUserAccount
        : IHasId<Guid>
    {
        UserType UserType { get; }
        DateTime CreatedTime { get; }
        bool IsEnabled { get; }
        bool IsActivated { get; }
    }
}