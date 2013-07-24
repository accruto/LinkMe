using System;
using System.Collections.Generic;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Location;

namespace LinkMe.Domain.Contacts
{
    public interface IOrganisation
    {
        Guid Id { get; }
        bool IsVerified { get; }
        string Name { get; }
        string FullName { get; }
        Address Address { get; }
        Guid? AffiliateId { get; }
    }

    public interface IEmployer
        : IRegisteredUser
    {
        EmailAddress EmailAddress { get; }
        PhoneNumber PhoneNumber { get; }
        IOrganisation Organisation { get; }
        IList<Industry> Industries { get; }
    }
}
