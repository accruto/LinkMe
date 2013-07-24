using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;

namespace LinkMe.Apps.Presentation.Converters
{
    public class MemberSearchSortCriteriaConverter
        : Converter<MemberSearchSortCriteria>
    {
        public override void Convert(MemberSearchSortCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;
            values.SetValue(MemberSearchCriteriaKeys.SortOrder, criteria.SortOrder);

            if (criteria.ReverseSortOrder)
                values.SetValue(MemberSearchCriteriaKeys.SortOrderDirection, MemberSearchCriteriaKeys.SortOrderIsAscending);
            else
                values.SetValue(MemberSearchCriteriaKeys.SortOrderDirection, MemberSearchCriteriaKeys.SortOrderIsDescending);
        }

        public override MemberSearchSortCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            var sortOrder = values.GetValue<MemberSortOrder>(MemberSearchCriteriaKeys.SortOrder);
            if (sortOrder == null)
                return null;

            var direction = values.GetStringValue(MemberSearchCriteriaKeys.SortOrderDirection);
            var isReversed = false;
            switch (direction)
            {
                case MemberSearchCriteriaKeys.SortOrderIsAscending:
                    isReversed = true;
                    break;
            }

            return new MemberSearchSortCriteria { SortOrder = sortOrder.Value, ReverseSortOrder = isReversed };
        }
    }
}