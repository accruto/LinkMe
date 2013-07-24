using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.JobAds
{
    public abstract class JobAdNote
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }
        [DefaultNow]
        public DateTime UpdatedTime { get; set; }

        [Required, StringLength(500)]
        public string Text { get; set; }

        internal protected Guid OwnerId { get; set; }

        [Required]
        public Guid JobAdId { get; set; }
    }
}