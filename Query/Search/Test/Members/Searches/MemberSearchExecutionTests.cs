using System;
using System.Collections.Generic;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class MemberSearchExecutionTests
        : MemberSearchTests
    {
        [TestMethod]
        public void TestCreateMemberSearchExecution()
        {
            var execution = new MemberSearchExecution
            {
                SearcherId = Guid.NewGuid(),
                Criteria = CreateAdvancedCriteria(0),
                Results = new MemberSearchResults
                {
                    MemberIds = new List<Guid> {Guid.NewGuid()},
                    TotalMatches = 1,
                }
            };
            _memberSearchesCommand.CreateMemberSearchExecution(execution);

            AssertExecution(execution, _memberSearchesQuery.GetMemberSearchExecution(execution.Id));
            AssertExecution(execution, _memberSearchesQuery.GetMemberSearchExecutions(execution.SearcherId.Value)[0]);
        }

        [TestMethod]
        public void TestCreateMemberSearchExecutionWithSearchId()
        {
            // Create the search.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = new MemberSearch
            {
                Criteria = CreateAdvancedCriteria(0),
                Name = SearchName,
            };
            _memberSearchesCommand.CreateMemberSearch(owner, search);

            // Create the execution.

            var execution = new MemberSearchExecution
            {
                Criteria = search.Criteria.Clone(),
                SearcherId = Guid.NewGuid(),
                SearchId = search.Id,
                Results = new MemberSearchResults
                {
                    MemberIds = new List<Guid> { Guid.NewGuid() },
                    TotalMatches = 1,
                }
            };
            _memberSearchesCommand.CreateMemberSearchExecution(execution);

            AssertExecution(execution, _memberSearchesQuery.GetMemberSearchExecution(execution.Id));
            AssertExecution(execution, _memberSearchesQuery.GetMemberSearchExecutions(execution.SearcherId.Value)[0]);
        }
    }
}