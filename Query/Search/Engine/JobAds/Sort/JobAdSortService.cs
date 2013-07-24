using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.JobAds.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Framework.Instrumentation;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using org.apache.lucene.index;
using org.apache.lucene.store;

namespace LinkMe.Query.Search.Engine.JobAds.Sort
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class JobAdSortService
        : SearchService<JobAdSortContent, ConcurrentDictionary<Guid, EmployerContent>>, IJobAdSortService
    {
        private static readonly EventSource EventSource = new EventSource<JobAdSortService>();

        private const string DefaultIndexFolder = @"C:\LinkMe\Sort\JobAdIndex";
        private readonly Indexer _indexer;

        private readonly IJobAdsQuery _jobAdsQuery;
        private readonly IJobAdSortFiltersQuery _jobAdSortFiltersQuery;
        private readonly IEmployersQuery _employersQuery;

        public JobAdSortService(IJobAdSortBooster booster, IJobAdSortEngineQuery searchEngineQuery, IJobAdsQuery jobAdsQuery, IEmployersQuery employersQuery, IJobAdSortFiltersQuery jobAdSortFiltersQuery, IJobAdFlagListsQuery jobAdFlagListsQuery)
            : base(EventSource, searchEngineQuery, DefaultIndexFolder)
        {
            _jobAdsQuery = jobAdsQuery;
            _employersQuery = employersQuery;
            _jobAdSortFiltersQuery = jobAdSortFiltersQuery;

            _indexer = new Indexer(booster, jobAdFlagListsQuery);
        }

        #region Implementation of IJobAdSortService

        JobAdSearchResults IJobAdSortService.SortFolder(Guid? memberId, Guid folderId, JobAdSortQuery query)
        {
            return Sort(memberId, query, m => _jobAdSortFiltersQuery.GetFolderIncludeJobAdIds(m, folderId), m => _jobAdSortFiltersQuery.GetFolderExcludeJobAdIds(m, folderId));
        }

        JobAdSearchResults IJobAdSortService.SortFlagged(Guid? memberId, JobAdSortQuery query)
        {
            return Sort(memberId, query, m => _jobAdSortFiltersQuery.GetFlaggedIncludeJobAdIds(m), m => _jobAdSortFiltersQuery.GetFlaggedExcludeJobAdIds(m));
        }

        JobAdSearchResults IJobAdSortService.SortBlocked(Guid? memberId, JobAdSortQuery query)
        {
            return Sort(memberId, query, m => _jobAdSortFiltersQuery.GetBlockedIncludeJobAdIds(m), m => _jobAdSortFiltersQuery.GetBlockedExcludeJobAdIds(m));
        }

        void IJobAdSortService.UpdateJobAd(Guid jobAdId)
        {
            UpdateContent(jobAdId);
        }

        void IJobAdSortService.Clear()
        {
            ClearIndex();
        }

        #endregion

        protected override void OnIndexInitialised(Directory directory)
        {
        }

        protected override IndexWriter CreateIndexWriter(Directory directory)
        {
            return new IndexWriter(directory, null, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        private JobAdSearchResults Sort(Guid? memberId, JobAdSortQuery sortQuery, Func<Member, IList<Guid>> getIncludeJobAdIds, Func<Member, IList<Guid>> getExcludeJobAdIds)
        {
            const string method = "Job Ad Supplemental Search";

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

                var filter = _indexer.GetFilter(getIncludeJobAdIds(member), getExcludeJobAdIds(member));
                var sort = _indexer.GetSort(member, sortQuery);

                #region Log

                if (EventSource.IsEnabled(Event.Trace))
                    EventSource.Raise(Event.Trace, method, "Executing query.",
                                      Event.Arg("filter", (filter != null) ? filter.toString() : string.Empty));

                #endregion

                var reader = GetReader();
                var sorter = new Sorter(reader);
                var sorts = (sort != null) ? sort.getSort() : null;
                var sortResults = sorter.Sort(filter, sorts, sortQuery.Skip, sortQuery.Take ?? reader.maxDoc());

                #region Log

                if (searchTime != null)
                {
                    searchTime.Stop();
                    EventSource.Raise(Event.Trace, method, "Query execution complete.",
                                      Event.Arg("filter", (filter != null) ? filter.toString() : string.Empty),
                                      Event.Arg("total hits", sortResults.TotalMatches),
                                      Event.Arg("result count", sortResults.JobAdIds.Count),
                                      Event.Arg("searchTime", searchTime.ElapsedMilliseconds));
                }

                #endregion

                return sortResults;
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

        protected override void IndexContent(IndexWriter indexWriter, JobAdSortContent content, bool isNew)
        {
            _indexer.IndexContent(indexWriter, content, isNew);
        }

        protected override void UnindexContent(IndexWriter indexWriter, Guid id)
        {
        }

        protected override JobAdSortContent GetContent(Guid jobAdId, ConcurrentDictionary<Guid, EmployerContent> cache)
        {
            var jobAd = _jobAdsQuery.GetJobAd<JobAd>(jobAdId);
            if (jobAd == null)
                return null;

            var employer = cache != null ? cache.GetOrAdd(jobAd.PosterId, GetEmployerContent) : GetEmployerContent(jobAd.PosterId);

            return new JobAdSortContent
            {
                JobAd = jobAd,
                LastRefreshTime = _jobAdsQuery.GetLastRefreshTime(jobAdId),
                Employer = employer,
            };
        }
    }
}
