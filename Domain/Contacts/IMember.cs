using System;
using System.Collections.ObjectModel;
using LinkMe.Domain.Location;

namespace LinkMe.Domain.Contacts
{
    public interface IMember
        : IRegisteredUser
    {
        DateTime LastUpdatedTime { get; }
        ReadOnlyCollection<EmailAddress> EmailAddresses { get; }
        ReadOnlyCollection<PhoneNumber> PhoneNumbers { get; }
        VisibilitySettings VisibilitySettings { get; }
        Guid? PhotoId { get; }
        Gender Gender { get; }
        PartialDate? DateOfBirth { get; }
        EthnicStatus EthnicStatus { get; }
        Address Address { get; }
    }
}

