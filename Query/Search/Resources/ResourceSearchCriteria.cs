using System;
using System.Text;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Framework.Utility.Results;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Resources;

namespace LinkMe.Query.Search.Resources
{
    public class ResourceSearchSortCriteria
    {
        public ResourceSortOrder SortOrder { get; set; }
        public bool ReverseSortOrder { get; set; }
    }

    public class ResourceSearchCriteria
        : ICanBeEmpty, ICloneable
    {
        private string _keywords;
        private ResourceSearchSortCriteria _sortCriteria;

        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public ResourceType ResourceType { get; set; }

        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value.NullIfEmpty(); }
        }


        public const ResourceSortOrder DefaultSortOrder = ResourceSortOrder.CreatedTime;
        public const ResourceType DefaultResourceType = ResourceType.Article;

        public ResourceSearchCriteria()
        {
            SortCriteria = new ResourceSearchSortCriteria {SortOrder = DefaultSortOrder, ReverseSortOrder = false};
            ResourceType = DefaultResourceType;
        }

        public ResourceSearchSortCriteria SortCriteria
        {
            get
            {
                return new ResourceSearchSortCriteria
                {
                    SortOrder = _sortCriteria.SortOrder,
                    ReverseSortOrder = _sortCriteria.ReverseSortOrder,
                };
            }
            set
            {
                _sortCriteria = value == null
                    ? new ResourceSearchSortCriteria { SortOrder = DefaultSortOrder, ReverseSortOrder = false }
                    : new ResourceSearchSortCriteria { SortOrder = value.SortOrder, ReverseSortOrder = value.ReverseSortOrder };
            }
        }

        public virtual bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(_keywords)
                       && CategoryId == null
                       && SubcategoryId == null;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat(
                "{0}: keywords: '{1}', category id: '{2}', subcategory id: '{3}', sort order: {4}, reverse sort order: {5}",
                GetType().Name,
                _keywords,
                CategoryId,
                SubcategoryId,
                SortCriteria.SortOrder,
                SortCriteria.ReverseSortOrder);
            return sb.ToString();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        public ResourceSearchCriteria Clone()
        {
            return new ResourceSearchCriteria
            {
                CategoryId = CategoryId,
                SubcategoryId = SubcategoryId,
                SortCriteria = SortCriteria,
                ResourceType = ResourceType,
                Keywords = Keywords,
            };
        }

        public ResourceSearchQuery GetSearchQuery(Range range)
        {
            return new ResourceSearchQuery
            {
                SortOrder = SortCriteria.SortOrder,
                ReverseSortOrder = SortCriteria.ReverseSortOrder,

                Skip = range == null ? 0 : range.Skip,
                Take = range == null ? null : range.Take,

                Keywords = Expression.Parse(_keywords),
                CategoryId = CategoryId,
                SubcategoryId = SubcategoryId,
                ResourceType = ResourceType,
            };
        }
    }
}