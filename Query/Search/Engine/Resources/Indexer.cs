using System;
using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Resources.Queries;
using LinkMe.Query.Resources;
using LinkMe.Query.Search.Engine.Resources.ContentHandlers;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.Resources
{
    public class Indexer
    {
        private readonly Analyzer _contentAnalyzer;
        private readonly QueryBuilder _queryBuilder;
        private readonly ContentHandler _contentHandler;
        private readonly IContentHandler _createdTimeHandler;
        private readonly IContentHandler _subcategoryHandler;
        private readonly IContentHandler _itemTypeHandler;
        private readonly IContentHandler _popularityHandler;
        private readonly IContentHandler[] _handlers;

        public Indexer(Analyzer contentAnalyzer, Analyzer queryAnalyzer, IResourceSearchBooster booster, IResourcesQuery resourcesQuery, IFaqsQuery faqsQuery)
        {
            _contentAnalyzer = contentAnalyzer;
            _queryBuilder = new QueryBuilder(queryAnalyzer);

            _contentHandler = new ContentHandler(booster);
            _createdTimeHandler = new CreatedTimeContentHandler(booster);
            _subcategoryHandler = new SubcategoryContentHandler(booster, resourcesQuery, faqsQuery);
            _itemTypeHandler = new ItemTypeContentHandler(booster);
            _popularityHandler = new PopularityContentHandler(booster);

            _handlers = new[]
            {
                _itemTypeHandler,
                _subcategoryHandler,
                _createdTimeHandler,
                _popularityHandler
            };
        }

        public void IndexContent(IndexWriter indexWriter, ResourceContent content, bool isNew)
        {
            var document = new Document();

            // ID

            var id = content.Resource.Id.ToFieldValue();
            var idField = new Field(SearchFieldName.Id, id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
            idField.setOmitTermFreqAndPositions(true);
            document.add(idField);

            // Handlers.

            _contentHandler.AddContent(document, content);
            _itemTypeHandler.AddContent(document, content);
            _subcategoryHandler.AddContent(document, content);
            _createdTimeHandler.AddContent(document, content);
            _popularityHandler.AddContent(document, content);

            // Save the document.

            if (isNew)
                indexWriter.addDocument(document, _contentAnalyzer);
            else
                indexWriter.updateDocument(new Term(SearchFieldName.Id, id), document, _contentAnalyzer);
        }

        public void DeleteContent(IndexWriter indexWriter, Guid id)
        {
            indexWriter.deleteDocuments(new Term(SearchFieldName.Id, id.ToFieldValue()));
        }

        public LuceneQuery GetQuery(ResourceSearchQuery query)
        {
            return _queryBuilder.GetQuery(query);
        }

        public LuceneFilter GetFilter(ResourceSearchQuery query)
        {
            var filters = (from h in _handlers
                           let f = h.GetFilter(query)
                           where f != null
                           select f).ToList();

            return filters.Count == 0
                ? null
                : new ChainedFilter(filters.ToArray(), ChainedFilter.AND);
        }

        public LuceneSort GetSort(ResourceSearchQuery query)
        {
            switch (query.SortOrder)
            {
                case ResourceSortOrder.Relevance:

                    // Default sort.

                    return null;

                case ResourceSortOrder.CreatedTime:
                    return _createdTimeHandler.GetSort(query);

                case ResourceSortOrder.Popularity:
                    return _popularityHandler.GetSort(query);

                default:
                    return null;
            }
        }
        public IEnumerable<BrowseSelection> GetSelections(ResourceSearchQuery query)
        {
            return from h in _handlers
                   let s = h.GetSelection(query)
                   where s != null
                   select s;
        }
    }
}
