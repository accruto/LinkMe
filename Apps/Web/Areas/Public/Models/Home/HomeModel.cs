using System.Collections.Generic;
using LinkMe.Apps.Asp.Security;
using LinkMe.Domain.Contacts;
using LinkMe.Web.Areas.Members.Models.Search;
using MemberJoin = LinkMe.Web.Models.Accounts.MemberJoin;

namespace LinkMe.Web.Areas.Public.Models.Home
{
    public class HomeModel
    {
        public UserType PreferredUserType { get; set; }
        public Login Login { get; set; }
        public MemberJoin Join { get; set; }
        public bool AcceptTerms { get; set; }
        public ReferenceModel Reference { get; set; }
        public IList<JobAdSearchModel> RecentSearches { get; set; } //for Mobile
    }
}
