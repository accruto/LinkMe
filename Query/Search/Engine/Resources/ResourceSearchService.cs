using System;
using System.Diagnostics;
using System.ServiceModel;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Resources;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using org.apache.solr.common;
using Directory=org.apache.lucene.store.Directory;

namespace LinkMe.Query.Search.Engine.Resources
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class ResourceSearchService
        : SearchService<ResourceContent, object>, IResourceSearchService
    {
        private static readonly EventSource EventSource = new EventSource<ResourceSearchService>();

        private const string DefaultIndexFolder = @"C:\LinkMe\Search\ResourceIndex";
        private readonly Indexer _indexer;

        private readonly IResourcesQuery _resourcesQuery;
        private readonly IFaqsQuery _faqsQuery;

        public ResourceSearchService(ResourceLoader resourceLoader, IResourceSearchBooster booster, IResourceSearchEngineQuery searchEngineQuery, IResourcesQuery resourcesQuery, IFaqsQuery faqsQuery)
            : base(EventSource, searchEngineQuery, DefaultIndexFolder)
        {
            _resourcesQuery = resourcesQuery;
            _faqsQuery = faqsQuery;

            var analyzerFactory = new AnalyzerFactory(resourceLoader);
            _indexer = new Indexer(analyzerFactory.CreateContentAnalyzer(), analyzerFactory.CreateQueryAnalyzer(), booster, resourcesQuery, faqsQuery);
        }

        #region Implementation of IResourceSearchService

        ResourceSearchResults IResourceSearchService.Search(ResourceSearchQuery searchQuery, bool includeFacets)
        {
            const string method = "Search";

            try
            {
                #region Log
                Stopwatch searchTime = null;
                if (EventSource.IsEnabled(Event.Trace))
                    searchTime = Stopwatch.StartNew();
                #endregion

                var query = _indexer.GetQuery(searchQuery);
                var filter = _indexer.GetFilter(searchQuery);
                var selections = _indexer.GetSelections(searchQuery);
                var sort = _indexer.GetSort(searchQuery);

                #region Log
                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Executing query.", Event.Arg("query", (query != null) ? query.toString() : string.Empty));
                #endregion

                var reader = GetReader();
                var searcher = new Searcher(reader);
                var sorts = (sort != null) ? sort.getSort() : null;
                var searchResults = searcher.Search(query, filter, selections, sorts, searchQuery.Skip, searchQuery.Take ?? reader.maxDoc(), includeFacets);

                #region Log
                if (searchTime != null)
                {
                    searchTime.Stop();
                    EventSource.Raise(Event.Trace, method, "Query execution complete.",
                                       Event.Arg("query", (query != null) ? query.toString() : string.Empty),
                                       Event.Arg("total hits", searchResults.TotalMatches),
                                       Event.Arg("result count", searchResults.ResourceIds.Count),
                                       Event.Arg("searchTime", searchTime.ElapsedMilliseconds));
                }
                #endregion

                return searchResults;
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, "Unexpected exception.", e);
                #endregion
                throw;
            }
        }

        void IResourceSearchService.UpdateItem(Guid itemId)
        {
            UpdateContent(itemId);
        }

        void IResourceSearchService.Clear()
        {
            ClearIndex();
        }

        #endregion

        protected override void OnIndexInitialised(Directory directory)
        {
        }

        protected override ResourceContent GetContent(Guid id, object cache)
        {
            // Look for the resource item.

            var article = _resourcesQuery.GetArticle(id);
            if (article != null)
                return new ResourceContent { Resource = article, Views = _resourcesQuery.GetViewingCount(id) };

            var video = _resourcesQuery.GetVideo(id);
            if (video != null)
                return new ResourceContent { Resource = video, Views = _resourcesQuery.GetViewingCount(id) };

            var qna = _resourcesQuery.GetQnA(id);
            if (qna != null)
                return new ResourceContent { Resource = qna, Views = _resourcesQuery.GetViewingCount(id) };

            var faq = _faqsQuery.GetFaq(id);
            if (faq != null)
                return new ResourceContent { Resource = faq, Views = 0 };

            return null;
        }

        protected override void IndexContent(IndexWriter indexWriter, ResourceContent content, bool isNew)
        {
            _indexer.IndexContent(indexWriter, content, false);
        }

        protected override void UnindexContent(IndexWriter indexWriter, Guid id)
        {
        }

        protected override IndexWriter CreateIndexWriter(Directory directory)
        {
            var indexWriter = new IndexWriter(directory, null, IndexWriter.MaxFieldLength.UNLIMITED);
            indexWriter.setSimilarity(CreateSimilarity());
            return indexWriter;
        }

        private static Similarity CreateSimilarity()
        {
            var similarity = new SweetSpotSimilarity();
            similarity.setLengthNormFactors(FieldName.Content, 200, 1000, 0.5f, false);
            return similarity;
        }
    }
}
