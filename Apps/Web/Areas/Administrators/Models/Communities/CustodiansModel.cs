using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;

namespace LinkMe.Web.Areas.Administrators.Models.Communities
{
    public class CustodiansModel
    {
        public Community Community { get; set; }
        public IList<Custodian> Custodians { get; set; }
    }
}