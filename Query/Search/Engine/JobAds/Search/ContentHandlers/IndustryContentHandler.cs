using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class IndustryContentHandler
        : IndustryFieldHandler, IContentHandler
    {
        public IndustryContentHandler(IBooster booster, IIndustriesQuery industriesQuery)
            : base(FieldName.Industries, booster, industriesQuery)
        {
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            AddContent(document, content.JobAd.Description.Industries == null || content.JobAd.Description.Industries.Count == 0 ? null : content.JobAd.Description.Industries.Select(industry => industry.Id));
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            return null;
        }

        LuceneSort IContentHandler.GetSort(JobAdSearchQuery searchQuery)
        {
            return null;
        }

        BrowseSelection IContentHandler.GetSelection(JobAdSearchQuery searchQuery)
        {
            return GetSelection(searchQuery.IndustryIds);
        }
    }
}