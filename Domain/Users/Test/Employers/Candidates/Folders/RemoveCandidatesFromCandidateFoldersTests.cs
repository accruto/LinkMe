using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class RemoveCandidatesFromCandidateFoldersTests
        : CandidateFoldersTests
    {
        [TestMethod]
        public void TestRemoveCandidatesPrivateFolder()
        {
            TestRemoveCandidates(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesSharedFolder()
        {
            TestRemoveCandidates(CreateSharedFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesShortlistFolder()
        {
            TestRemoveCandidates(GetShortlistFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesMobileFolder()
        {
            TestRemoveCandidates(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherPrivateFolder()
        {
            TestRemoveFromOtherFolder(CreatePrivateFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherSharedFolder()
        {
            TestRemoveFromOtherFolder(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherShortlistFolder()
        {
            TestRemoveFromOtherFolder(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherMobileFolder()
        {
            TestRemoveFromOtherFolder(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherPrivateFolderSameOrganisation()
        {
            TestRemoveFromOtherFolderSameOrganisation(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestCanRemoveFromOtherSharedFolderSameOrganisation()
        {
            TestRemoveFromOtherFolderSameOrganisation(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherShortlistFolderSameOrganisation()
        {
            TestRemoveFromOtherFolderSameOrganisation(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveFromOtherMobileFolderSameOrganisation()
        {
            TestRemoveFromOtherFolderSameOrganisation(GetMobileFolder);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesPrivateFolder()
        {
            TestRemoveAllCandidates(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesSharedFolder()
        {
            TestRemoveAllCandidates(CreateSharedFolder);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesShortlistFolder()
        {
            TestRemoveAllCandidates(GetShortlistFolder);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesMobileFolder()
        {
            TestRemoveAllCandidates(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherPrivateFolder()
        {
            TestRemoveAllFromOtherFolder(CreatePrivateFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherSharedFolder()
        {
            TestRemoveAllFromOtherFolder(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherShortlistFolder()
        {
            TestRemoveAllFromOtherFolder(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherMobileFolder()
        {
            TestRemoveAllFromOtherFolder(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherPrivateFolderSameOrganisation()
        {
            TestRemoveAllFromOtherFolderSameOrganisation(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestCanRemoveAllFromOtherSharedFolderSameOrganisation()
        {
            TestRemoveAllFromOtherFolderSameOrganisation(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherShortlistFolderSameOrganisation()
        {
            TestRemoveAllFromOtherFolderSameOrganisation(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotRemoveAllFromOtherMobileFolderSameOrganisation()
        {
            TestRemoveAllFromOtherFolderSameOrganisation(GetMobileFolder);
        }

        private void TestRemoveFromOtherFolder(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member.Id));

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidateFromFolder(employer1, folder, member.Id));
        }

        private void TestRemoveAllFromOtherFolder(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member1.Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member2.Id));

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(1, _candidateListsCommand.RemoveAllCandidatesFromFolder(employer1, folder));
        }

        private void TestRemoveFromOtherFolderSameOrganisation(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member.Id));

            // If that succeeded then the entry should be there.

            AssertFolderEntries(employer2, folder, new[] { member }, new Member[0]);
            AssertFolderEntries(employer2, new[] { member }, new Member[0]);

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(0, _candidateListsCommand.RemoveCandidateFromFolder(employer1, folder, member.Id));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertFolderEntries(employer1, folder, new Member[0], new[] { member });
            AssertFolderEntries(employer1, new Member[0], new[] { member });
            AssertFolderEntries(employer2, folder, new Member[0], new[] { member });
            AssertFolderEntries(employer2, new Member[0], new[] { member });
        }

        private void TestRemoveAllFromOtherFolderSameOrganisation(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the folder for employer 2 and have them add a candidate.

            var folder = getFolder(employer2, 1);
            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member1.Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFolder(employer2, folder, member2.Id));

            // If that succeeded then the entry should be there.

            AssertFolderEntries(employer2, folder, new[] { member1, member2 }, new Member[0]);
            AssertFolderEntries(employer2, new[] { member1, member2 }, new Member[0]);

            // Employer 1 tries to remove candidate from that folder.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFolder(employer1, folder));

            // If that succeeded then the entry should be now gone from both and visible to neither.

            AssertFolderEntries(employer1, folder, new Member[0], new[] { member1, member2 });
            AssertFolderEntries(employer1, new Member[0], new[] { member1, member2 });
            AssertFolderEntries(employer2, folder, new Member[0], new[] { member1, member2 });
            AssertFolderEntries(employer2, new Member[0], new[] { member1, member2 });
        }

        private void TestRemoveCandidates(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create folder.

            var employer = CreateEmployer(1);
            var folder = getFolder(employer, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id));
            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromFolder(employer, folder, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertFolderEntries(employer, folder, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
            AssertFolderEntries(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());

            // Remove candidates again.

            Assert.AreEqual(1, _candidateListsCommand.RemoveCandidatesFromFolder(employer, folder, new[] { members[0].Id, members[2].Id }));

            // Assert.

            AssertFolderEntries(employer, folder, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
            AssertFolderEntries(employer, members.Skip(1).Take(1).ToArray(), members.Take(1).Concat(members.Skip(2).Take(1)).ToArray());
        }

        private void TestRemoveAllCandidates(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create folder.

            var employer = CreateEmployer(1);
            var folder = getFolder(employer, 1);

            // Add candidates and remove candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id));
            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFolder(employer, folder));

            // Assert.

            AssertFolderEntries(employer, folder, new Member[0], members);
            AssertFolderEntries(employer, new Member[0], members);

            // Remove candidates again.

            Assert.AreEqual(0, _candidateListsCommand.RemoveAllCandidatesFromFolder(employer, folder));

            // Assert.

            AssertFolderEntries(employer, folder, new Member[0], members);
            AssertFolderEntries(employer, new Member[0], members);
        }
    }
}