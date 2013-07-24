using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearchExecution
        : SearchExecution
    {
        [Required, Prepare, Validate]
        public MemberSearchCriteria Criteria { get; set; }
        [Required, Prepare, Validate]
        public MemberSearchResults Results { get; set; }
    }
}