using System;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Query.JobAds;
using org.apache.lucene.index;
using org.apache.lucene.search;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    public class Sorter
    {
        private readonly BoboBrowser _browser;

        public Sorter(IndexReader indexReader)
        {
            var reader = BoboIndexReader.getInstance(indexReader);
            _browser = new BoboBrowser(reader);
        }

        public JobAdSearchResults Sort(Filter filter, SortField[] sorts, int skip, int take)
        {
            var request = CreateRequest(filter, sorts, skip, take);
            var result = _browser.browse(request);
            return GetSortResults(result, skip, take);
        }

        private static BrowseRequest CreateRequest(Filter filter, SortField[] sorts, int skip, int take)
        {
            var request = new BrowseRequest();
            request.setFilter(filter);

            if (sorts != null)
                request.setSort(sorts);

            request.setOffset(0);
            request.setCount(skip + take);

            return request;
        }

        private JobAdSearchResults GetSortResults(BrowseResult result, int skip, int take)
        {
            // TotalMatches

            var sortResults = new JobAdSearchResults { TotalMatches = result.getNumHits() };

            // JobAdIds

            var hits = result.getHits();
            sortResults.JobAdIds = hits
                .Skip(skip)
                .Take(take)
                .Select(hit => new Guid(_browser.doc(hit.getDocid()).get(SearchFieldName.Id)))
                .ToArray();

            return sortResults;
        }
    }
}
