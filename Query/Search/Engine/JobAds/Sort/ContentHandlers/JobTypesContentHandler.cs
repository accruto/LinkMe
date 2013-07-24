using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.JobAds.Sort.ContentHandlers
{
    internal class JobTypesContentHandler
        : JobTypesFieldHandler, IContentHandler
    {
        public JobTypesContentHandler(IBooster booster)
            : base(FieldName.JobTypes, FieldName.JobTypesSort, booster)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSortContent content)
        {
            AddContent(document, content.JobAd.Description.JobTypes);
        }

        LuceneSort IContentHandler.GetSort(JobAdSortQuery query)
        {
            return GetSort(query.ReverseSortOrder);
        }
    }
}