using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Query.Search.Members
{
    public class MemberSearch
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [IsSet]
        public Guid OwnerId { get; set; }
        [Required, MemberSearchName]
        public string Name { get; set; }
        [Required, IsNotEmpty, Prepare]
        public MemberSearchCriteria Criteria { get; set; }
    }
}