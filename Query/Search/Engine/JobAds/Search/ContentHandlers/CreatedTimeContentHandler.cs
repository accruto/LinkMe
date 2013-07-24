using System;
using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class CreatedTimeContentHandler
        : TimestampFieldHandler, IContentHandler
    {
        public CreatedTimeContentHandler(IJobAdSearchBooster booster)
            : base(FieldName.CreatedTime, TimeGranularity.Hour, booster)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            var time = content.LastRefreshTime == null
                ? content.JobAd.CreatedTime
                : content.LastRefreshTime.Value < content.JobAd.CreatedTime
                    ? content.JobAd.CreatedTime
                    : content.LastRefreshTime.Value;

            AddContent(document, time);
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            if (searchQuery.Recency == null)
                return null;

            var modifiedSince = DateTime.Now - searchQuery.Recency.Value;
            return GetFilter(modifiedSince);
        }

        LuceneSort IContentHandler.GetSort(JobAdSearchQuery searchQuery)
        {
            // Reverse the natural order, i.e. newest at the top.

            return GetSort(!searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(JobAdSearchQuery searchQuery)
        {
            return null;
        }
    }
}
