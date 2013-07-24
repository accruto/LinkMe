using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class RenameCandidateFoldersTests
        : CandidateFoldersTests
    {
        private const string NewName = "My new folder";

        [TestMethod]
        public void TestRenamePrivateFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreatePrivateFolder(employer, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanRenameFolder(employer, folder));
            _candidateFoldersCommand.RenameFolder(employer, folder, NewName);

            Assert.AreEqual(folder.Name, NewName);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));
            AssertFolder(employer, folder, _candidateFoldersQuery.GetPrivateFolders(employer));
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new[] { folder }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenamePrivateFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = CreatePrivateFolder(employer, 2);

            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);
        }

        [TestMethod]
        public void TestRenameSharedFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreateSharedFolder(employer, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanRenameFolder(employer, folder));
            _candidateFoldersCommand.RenameFolder(employer, folder, NewName);

            Assert.AreEqual(folder.Name, NewName);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, new[] { folder }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenameSharedFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreateSharedFolder(employer, 1);
            var folder2 = CreateSharedFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);
        }

        [TestMethod]
        public void TestRenameShortlistFolder()
        {
            var employer = CreateEmployer(1);
            var folder = GetShortlistFolder(employer);

            Assert.IsTrue(_candidateFoldersCommand.CanRenameFolder(employer, folder));
            _candidateFoldersCommand.RenameFolder(employer, folder, NewName);

            Assert.AreEqual(folder.Name, NewName);
            AssertFolder(employer, folder, _candidateFoldersQuery.GetFolder(employer, folder.Id));
            AssertFolder(employer, folder, _candidateFoldersQuery.GetShortlistFolder(employer));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, NewName, null, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestRenameMobileFolder()
        {
            var employer = CreateEmployer(1);
            var folder = GetMobileFolder(employer);

            Assert.IsFalse(_candidateFoldersCommand.CanRenameFolder(employer, folder));
            _candidateFoldersCommand.RenameFolder(employer, folder, NewName);
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenameShortlistFolderPrivateFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = GetShortlistFolder(employer);
            var folder2 = CreatePrivateFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenamePrivateFolderShortlistFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = GetShortlistFolder(employer);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenamePrivateFolderMobileFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = GetMobileFolder(employer);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);
        }

        [TestMethod]
        public void TestRenamePrivateFolderSharedFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreatePrivateFolder(employer, 1);
            var folder2 = CreateSharedFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);

            Assert.AreEqual(folder1.Name, folder2.Name);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetPrivateFolders(employer));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestRenameSharedFolderPrivateFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreateSharedFolder(employer, 1);
            var folder2 = CreatePrivateFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);

            Assert.AreEqual(folder1.Name, folder2.Name);
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetPrivateFolders(employer));
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, new[] { folder1, folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestRenameShortlistFolderSharedFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = GetShortlistFolder(employer);
            var folder2 = CreateSharedFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);

            Assert.AreEqual(folder1.Name, folder2.Name);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetShortlistFolder(employer));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolders(employer, folder1.Name, null, new[] { folder2 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestRenameSharedFolderShortlistFolderAlreadyExists()
        {
            var employer = CreateEmployer(1);
            var folder1 = CreateSharedFolder(employer, 1);
            var folder2 = GetShortlistFolder(employer, 2);
            _candidateFoldersCommand.RenameFolder(employer, folder2, NewName);
            _candidateFoldersCommand.RenameFolder(employer, folder1, folder2.Name);

            Assert.AreEqual(folder1.Name, folder2.Name);
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetFolder(employer, folder1.Id));
            AssertFolder(employer, folder1, _candidateFoldersQuery.GetSharedFolders(employer));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetFolder(employer, folder2.Id));
            AssertFolder(employer, folder2, _candidateFoldersQuery.GetShortlistFolder(employer));
            AssertFolders(employer, folder2.Name, null, new[] { folder1 }, _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestRenameSharedFolderSameOrganisation()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);
            var folder = CreateSharedFolder(employer1, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanRenameFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanRenameFolder(employer2, folder));

            _candidateFoldersCommand.RenameFolder(employer2, folder, NewName);
        }
    }
}