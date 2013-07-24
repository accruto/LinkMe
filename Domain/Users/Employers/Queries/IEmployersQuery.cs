using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Users.Employers.Queries
{
    public interface IEmployersQuery
    {
        Employer GetEmployer(Guid id);
        IList<Employer> GetEmployers(IEnumerable<Guid> ids);
        IList<Employer> GetEmployers(string emailAddress);

        IList<Guid> GetEmployerIds();

        IList<Employer> GetOrganisationEmployers(Guid organisationId);
        IList<Employer> GetOrganisationEmployers(IEnumerable<Guid> organisationIds);
    }
}