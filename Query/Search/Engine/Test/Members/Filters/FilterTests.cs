using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.store;
using Searcher = LinkMe.Query.Search.Engine.Members.Searcher;

namespace LinkMe.Query.Search.Engine.Test.Members.Filters
{
    [TestClass]
    public abstract class FilterTests
        : TestClass
    {
        private IndexWriter _indexWriter;
        private readonly Indexer _indexer = new Indexer(new MemberSearchBooster(), Resolve<ILocationQuery>(), Resolve<IIndustriesQuery>(), null);

        [TestInitialize]
        public void FilterTestsInitialize()
        {
            _indexWriter = new IndexWriter(new RAMDirectory(), new SimpleAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        [TestCleanup]
        public void FilterTestsCleanup()
        {
            _indexWriter.close();
        }

        protected void IndexContent(MemberContent content)
        {
            _indexer.IndexContent(_indexWriter, content, true);
        }

        protected MemberSearchResults Search(MemberSearchQuery memberQuery, int skip, int take)
        {
            var searcher = new Searcher(_indexWriter.getReader());
            var query = _indexer.GetQuery(memberQuery);
            var filter = _indexer.GetFilter(memberQuery, null, null);
            var selections = _indexer.GetSelections(memberQuery);
            var sort = _indexer.GetSort(null, memberQuery);
            var searchResults = searcher.Search(query, filter, selections, sort == null ? null : sort.getSort(), skip, take, true);
            return searchResults;
        }
    }
}