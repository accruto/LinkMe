using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Framework.Utility;

namespace LinkMe.Domain.Users.Employers.Credits.Queries
{
    public class EmployerCreditsQuery
        : EmployerCreditsComponent, IEmployerCreditsQuery
    {
        private readonly IRecruitersQuery _recruitersQuery;

        public EmployerCreditsQuery(ICreditsQuery creditsQuery, IAllocationsQuery allocationsQuery, IRecruitersQuery recruitersQuery)
            : base(creditsQuery, allocationsQuery)
        {
            _recruitersQuery = recruitersQuery;
        }

        Allocation IEmployerCreditsQuery.GetEffectiveActiveAllocation<TCredit>(IEmployer employer)
        {
            return GetEffectiveActiveAllocation<TCredit>(GetOrganisationHierarchyPath(employer));
        }

        IDictionary<Guid, Allocation> IEmployerCreditsQuery.GetEffectiveActiveAllocations(IEmployer employer, IEnumerable<Guid> creditIds)
        {
            return GetEffectiveActiveAllocations(GetOrganisationHierarchyPath(employer), creditIds);
        }

        IDictionary<Guid, Allocation> IEmployerCreditsQuery.GetEffectiveExpiringAllocations<TCredit>(DateTime expiryDate)
        {
            return GetEffectiveExpiringAllocations<TCredit>(expiryDate);
        }

        private IEnumerable<Guid> GetOrganisationHierarchyPath(IHasId<Guid> employer)
        {
            return employer == null
                ? new OrganisationHierarchyPath()
                : _recruitersQuery.GetOrganisationHierarchyPath(employer.Id);
        }
    }
}