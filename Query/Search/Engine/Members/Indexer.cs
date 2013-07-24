using System;
using System.Collections.Generic;
using System.Linq;
using com.browseengine.bobo.api;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Engine.Members.ContentHandlers;
using org.apache.lucene.analysis;
using org.apache.lucene.document;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using LuceneQuery = org.apache.lucene.search.Query;
using LuceneFilter = org.apache.lucene.search.Filter;
using LuceneSort = org.apache.lucene.search.Sort;

namespace LinkMe.Query.Search.Engine.Members
{
    public class Indexer
    {
        private static readonly EventSource EventSource = new EventSource<Indexer>();
        private const string Method = "Indexing";

        private readonly Analyzer _contentAnalyzer;
        private readonly Analyzer _queryAnalyzer;
        private readonly QueryBuilder _queryBuilder;

        private readonly ContentHandler _contentHandler;
        private readonly IContentHandler _desiredJobTypesHandler;
        private readonly IContentHandler _candidateStatusHandler;
        private readonly IContentHandler _ethnicStatusHandler;
        private readonly IContentHandler _visaStatusHandler;
        private readonly IContentHandler _industryHandler;
        private readonly IContentHandler _communityHandler;
        private readonly IContentHandler _salaryHandler;
        private readonly IContentHandler _lastUpdatedHandler;
        private readonly IContentHandler _nameHandler;
        private readonly IContentHandler _locationHandler;
        private readonly IContentHandler _hasResumeHandler;
        private readonly IContentHandler _isActivatedHandler;
        private readonly IContentHandler _isContactableHandler;
        private readonly IContentHandler[] _handlers;

        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery;

        private Indexer(Analyzer contentAnalyzer, Analyzer queryAnalyzer, IMemberSearchBooster booster, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, ICandidateFlagListsQuery candidateFlagListsQuery)
        {
            _contentAnalyzer = contentAnalyzer;
            _queryAnalyzer = queryAnalyzer;
            _queryBuilder = new QueryBuilder(_queryAnalyzer, booster);

            _contentHandler = new ContentHandler();
            _locationHandler = new LocationContentHandler(booster, locationQuery);
            _industryHandler = new IndustryContentHandler(booster, industriesQuery);
            _nameHandler = new NameContentHandler(contentAnalyzer, booster);
            _desiredJobTypesHandler = new DesiredJobTypesContentHandler(booster);
            _candidateStatusHandler = new CandidateStatusContentHandler(booster);
            _ethnicStatusHandler = new EthnicStatusContentHandler(booster);
            _visaStatusHandler = new VisaStatusContentHandler(booster);
            _communityHandler = new CommunityContentHandler(booster);
            _salaryHandler = new SalaryContentHandler(booster);
            _lastUpdatedHandler = new LastUpdatedContentHandler(booster);
            _hasResumeHandler = new HasResumeContentHandler();
            _isActivatedHandler = new IsActivatedContentHandler();
            _isContactableHandler = new IsContactableContentHandler();

            _handlers = new[]
            {
                _desiredJobTypesHandler,
                _candidateStatusHandler, 
                _ethnicStatusHandler, 
                _visaStatusHandler,
                _industryHandler, 
                _communityHandler, 
                _salaryHandler,
                _lastUpdatedHandler,
                _locationHandler,
                _hasResumeHandler,
                _isActivatedHandler,
                _isContactableHandler
            };

            _candidateFlagListsQuery = candidateFlagListsQuery;
        }

        public Indexer(IMemberSearchBooster booster, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, ICandidateFlagListsQuery candidateFlagListsQuery)
            : this(new SimpleAnalyzer(), new SimpleAnalyzer(), booster, locationQuery, industriesQuery, candidateFlagListsQuery)
        {
        }

        public Indexer(IAnalyzerFactory analyzerFactory, IMemberSearchBooster booster, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, ICandidateFlagListsQuery candidateFlagListsQuery)
            : this(analyzerFactory.CreateContentAnalyzer(), analyzerFactory.CreateQueryAnalyzer(), booster, locationQuery, industriesQuery, candidateFlagListsQuery)
        {
        }

        public void IndexContent(IndexWriter indexWriter, MemberContent content, bool isNew)
        {
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, Method, "Adding Content.", Event.Arg("member", content.Member.Id));

            var document = new Document();
            var docBuilder = new BoostingDocumentBuilder(_contentAnalyzer);

            // Id.

            var id = content.Member.Id.ToFieldValue();
            var idField = new Field(SearchFieldName.Id, id, Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS);
            idField.setOmitTermFreqAndPositions(true);
            document.add(idField);

            // Handlers.

            _contentHandler.AddContent(document, docBuilder, content);
            _lastUpdatedHandler.AddContent(document, content);
            _desiredJobTypesHandler.AddContent(document, content);
            _candidateStatusHandler.AddContent(document, content);
            _ethnicStatusHandler.AddContent(document, content);
            _visaStatusHandler.AddContent(document, content);
            _industryHandler.AddContent(document, content);
            _communityHandler.AddContent(document, content);
            _salaryHandler.AddContent(document, content);
            _locationHandler.AddContent(document, content);
            _nameHandler.AddContent(document, content);
            _hasResumeHandler.AddContent(document, content);
            _isActivatedHandler.AddContent(document, content);
            _isContactableHandler.AddContent(document, content);

            // Save the document.

            docBuilder.CopyTo(document);
            if (isNew)
                indexWriter.addDocument(document, _contentAnalyzer);
            else
                indexWriter.updateDocument(new Term(SearchFieldName.Id, id), document, _contentAnalyzer);

            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, Method, string.Format("Content {0}.", isNew ? "Added" : "Updated"), Event.Arg("member", content.Member.Id), Event.Arg("lastupdate date", new[] { content.Member.LastUpdatedTime, content.Candidate.LastUpdatedTime, content.Resume == null ? DateTime.MinValue : content.Resume.LastUpdatedTime }.Max()), Event.Arg("document date", document.getValues(FieldName.LastUpdatedDay)));
        }

        public void DeleteContent(IndexWriter indexWriter, Guid id)
        {
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace,Method, "Deleting Content.", Event.Arg("member", id));

            indexWriter.deleteDocuments(new Term(SearchFieldName.Id, id.ToFieldValue()));
        }

        public LuceneQuery GetQuery(MemberSearchQuery query)
        {
            return _queryBuilder.GetQuery(query);
        }

        public LuceneFilter GetFilter(MemberSearchQuery query, IEnumerable<Guid> includeMemberIds, IEnumerable<Guid> excludeMemberIds)
        {
            var filters = (from h in _handlers
                           let f = h.GetFilter(query)
                           where f != null
                           select f).ToList();

            if (includeMemberIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, false, includeMemberIds.Select(id => id.ToFieldValue())));

            if (excludeMemberIds != null)
                filters.Add(new SpecialsFilter(SearchFieldName.Id, true, excludeMemberIds.Select(id => id.ToFieldValue())));

            return filters.Count == 0
                ? null
                : new ChainedFilter(filters.ToArray(), ChainedFilter.AND);
        }

        public LuceneSort GetSort(IEmployer employer, MemberSearchQuery query)
        {
            switch (query.SortOrder)
            {
                case MemberSortOrder.Relevance:

                    // Default sort.

                    return null;

                case MemberSortOrder.DateUpdated:
                    return _lastUpdatedHandler.GetSort(query);

                case MemberSortOrder.Salary:
                    return _salaryHandler.GetSort(query);

                case MemberSortOrder.Availability:
                    return _candidateStatusHandler.GetSort(query);

                case MemberSortOrder.Flagged:
                    return new Sort(new[] 
                    {
                        new BoboCustomSortField(SearchFieldName.Id, !query.ReverseSortOrder, new SpecialsComparatorSource(false, _candidateFlagListsQuery.GetFlaggedCandidateIds(employer).Select(id => id.ToFieldValue()), SearchFieldName.Id)),
                        SortField.FIELD_SCORE
                    });

                case MemberSortOrder.FirstName:
                    return _nameHandler.GetSort(query);

                case MemberSortOrder.Distance:
                    return _locationHandler.GetSort(query);

                default:
                    return null;
            }
        }

        public IEnumerable<BrowseSelection> GetSelections(MemberSearchQuery query)
        {
            return from h in _handlers
                   let s = h.GetSelection(query)
                   where s != null
                   select s;
        }
    }
}
