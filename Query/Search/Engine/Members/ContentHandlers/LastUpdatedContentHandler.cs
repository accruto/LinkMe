using System;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Query.Members;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Members.ContentHandlers
{
    internal class LastUpdatedContentHandler
        : TimestampFieldHandler, IContentHandler
    {
        public LastUpdatedContentHandler(IBooster booster)
            : base(FieldName.LastUpdatedDay, TimeGranularity.Day, booster)
        {
        }

        void IContentHandler.AddContent(Document document, MemberContent content)
        {
            var lastUpdatedTime = new[]
            {
                content.Member.LastUpdatedTime,
                content.Candidate.LastUpdatedTime,
                content.Resume == null ? DateTime.MinValue : content.Resume.LastUpdatedTime,
            }.Max();

            AddContent(document, lastUpdatedTime);
        }

        LuceneFilter IContentHandler.GetFilter(MemberSearchQuery searchQuery)
        {
            if (searchQuery.Recency == null)
                return null;

            var modifiedSince = DateTime.Now - searchQuery.Recency.Value;
            return GetFilter(modifiedSince);
        }

        Sort IContentHandler.GetSort(MemberSearchQuery searchQuery)
        {
            // Reverse the natural order, i.e. newest at the top.

            return GetSort(!searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(MemberSearchQuery searchQuery)
        {
            return null;
        }
    }
}
