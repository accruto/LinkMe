using System;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Recruiters
{
    public interface IRecruiter
    {
        Guid Id { get; }
        IOrganisation Organisation { get; }
    }
}