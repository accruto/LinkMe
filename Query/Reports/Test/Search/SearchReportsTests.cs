using System;
using LinkMe.Domain;
using LinkMe.Query.JobAds;
using LinkMe.Query.Reports.Search.Queries;
using LinkMe.Query.Search.JobAds;
using LinkMe.Query.Search.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Search
{
    [TestClass]
    public class SearchReportsTests
        : TestClass
    {
        private readonly IJobAdSearchesCommand _jobAdSearchesCommand = Resolve<IJobAdSearchesCommand>();
        private readonly IJobAdSearchReportsQuery _jobAdSearchReportsQuery = Resolve<IJobAdSearchReportsQuery>();

        [TestMethod]
        public void TestGetJobAdSearches()
        {
            var criteria = new JobAdSearchCriteria {AdTitle = "jobTitle"};
            criteria.SetKeywords("keywords");
            var execution = new JobAdSearchExecution
            {
                SearcherId = Guid.NewGuid(),
                Criteria = criteria,
                Context = "AdvancedJobSearchContext",
                StartTime = DateTime.Now.AddDays(-1),
                Results = new JobAdSearchResults()
            };
            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            criteria = new JobAdSearchCriteria { AdTitle = "Developer" };
            criteria.SetKeywords("Microsoft");
            execution = new JobAdSearchExecution
            {
                SearcherId = Guid.NewGuid(),
                Criteria = criteria,
                Context = "AdvancedJobSearchContext",
                StartTime = DateTime.Now.AddDays(-1),
                Results = new JobAdSearchResults()
            };
            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            Assert.AreEqual(2, _jobAdSearchReportsQuery.GetJobAdSearches(DayRange.Yesterday));
        }
    }
}
