using System;
using LinkMe.Framework.Communications;

namespace LinkMe.Domain.Contacts
{
    public interface ICommunicationUser
        : ICommunicationRecipient
    {
        UserType UserType { get; }
        bool IsEnabled { get; }
        bool IsActivated { get; }
        bool IsEmailAddressVerified { get; }
        Guid? AffiliateId { get; }
    }
}
