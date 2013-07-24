using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSortExecution
        : SearchExecution
    {
        [Prepare, Validate]
        public JobAdSortCriteria Criteria { get; set; }
        [Prepare, Validate]
        public JobAdSearchResults Results { get; set; }
    }
}