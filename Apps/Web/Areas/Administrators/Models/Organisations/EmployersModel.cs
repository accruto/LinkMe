using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class EmployersModel
    {
        public Organisation Organisation { get; set; }
        public bool IncludeChildOrganisations { get; set; }
        public IList<Employer> Employers { get; set; }
    }
}