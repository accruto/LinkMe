using System;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class UpdateCriteriaTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestUpdate()
        {
            var criteria1 = CreateCriteria(1);
            var criteria2 = CreateCriteria(2);

            // Create first.

            var ownerId = Guid.NewGuid();
            var savedSearch = new JobAdSearch { Name = SearchName, Criteria = criteria1 };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, savedSearch);

            // Get it.

            var gotSearch = _jobAdSearchesQuery.GetJobAdSearch(savedSearch.Id);
            Assert.AreEqual(criteria1, gotSearch.Criteria);

            var gotSearches = _jobAdSearchesQuery.GetJobAdSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);
            Assert.AreEqual(criteria1, gotSearches[0].Criteria);

            // Update it.

            savedSearch.Criteria = criteria2;
            _jobAdSearchesCommand.UpdateJobAdSearch(ownerId, savedSearch);

            // Get it.

            gotSearch = _jobAdSearchesQuery.GetJobAdSearch(savedSearch.Id);
            Assert.AreEqual(criteria2, gotSearch.Criteria);

            gotSearches = _jobAdSearchesQuery.GetJobAdSearches(savedSearch.OwnerId);
            Assert.AreEqual(1, gotSearches.Count);
            Assert.AreEqual(criteria2, gotSearches[0].Criteria);
        }
    }
}