using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Test;
using LinkMe.Framework.Utility.Validation;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class UpdateMemberSearchTests
        : MemberSearchTests
    {
        [TestMethod]
        public void TestUpdateNameNullName()
        {
            TestUpdateName("TestName", null);
        }

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
            var owner = new Employer { Id = Guid.NewGuid() };

            // Create 2 searches.

            var savedSearch1 = new MemberSearch
                                   {
                                       Name = "TestName1",
                                       Criteria = CreateAdvancedCriteria(1)
                                   };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch1);

            var savedSearch2 = new MemberSearch
                                   {
                                       Name = "TestName2",
                                       Criteria = CreateAdvancedCriteria(2)
                                   };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch2);

            // Update the second to match the first.

            savedSearch2.Name = savedSearch1.Name;
            AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.UpdateMemberSearch(owner, savedSearch2), "A 'LinkMe.Framework.Utility.Validation.DuplicateValidationError' error has occurred for the Name property.");
        }

        private void TestUpdateName(string name1, string name2)
        {
            // Create.

            var owner = new Employer { Id = Guid.NewGuid() };
            var savedSearch = new MemberSearch
                                  {
                                      Name = name1,
                                      Criteria = CreateAdvancedCriteria(1)
                                  };
            _memberSearchesCommand.CreateMemberSearch(owner, savedSearch);

            var gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
            Assert.AreEqual(name1, gotSearch.Name);

            // Update.

            savedSearch.Name = name2;

            if (string.IsNullOrEmpty(name2))
            {
                AssertException.Thrown<ValidationErrorsException>(() => _memberSearchesCommand.UpdateMemberSearch(owner, savedSearch), "A 'LinkMe.Framework.Utility.Validation.RequiredValidationError' error has occurred for the Name property.");
            }
            else
            {
                _memberSearchesCommand.UpdateMemberSearch(owner, savedSearch);

                gotSearch = _memberSearchesQuery.GetMemberSearch(savedSearch.Id);
                Assert.AreEqual(name2, gotSearch.Name);
            }
        }
    }
}