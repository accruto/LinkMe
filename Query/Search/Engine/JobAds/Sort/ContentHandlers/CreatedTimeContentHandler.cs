using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers
{
    internal class CreatedTimeContentHandler
        : TimestampFieldHandler, IContentHandler
    {
        public CreatedTimeContentHandler(IBooster booster)
            : base(FieldName.CreatedTime, TimeGranularity.Hour, booster)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSortContent content)
        {
            var time = content.LastRefreshTime == null
                ? content.JobAd.CreatedTime
                : content.LastRefreshTime.Value < content.JobAd.CreatedTime
                    ? content.JobAd.CreatedTime
                    : content.LastRefreshTime.Value;

            AddContent(document, time);
        }

        LuceneSort IContentHandler.GetSort(JobAdSortQuery query)
        {
            // Reverse the natural order, i.e. newest at the top.

            return GetSort(!query.ReverseSortOrder);
        }
    }
}
