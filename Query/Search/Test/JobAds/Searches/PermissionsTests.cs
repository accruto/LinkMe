using System;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class PermissionsTests
        : JobAdSearchTests
    {
        [TestMethod, ExpectedException(typeof(JobAdSearchesPermissionsException))]
        public void TestCannotUpdateOther()
        {
            // Create first.

            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            var otherId = Guid.NewGuid();
            _jobAdSearchesCommand.UpdateJobAdSearch(otherId, search);
        }

        [TestMethod, ExpectedException(typeof(JobAdSearchesPermissionsException))]
        public void TestCannotDeleteOther()
        {
            // Create first.

            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            var otherId = Guid.NewGuid();
            _jobAdSearchesCommand.DeleteJobAdSearch(otherId, search.Id);
        }
    }
}