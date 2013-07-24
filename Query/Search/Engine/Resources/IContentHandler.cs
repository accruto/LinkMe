using com.browseengine.bobo.api;
using LinkMe.Query.Resources;
using org.apache.lucene.document;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.Resources
{
    internal interface IContentHandler
    {
        void AddContent(Document document, ResourceContent content);
        LuceneFilter GetFilter(ResourceSearchQuery query);
        LuceneSort GetSort(ResourceSearchQuery query);
        BrowseSelection GetSelection(ResourceSearchQuery query);
    }
}