using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.misc;
using org.apache.lucene.store;
using FieldName = LinkMe.Query.Search.Engine.JobAds.Search.FieldName;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Search
{
    [TestClass]
    public abstract class JobAdSearchTests
        : TestClass
    {
        protected IndexWriter _indexWriter;
        protected Indexer _indexer;
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IJobAdActivityFiltersQuery _jobAdActivityFiltersQuery = Resolve<IJobAdActivityFiltersQuery>();
        protected readonly IJobAdsRepository _jobAdsRepository = Resolve<IJobAdsRepository>();
        private readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();

        [TestInitialize]
        public void JobAdSearchTestsInitialize()
        {
            _indexer = new Indexer(new SimpleAnalyzer(), new SimpleAnalyzer(), new JobAdSearchBooster(), _locationQuery, _industriesQuery, _jobAdFlagListsQuery);
            _indexWriter = new IndexWriter(new RAMDirectory(), null, IndexWriter.MaxFieldLength.UNLIMITED);
            var similarity = new SweetSpotSimilarity();
            similarity.setLengthNormFactors(FieldName.Content, 200, 1000, 0.5f, false);
            _indexWriter.setSimilarity(similarity);
        }

        [TestCleanup]
        public void JobAdSearchTestsCleanup()
        {
            _indexWriter.close();
        }

        protected void IndexJobAd(JobAd jobAd, string employerCompanyName)
        {
            IndexJobAd(jobAd, employerCompanyName, null, true);
        }

        protected void IndexJobAd(JobAd jobAd, string employerCompanyName, DateTime? lastRefreshTime, bool isNew)
        {
            _indexer.IndexContent(_indexWriter, new JobAdSearchContent { JobAd = jobAd, Employer = new EmployerContent { CompanyName = employerCompanyName }, LastRefreshTime = lastRefreshTime }, isNew);
        }

        protected JobAdSearchResults Search(JobAdSearchQuery jobAdQuery)
        {
            return Search(null, jobAdQuery);
        }

        protected JobAdSearchResults Search(IMember member, JobAdSearchQuery jobAdQuery)
        {
            var reader = _indexWriter.getReader();
            var searcher = new Searcher(reader);
            var query = _indexer.GetQuery(jobAdQuery);
            var filter = _indexer.GetFilter(
                jobAdQuery,
                _jobAdActivityFiltersQuery.GetIncludeJobAdIds(member, jobAdQuery),
                _jobAdActivityFiltersQuery.GetExcludeJobAdIds(member, jobAdQuery));
            var selections = _indexer.GetSelections(jobAdQuery);
            var sort = _indexer.GetSort(member, jobAdQuery);
            var searchResults = searcher.Search(query, filter, selections, sort == null ? null : sort.getSort(), jobAdQuery.Skip, jobAdQuery.Take ?? reader.maxDoc(), true);
            return searchResults;
        }
    }
}
