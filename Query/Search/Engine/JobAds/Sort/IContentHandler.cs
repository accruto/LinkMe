using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    internal interface IContentHandler
    {
        void AddContent(Document document, JobAdSortContent content);
        LuceneSort GetSort(JobAdSortQuery query);
    }
}