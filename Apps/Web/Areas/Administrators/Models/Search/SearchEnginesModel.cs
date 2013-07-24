using System;

namespace LinkMe.Web.Areas.Administrators.Models.Search
{
    public class MemberSearchEngineModel
    {
        public string LoginId { get; set; }
        public bool? IsIndexed { get; set; }
        public int? TotalMembers { get; set; }
    }

    public class JobAdSearchEngineModel
    {
        public Guid? JobAdId { get; set; }
        public bool? IsIndexed { get; set; }
        public int? TotalJobAds { get; set; }
    }

    public class SearchEnginesModel
    {
        public MemberSearchEngineModel MemberSearch { get; set; }
        public JobAdSearchEngineModel JobAdSearch { get; set; }
    }
}
