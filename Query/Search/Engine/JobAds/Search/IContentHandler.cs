using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    internal interface IContentHandler
    {
        void AddContent(Document document, JobAdSearchContent content);
        LuceneFilter GetFilter(JobAdSearchQuery searchQuery);
        LuceneSort GetSort(JobAdSearchQuery searchQuery);
        BrowseSelection GetSelection(JobAdSearchQuery searchQuery);
    }
}