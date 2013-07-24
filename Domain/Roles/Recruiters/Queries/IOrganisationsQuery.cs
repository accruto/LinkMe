using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;

namespace LinkMe.Domain.Roles.Recruiters.Queries
{
    public interface IOrganisationsQuery
    {
        Organisation GetOrganisation(Guid id);
        Organisation GetOrganisation(Guid? parentId, string name);
        VerifiedOrganisation GetVerifiedOrganisation(string fullName);
        IList<Organisation> GetOrganisations(IEnumerable<Guid> ids);

        Organisation GetRootOrganisation(Guid id);
        bool IsDescendent(Guid parentId, Guid childId);
        OrganisationHierarchy GetOrganisationHierarchy(Guid organisationId);
        OrganisationHierarchy GetSubOrganisationHierarchy(Guid organisationId);

        ContactDetails GetEffectiveContactDetails(Guid organisationId);
    }
}