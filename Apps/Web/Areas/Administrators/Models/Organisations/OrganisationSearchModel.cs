using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Query.Search.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class OrganisationSearchModel
    {
        public OrganisationSearchCriteria Criteria { get; set; }
        public IList<Administrator> AccountManagers { get; set; }
        public IList<Organisation> Organisations { get; set; }
    }
}