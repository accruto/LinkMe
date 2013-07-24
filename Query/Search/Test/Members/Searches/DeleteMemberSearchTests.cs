using System;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class DeleteMemberSearchTests
        : MemberSearchTests
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();

        private const string SearchName2 = "Test Search2";

        [TestMethod]
        public void TestDeleteAdvancedCriteria()
        {
            TestDeleteCriteria(CreateAdvancedCriteria(1));
        }

        [TestMethod]
        public void TestDeleteSearchSharedCriteria()
        {
            var owner = new Employer { Id = Guid.NewGuid() };

            // For some older searches the criteria are shared between searches.

            var search1 = new MemberSearch { Name = SearchName, Criteria = CreateAdvancedCriteria(0) };
            _memberSearchesCommand.CreateMemberSearch(owner, search1);

            var search2 = new MemberSearch { Name = SearchName2, Criteria = CreateAdvancedCriteria(0) };
            _memberSearchesCommand.CreateMemberSearch(owner, search2);

            UpdateCriteriaId(search1.Id, search2.Criteria.Id);

            // Now delete the first.

            _memberSearchesCommand.DeleteMemberSearch(owner, search1.Id);

            Assert.IsNull(_memberSearchesQuery.GetMemberSearch(search1.Id));
            AssertSearch(search2, _memberSearchesQuery.GetMemberSearch(search2.Id));
        }

        [TestMethod]
        public void TestDeleteSearchExecutionSharedCriteria()
        {
            var owner = new Employer { Id = Guid.NewGuid() };

            // For some older searches the criteria are shared between searches and executions.

            var search = new MemberSearch { Name = SearchName, Criteria = CreateAdvancedCriteria(0) };
            _memberSearchesCommand.CreateMemberSearch(owner, search);

            var execution = new MemberSearchExecution { Criteria = CreateAdvancedCriteria(0), Results = new MemberSearchResults(), Context = "xxx" };
            _memberSearchesCommand.CreateMemberSearchExecution(execution);

            UpdateCriteriaId(search.Id, execution.Criteria.Id);

            // Delete.

            _memberSearchesCommand.DeleteMemberSearch(owner, search.Id);

            Assert.IsNull(_memberSearchesQuery.GetMemberSearch(search.Id));
            AssertExecution(execution, _memberSearchesQuery.GetMemberSearchExecution(execution.Id));
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

        private void TestDeleteCriteria(MemberSearchCriteria criteria)
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch { Name = SearchName, Criteria = criteria };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            // Get it.

            var gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.IsNotNull(gotSearch);

            var gotSearches = _memberSearchesQuery.GetMemberSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);

            // Delete it.

            _memberSearchesCommand.DeleteMemberSearch(owner, savedSearch.Id);

            gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.IsNull(gotSearch);

            gotSearches = _memberSearchesQuery.GetMemberSearches(savedSearch.OwnerId);
            Assert.AreEqual(0, gotSearches.Count);
        }
    }
}