using System.Collections.Generic;
using System.Text;
using LinkMe.Framework.Utility.Results;
using LinkMe.Query.JobAds;

namespace LinkMe.Query.Search.JobAds
{
    public class JobAdSortCriteria
        : SearchCriteria
    {
        public JobAdSearchSortCriteria SortCriteria { get; set; }

        public const JobAdSortOrder DefaultSortOrder = JobAdSortOrder.CreatedTime;

        private static readonly IDictionary<string, CriteriaDescription> Descriptions = new Dictionary<string, CriteriaDescription>();

        private static readonly IDictionary<string, CriteriaDescription> CombinedDescriptions = Combine(Descriptions);

        public JobAdSortCriteria()
            : base(CombinedDescriptions)
        {
            SortCriteria = new JobAdSearchSortCriteria {SortOrder = DefaultSortOrder, ReverseSortOrder = false};
        }

        public bool IsEmpty
        {
            get { return false; }
        }

        public override bool Equals(object obj)
        {
            var other = obj as JobAdSortCriteria;
            if (other == null)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode()
                ^ SortCriteria.SortOrder.GetHashCode()
                ^ SortCriteria.ReverseSortOrder.GetHashCode();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            ToString(sb);
            return sb.ToString();
        }

        protected void ToString(StringBuilder sb)
        {
            sb.AppendFormat(
                "sort order: {0}, reverse sort order: {1}",
                SortCriteria.SortOrder,
                SortCriteria.ReverseSortOrder);
        }

        public JobAdSortQuery GetSortQuery(Range range)
        {
            return new JobAdSortQuery
            {
                SortOrder = SortCriteria.SortOrder,
                ReverseSortOrder = SortCriteria.ReverseSortOrder,

                Skip = range == null ? 0 : range.Skip,
                Take = range == null ? null : range.Take,
            };
        }
    }
}