using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Web.Models.Credits;

namespace LinkMe.Web.Areas.Employers.Models.Products
{
    public class EmployerCreditsModel
        : CreditsModel
    {
        public IEmployer Employer { get; set; }
        public IList<Organisation> OrganisationHierarchy { get; set; }
    }
}