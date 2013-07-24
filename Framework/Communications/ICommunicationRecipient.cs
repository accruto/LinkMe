using System;
using LinkMe.Framework.Utility;

namespace LinkMe.Framework.Communications
{
    public interface ICommunicationRecipient
        : IHasId<Guid>
    {
        string FirstName { get; }
        string LastName { get; }
        string FullName { get; }
        string EmailAddress { get; }
    }
}