using System;
using LinkMe.Domain.Test;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class UpdateJobAdSearchTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestUpdateNameEmptyName()
        {
            TestUpdateName("TestName", string.Empty);
        }

        [TestMethod]
        public void TestUpdateNameName()
        {
            TestUpdateName("TestName1", "TestName2");
        }

        [TestMethod]
        public void TestValidCharacters()
        {
            TestUpdateName("TestName", "abcd (def)");
            TestUpdateName("TestName", "abcd: def");
            TestUpdateName("TestName", "abcd & def");
            TestUpdateName("TestName", "abcd, def 3000");
            TestUpdateName("TestName", "abcd; def 3000");

            // C#, VB.NET

            TestUpdateName("TestName", "abcd# def 3000");
            TestUpdateName("TestName", "abcd. def 3000");
        }

        [TestMethod]
        public void TestUpdateSameName()
        {
            var ownerId = Guid.NewGuid();

            // Create 2 searches.

            var savedSearch1 = new JobAdSearch
                                   {
                                       Name = "TestName1",
                                       Criteria = CreateCriteria(1)
                                   };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, savedSearch1);

            var savedSearch2 = new JobAdSearch
                                   {
                                       Name = "TestName2",
                                       Criteria = CreateCriteria(2)
                                   };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, savedSearch2);

            // Update the second to match the first.

            savedSearch2.Name = savedSearch1.Name;
            AssertException.Thrown<ValidationErrorsException>(() => _jobAdSearchesCommand.UpdateJobAdSearch(ownerId, savedSearch2), "A 'LinkMe.Framework.Utility.Validation.DuplicateValidationError' error has occurred for the Name property.");
        }

        private void TestUpdateName(string name1, string name2)
        {
            // Create.

            var ownerId = Guid.NewGuid();
            var savedSearch = new JobAdSearch
                                  {
                                      Name = name1,
                                      Criteria = CreateCriteria(1)
                                  };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, savedSearch);

            var gotSearch = _jobAdSearchesQuery.GetJobAdSearch(savedSearch.Id);
            Assert.AreEqual(name1, gotSearch.Name);

            // Update.

            savedSearch.Name = name2;
            _jobAdSearchesCommand.UpdateJobAdSearch(ownerId, savedSearch);
            gotSearch = _jobAdSearchesQuery.GetJobAdSearch(savedSearch.Id);
            Assert.AreEqual(name2.NullIfEmpty(), gotSearch.Name);
        }
    }
}