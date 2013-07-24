using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class OrganisationModel
    {
        public Organisation Organisation { get; set; }
        public OrganisationHierarchy OrganisationHierarchy { get; set; }
        public IList<Administrator> AccountManagers { get; set; }
        public Administrator VerifiedByAccountManager { get; set; }
        public IList<Community> Communities { get; set; }
    }
}