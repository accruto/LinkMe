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
    public class FaqSearchCriteria
        : ICanBeEmpty
    {
        private string _keywords;

        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }

        public string Keywords
        {
            get { return _keywords; }
            set { _keywords = value.NullIfEmpty(); }
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
                "{0}: keywords: '{1}', category id: '{2}', subcategory id: '{3}'",
                GetType().Name,
                _keywords,
                CategoryId,
                SubcategoryId);
            return sb.ToString();
        }

        public ResourceSearchQuery GetSearchQuery(Range range)
        {
            return new ResourceSearchQuery
            {
                SortOrder = ResourceSortOrder.Relevance,

                Skip = range == null ? 0 : range.Skip,
                Take = range == null ? null : range.Take,

                Keywords = Expression.Parse(_keywords),
                CategoryId = CategoryId,
                SubcategoryId = SubcategoryId,
                ResourceType = ResourceType.Faq,
            };
        }
    }
}
