using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders
{
    public abstract class ContenderNote
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
        internal protected Guid ContenderId { get; set; }

        internal protected Guid? SharedWithId { get; set; }
    }
}
