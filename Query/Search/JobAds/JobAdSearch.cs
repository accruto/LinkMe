using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSearch
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [IsSet]
        public Guid OwnerId { get; set; }
        [JobAdSearchName]
        public string Name { get; set; }
        [Prepare, Validate]
        public JobAdSearchCriteria Criteria { get; set; }
    }
}