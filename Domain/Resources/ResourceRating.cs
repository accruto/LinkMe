using System;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources
{
    public class ResourceRating
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ResourceId { get; set; }
        [Required]
        public ResourceType ResourceType { get; set; }
        public byte Rating { get; set; }
        [DefaultNow]
        public DateTime LastUpdatedTime { get; set; }
    }

    public class ResourceRatingSummary
    {
        public int RatingCount { get; set; }
        public double AverageRating { get; set; }
        public byte? UserRating { get; set; }
    }
}
