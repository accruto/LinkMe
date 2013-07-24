using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Query.Search.Engine.JobAds.Search.ContentHandlers;
using com.browseengine.bobo.api;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneSort = org.apache.lucene.search.Sort;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    public class Indexer
    {
        private readonly Analyzer _contentAnalyzer;
        private readonly QueryBuilder _queryBuilder;
        private readonly ContentHandler _contentHandler;
        private readonly IContentHandler _locationHandler;
        private readonly IContentHandler _salaryHandler;
        private readonly IContentHandler _industryHandler;
        private readonly IContentHandler _jobTypesHandler;
        private readonly IContentHandler _createdTimeHandler;
        private readonly IContentHandler _communityHandler;
        private readonly IContentHandler _featuredHandler;
        private readonly IContentHandler[] _handlers;

        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery;

        public Indexer(Analyzer contentAnalyzer, Analyzer queryAnalyzer, IJobAdSearchBooster booster, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IJobAdFlagListsQuery jobAdFlagListsQuery)
        {
            _contentAnalyzer = contentAnalyzer;
            _queryBuilder = new QueryBuilder(queryAnalyzer, booster);

            _contentHandler = new ContentHandler(booster);
            _locationHandler = new LocationContentHandler(booster, locationQuery);
            _industryHandler = new IndustryContentHandler(booster, industriesQuery);
            _salaryHandler = new SalaryContentHandler(booster);
            _jobTypesHandler = new JobTypesContentHandler(booster);
            _createdTimeHandler = new CreatedTimeContentHandler(booster);
            _communityHandler = new CommunityContentHandler(booster);
            _featuredHandler = new FeaturedContentHandler(booster);

            _handlers = new[]
            {
                _locationHandler,
                _salaryHandler,
                _industryHandler,
                _jobTypesHandler,
                _createdTimeHandler,
                _communityHandler,
                _featuredHandler
            };

            _jobAdFlagListsQuery = jobAdFlagListsQuery;
        }

        public void IndexContent(IndexWriter indexWriter, JobAdSearchContent content, bool isNew)
        {
            var document = new Document();

            // ID

            var id = content.JobAd.Id.ToFieldValue();
            var idField = new Field(SearchFieldName.Id, id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
            idField.setOmitTermFreqAndPositions(true);
            document.add(idField);

            // Handlers.

            _contentHandler.AddContent(document, content);
            _locationHandler.AddContent(document, content);
            _salaryHandler.AddContent(document, content);
            _industryHandler.AddContent(document, content);
            _jobTypesHandler.AddContent(document, content);
            _createdTimeHandler.AddContent(document, content);
            _communityHandler.AddContent(document, content);
            _featuredHandler.AddContent(document, content);

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

        public LuceneQuery GetQuery(JobAdSearchQuery searchQuery)
        {
            return _queryBuilder.GetQuery(searchQuery);
        }

        public LuceneFilter GetFilter(JobAdSearchQuery query, IEnumerable<Guid> includeJobAdIds, IEnumerable<Guid> excludeJobAdIds)
        {
            var filters = (from h in _handlers
                           let f = h.GetFilter(query)
                           where f != null
                           select f).ToList();

            if (includeJobAdIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, false, includeJobAdIds.Select(id => id.ToFieldValue())));

            if (excludeJobAdIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, true, excludeJobAdIds.Select(id => id.ToFieldValue())));

            return filters.Count == 0
                ? null
                : new ChainedFilter(filters.ToArray(), ChainedFilter.AND);
        }

        public IEnumerable<BrowseSelection> GetSelections(JobAdSearchQuery query)
        {
            return from h in _handlers
                   let s = h.GetSelection(query)
                   where s != null
                   select s;
        }

        public LuceneSort GetSort(IMember member, JobAdSearchQuery searchQuery)
        {
            switch (searchQuery.SortOrder)
            {
                case JobAdSortOrder.Relevance:

                    // Default sort.

                    return null;

                case JobAdSortOrder.CreatedTime:
                    return _createdTimeHandler.GetSort(searchQuery);

                case JobAdSortOrder.JobType:
                    return _jobTypesHandler.GetSort(searchQuery);

                case JobAdSortOrder.Distance:
                    return _locationHandler.GetSort(searchQuery);

                case JobAdSortOrder.Salary:
                    return _salaryHandler.GetSort(searchQuery);

                case JobAdSortOrder.Flagged:
                    return new LuceneSort(new[]
                    {
                        new BoboCustomSortField(SearchFieldName.Id, !searchQuery.ReverseSortOrder, new SpecialsComparatorSource(false, _jobAdFlagListsQuery.GetFlaggedJobAdIds(member). Select(id => id.ToFieldValue()), SearchFieldName.Id)), SortField.FIELD_SCORE
                    });

                default:
                    return null;
            }
        }
    }
}
