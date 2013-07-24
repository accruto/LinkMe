using System;

namespace LinkMe.Domain.Resources
{
    public enum FeaturedResourceType
    {
        Slideshow,
        New
    };

    public class FeaturedResource
    {
        public Guid Id { get; set; }
        public string CssClass { get; set; }
        public FeaturedResourceType FeaturedResourceType { get; set; }
        public Guid ResourceId { get; set; }
    }
}
