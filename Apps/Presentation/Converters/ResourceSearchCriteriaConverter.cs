using LinkMe.Domain.Resources;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Resources;

namespace LinkMe.Apps.Presentation.Converters
{
    public class ResourceSearchCriteriaConverter
        : Converter<ResourceSearchCriteria>
    {
        public override void Convert(ResourceSearchCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            values.SetValue(ResourceSearchCriteriaKeys.CategoryId, criteria.CategoryId);
            values.SetValue(ResourceSearchCriteriaKeys.SubcategoryId, criteria.SubcategoryId);
            values.SetValue(ResourceSearchCriteriaKeys.Keywords, criteria.Keywords);
            if (criteria.ResourceType != ResourceSearchCriteria.DefaultResourceType)
                values.SetValue(ResourceSearchCriteriaKeys.ResourceType, criteria.ResourceType);

            ConvertSortOrder(criteria, values);
        }

        public override ResourceSearchCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var criteria = new ResourceSearchCriteria();

            DeconvertSortOrder(criteria, values, errors);

            criteria.Keywords = values.GetStringValue(ResourceSearchCriteriaKeys.Keywords);
            criteria.CategoryId = values.GetGuidValue(ResourceSearchCriteriaKeys.CategoryId);
            criteria.SubcategoryId = values.GetGuidValue(ResourceSearchCriteriaKeys.SubcategoryId);
            criteria.ResourceType = values.GetValue<ResourceType>(ResourceSearchCriteriaKeys.ResourceType) ?? ResourceSearchCriteria.DefaultResourceType;

            return criteria;
        }


        private static void ConvertSortOrder(ResourceSearchCriteria criteria, ISetValues values)
        {
            if (criteria.SortCriteria.SortOrder != ResourceSearchCriteria.DefaultSortOrder || criteria.SortCriteria.ReverseSortOrder)
                new ResourceSearchSortCriteriaConverter().Convert(criteria.SortCriteria, values);
        }

        private static void DeconvertSortOrder(ResourceSearchCriteria criteria, IGetValues values, IDeconverterErrors errors)
        {
            criteria.SortCriteria = new ResourceSearchSortCriteriaConverter().Deconvert(values, errors);
        }
    }

    public class ResourceSearchSortCriteriaConverter
        : Converter<ResourceSearchSortCriteria>
    {
        public override void Convert(ResourceSearchSortCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;

            values.SetValue(ResourceSearchCriteriaKeys.SortOrder, criteria.SortOrder);
            values.SetValue(ResourceSearchCriteriaKeys.SortOrderDirection, criteria.ReverseSortOrder ? ResourceSearchCriteriaKeys.SortOrderIsAscending : ResourceSearchCriteriaKeys.SortOrderIsDescending);
        }

        public override ResourceSearchSortCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var sortOrder = values.GetValue<ResourceSortOrder>(ResourceSearchCriteriaKeys.SortOrder);
            if (sortOrder == null)
                return null;

            var direction = values.GetStringValue(ResourceSearchCriteriaKeys.SortOrderDirection);
            var isReversed = false;
            switch (direction)
            {
                case ResourceSearchCriteriaKeys.SortOrderIsAscending:
                    isReversed = true;
                    break;
            }

            return new ResourceSearchSortCriteria { SortOrder = sortOrder.Value, ReverseSortOrder = isReversed };
        }
    }
}