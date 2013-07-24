using System;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources
{
    public class ResourceViewing
        : IHasId<Guid>
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ResourceId { get; set; }
        [Required]
        public ResourceType ResourceType { get; set; }
        [DefaultNow]
        public DateTime Time { get; set; }
    }
}
