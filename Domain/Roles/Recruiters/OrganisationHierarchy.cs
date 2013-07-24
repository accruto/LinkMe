using System.Collections.Generic;

namespace LinkMe.Domain.Roles.Recruiters
{
    public class OrganisationHierarchy
    {
        public Organisation Organisation { get; set; }
        public IList<OrganisationHierarchy> ChildOrganisationHierarchies { get; set; }
    }
}
