using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Web.Areas.Members.Models.Search
{
    public class SearchModel
    {
        public JobAdSearchCriteria Criteria { get; set; }
        public JobAdsPresentationModel Presentation { get; set; }
        public JobAdListType ListType { get; set; }
        public IList<MemberJobAdView> SuggestedJobs { get; set; }
        public IDictionary<Guid, ContactDetails> ContactDetails { get; set; }
        public SearchAncillaryModel AncillaryData { get; set; }
    }
}
