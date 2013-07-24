using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class SharedCandidateFoldersTests
        : CandidateFoldersTests
    {
        [TestMethod]
        public void TestNoFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            var mobileFolder0 = _candidateFoldersQuery.GetMobileFolder(employer0);
            var mobileFolder1 = _candidateFoldersQuery.GetMobileFolder(employer1);

            // Check.

            AssertFolders(employer0, shortlistFolder0, mobileFolder0, new CandidateFolder[0], new CandidateFolder[0]);
            AssertFolders(employer1, shortlistFolder1, mobileFolder1, new CandidateFolder[0], new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestPrivateFoldersNoSharedFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);
            var folderIndex = 0;

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            var mobileFolder0 = _candidateFoldersQuery.GetMobileFolder(employer0);
            var mobileFolder1 = _candidateFoldersQuery.GetMobileFolder(employer1);

            // Create private folders for employer 0.

            var privateFolders0 = new List<CandidateFolder>();
            for (var index = 0; index < 2; ++index)
                privateFolders0.Add(CreatePrivateFolder(employer0, ++folderIndex));

            // Create private folders for employer 1.

            var privateFolders1 = new List<CandidateFolder>();
            for (var index = 0; index < 3; ++index)
                privateFolders1.Add(CreatePrivateFolder(employer1, ++folderIndex));

            // Check.

            AssertFolders(employer0, shortlistFolder0, mobileFolder0, privateFolders0, new CandidateFolder[0]);
            AssertFolders(employer1, shortlistFolder1, mobileFolder1, privateFolders1, new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestNoPrivateFoldersSharedFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);
            var folderIndex = 0;
            var sharedFolders = new List<CandidateFolder>();

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            var mobileFolder0 = _candidateFoldersQuery.GetMobileFolder(employer0);
            var mobileFolder1 = _candidateFoldersQuery.GetMobileFolder(employer1);

            // Create shared folders for employer 0.

            for (var index = 0; index < 2; ++index)
                sharedFolders.Add(CreateSharedFolder(employer0, ++folderIndex));

            // Create shared folders for employer 1.

            for (var index = 0; index < 3; ++index)
                sharedFolders.Add(CreateSharedFolder(employer1, ++folderIndex));

            // Check.

            AssertFolders(employer0, shortlistFolder0, mobileFolder0, new CandidateFolder[0], sharedFolders);
            AssertFolders(employer1, shortlistFolder1, mobileFolder1, new CandidateFolder[0], sharedFolders);
        }

        [TestMethod]
        public void TestPrivateFoldersSharedFolders()
        {
            var employer0 = CreateEmployer(0);
            var employer1 = CreateEmployer(1, employer0.Organisation);
            var folderIndex = 0;
            var sharedFolders = new List<CandidateFolder>();

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            var mobileFolder0 = _candidateFoldersQuery.GetMobileFolder(employer0);
            var mobileFolder1 = _candidateFoldersQuery.GetMobileFolder(employer1);

            // Create private folders for employer 0.

            var privateFolders0 = new List<CandidateFolder>();
            for (var index = 0; index < 2; ++index)
                privateFolders0.Add(CreatePrivateFolder(employer0, ++folderIndex));

            // Create private folders for employer 1.

            var privateFolders1 = new List<CandidateFolder>();
            for (var index = 0; index < 3; ++index)
                privateFolders1.Add(CreatePrivateFolder(employer1, ++folderIndex));

            // Create shared folders for employer 0.

            for (var index = 0; index < 2; ++index)
                sharedFolders.Add(CreateSharedFolder(employer0, ++folderIndex));

            // Create shared folders for employer 1.

            for (var index = 0; index < 3; ++index)
                sharedFolders.Add(CreateSharedFolder(employer1, ++folderIndex));

            // Check.

            AssertFolders(employer0, shortlistFolder0, mobileFolder0, privateFolders0, sharedFolders);
            AssertFolders(employer1, shortlistFolder1, mobileFolder1, privateFolders1, sharedFolders);
        }

        private void AssertFolders(IEmployer employer, CandidateFolder shortlistFolder, CandidateFolder mobileFolder, ICollection<CandidateFolder> privateFolders, ICollection<CandidateFolder> sharedFolders)
        {
            // Shortlist folder.

            AssertFolder(employer, shortlistFolder, _candidateFoldersQuery.GetFolder(employer, shortlistFolder.Id));

            // Mobile folder.

            AssertFolder(employer, mobileFolder, _candidateFoldersQuery.GetFolder(employer, mobileFolder.Id));

            // Private folders.

            foreach (var folder in privateFolders)
                AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));

            var folders = _candidateFoldersQuery.GetPrivateFolders(employer);
            Assert.AreEqual(privateFolders.Count, folders.Count);
            foreach (var folder in privateFolders)
            {
                var folderId = folder.Id;
                AssertFolder(employer, folder, (from f in folders where f.Id == folderId select f).Single());
            }

            // Shared folders.

            foreach (var folder in sharedFolders)
                AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));

            folders = _candidateFoldersQuery.GetSharedFolders(employer);
            Assert.AreEqual(sharedFolders.Count, folders.Count);
            foreach (var folder in sharedFolders)
            {
                var folderId = folder.Id;
                AssertFolder(employer, folder, (from f in folders where f.Id == folderId select f).Single());
            }

            // All folders.

            folders = _candidateFoldersQuery.GetFolders(employer);
            Assert.AreEqual(1 + 1 + privateFolders.Count + sharedFolders.Count, folders.Count);
            AssertFolder(employer, shortlistFolder, (from f in folders where f.FolderType == FolderType.Shortlist select f).Single());
            AssertFolder(employer, mobileFolder, (from f in folders where f.FolderType == FolderType.Mobile select f).Single());

            foreach (var folder in privateFolders)
            {
                var folderId = folder.Id;
                AssertFolder(employer, folder, (from f in folders where f.Id == folderId select f).Single());
            }

            foreach (var folder in sharedFolders)
            {
                var folderId = folder.Id;
                AssertFolder(employer, folder, (from f in folders where f.Id == folderId select f).Single());
            }
        }
    }
}