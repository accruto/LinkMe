using System;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.JobAds;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class DeleteJobAdSearchTests
        : JobAdSearchTests
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();

        private const string SearchName2 = "Test Search2";

        [TestMethod]
        public void TestDeleteJobAdSearch()
        {
            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            var ownerId = Guid.NewGuid();
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);
            _jobAdSearchesCommand.DeleteJobAdSearch(ownerId, search.Id);
            Assert.IsNull(_jobAdSearchesQuery.GetJobAdSearch(search.Id));
        }

        [TestMethod]
        public void TestDeleteSearchSharedCriteria()
        {
            var ownerId = Guid.NewGuid();

            // For some older searches the criteria are shared between searches.

            var search1 = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search1);

            var search2 = new JobAdSearch { Name = SearchName2, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search2);

            UpdateCriteriaId(search1.Id, search2.Criteria.Id);

            // Now delete the first.

            _jobAdSearchesCommand.DeleteJobAdSearch(ownerId, search1.Id);

            Assert.IsNull(_jobAdSearchesQuery.GetJobAdSearch(search1.Id));
            AssertSearch(search2, _jobAdSearchesQuery.GetJobAdSearch(search2.Id));
        }

        [TestMethod]
        public void TestDeleteSearchExecutionSharedCriteria()
        {
            // For some older searches the criteria are shared between searches and executions.

            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            var ownerId = Guid.NewGuid();
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            var execution = new JobAdSearchExecution { Criteria = CreateCriteria(0), Results = new JobAdSearchResults(), Context = "xxx" };
            _jobAdSearchesCommand.CreateJobAdSearchExecution(execution);

            UpdateCriteriaId(search.Id, execution.Criteria.Id);

            // Delete.

            _jobAdSearchesCommand.DeleteJobAdSearch(ownerId, search.Id);

            Assert.IsNull(_jobAdSearchesQuery.GetJobAdSearch(search.Id));
            AssertExecution(execution, _jobAdSearchesQuery.GetJobAdSearchExecution(execution.Id));
        }

        private void UpdateCriteriaId(Guid searchId, Guid criteriaId)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                connection.Open();

                using (var command = DatabaseHelper.CreateTextCommand(connection, "UPDATE dbo.SavedJobSearch SET criteriaSetId = '" + criteriaId + "' WHERE id = '" + searchId + "'", 30, null))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}