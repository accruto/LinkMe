using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources
{
    public class ResourceSearchExecution
        : SearchExecution
    {
        [Prepare, Validate]
        public ResourceSearchCriteria Criteria { get; set; }
        [Prepare, Validate]
        public ResourceSearchResults Results { get; set; }
    }
}
