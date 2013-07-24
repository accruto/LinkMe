using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Areas.Administrators.Models.Organisations
{
    public class NewEmployerModel
    {
        public Organisation Organisation { get; set; }
        public IList<Employer> Employers { get; set; }
        public CreateEmployerModel Employer { get; set; }
    }
}