using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public class JobAdViewing
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }

        public Guid? ViewerId { get; set; }
        [IsSet]
        public Guid JobAdId { get; set; }
    }
}
