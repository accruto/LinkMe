using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Domain.Roles.Contenders.Queries
{
    public interface IContendersQuery
    {
        ProfessionalContactDegree GetContactDegree(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId);

        IList<Guid> GetApplicants(OrganisationHierarchyPath organisationHierarchyPath);
        IList<Guid> GetApplicants(OrganisationHierarchyPath organisationHierarchyPath, IEnumerable<Guid> contenderIds);
        bool IsApplicant(OrganisationHierarchyPath organisationHierarchyPath, Guid contenderId);
    }
}