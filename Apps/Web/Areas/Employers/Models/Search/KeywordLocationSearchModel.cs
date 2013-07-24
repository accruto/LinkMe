using System.Collections.Generic;
using LinkMe.Domain.Location;
using LinkMe.Query.Search.Members;

namespace LinkMe.Web.Areas.Employers.Models.Search
{
    public class KeywordLocationSearchModel
    {
        public MemberSearchCriteria Criteria { get; set; }
        public bool CanSearchByName { get; set; }
        public IList<int> Distances { get; set; }
        public int DefaultDistance { get; set; }
        public IList<Country> Countries { get; set; }
        public Country DefaultCountry { get; set; }
    }
}