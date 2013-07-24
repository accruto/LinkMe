using LinkMe.Query.Search.Engine.JobAds.Search;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    public class JobAdSortContent
        : JobAdContent
    {
        public override bool IsSearchable
        {
            get { return true; }
        }
    }
}