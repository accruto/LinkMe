using System;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Searches
{
    [TestClass]
    public class PermissionsTests
        : MemberSearchTests
    {
        [TestMethod, ExpectedException(typeof(MemberSearchesPermissionsException))]
        public void TestCannotUpdateOther()
        {
            // Create first.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = new MemberSearch { Name = SearchName, Criteria = CreateAdvancedCriteria(0) };
            _memberSearchesCommand.CreateMemberSearch(owner, search);

            var other = new Employer { Id = Guid.NewGuid() };
            _memberSearchesCommand.UpdateMemberSearch(other, search);
        }

        [TestMethod, ExpectedException(typeof(MemberSearchesPermissionsException))]
        public void TestCannotDeleteOther()
        {
            // Create first.

            var owner = new Employer { Id = Guid.NewGuid() };
            var search = new MemberSearch { Name = SearchName, Criteria = CreateAdvancedCriteria(0) };
            _memberSearchesCommand.CreateMemberSearch(owner, search);

            var other = new Employer { Id = Guid.NewGuid() };
            _memberSearchesCommand.DeleteMemberSearch(other, search.Id);
        }
    }
}