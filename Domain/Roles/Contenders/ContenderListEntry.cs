using System;
using LinkMe.Framework.Utility.Preparation;

namespace LinkMe.Domain.Roles.Contenders
{
    public abstract class ContenderListEntry
    {
        internal protected Guid ListId { get; set; }
        internal protected Guid ContenderId { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }

        internal protected Guid? ApplicationId { get; set; }
        internal protected ApplicantStatus? ApplicantStatus { get; set; }
    }
}