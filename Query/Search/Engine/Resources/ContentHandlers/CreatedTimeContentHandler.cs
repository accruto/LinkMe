using com.browseengine.bobo.api;
using LinkMe.Query.Resources;
using org.apache.lucene.document;
using org.apache.lucene.search;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.Resources.ContentHandlers
{
    internal class CreatedTimeContentHandler
        : TimestampFieldHandler, IContentHandler
    {
        public CreatedTimeContentHandler(IBooster booster)
            : base(FieldName.CreatedTime, TimeGranularity.Day, booster)
        {
        }

        void IContentHandler.AddContent(Document document, ResourceContent content)
        {
            AddContent(document, content.Resource.CreatedTime);
        }

        LuceneFilter IContentHandler.GetFilter(ResourceSearchQuery searchQuery)
        {
            return null;
        }

        Sort IContentHandler.GetSort(ResourceSearchQuery searchQuery)
        {
            // Reverse the natural order, i.e. newest at the top.

            return GetSort(!searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(ResourceSearchQuery searchQuery)
        {
            return null;
        }
    }
}
