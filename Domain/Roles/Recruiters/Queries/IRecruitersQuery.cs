using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Recruiters.Queries
{
    public interface IRecruitersQuery
    {
        Organisation GetOrganisation(Guid recruiterId);
        IDictionary<Guid, Organisation> GetOrganisations(IEnumerable<Guid> recruiterIds);
        IDictionary<Guid, Organisation> GetOrganisations(IEnumerable<Guid> recruiterIds, IEnumerable<Guid> organisationIds);

        OrganisationHierarchyPath GetOrganisationHierarchyPath(Guid recruiterId);

        IList<Guid> GetRecruiters(Guid organisationId);
        IList<Guid> GetRecruiters(OrganisationHierarchy organisationHierarchy);
    }
}