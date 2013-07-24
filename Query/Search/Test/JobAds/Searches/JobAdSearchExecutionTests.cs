using System;
using System.Collections.Generic;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class JobAdSearchExecutionTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestCreateJobAdSearchExecution()
        {
            var execution = new JobAdSearchExecution
            {
                SearcherId = Guid.NewGuid(),
                Criteria = CreateCriteria(0),
                Context = "Test",
                Results = new JobAdSearchResults
                {
                    JobAdIds = new List<Guid> {Guid.NewGuid()},
                    TotalMatches = 1,
                }
            };
            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            AssertExecution(execution, _jobAdSearchesQuery.GetJobAdSearchExecution(execution.Id));
            AssertExecution(execution, _jobAdSearchesQuery.GetJobAdSearchExecutions(execution.SearcherId.Value, 100)[0]);
        }

        [TestMethod]
        public void TestCreateJobAdSearchExecutionWithSearchId()
        {
            // Create the search.

            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = SearchName,
            };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            // Create the execution.

            var execution = new JobAdSearchExecution
            {
                Criteria = search.Criteria.Clone(),
                Context = "Test",
                SearcherId = Guid.NewGuid(),
                SearchId = search.Id,
                Results = new JobAdSearchResults
                {
                    JobAdIds = new List<Guid> { Guid.NewGuid() },
                    TotalMatches = 1,
                }
            };
            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            AssertExecution(execution, _jobAdSearchesQuery.GetJobAdSearchExecution(execution.Id));
            AssertExecution(execution, _jobAdSearchesQuery.GetJobAdSearchExecutions(execution.SearcherId.Value, 100)[0]);
        }
    }
}