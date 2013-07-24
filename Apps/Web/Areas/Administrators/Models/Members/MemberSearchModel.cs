using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Administrators.Models.Members
{
    public class MemberSearchModel
    {
        public AdministrativeMemberSearchCriteria Criteria { get; set; }
        public IList<Member> Members { get; set; }
    }
}