using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.JobAds
{
    public abstract class JobAdListEntry
    {
        internal protected Guid ListId { get; set; }
        public Guid JobAdId { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }

    }
}
