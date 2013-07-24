using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class JobTypesContentHandler
        : JobTypesFieldHandler, IContentHandler
    {
        public JobTypesContentHandler(IBooster booster)
            : base(FieldName.JobTypes, FieldName.JobTypesSort, booster)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            AddContent(document, content.JobAd.Description.JobTypes);
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            return null;
        }

        LuceneSort IContentHandler.GetSort(JobAdSearchQuery searchQuery)
        {
            return GetSort(searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(JobAdSearchQuery searchQuery)
        {
            return GetSelection(searchQuery.JobTypes);
        }
    }
}