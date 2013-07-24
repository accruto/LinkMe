using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public abstract class JobAdList : IHasId<Guid>
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [StringLength(Constants.JobAdListNameMaxLength)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [IsSet]
        internal protected Guid OwnerId { get; set; }
        internal protected int ListType { get; set; }
        
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}
