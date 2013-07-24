using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources
{
    public class FaqSearchExecution
        : SearchExecution
    {
        [Prepare, Validate]
        public FaqSearchCriteria Criteria { get; set; }
        [Prepare, Validate]
        public ResourceSearchResults Results { get; set; }
    }
}
