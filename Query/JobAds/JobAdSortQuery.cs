using System;

namespace LinkMe.Query.JobAds
{
    [Serializable]
    public class JobAdSortQuery
    {
        public JobAdSortOrder SortOrder;
        public bool ReverseSortOrder;

        public int Skip;
        public int? Take;
    }
}
