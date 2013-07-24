using System;
using LinkMe.Domain.Resources;
using LinkMe.Framework.Utility.Expressions;

namespace LinkMe.Query.Resources
{
    [Serializable]
    public class ResourceSearchQuery
    {
        public ResourceSortOrder SortOrder;
        public bool ReverseSortOrder;

        public int Skip;
        public int? Take;

        public Guid? CategoryId;
        public Guid? SubcategoryId;
        public IExpression Keywords;
        public ResourceType? ResourceType;
    }
}
