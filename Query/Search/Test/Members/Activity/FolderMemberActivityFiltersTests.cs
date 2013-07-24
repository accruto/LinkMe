using System;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class FolderMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        [TestMethod]
        public void TestFilterInFolders()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            TestFilter(employer, CreateInFolderQuery, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterFolderId()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member1.Id);

            // Filter.

            var allMemberIds = new[] {member1.Id, member2.Id};
            TestFilter(allMemberIds, employer, new MemberSearchQuery(), allMemberIds);
            TestFolderFilter(new[] { member1.Id }, employer, folder.Id, allMemberIds);
        }

        [TestMethod]
        public void TestFilterBlockInFolders()
        {
            var member = _membersCommand.CreateTestMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member in folder.

            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidateToFolder(employer, folder, member.Id);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateInFolderQuery(true), new[] { member.Id });

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateInFolderQuery(true), new[] { member.Id });
        }

        private static MemberSearchQuery CreateInFolderQuery(bool? value)
        {
            return new MemberSearchQuery { InFolder = value };
        }
    }
}
