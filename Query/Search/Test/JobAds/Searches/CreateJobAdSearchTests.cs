using System;
using LinkMe.Domain.Test;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.JobAds.Searches
{
    [TestClass]
    public class CreateJobAdSearchTests
        : JobAdSearchTests
    {
        [TestMethod]
        public void TestCreateJobAdSearch()
        {
            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(Guid.NewGuid(), search);
            AssertSearch(search, _jobAdSearchesQuery.GetJobAdSearch(search.Id));
        }

        [TestMethod]
        public void TestNoName()
        {
            var search = new JobAdSearch { Name = null, Criteria = CreateCriteria(0) };
            _jobAdSearchesCommand.CreateJobAdSearch(Guid.NewGuid(), search);
            AssertSearch(search, _jobAdSearchesQuery.GetJobAdSearch(search.Id));
        }

        [TestMethod]
        public void TestRenameJobAdSearch()
        {
            var search = new JobAdSearch { Name = SearchName, Criteria = CreateCriteria(0) };
            var ownerId = Guid.NewGuid();
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            search.Name = "Another test search";
            _jobAdSearchesCommand.UpdateJobAdSearch(ownerId, search);

            AssertSearch(search, _jobAdSearchesQuery.GetJobAdSearch(search.Id));
        }

        [TestMethod]
        public void TestTooLongName()
        {
            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = new string('a', 250),
            };

            AssertException.Thrown<ValidationErrorsException>(() => _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the Name property.");
        }

        [TestMethod]
        public void TestInvalidName()
        {
            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = "#&*&^*(&_(_*()",
            };

            AssertException.Thrown<ValidationErrorsException>(() => _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the Name property.");
        }

        [TestMethod]
        public void TestValidCharacters()
        {
            TestCreate("abcd (def)");
            TestCreate("abcd: def");
            TestCreate("abcd & def");
            TestCreate("abcd, def 3000");
            TestCreate("abcd; def 3000");

            // C#, VB.NET

            TestCreate("abcd# def 3000");
            TestCreate("abcd. def 3000");
        }

        [TestMethod]
        public void TestCreateSameName()
        {
            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = SearchName,
            };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            // Create another.

            search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = SearchName,
            };
            AssertException.Thrown<ValidationErrorsException>(() => _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search), "A 'LinkMe.Framework.Utility.Validation.DuplicateValidationError' error has occurred for the Name property.");
        }

        [TestMethod]
        public void TestCreateNoNames()
        {
            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = null,
            };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            // Create another.

            search = new JobAdSearch
            {
                Criteria = CreateCriteria(1),
                Name = null,
            };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);
        }

        private void TestCreate(string name)
        {
            var ownerId = Guid.NewGuid();
            var search = new JobAdSearch
            {
                Criteria = CreateCriteria(0),
                Name = name,
            };
            _jobAdSearchesCommand.CreateJobAdSearch(ownerId, search);

            var gotSearch = _jobAdSearchesQuery.GetJobAdSearch(search.Id);
            Assert.AreEqual(name, gotSearch.Name);
        }
    }
}