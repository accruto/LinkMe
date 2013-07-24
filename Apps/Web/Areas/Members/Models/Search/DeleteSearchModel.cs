using LinkMe.Query.Search.JobAds;

namespace LinkMe.Web.Areas.Members.Models.Search
{
    public class DeleteSearchModel
    {
        public JobAdSearch Search { get; set; }
        public bool HasDeleted { get; set; }
    }
}
