using LinkMe.Domain.Roles.JobAds;
using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    public class FeaturedContentHandler
        : IContentHandler
    {
        private readonly IJobAdSearchBooster _booster;

        public FeaturedContentHandler(IJobAdSearchBooster booster)
        {
            _booster = booster;
        }

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            if (content.JobAd.FeatureBoost == JobAdFeatureBoost.None)
                return;

            var field = new NumericField(FieldName.Featured).setIntValue(content.JobAd.FeatureBoost == JobAdFeatureBoost.Low ? 1 : 2);
            _booster.SetBoost(field);
            document.add(field);

            // Also boost the document as well.

            _booster.SetFeatureBoost(document, content.JobAd.FeatureBoost);
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
            return null;
        }
    }
}
