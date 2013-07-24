using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;

namespace LinkMe.Domain.Users.Employers.Queries
{
    public abstract class EmployerRecruitersQuery
    {
        protected readonly IRecruitersQuery _recruitersQuery;

        protected EmployerRecruitersQuery(IRecruitersQuery recruitersQuery)
        {
            _recruitersQuery = recruitersQuery;
        }

        protected IList<Employer> GetOrganisations(IList<Employer> employers)
        {
            var organisations = _recruitersQuery.GetOrganisations(from e in employers select e.Id);
            return (from e in employers
                    select GetEmployer(e, organisations)).ToList();
        }

        protected Employer GetEmployer(Employer employer, IDictionary<Guid, Organisation> organisations)
        {
            Organisation organisation;
            organisations.TryGetValue(employer.Id, out organisation);
            return GetEmployer(employer, organisation);
        }

        protected Employer GetEmployer(Employer employer, Organisation organisation)
        {
            employer.Organisation = organisation;
            return employer;
        }
    }
}