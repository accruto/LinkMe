using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Employers;

namespace LinkMe.Web.Areas.Administrators.Models.Employers
{
    public class EmployerModel
    {
        public IEmployer Employer { get; set; }
        public string LoginId { get; set; }
    }

    public class EmployerSearchModel
    {
        public AdministrativeEmployerSearchCriteria Criteria { get; set; }
        public IList<EmployerModel> Employers { get; set; }
    }
}