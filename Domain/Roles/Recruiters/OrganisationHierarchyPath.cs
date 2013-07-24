using System;
using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Recruiters
{
    public class OrganisationHierarchyPath
        : HierarchyPath
    {
        public OrganisationHierarchyPath(IEnumerable<Guid> ids)
            : base(ids)
        {
        }

        public OrganisationHierarchyPath()
        {
        }

        public Guid? RecruiterId
        {
            get
            {
                // The order of the ids is assumed to be employer up.

                if (OwnerIds.Count == 0)
                    return null;
                return OwnerIds[0];
            }
        }
    }
}
