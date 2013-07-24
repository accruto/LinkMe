using com.browseengine.bobo.api;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class LocationContentHandler
        : LocationFieldHandler, IContentHandler
    {
        public LocationContentHandler(IBooster booster, ILocationQuery locationQuery)
            : base(FieldName.Location, null, booster, locationQuery)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            AddContent(document, content.JobAd.Description.Location, null);
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            return GetFilter(searchQuery.Location, searchQuery.Relocations, searchQuery.Distance, false, false);
        }

        LuceneSort IContentHandler.GetSort(JobAdSearchQuery searchQuery)
        {
            return searchQuery.Location == null ? null : GetSort(searchQuery.Location, searchQuery.ReverseSortOrder);
        }

        BrowseSelection IContentHandler.GetSelection(JobAdSearchQuery searchQuery)
        {
            return null;
        }
    }
}
