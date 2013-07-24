using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class CreateMemberSearchTests
        : MemberSearchTests
    {
        [TestMethod]
        public void TestCreateWithoutName()
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch { Criteria = CreateAdvancedCriteria(1) };
            Assert.IsNull(savedSearch.Name);

            AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.CreateMemberSearch(owner, savedSearch), "A 'LinkMe.Framework.Utility.Validation.RequiredValidationError' error has occurred for the Name property.");
        }

        [TestMethod]
        public void TestCreateWithName()
        {
            TestCreate("TestName");
        }

        [TestMethod]
        public void TestTooLongName()
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch
                                  {
                                      Criteria = CreateAdvancedCriteria(1),
                                      Name = new string('a', 250),
                                  };

            AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.CreateMemberSearch(owner, savedSearch), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the Name property.");
        }

        [TestMethod]
        public void TestInvalidName()
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch
                                  {
                                      Criteria = CreateAdvancedCriteria(1),
                                      Name = "#&*&^*(&_(_*()",
                                  };

            AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.CreateMemberSearch(owner, savedSearch), "A 'LinkMe.Framework.Utility.Validation.RegexLengthRangeValidationError' error has occurred for the Name property.");
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
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch
                                  {
                                      Criteria = CreateAdvancedCriteria(1),
                                      Name = SearchName,
                                  };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            // Create another.

            savedSearch = new MemberSearch
                              {
                                  Criteria = CreateAdvancedCriteria(1),
                                  Name = SearchName,
                              };
            AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.CreateMemberSearch(owner, savedSearch), "A 'LinkMe.Framework.Utility.Validation.DuplicateValidationError' error has occurred for the Name property.");
        }

        private void TestCreate(string name)
        {
            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch
                                  {
                                      Criteria = CreateAdvancedCriteria(1),
                                      Name = name,
                                  };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            var gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.AreEqual(name, gotSearch.Name);
        }
    }
}