using System;
using System.Collections.Generic;
using LinkMe.Apps.Asp.Mvc.Models;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Web.Areas.Members.Models.Search
{
    public class JobAdSearchModel
    {
        public Guid? ExecutionId { get; set; }
        public Guid? SearchId { get; set; }
        public JobAdSearchCriteria Criteria { get; set; }
        public string Name { get; set; }
        public bool HasAlert { get; set; }
    }

    public class SearchesModel
    {
        public IList<JobAdSearchModel> Searches { get; set; }
        public int TotalItems { get; set; }
        public Pagination Pagination { get; set; }
        public SearchesType Type { get; set; }
    }

    public enum SearchesType
    {
        Recent,
        Saved,
    }
}
