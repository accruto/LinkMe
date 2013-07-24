using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class AddCandidatesToCandidateFoldersTests
        : CandidateFoldersTests
    {
        [TestMethod]
        public void TestAddCandidatePrivateFolder()
        {
            TestAddCandidate(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestAddCandidateSharedFolder()
        {
            TestAddCandidate(CreateSharedFolder);
        }

        [TestMethod]
        public void TestAddCandidateShortlistFolder()
        {
            TestAddCandidate(GetShortlistFolder);
        }

        [TestMethod]
        public void TestAddCandidateMobileFolder()
        {
            TestAddCandidate(GetMobileFolder);
        }

        [TestMethod]
        public void TestAddCandidatesPrivateFolder()
        {
            TestAddCandidates(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestAddCandidatesSharedFolder()
        {
            TestAddCandidates(CreateSharedFolder);
        }

        [TestMethod]
        public void TestAddCandidatesShortlistFolder()
        {
            TestAddCandidates(GetShortlistFolder);
        }

        [TestMethod]
        public void TestAddCandidatesMobileFolder()
        {
            TestAddCandidates(GetMobileFolder);
        }

        [TestMethod]
        public void TestAddExistingCandidatesPrivateFolder()
        {
            TestAddExistingCandidates(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestAddExistingCandidatesSharedFolder()
        {
            TestAddExistingCandidates(CreateSharedFolder);
        }

        [TestMethod]
        public void TestAddExistingCandidatesShortlistFolder()
        {
            TestAddExistingCandidates(GetShortlistFolder);
        }

        [TestMethod]
        public void TestAddExistingCandidatesMobileFolder()
        {
            TestAddExistingCandidates(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherPrivateFolder()
        {
            TestAddToOtherFolder(CreatePrivateFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherSharedFolder()
        {
            TestAddToOtherFolder(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherShortlistFolder()
        {
            TestAddToOtherFolder(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherMobileFolder()
        {
            TestAddToOtherFolder(GetMobileFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherPrivateFolderSameOrganisation()
        {
            TestAddToOtherFolderSameOrganisation(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestCanAddToOtherSharedFolderSameOrganisation()
        {
            TestAddToOtherFolderSameOrganisation(CreateSharedFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherShortlistFolderSameOrganisation()
        {
            TestAddToOtherFolderSameOrganisation(GetShortlistFolder);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestCannotAddToOtherMobileFolderSameOrganisation()
        {
            TestAddToOtherFolderSameOrganisation(GetMobileFolder);
        }

        [TestMethod]
        public void TestAddToMultipleFolders()
        {
            const int count = 10;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);
            var employer3 = CreateEmployer(3);

            var folder1 = CreatePrivateFolder(employer1, 1);
            var folder2 = CreateSharedFolder(employer2, 2);
            var folder3 = GetShortlistFolder(employer1);
            var folder4 = CreatePrivateFolder(employer2, 4);
            var folder5 = CreatePrivateFolder(employer3, 6);

            Assert.AreEqual(2, _candidateListsCommand.AddCandidatesToFolder(employer1, folder1, new[] { members[0].Id, members[1].Id }));
            Assert.AreEqual(1, _candidateListsCommand.AddCandidatesToFolder(employer2, folder2, new[] { members[2].Id }));
            Assert.AreEqual(1, _candidateListsCommand.AddCandidatesToFolder(employer1, folder3, new[] { members[3].Id }));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidatesToFolder(employer2, folder4, new[] { members[6].Id, members[7].Id }));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidatesToFolder(employer3, folder5, new[] { members[8].Id, members[9].Id }));

            AssertFolderEntries(employer1, folder1, new[] { members[0], members[1] }, new[] { members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, folder1, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer3, folder1, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });

            AssertFolderEntries(employer1, folder2, new[] { members[2] }, new[] { members[0], members[1], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, folder2, new[] { members[2] }, new[] { members[0], members[1], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer3, folder2, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });

            AssertFolderEntries(employer1, folder3, new[] { members[3] }, new[] { members[0], members[1], members[2], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, folder3, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer3, folder3, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });

            AssertFolderEntries(employer1, folder4, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, folder4, new[] { members[6], members[7] }, new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[8], members[9] });
            AssertFolderEntries(employer3, folder4, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });

            AssertFolderEntries(employer1, folder5, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, folder5, new Member[0], new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer3, folder5, new[] { members[8], members[9] }, new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7] });
            
            AssertFolderEntries(employer1, new[] { members[0], members[1], members[2], members[3] }, new[] { members[4], members[5], members[6], members[7], members[8], members[9] });
            AssertFolderEntries(employer2, new[] { members[2], members[6], members[7] }, new[] { members[0], members[1], members[3], members[4], members[5], members[8], members[9] });
            AssertFolderEntries(employer3, new[] { members[8], members[9] }, new[] { members[0], members[1], members[2], members[3], members[4], members[5], members[6], members[7] });
        }

        private void TestAddToOtherFolder(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);

            // Get the folder for employer 2.

            var folder = getFolder(employer2, 1);

            // Employer 1 tries to add candidate to that folder.

            _candidateListsCommand.AddCandidateToFolder(employer1, folder, member.Id);
        }

        private void TestAddToOtherFolderSameOrganisation(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            // Create member and employers.

            var member = CreateMember(1);
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);

            // Get the folder for employer 2.

            var folder = getFolder(employer2, 1);

            // Employer 1 tries to add candidate to that folder.

            var count = _candidateListsCommand.AddCandidateToFolder(employer1, folder, member.Id);

            // If that succeeded then the entry should be there and visible to both.

            Assert.AreEqual(1, count);
            AssertFolderEntries(employer1, folder, new[] {member}, new Member[0]);
            AssertFolderEntries(employer1, new[] { member }, new Member[0]);
            AssertFolderEntries(employer2, folder, new[] { member }, new Member[0]);
            AssertFolderEntries(employer2, new[] { member }, new Member[0]);
        }

        private void TestAddCandidate(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create folder.

            var employer = CreateEmployer(1);
            var folder = getFolder(employer, 1);

            // Add candidates.

            for (var index = 0; index < count; ++index)
            {
                var folderCount = _candidateListsCommand.AddCandidateToFolder(employer, folder, members[index].Id);
                Assert.AreEqual(index + 1, folderCount);
            }

            // Assert.

            AssertFolderEntries(employer, folder, members, new Member[0]);
            AssertFolderEntries(employer, members, new Member[0]);
        }

        private void TestAddCandidates(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create folder.

            var employer = CreateEmployer(1);
            var folder = getFolder(employer, 1);

            // Add candidates.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id));

            // Assert.

            AssertFolderEntries(employer, folder, members, new Member[0]);
            AssertFolderEntries(employer, members, new Member[0]);
        }

        private void TestAddExistingCandidates(Func<IEmployer, int, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            // Create folder.

            var employer = CreateEmployer(1);
            var folder = getFolder(employer, 1);

            // Add a couple of candidates.

            Assert.AreEqual(1, _candidateListsCommand.AddCandidateToFolder(employer, folder, members[0].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFolder(employer, folder, members[1].Id));
            Assert.AreEqual(2, _candidateListsCommand.AddCandidateToFolder(employer, folder, members[0].Id));

            // Add all again.

            Assert.AreEqual(3, _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id));

            // Assert.

            AssertFolderEntries(employer, folder, members, new Member[0]);
            AssertFolderEntries(employer, members, new Member[0]);
        }
    }
}