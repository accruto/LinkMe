using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.Members;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using org.apache.lucene.store;
using org.apache.solr.common;

namespace LinkMe.Query.Search.Engine.Members
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberSearchService
        : SearchService<MemberContent, object>, IMemberSearchService
    {
        private static readonly EventSource EventSource = new EventSource<MemberSearchService>();

        private const string DefaultIndexFolder = @"C:\LinkMe\Search\MemberIndex";
        private bool _includeHidden;
        private int _maxSpellings = 5;

        private readonly IMembersQuery _membersQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly IMemberActivityFiltersQuery _memberActivityFiltersQuery;
        private readonly Indexer _indexer;
        private readonly SpellCheckHandler _spellCheckHandler;

        public bool IncludeHidden
        {
            set { _includeHidden = value; }
        }

        public int MaxSpellings
        {
            set { _maxSpellings = value; }
        }

        public float ThresholdTokenFrequency
        {
            set { _spellCheckHandler.ThresholdTokenFrequency = value; }
        }

        public MemberSearchService(ResourceLoader resourceLoader, IMemberSearchBooster booster, IMemberSearchEngineQuery searchEngineQuery, IMembersQuery membersQuery, ICandidatesQuery candidatesQuery, IResumesQuery resumesQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IMemberActivityFiltersQuery memberActivityFiltersQuery, ICandidateFlagListsQuery candidateFlagListsQuery)
            : base(EventSource, searchEngineQuery, DefaultIndexFolder)
        {
            _membersQuery = membersQuery;
            _candidatesQuery = candidatesQuery;
            _resumesQuery = resumesQuery;
            _memberActivityFiltersQuery = memberActivityFiltersQuery;

            var analyzerFactory = new AnalyzerFactory(resourceLoader);
            _indexer = new Indexer(analyzerFactory, booster, locationQuery, industriesQuery, candidateFlagListsQuery);
            _spellCheckHandler = new SpellCheckHandler(analyzerFactory.CreateSpellingAnalyzer(), FieldName.Content_Exact);
        }

        #region Implementation of ISearchService

        MemberSearchResults IMemberSearchService.Search(Guid? employerId, Guid? organisationId, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetIncludeMemberIds(e, q),
                (e, q) => _memberActivityFiltersQuery.GetExcludeMemberIds(e, q));
        }

        MemberSearchResults IMemberSearchService.SearchFolder(Guid? employerId, Guid? organisationId, Guid folderId, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetFolderIncludeMemberIds(e, folderId, q),
                (e, q) => _memberActivityFiltersQuery.GetFolderExcludeMemberIds(e, folderId, q));
        }

        MemberSearchResults IMemberSearchService.SearchFlagged(Guid? employerId, Guid? organisationId, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetFlaggedIncludeMemberIds(e, q),
                (e, q) => _memberActivityFiltersQuery.GetFlaggedExcludeMemberIds(e, q));
        }

        MemberSearchResults IMemberSearchService.SearchBlockList(Guid? employerId, Guid? organisationId, Guid blockListId, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetBlockListIncludeMemberIds(e, blockListId, q),
                (e, q) => _memberActivityFiltersQuery.GetBlockListExcludeMemberIds(e, blockListId, q));
        }

        MemberSearchResults IMemberSearchService.SearchSuggested(Guid? employerId, Guid? organisationId, Guid jobAdId, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetSuggestedIncludeMemberIds(e, jobAdId, q),
                (e, q) => _memberActivityFiltersQuery.GetSuggestedExcludeMemberIds(e, jobAdId, q));
        }

        MemberSearchResults IMemberSearchService.SearchManaged(Guid? employerId, Guid? organisationId, Guid jobAdId, ApplicantStatus status, MemberSearchQuery query)
        {
            return Search(
                employerId,
                organisationId,
                query,
                (e, q) => _memberActivityFiltersQuery.GetManagedIncludeMemberIds(e, jobAdId, status, q),
                (e, q) => _memberActivityFiltersQuery.GetManagedExcludeMemberIds(e, jobAdId, status, q));
        }

        void IMemberSearchService.UpdateMember(Guid memberId)
        {
            UpdateContent(memberId);
        }

        void IMemberSearchService.IndexMember(Guid memberId)
        {
            IndexContent(memberId);
        }

        void IMemberSearchService.UnindexMember(Guid memberId)
        {
            UnindexContent(memberId);
        }

        bool IMemberSearchService.IsIndexed(Guid memberId)
        {
            return Search(
                null,
                null,
                new MemberSearchQuery(),
                (e, q) => new[] { memberId }.ToList(),
                (e, q) => null).TotalMatches > 0;
        }

        void IMemberSearchService.Clear()
        {
            ClearIndex();
        }

        SpellCheckCollation[] IMemberSearchService.GetSpellingSuggestions(string queryString, int maxCollations)
        {
            const string method = "GetSpellingSuggestions";

            try
            {
                #region Log
                Stopwatch executionTime = null;
                if (EventSource.IsEnabled(Event.Trace))
                    executionTime = Stopwatch.StartNew();
                #endregion

                var collations = _spellCheckHandler.GetSpellingSuggestions(queryString, GetReader(), _maxSpellings, maxCollations, true);

                #region Log
                if (executionTime != null)
                {
                    executionTime.Stop();
                    EventSource.Raise(Event.Trace, method, "Execution complete.",
                                       Event.Arg("queryString", queryString),
                                       Event.Arg("suggestion count", collations.Count),
                                       Event.Arg("executionTime", executionTime.ElapsedMilliseconds));
                }
                #endregion

                return collations.ToArray();
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, "Unexpected exception.", e);
                #endregion
                throw;
            }
        }

        #endregion

        private MemberSearchResults Search(Guid? employerId, Guid? organisationId, MemberSearchQuery searchQuery, Func<Employer, MemberSearchQuery, IList<Guid>> getIncludeMemberIds, Func<Employer, MemberSearchQuery, IList<Guid>> getExcludeMemberIds)
        {
            const string method = "SearchFolder";

            try
            {
                #region Log
                Stopwatch searchTime = null;
                if (EventSource.IsEnabled(Event.Trace))
                    searchTime = Stopwatch.StartNew();
                #endregion

                // Employer.

                var employer = employerId == null || organisationId == null
                    ? null
                    : new Employer { Id = employerId.Value, Organisation = new Organisation { Id = organisationId.Value } };

                // Gather the pieces.

                var query = _indexer.GetQuery(searchQuery);
                var filter = _indexer.GetFilter(searchQuery, getIncludeMemberIds(employer, searchQuery), getExcludeMemberIds(employer, searchQuery));
                var selections = _indexer.GetSelections(searchQuery);
                var sort = _indexer.GetSort(employer, searchQuery);

                #region Log
                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Executing query.", Event.Arg("query", (query != null) ? query.toString() : string.Empty));
                #endregion

                // Search.

                var reader = GetReader();
                var searcher = new Searcher(reader);
                var sorts = (sort != null) ? sort.getSort() : null;
                var searchResults = searcher.Search(query, filter, selections, sorts, searchQuery.Skip, searchQuery.Take ?? reader.maxDoc(), true);

                #region Log
                if (searchTime != null)
                {
                    searchTime.Stop();
                    EventSource.Raise(Event.Trace, method, "Query execution complete.", Event.Arg("query", (query != null) ? query.toString() : string.Empty), Event.Arg("total hits", searchResults.TotalMatches), Event.Arg("result count", searchResults.MemberIds.Count), Event.Arg("searchTime", searchTime.ElapsedMilliseconds));
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

        protected override MemberContent GetContent(Guid memberId, object cache)
        {
            var member = _membersQuery.GetMember(memberId);
            if (member == null)
                return null;

            var candidate = _candidatesQuery.GetCandidate(memberId);
            if (candidate == null)
                return null;

            var resume = candidate.ResumeId == null
                ? null
                : _resumesQuery.GetResume(candidate.ResumeId.Value);

            return new MemberContent
            {
                Member = member,
                Candidate = candidate,
                Resume = resume,
                IncludeHidden = _includeHidden,
            };
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
            similarity.setLengthNormFactors(FieldName.Content, 0, 1000, 0.5f, false);
            return similarity;
        }

        protected override void OnIndexInitialised(Directory directory)
        {
            var indexReader = IndexReader.open(directory, true);
            try
            {
                _spellCheckHandler.Build(indexReader);
            }
            finally
            {
                indexReader.close();
            }
        }

        protected override void IndexContent(IndexWriter indexWriter, MemberContent content, bool isNew)
        {
            _indexer.IndexContent(indexWriter, content, isNew);
        }

        protected override void UnindexContent(IndexWriter indexWriter, Guid id)
        {
            _indexer.DeleteContent(indexWriter, id);
        }
    }
}
