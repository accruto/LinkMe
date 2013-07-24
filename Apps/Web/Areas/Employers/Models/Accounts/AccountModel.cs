using System.Collections.Generic;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Industries;
using LinkMe.Web.Models.Accounts;

namespace LinkMe.Web.Areas.Employers.Models.Accounts
{
    public class AccountModel
    {
        public Login Login { get; set; }
        public EmployerJoin Join { get; set; }
        public bool AcceptTerms { get; set; }
        public IList<Industry> Industries { get; set; }
    }
}