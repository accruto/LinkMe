using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearchExecution
        : SearchExecution
    {
        [Prepare, Validate]
        public JobAdSearchCriteria Criteria { get; set; }
        [Prepare, Validate]
        public JobAdSearchResults Results { get; set; }
    }
}