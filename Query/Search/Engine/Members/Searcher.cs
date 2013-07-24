using System;
using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using com.browseengine.bobo.facets.impl;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneFilter = org.apache.lucene.search.Filter;
using LinkMe.Query.Members;

namespace LinkMe.Query.Search.Engine.Members
{
    public class Searcher
    {
        private static readonly java.util.LinkedList FacetHandlers;
        private readonly BoboBrowser _browser;

        static Searcher()
        {
            FacetHandlers = new java.util.LinkedList();
            FacetHandlers.add(new SimpleFacetHandler(FieldName.CandidateStatus));
            FacetHandlers.add(new CompactMultiValueFacetHandler(FieldName.Industries));
            FacetHandlers.add(new CompactMultiValueFacetHandler(FieldName.DesiredJobTypes));
        }

        public Searcher(IndexReader indexReader)
        {
            var reader = BoboIndexReader.getInstance(indexReader, FacetHandlers);
            _browser = new BoboBrowser(reader);
            _browser.setSimilarity(CreateSimilarity());
        }

        public MemberSearchResults Search(LuceneQuery query, LuceneFilter filter, IEnumerable<BrowseSelection> selections, SortField[] sorts, int skip, int take, bool includeFacets)
        {
            var request = CreateRequest(query, filter, selections, sorts, skip, take, includeFacets);
            var result = _browser.browse(request);
            return GetSearchResults(result, skip, take);
        }

        private static BrowseRequest CreateRequest(LuceneQuery query, LuceneFilter filter, IEnumerable<BrowseSelection> selections, SortField[] sorts, int skip, int take, bool includeFacets)
        {
            var request = new BrowseRequest();
            request.setQuery(query);
            request.setFilter(filter);

            if (selections != null)
            {
                foreach (var selection in selections)
                    request.addSelection(selection);
            }

            if (sorts != null)
                request.setSort(sorts);

            request.setOffset(0);
            request.setCount(skip + take);

            if (includeFacets)
            {
                request.setFacetSpec(FieldName.CandidateStatus, CreateFacetSpec());
                request.setFacetSpec(FieldName.Industries, CreateFacetSpec());
                request.setFacetSpec(FieldName.DesiredJobTypes, CreateFacetSpec());
            }

            return request;
        }

        private MemberSearchResults GetSearchResults(BrowseResult result, int skip, int take)
        {
            // TotalMatches

            var searchResults = new MemberSearchResults { TotalMatches = result.getNumHits() };

            // MemberIds

            var hits = result.getHits();
            searchResults.MemberIds = hits
                .Skip(skip)
                .Take(take)
                .Select(hit => new Guid(_browser.doc(hit.getDocid()).get(SearchFieldName.Id)))
                .ToArray();

            // Facets

            var facetMap = result.getFacetMap();
            if (facetMap != null)
            {
                searchResults.CandidateStatusHits = GetFieldHits(facetMap, FieldName.CandidateStatus, v => v.DecodeToCandidateStatus());
                searchResults.IndustryHits = GetFieldHits(facetMap, FieldName.Industries, v => new Guid(v));
                searchResults.DesiredJobTypeHits = GetFieldHits(facetMap, FieldName.DesiredJobTypes, v => JobTypesFieldHandler.Decode(v));
            }

            return searchResults;
        }

        private static Similarity CreateSimilarity()
        {
            var similarity = new SweetSpotSimilarity();
            similarity.setLengthNormFactors(FieldName.Content, 200, 1000, 0.5f, false);
            return similarity;
        }

        private static FacetSpec CreateFacetSpec()
        {
            var facetSpec = new FacetSpec();
            facetSpec.setExpandSelection(true);
            facetSpec.setOrderBy(FacetSpec.FacetSortSpec.OrderHitsDesc);
            return facetSpec;
        }

        private static IList<KeyValuePair<T, int>> GetFieldHits<T>(java.util.Map facetMap, string fieldName, Func<string, T> decode)
        {
            var facetAccessor = (FacetAccessible)facetMap.get(fieldName);
            if (facetAccessor == null)
                return null;

            var hits = new List<KeyValuePair<T, int>>();
            var facets = facetAccessor.getFacets();

            for (var iter = facets.iterator(); iter.hasNext(); )
            {
                var facet = (BrowseFacet)iter.next();
                var valueString = facet.getValue();
                var value = decode(valueString);
                var hitCount = facet.getFacetValueHitCount();
                hits.Add(new KeyValuePair<T, int>(value, hitCount));
            }

            return hits;
        }
    }
}
