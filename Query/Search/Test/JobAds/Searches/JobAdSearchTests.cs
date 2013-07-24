using System;
using System.Collections.Generic;
using LinkMe.Domain;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Location.Queries;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using LinkMe.Query.Search.JobAds.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    public abstract class JobAdSearchTests
        : TestClass
    {
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();
        protected readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        protected readonly IJobAdSearchesQuery _jobAdSearchesQuery = Resolve<IJobAdSearchesQuery>();

        protected const string SearchName = "Test Search";

        protected JobAdSearchCriteria CreateCriteria(int index)
        {
            return new JobAdSearchCriteria
            {
                AdTitle = "title" + index,
                AdvertiserName = "advertiser" + index,
                Distance = 100,
                IndustryIds = new List<Guid> { _industriesQuery.GetIndustries()[0].Id },
                JobTypes = JobTypes.FullTime,
                Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), "Melbourne VIC 3000"),
                Salary = new Salary { Currency = Currency.AUD, LowerBound = 50000, UpperBound = 100000, Rate = SalaryRate.Year },
                CommunityId = Guid.NewGuid(),
                CommunityOnly = false,
            };
        }

        protected static void AssertSearch(JobAdSearch expectedSearch, JobAdSearch search)
        {
            Assert.AreEqual(expectedSearch.OwnerId, search.OwnerId);
            Assert.AreEqual(expectedSearch.Criteria, search.Criteria);
            Assert.AreEqual(expectedSearch.Name, search.Name);
        }

        protected static void AssertExecution(JobAdSearchExecution expectedExecution, JobAdSearchExecution execution)
        {
            Assert.AreEqual(expectedExecution.Context, execution.Context);
            Assert.AreEqual(expectedExecution.Criteria, execution.Criteria);
            Assert.AreEqual(expectedExecution.SearcherId, execution.SearcherId);
            Assert.AreEqual(expectedExecution.SearcherIp, execution.SearcherIp);
            Assert.AreEqual(expectedExecution.SearchId, execution.SearchId);
        }
    }
}