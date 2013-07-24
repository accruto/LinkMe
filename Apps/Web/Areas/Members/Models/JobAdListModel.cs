using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Verticals;
using LinkMe.Domain.Users.Members.JobAds;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search;
using LinkMe.Query.Search.JobAds;
using LinkMe.Web.Areas.Members.Models.JobAds;

namespace LinkMe.Web.Areas.Members.Models
{
    public class JobAdListResultsModel
    {
        public int TotalJobAds { get; set; }
        public IList<Guid> JobAdIds { get; set; }
        public IDictionary<Guid, MemberJobAdView> JobAds { get; set; }
        public IDictionary<string, int> IndustryHits { get; set; }
        public IDictionary<string, int> JobTypeHits { get; set; }
    }

    public abstract class JobAdListModel
    {
        public JobAdListType ListType { get; set; }
        public JobAdsPresentationModel Presentation { get; set; }
        public JobAdListResultsModel Results { get; set; }

        public IList<JobAdSortOrder> SortOrders { get; set; }
        public IDictionary<Guid, Community> Communities { get; set; }
        public IDictionary<Guid, Vertical> Verticals { get; set; }
        public FoldersModel Folders { get; set; }
        public BlockListsModel BlockLists { get; set; }
        public IList<Industry> Industries { get; set; }
    }

    public abstract class JobAdSearchListModel
        : JobAdListModel
    {
        public JobAdSearchCriteria Criteria { get; set; }
    }
    
    public abstract class JobAdSortListModel
        : JobAdListModel
    {
        public JobAdSortCriteria Criteria { get; set; }
    }

    public class JobAdListResultsMobileModel
    {
        public IList<Guid> JobAdIds { get; set; }
        public IDictionary<Guid, MemberJobAdView> JobAds { get; set; }
    }

    public abstract class JobAdListMobileModel
    {
        public JobAdListResultsMobileModel Results { get; set; }
    }

    public class JsonJobAdsResponseModel
        : JsonResponseModel
    {
        public string CriteriaHtml { get; set; }
        public int TotalJobAds { get; set; }
        public IList<MemberJobAdView> JobAds { get; set; }
        public IDictionary<string, int> IndustryHits { get; set; }
        public IDictionary<string, int> JobTypeHits { get; set; }
        public string Hash { get; set; }
        public string QueryStringForGa { get; set; }
    }
}
