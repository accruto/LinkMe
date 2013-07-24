using org.apache.lucene.document;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.Members
{
    public interface IMemberSearchBooster
        : IBooster
    {
        LuceneQuery GetRecencyBoostingQuery(LuceneQuery query);
        LuceneQuery GetJobTitleBoostingQuery(LuceneQuery query, LuceneQuery jobTitleQuery);
    }

    public class MemberSearchBooster
        : Booster, IMemberSearchBooster
    {
        public MemberSearchBooster()
        {
            // Defaults.

            RecencyAlpha = 0.01;
            RecencyHalfDecay = 1000;
            JobTitleBoost = 3.0f;
        }

        void IBooster.SetBoost(AbstractField field)
        {
        }

        LuceneQuery IMemberSearchBooster.GetRecencyBoostingQuery(LuceneQuery query)
        {
            return GetRecencyBoostingQuery(query, FieldName.LastUpdatedDay, TimeGranularity.Day, RecencyAlpha, RecencyHalfDecay);
        }

        LuceneQuery IMemberSearchBooster.GetJobTitleBoostingQuery(LuceneQuery query, LuceneQuery jobTitleQuery)
        {
            return GetBoostingQuery(query, jobTitleQuery, JobTitleBoost);
        }

        public double? RecencyAlpha { get; set; }
        public double? RecencyHalfDecay { get; set; }
        public float? JobTitleBoost { get; set; }
    }
}
