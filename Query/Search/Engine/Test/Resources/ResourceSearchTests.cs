using LinkMe.Domain.Resources;
using LinkMe.Domain.Resources.Commands;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Engine.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.store;
using FieldName=LinkMe.Query.Search.Engine.JobAds.Search.FieldName;

namespace LinkMe.Query.Search.Engine.Test.Resources
{
    [TestClass]
    public abstract class ResourceSearchTests
        : TestClass
    {
        protected IndexWriter _indexWriter;
        protected Indexer _indexer;
        protected IResourcesQuery _resourcesQuery;
        protected IResourcesCommand _resourcesCommand;
        protected IFaqsQuery _faqsQuery;

        [TestInitialize]
        public void ResourceSearchTestsInitialize()
        {
            _resourcesQuery = Resolve<IResourcesQuery>();
            _resourcesCommand = Resolve<IResourcesCommand>();
            _faqsQuery = Resolve<IFaqsQuery>();
            _indexer = new Indexer(new SimpleAnalyzer(), new SimpleAnalyzer(), new ResourceSearchBooster(), _resourcesQuery, _faqsQuery);

            _indexWriter = new IndexWriter(new RAMDirectory(), null, IndexWriter.MaxFieldLength.UNLIMITED);
            var similarity = new SweetSpotSimilarity();
            similarity.setLengthNormFactors(FieldName.Content, 200, 1000, 0.5f, false);
            _indexWriter.setSimilarity(similarity);
        }

        [TestCleanup]
        public void ResourceSearchTestsCleanup()
        {
            _indexWriter.close();
        }

        protected void IndexItem(Resource resource)
        {
            IndexItem(resource, true);
        }

        protected void IndexItem(Resource resource, bool isNew)
        {
            var views = _resourcesQuery.GetViewingCount(resource.Id);
            _indexer.IndexContent(_indexWriter, new ResourceContent { Resource = resource, Views = views }, isNew);
        }

        protected ResourceSearchResults Search(ResourceSearchQuery resourceQuery)
        {
            return Search(resourceQuery, 0);
        }

        protected ResourceSearchResults Search(ResourceSearchQuery resourceQuery, float minScore)
        {
            var reader = _indexWriter.getReader();
            var searcher = new Searcher(reader);
            var query = _indexer.GetQuery(resourceQuery);
            var filter = _indexer.GetFilter(resourceQuery);
            var selections = _indexer.GetSelections(resourceQuery);
            var sort = _indexer.GetSort(resourceQuery);
            var searchResults = searcher.Search(query, filter, selections, sort == null ? null : sort.getSort(), resourceQuery.Skip, resourceQuery.Take ?? reader.maxDoc(), true);
            return searchResults;
        }
    }
}
