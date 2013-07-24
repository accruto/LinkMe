using System;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class FlagListMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        [TestMethod]
        public void TestFilterFlagListId()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member1.Id);

            // Filter.

            var allMemberIds = new[] { member1.Id, member2.Id };
            TestFilter(allMemberIds, employer, new MemberSearchQuery(), allMemberIds);
            TestFlagListFilter(new[] { member1.Id }, employer, allMemberIds);
        }

        [TestMethod]
        public void TestFilterIsFlagged()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member1.Id);

            // Filter.

            TestFilter(employer, CreateIsFlaggedQuery, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsFlagged()
        {
            var member = _membersCommand.CreateTestMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var flagList = _candidateFlagListsQuery.GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateIsFlaggedQuery(true), new[] { member.Id });

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateIsFlaggedQuery(true), new[] { member.Id });
        }

        private static MemberSearchQuery CreateIsFlaggedQuery(bool? value)
        {
            return new MemberSearchQuery { IsFlagged = value };
        }
    }
}
