using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Roles.Contenders
{
    public abstract class ContenderList
        : IHasId<Guid>
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }

        [StringLength(Constants.ContenderListNameMaxLength)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [IsSet]
        internal protected Guid OwnerId { get; set; }
        internal protected Guid? SharedWithId { get; set; }
        internal protected int ListType { get; set; }

        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}