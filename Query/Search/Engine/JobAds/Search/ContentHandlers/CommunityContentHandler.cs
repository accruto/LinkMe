using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;

namespace LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers
{
    internal class CommunityContentHandler
        : IContentHandler
    {
        private readonly IBooster _booster;

        public CommunityContentHandler(IBooster booster)
        {
            _booster = booster;
        }

        private const string HasCommunityId = "1";
        private const string HasNoCommunityId = "0";

        void IContentHandler.AddContent(Document document, JobAdSearchContent content)
        {
            var communityId = content.Employer.CommunityId;
            var hasCommunityIdIndexValue = HasNoCommunityId;

            if (communityId != null)
            {
                hasCommunityIdIndexValue = HasCommunityId;
                var field = new Field(FieldName.Community, communityId.Value.ToFieldValue(), Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
                field.setOmitTermFreqAndPositions(true);
                _booster.SetBoost(field);
                document.add(field);
            }

            var hasCommunityField = new Field(FieldName.HasCommunity, hasCommunityIdIndexValue, Field.Store.NO, Field.Index.NOT_ANALYZED_NO_NORMS);
            _booster.SetBoost(hasCommunityField);
            document.add(hasCommunityField);
        }

        LuceneFilter IContentHandler.GetFilter(JobAdSearchQuery searchQuery)
        {
            // If the community is not set then only include non-community jobs.

            if (!searchQuery.CommunityId.HasValue)
            {
                var filter = new TermsFilter();
                filter.addTerm(new Term(FieldName.HasCommunity, HasNoCommunityId));
                return filter;
            }

            // The community is set so it needs to be included.

            var specificCommunityFilter = new TermsFilter();
            specificCommunityFilter.addTerm(new Term(FieldName.Community, searchQuery.CommunityId.Value.ToFieldValue()));

            // If it is only the community that must be returned then return it.

            if (searchQuery.CommunityOnly.HasValue && searchQuery.CommunityOnly.Value)
                return specificCommunityFilter;

            // Need to include those that match the community, or have no community set, i.e. all other community jobs should not be retutned.

            var noCommunityFilter = new TermsFilter();
            noCommunityFilter.addTerm(new Term(FieldName.HasCommunity, HasNoCommunityId));

            return new ChainedFilter(new Filter[] { specificCommunityFilter, noCommunityFilter }, ChainedFilter.OR);
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
