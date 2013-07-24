using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Expressions;
using LinkMe.Query.JobAds;
using java.io;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.search;
using org.apache.lucene.search.similar;
using org.apache.lucene.store;
using org.apache.solr.common;
using LuceneQuery = org.apache.lucene.search.Query;

namespace LinkMe.Query.Search.Engine.JobAds.Search
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true)]
    public class JobAdSearchService
        : SearchService<JobAdSearchContent, ConcurrentDictionary<Guid, EmployerContent>>, IJobAdSearchService
    {
        private static readonly EventSource EventSource = new EventSource<JobAdSearchService>();

        private const string DefaultIndexFolder = @"C:\LinkMe\Search\JobAdIndex";

        private readonly Analyzer _contentAnalyzer;
        private readonly Indexer _indexer;
        private readonly SpellCheckHandler _spellCheckHandler;

        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IEmployersQuery _employersQuery;
        private readonly IMembersQuery _membersQuery;
        private readonly IResumesQuery _resumesQuery;
        private readonly ICandidatesQuery _candidatesQuery;
        private readonly IMemberApplicationsQuery _memberApplicationsQuery;
        private readonly IJobAdActivityFiltersQuery _jobAdActivityFiltersQuery;

        public JobAdSearchService(ResourceLoader resourceLoader, IJobAdSearchBooster booster, IJobAdSearchEngineQuery searchEngineQuery, IJobAdsQuery jobAdsQuery, IEmployersQuery employersQuery, IMembersQuery membersQuery, IResumesQuery resumesQuery, ICandidatesQuery candidatesQuery, ILocationQuery locationQuery, IIndustriesQuery industriesQuery, IJobAdActivityFiltersQuery jobAdActivityFiltersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery, IMemberApplicationsQuery memberApplicationsQuery)
            : base(EventSource, searchEngineQuery, DefaultIndexFolder)
        {
            _jobAdsQuery = jobAdsQuery;
            _employersQuery = employersQuery;
            _membersQuery = membersQuery;
            _resumesQuery = resumesQuery;
            _candidatesQuery = candidatesQuery;
            _memberApplicationsQuery = memberApplicationsQuery;
            _jobAdActivityFiltersQuery = jobAdActivityFiltersQuery;

            var analyzerFactory = new AnalyzerFactory(resourceLoader);
            _contentAnalyzer = analyzerFactory.CreateContentAnalyzer();
            _indexer = new Indexer(_contentAnalyzer, analyzerFactory.CreateQueryAnalyzer(), booster, locationQuery, industriesQuery, jobAdFlagListsQuery);
            _spellCheckHandler = new SpellCheckHandler(analyzerFactory.CreateSpellingAnalyzer(), FieldName.ContentExact);
        }

        #region Implementation of IJobAdSearchService

        JobAdSearchResults IJobAdSearchService.Search(Guid? memberId, JobAdSearchQuery query)
        {
            return Search(
                memberId,
                query,
                (m, q) => _jobAdActivityFiltersQuery.GetIncludeJobAdIds(m, q),
                (m, q) => _jobAdActivityFiltersQuery.GetExcludeJobAdIds(m, q));
        }

        JobAdSearchResults IJobAdSearchService.SearchFlagged(Guid? memberId, JobAdSearchQuery query)
        {
            return Search(
                memberId,
                query,
                (m, q) => _jobAdActivityFiltersQuery.GetFlaggedIncludeJobAdIds(m, q),
                (m, q) => _jobAdActivityFiltersQuery.GetFlaggedExcludeJobAdIds(m, q));
        }

        JobAdSearchResults IJobAdSearchService.SearchSimilar(Guid? memberId, Guid jobAdId, JobAdSearchQuery searchQuery)
        {
            const string method = "GetSimilarJobs";

            try
            {
                var reader = GetReader();
                var searcher = new Searcher(reader);

                var docId = searcher.Fetch(jobAdId);

                // If the job ad cannot be found then return no results.

                if (docId == -1)
                    return new JobAdSearchResults();

                var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
                if (jobAd == null)
                    return new JobAdSearchResults();

                // Look for more like this.

                var mlt = new MoreLikeThis(reader);
                mlt.setAnalyzer(_contentAnalyzer);
                mlt.setFieldNames(new []{FieldName.Content, FieldName.Title});
                var query = mlt.like(docId);

                //query = new SeniorityIndexHandler().GetQuery(query, new JobAdSearchQuery {SeniorityIndex = jobAd.SeniorityIndex});

                // Ensure the initial job is not in the results.

                var searchFilter = new BooleanFilter();
                searchFilter.add(new FilterClause(new SpecialsFilter(SearchFieldName.Id, false, new[] { jobAdId.ToFieldValue() }), BooleanClause.Occur.MUST_NOT));

                // Add salary and location restriction.

                var filter = _indexer.GetFilter(
                    new JobAdSearchQuery
                    {
                        Salary = FudgeSalary(jobAd.Description.Salary),
                        ExcludeNoSalary = true,
                        Location = jobAd.Description.Location,
                        Distance = 50,
                    },
                    null,
                    null);

                searchFilter.add(new FilterClause(filter, BooleanClause.Occur.MUST));

                return searcher.Search(query, searchFilter, null, null, searchQuery.Skip, searchQuery.Take ?? reader.maxDoc(), false);
            }
            catch (Exception e)
            {
                #region Log
                EventSource.Raise(Event.Error, method, "Unexpected exception.", e);
                #endregion
                throw;
            }
        }

        JobAdSearchResults IJobAdSearchService.SearchSuggested(Guid? memberId, JobAdSearchQuery searchQuery)
        {
            const string method = "GetSuggestedJobs";

            try
            {
                if (memberId == null)
                    return new JobAdSearchResults();

                var reader = GetReader();

                var searcher = new Searcher(reader);

                var member = _membersQuery.GetMember(memberId.Value);
                var candidate = _candidatesQuery.GetCandidate(memberId.Value);
                if (member == null || candidate == null || candidate.ResumeId == null)
                    return new JobAdSearchResults();

                var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);

                //Get a MLT query based on the candidate's details
                var mlt = new MoreLikeThis(reader, CreateSimilarity());
                mlt.setAnalyzer(_contentAnalyzer);
                mlt.setMaxNumTokensParsed(10000); //increase for long resumes
                mlt.setFieldNames(new[] {FieldName.Content});
                mlt.setMinWordLen(3); //exclude UK, BBC and the like
                mlt.setMaxQueryTerms(20);
                //mlt.setBoost(true);
                var candidateMltQuery = GetCandidateMltQuery(mlt, candidate, resume, method);

                mlt.setFieldNames(new[] {FieldName.Title});
                var candidateTitleMltQuery = GetCandidateMltQuery(mlt, candidate, resume, method);

                #region Log

                if (EventSource.IsEnabled(Event.Trace))
                {
                    EventSource.Raise(Event.Trace, method, "Candidate MLT Query",
                                      Event.Arg("Query",
                                                candidateMltQuery == null ? "null" : candidateMltQuery.toString()));
                    EventSource.Raise(Event.Trace, method, "Candidate Title MLT Query",
                                      Event.Arg("Query",
                                                candidateTitleMltQuery == null
                                                    ? "null"
                                                    : candidateTitleMltQuery.toString()));
                }

                #endregion

                var mltQueries = new BooleanQuery(); // Up to 3 MLT queries ORed together
                var titleQueries = new BooleanQuery();
                    // (Optionally) Desired Job Title and up to 3 job titles ORed together
                var combinedFilter = new BooleanFilter();
                    // Filtering including exclusion of past applications and salary, location, etc.
                var combinedQuery = new BooleanQuery(); // mltQueries and titleQueries ANDed together

                if (candidateMltQuery != null)
                    mltQueries.add(candidateMltQuery, BooleanClause.Occur.SHOULD);

                if (candidateTitleMltQuery != null)
                    mltQueries.add(candidateTitleMltQuery, BooleanClause.Occur.SHOULD);

                //Get a MLT query based on the candidate's past 3 months of applications

                var jobAdIds = (from a in _memberApplicationsQuery.GetApplications(member.Id)
                                where a.CreatedTime >= DateTime.Now.AddMonths(-3)
                                select a.PositionId).ToList();

                if (jobAdIds.Any())
                {
                    mlt.setFieldNames(new[] {FieldName.Content});
                    var applicationsMltQuery = GetApplicationsMltQuery(mlt, jobAdIds);

                    if (applicationsMltQuery != null)
                        mltQueries.add(applicationsMltQuery, BooleanClause.Occur.SHOULD);

                    #region Log

                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Applications MLT Query",
                                          Event.Arg("Query",
                                                    applicationsMltQuery == null
                                                        ? "null"
                                                        : applicationsMltQuery.toString()));

                    #endregion

                    // ensure the jobs that have already been applied for are not in the results
                    var idFilter = new SpecialsFilter(SearchFieldName.Id, false,
                                                      jobAdIds.Select(id => id.ToFieldValue()));
                    combinedFilter.add(new FilterClause(idFilter, BooleanClause.Occur.MUST_NOT));

                    #region Log

                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Combined Filter #1",
                                          Event.Arg("Query", combinedFilter.toString()));

                    #endregion
                }

                if (mltQueries.getClauses().Any())
                    combinedQuery.add(new BooleanClause(mltQueries, BooleanClause.Occur.SHOULD));

                // now add in additional weighting data from the candidate's profile
                if (!string.IsNullOrEmpty(candidate.DesiredJobTitle))
                {
                    //Boost the DesiredJobTitle above the rest of the terms
                    var jobTitleExpression =
                        Expression.Parse(Expression.StripOperatorsAndBrackets(candidate.DesiredJobTitle),
                                         ModificationFlags.AllowShingling);

                    // First match on title
                    var boostQuery =
                        _indexer.GetQuery(new JobAdSearchQuery
                                                    {AdTitle = jobTitleExpression, IncludeSynonyms = true});
                    boostQuery.setBoost(1.5F);
                    titleQueries.add(boostQuery, BooleanClause.Occur.SHOULD);

                    // Also match general content
                    //boostQuery = _indexer.GetLuceneQuery(new JobAdSearchQuery {Keywords = jobTitleExpression, IncludeSynonyms = true});
                    //boostQuery.setBoost(1.3F);
                    //titleQueries.add(boostQuery, BooleanClause.Occur.SHOULD);

                    #region Log

                    if (EventSource.IsEnabled(Event.Trace))
                        EventSource.Raise(Event.Trace, method, "Combined Query #1",
                                          Event.Arg("Query", combinedQuery.toString()));

                    #endregion
                }

                if (resume.Jobs != null)
                {
                    foreach (var jobTitle in resume.Jobs.Select(j => j.Title).Take(3))
                    {
                        if (string.IsNullOrEmpty(jobTitle)) continue;

                        var jobTitleExpression = Expression.Parse(Expression.StripOperatorsAndBrackets(jobTitle),
                                                                  ModificationFlags.AllowShingling);

                        // First match on title
                        var boostQuery =
                            _indexer.GetQuery(new JobAdSearchQuery
                                                        {AdTitle = jobTitleExpression, IncludeSynonyms = true});
                        boostQuery.setBoost(1.2F);
                        titleQueries.add(boostQuery, BooleanClause.Occur.SHOULD);

                        // Also match general content
                        //boostQuery = _indexer.GetLuceneQuery(new JobAdSearchQuery { Keywords = jobTitleExpression, IncludeSynonyms = true });
                        //boostQuery.setBoost(1.1F);
                        //titleQueries.add(boostQuery, BooleanClause.Occur.SHOULD);

                        // Also match general content
                        //boostQuery = _indexer.GetLuceneQuery(new JobAdSearchQuery { Keywords = jobTitleExpression, IncludeSynonyms = true });
                        //boostQuery.setBoost(1.1F);
                        //combinedQuery.add(boostQuery, BooleanClause.Occur.SHOULD);
                    }
                }

                // now combine the queries

                if (mltQueries.getClauses().Any())
                    combinedQuery.add(new BooleanClause(mltQueries, BooleanClause.Occur.SHOULD));

                if (titleQueries.getClauses().Any())
                    combinedQuery.add(new BooleanClause(titleQueries, BooleanClause.Occur.MUST));

                #region Log

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Combined Query #2",
                                      Event.Arg("Query", combinedQuery.toString()));

                #endregion

                searchQuery.Salary = candidate.DesiredSalary == null
                    ? null
                    : candidate.DesiredSalary.Clone();
                searchQuery.ExcludeNoSalary = false;
                searchQuery.Location = member.Address.Location;
                searchQuery.Distance = 50;
                searchQuery.JobTypes = candidate.DesiredJobTypes;
                searchQuery.Relocations = candidate.RelocationPreference != RelocationPreference.No && candidate.RelocationLocations != null
                    ? candidate.RelocationLocations = candidate.RelocationLocations.ToArray()
                    : null;

                //Add salary, job type, location restriction & (optionally) last run time
                var filter = _indexer.GetFilter(searchQuery, null, null);

                // combine salary, etc. filter with the excluded job ads
                combinedFilter.add(new FilterClause(filter, BooleanClause.Occur.MUST));

                // exclude blocked jobs
                var excludedIds = _jobAdActivityFiltersQuery.GetExcludeJobAdIds(member, searchQuery);
                if (excludedIds != null && excludedIds.Count > 0)
                    combinedFilter.add(new FilterClause(new SpecialsFilter(SearchFieldName.Id, true, excludedIds.Select(id => id.ToFieldValue())), BooleanClause.Occur.MUST));


                #region Log

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Combined Filter #2",
                                      Event.Arg("Query", combinedFilter.toString()));

                #endregion

                var searchResults = searcher.Search(combinedQuery, combinedFilter, null, null, searchQuery.Skip, searchQuery.Take ?? reader.maxDoc(), false);

                #region Log

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "MLT Results",
                                      Event.Arg("Results Count", searchResults == null ? 0 : searchResults.JobAdIds.Count));

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

        void IJobAdSearchService.UpdateJobAd(Guid jobAdId)
        {
            UpdateContent(jobAdId);
        }

        bool IJobAdSearchService.IsIndexed(Guid jobAdId)
        {
            return Search(
                null,
                new JobAdSearchQuery(),
                (m, q) => new[] { jobAdId }.ToList(),
                (m, q) => null).TotalMatches > 0;
        }

        void IJobAdSearchService.Clear()
        {
            ClearIndex();
        }

        #endregion

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

        private JobAdSearchResults Search(Guid? memberId, JobAdSearchQuery searchQuery, Func<IMember, JobAdSearchQuery, IList<Guid>> getIncludeJobAdIds, Func<IMember, JobAdSearchQuery, IList<Guid>> getExcludeJobAdIds)
        {
            const string method = "Search";

            try
            {
                #region Log
                Stopwatch searchTime = null;
                if (EventSource.IsEnabled(Event.Trace))
                    searchTime = Stopwatch.StartNew();
                #endregion

                var member = memberId == null
                    ? null
                    : new Member { Id = memberId.Value };

                var query = _indexer.GetQuery(searchQuery);
                var filter = _indexer.GetFilter(searchQuery, getIncludeJobAdIds(member, searchQuery), getExcludeJobAdIds(member, searchQuery));
                var sort = _indexer.GetSort(member, searchQuery);
                var selections = _indexer.GetSelections(searchQuery);

                #region Log
                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Executing query.", Event.Arg("query", query != null ? query.toString() : string.Empty));
                #endregion

                var reader = GetReader();
                var searcher = new Searcher(reader);
                var sorts = sort != null ? sort.getSort() : null;
                var searchResults = searcher.Search(query, filter, selections, sorts, searchQuery.Skip, searchQuery.Take ?? reader.maxDoc(), true);

                #region Log
                if (searchTime != null)
                {
                    searchTime.Stop();
                    EventSource.Raise(Event.Trace, method, "Query execution complete.", Event.Arg("query", (query != null) ? query.toString() : string.Empty), Event.Arg("total hits", searchResults.TotalMatches), Event.Arg("result count", searchResults.JobAdIds.Count), Event.Arg("searchTime", searchTime.ElapsedMilliseconds));
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

        private EmployerContent GetEmployerContent(Guid employerId)
        {
            var employer = _employersQuery.GetEmployer(employerId);
            if (employer == null)
                return new EmployerContent();

            return new EmployerContent
            {
                CompanyName = employer.Organisation.FullName,
                CommunityId = employer.Organisation.AffiliateId,
            };
        }

        private static Salary FudgeSalary(Salary salary)
        {
            // increase the salary slightly from the job ad range

            if (salary == null)
                return null;

            var fudgedSalary = salary.Clone();

            if (salary.LowerBound != null)
                fudgedSalary.LowerBound = Decimal.Multiply(salary.LowerBound.Value, .9M);

            if (salary.UpperBound != null)
                fudgedSalary.UpperBound = Decimal.Multiply(salary.UpperBound.Value, 1.1M);

            return fudgedSalary;
        }

        private static LuceneQuery GetCandidateMltQuery(MoreLikeThis mlt, ICandidate candidate, Resume resume, string method)
        {
            var candidateString = new StringBuilder();

            if (resume.Jobs != null)
            {
                // construct a stream of relevant job data for passing to mlt
                foreach (var job in resume.Jobs.Take(5))
                {
                    candidateString.AppendLine(job.Description).AppendLine(job.Title);
                }
            }

            #region Log
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Building MLT Query #1", Event.Arg("Analysis Text", candidateString.ToString()));
            #endregion

            // add additional relevant resume data
            candidateString.AppendLine(candidate.DesiredJobTitle);
            candidateString.AppendLine(resume.Summary).AppendLine(resume.Skills);

            if (candidateString.Length < 1000)
            {
                #region Log
                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "MLT Query aborted - insufficient text for analysis", Event.Arg("Analysis Text", candidateString.ToString()));
                #endregion

                return null;
            }

            #region Log
            if (EventSource.IsEnabled(Event.Trace))
                EventSource.Raise(Event.Trace, method, "Building MLT Query #2", Event.Arg("Analysis Text", candidateString.ToString()));
            #endregion

            return mlt.like(new StringReader(candidateString.ToString()));
        }

        private LuceneQuery GetApplicationsMltQuery(MoreLikeThis mlt, IEnumerable<Guid> jobAdIds)
        {
            var jobAds = _jobAdsQuery.GetJobAds<JobAd>(jobAdIds);

            var applicationsString = new StringBuilder();

            foreach (var jobAd in jobAds)
            {
                applicationsString.AppendLine(jobAd.Title)
                    .AppendLine(jobAd.Description.BulletPoints == null ? string.Empty : jobAd.Description.BulletPoints.ToString())
                    .AppendLine(jobAd.Description.Content);
            }

            return mlt.like(new StringReader(applicationsString.ToString()));
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

        protected override void IndexContent(IndexWriter indexWriter, JobAdSearchContent content, bool isNew)
        {
            _indexer.IndexContent(indexWriter, content, isNew);
        }

        protected override void UnindexContent(IndexWriter indexWriter, Guid id)
        {
            _indexer.DeleteContent(indexWriter, id);
        }

        protected override JobAdSearchContent GetContent(Guid jobAdId, ConcurrentDictionary<Guid, EmployerContent> cache)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null)
                return null;

            var employer = cache != null ? cache.GetOrAdd(jobAd.PosterId, GetEmployerContent) : GetEmployerContent(jobAd.PosterId);

            return new JobAdSearchContent
            {
                JobAd = jobAd,
                LastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAdId),
                Employer = employer,
            };
        }
    }
}
