using System;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class UpdateCriteriaTests
        : MemberSearchTests
    {
        [TestMethod]
        public void TestAdvancedAdvancedUpdate()
        {
            TestUpdate(CreateAdvancedCriteria(1), CreateAdvancedCriteria(2));
        }

        private void TestUpdate(MemberSearchCriteria criteria1, MemberSearchCriteria criteria2)
        {
            // Create first.

            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch { Name = SearchName, Criteria = criteria1 };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            // Get it.

            var gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.AreEqual(criteria1, gotSearch.Criteria);

            var gotSearches = _memberSearchesQuery.GetMemberSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);
            Assert.AreEqual(criteria1, gotSearches[0].Criteria);

            // Update it.

            savedSearch.Criteria = criteria2;
            _memberSearchesCommand.UpdateMemberSearch(owner, savedSearch);

            // Get it.

            gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.AreEqual(criteria2, gotSearch.Criteria);

            gotSearches = _memberSearchesQuery.GetMemberSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);
            Assert.AreEqual(criteria2, gotSearches[0].Criteria);
        }
    }
}