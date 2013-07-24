using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using LinkMe.Query.Search.Members.Data;
using LinkMe.Query.Search.Members.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public abstract class MemberSearchTests
        : TestClass
    {
        private const string JobTitleFormat = "Architect{0}";
        private const string KeywordsFormat = ".NET{0}";
        protected const string SearchName = "My saved search";

        private IMembersRepository _repository;
        protected IMemberSearchesCommand _memberSearchesCommand;
        protected IMemberSearchesQuery _memberSearchesQuery;
        protected readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestInitialize]
        public void MemberSearchTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();

            var connectionFactory = Resolve<IDataContextFactory>();
            var locationQuery = Resolve<ILocationQuery>();
            var industriesQuery = Resolve<IIndustriesQuery>();
            _repository = new MembersRepository(connectionFactory, new MemberSearchCriteriaPersister(locationQuery, industriesQuery));
            _memberSearchesCommand = new MemberSearchesCommand(_repository);
            _memberSearchesQuery = new MemberSearchesQuery(_repository);
        }

        protected MemberSearchCriteria CreateAdvancedCriteria(int index)
        {
            var criteria = new MemberSearchCriteria
            {
                CommunityId = Guid.NewGuid(),
                IndustryIds = new List<Guid> { _industriesQuery.GetIndustries()[0].Id },
                JobTitle = string.Format(JobTitleFormat, index),
                SortCriteria = new MemberSearchSortCriteria { SortOrder = MemberSortOrder.DateUpdated },
                CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking,
                CompanyKeywords = "LinkMe",
                DesiredJobTitle = "Landscape gardener",
                Distance = 32,
                EthnicStatus = EthnicStatus.Aboriginal,
                JobTypes = JobTypes.Contract,
                IncludeRelocating = true,
                JobTitlesToSearch = JobsToSearch.LastJob,
            };
            criteria.SetKeywords(string.Format(KeywordsFormat, index));
            return criteria;
        }

        protected static void AssertSearch(MemberSearch expectedSearch, MemberSearch search)
        {
            Assert.AreEqual(expectedSearch.OwnerId, search.OwnerId);
            Assert.AreEqual(expectedSearch.Criteria, search.Criteria);
            Assert.AreEqual(expectedSearch.Name, search.Name);
        }

        protected static void AssertExecution(MemberSearchExecution expectedExecution, MemberSearchExecution execution)
        {
            Assert.AreEqual(expectedExecution.Context, execution.Context);
            Assert.AreEqual(expectedExecution.Criteria, execution.Criteria);
            Assert.AreEqual(expectedExecution.SearcherId, execution.SearcherId);
            Assert.AreEqual(expectedExecution.SearcherIp, execution.SearcherIp);
            Assert.AreEqual(expectedExecution.SearchId, execution.SearchId);
        }
    }
}