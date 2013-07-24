using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Users.Members.JobAds.Commands;
using LinkMe.Domain.Users.Members.JobAds.Queries;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.Engine.JobAds.Search;
using LinkMe.Query.Search.Engine.JobAds.Sort;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using org.apache.lucene.analysis;
using org.apache.lucene.index;
using org.apache.lucene.store;
using Indexer = LinkMe.Query.Search.Engine.JobAds.Sort.Indexer;

namespace LinkMe.Query.Search.Engine.Test.JobAds.Sort
{
    [TestClass]
    public abstract class JobAdSortTests
        : TestClass
    {
        protected IndexWriter _indexWriter;
        protected Indexer _indexer;
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        protected readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        protected readonly IJobAdSortFiltersQuery _jobAdSortFiltersQuery = Resolve<IJobAdSortFiltersQuery>();
        protected readonly IJobAdFlagListsQuery _jobAdFlagListsQuery = Resolve<IJobAdFlagListsQuery>();
        protected readonly IMemberJobAdListsCommand _memberJobAdListsCommand = Resolve<IMemberJobAdListsCommand>();

        [TestInitialize]
        public void JobAdSortTestsTestInitialize()
        {
            _indexer = new Indexer(new JobAdSortBooster(), _jobAdFlagListsQuery);
            _indexWriter = new IndexWriter(new RAMDirectory(), new SimpleAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        [TestCleanup]
        public void JobAdSortTestsCleanup()
        {
            _indexWriter.close();
        }

        protected void IndexJobAd(JobAd jobAd, string employerCompanyName, bool featured)
        {
            IndexJobAd(jobAd, employerCompanyName, featured, true);
        }

        protected void IndexJobAd(JobAd jobAd, string employerCompanyName, bool featured, bool isNew)
        {
            _indexer.IndexContent(_indexWriter, new JobAdSortContent { JobAd = jobAd, Employer = new EmployerContent { CompanyName = employerCompanyName } }, isNew);
        }

        protected JobAdSearchResults SortFolder(IMember member, Guid folderId, JobAdSortQuery query)
        {
            var reader = _indexWriter.getReader();
            var sorter = new Sorter(reader);
            var filter = _indexer.GetFilter(
                _jobAdSortFiltersQuery.GetFolderIncludeJobAdIds(member, folderId),
                _jobAdSortFiltersQuery.GetFolderExcludeJobAdIds(member, folderId));
            var sort = _indexer.GetSort(member, query);
            var searchResults = sorter.Sort(filter, sort == null ? null : sort.getSort(), query.Skip, query.Take ?? reader.maxDoc());
            return searchResults;
        }

        protected JobAdSearchResults SortFlagged(IMember member, JobAdSortQuery jobAdQuery)
        {
            var reader = _indexWriter.getReader();
            var sorter = new Sorter(reader);
            var filter = _indexer.GetFilter(
                _jobAdSortFiltersQuery.GetFlaggedIncludeJobAdIds(member),
                _jobAdSortFiltersQuery.GetFlaggedExcludeJobAdIds(member));
            var sort = _indexer.GetSort(member, jobAdQuery);
            var searchResults = sorter.Sort(filter, sort == null ? null : sort.getSort(), jobAdQuery.Skip, jobAdQuery.Take ?? reader.maxDoc());
            return searchResults;
        }
    }
}
