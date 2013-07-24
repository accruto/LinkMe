using System.Collections.Generic;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Employers.Models
{
    public class MemberSearchSortModel
    {
        public MemberSearchSortCriteria Criteria { get; set; }
        public IList<MemberSortOrder> SortOrders { get; set; } 
    }
}
