using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class BlockListMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        [TestMethod]
        public void TestFilterBlockListId()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in blockList.

            var blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            _candidateListsCommand.AddCandidateToBlockList(employer, blockList, member1.Id);

            // Filter.

            var allMemberIds = new[] { member1.Id, member2.Id };
            TestFilter(new[] { member2.Id }, employer, new MemberSearchQuery(), allMemberIds);
            TestBlockListFilter(new[] { member1.Id }, employer, blockList.Id, allMemberIds);
        }
    }
}
