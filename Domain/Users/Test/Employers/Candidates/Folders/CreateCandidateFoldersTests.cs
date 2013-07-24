using System.Linq;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class CreateCandidateFoldersTests
        : CandidateFoldersTests
    {
        [TestMethod]
        public void TestCreatePrivateFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreatePrivateFolder(employer, 1);

            // Assert.

            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));
            AssertFolder(employer, folder, _candidateFoldersQuery.GetPrivateFolders(employer));
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new[] {folder}, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestCreatePrivateFolders()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = CreatePrivateFolder(employer, 2);

            // Assert.

            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));

            var folders = _candidateFoldersQuery.GetPrivateFolders(employer);
            Assert.AreEqual(2, folders.Count);
            AssertFolder(employer, folder1, (from f in folders where f.Id == folder1.Id select f).Single());
            AssertFolder(employer, folder2, (from f in folders where f.Id == folder2.Id select f).Single());

            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestCreatePrivateFoldersSameName()
        {
            var employer = CreateEmployer(1);
            CreatePrivateFolder(employer, 1);
            CreatePrivateFolder(employer, 1);
        }

        [TestMethod]
        public void TestCreateSharedFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreateSharedFolder(employer, 1);

            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, new[] { folder }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestCreateSharedFolders()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreateSharedFolder(employer, 1);
            var folder2 = CreateSharedFolder(employer, 2);

            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);

            var folders = _candidateFoldersQuery.GetSharedFolders(employer);
            Assert.AreEqual(2, folders.Count);
            AssertFolder(employer, folder1, (from f in folders where f.Id == folder1.Id select f).Single());
            AssertFolder(employer, folder2, (from f in folders where f.Id == folder2.Id select f).Single());
            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestCreateSharedFoldersSameName()
        {
            var employer = CreateEmployer(1);
            CreateSharedFolder(employer, 1);
            CreateSharedFolder(employer, 1);
        }

        [TestMethod]
        public void TestCreatePrivateSharedFoldersSameName()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = CreateSharedFolder(employer, 1);

            // Assert.

            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetPrivateFolders(employer));

            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestGetShortListFolder()
        {
            var employer = CreateEmployer(1);
            var folder = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Shortlist };

            var shortlistFolder = _candidateFoldersQuery.GetShortlistFolder(employer);
            AssertFolder(employer, folder, shortlistFolder);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, shortlistFolder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestGetMobileFolder()
        {
            var employer = CreateEmployer(1);
            var folder = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Mobile };

            var mobileFolder = _candidateFoldersQuery.GetMobileFolder(employer);
            AssertFolder(employer, folder, mobileFolder);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, mobileFolder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestGetShortlistFlaggedFolders()
        {
            var employer = CreateEmployer(1);
            var folder1 = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Shortlist };

            var shortlistFolder = _candidateFoldersQuery.GetShortlistFolder(employer);
            AssertFolder(employer, folder1, shortlistFolder);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, shortlistFolder.Id));

            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestGetMobileFlaggedFolders()
        {
            var employer = CreateEmployer(1);
            var folder1 = new CandidateFolder { RecruiterId = employer.Id, FolderType = FolderType.Mobile };

            var mobileFolder = _candidateFoldersQuery.GetMobileFolder(employer);
            AssertFolder(employer, folder1, mobileFolder);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, mobileFolder.Id));

            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }
    }
}