using System.Collections.Generic;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public class SearchesModel
    {
        public IList<MemberSearch> Searches { get; set; }
        public int TotalItems { get; set; }
        public int MoreItems { get; set; }
    }
}
