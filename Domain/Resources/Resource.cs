using System;
using System.Runtime.Serialization;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Resources
{
    [KnownType(typeof(Article))]
    [KnownType(typeof(Video))]
    [KnownType(typeof(QnA))]
    public abstract class Resource
        : IHasId<Guid>
    {
        [DefaultNewGuid]
        public Guid Id { get; set; }
        public Guid SubcategoryId { get; set; }
        [StringLength(Constants.MaxTitleLength)]
        public string Title { get; set; }
        public string Text { get; set; }
        public string ShortUrl { get; set; }
        [DefaultNow]
        public DateTime CreatedTime { get; set; }
    }
}
