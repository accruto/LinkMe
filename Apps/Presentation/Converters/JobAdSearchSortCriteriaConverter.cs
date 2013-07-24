using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;

namespace LinkMe.Apps.Presentation.Converters
{
    public class JobAdSearchSortCriteriaConverter
        : Converter<JobAdSearchSortCriteria>
    {
        public override void Convert(JobAdSearchSortCriteria criteria, ISetValues values)
        {
            if (criteria == null)
                return;
            values.SetValue(JobAdSearchCriteriaKeys.SortOrder, criteria.SortOrder);
            values.SetValue(JobAdSearchCriteriaKeys.SortOrderDirection, criteria.ReverseSortOrder ? JobAdSearchCriteriaKeys.SortOrderIsAscending : JobAdSearchCriteriaKeys.SortOrderIsDescending);
        }

        public override JobAdSearchSortCriteria Deconvert(IGetValues values, IDeconverterErrors errors)
        {
            JobAdSortOrder? sortOrder;

            // This used to be the name so look for it first.

            var value = values.GetStringValue(JobAdSearchCriteriaKeys.SortOrder);
            if (value == "CreatedTimeDescending")
            {
                sortOrder = JobAdSortOrder.CreatedTime;
            }
            else
            {
                sortOrder = values.GetValue<JobAdSortOrder>(JobAdSearchCriteriaKeys.SortOrder);
                if (sortOrder == null)
                    return null;
            }

            var direction = values.GetStringValue(JobAdSearchCriteriaKeys.SortOrderDirection);
            var isReversed = false;
            switch (direction)
            {
                case JobAdSearchCriteriaKeys.SortOrderIsAscending:
                    isReversed = true;
                    break;
            }

            return new JobAdSearchSortCriteria { SortOrder = sortOrder.Value, ReverseSortOrder = isReversed };
        }
    }
}