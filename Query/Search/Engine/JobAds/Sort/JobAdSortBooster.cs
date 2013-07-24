using org.apache.lucene.document;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    public interface IJobAdSortBooster
        : IBooster
    {
    }

    public class JobAdSortBooster
        : IJobAdSortBooster
    {
        void IBooster.SetBoost(AbstractField field)
        {
        }
    }
}
