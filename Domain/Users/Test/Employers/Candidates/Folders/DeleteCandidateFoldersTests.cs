using LinkMe.Domain.Users.Employers.Candidates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Candidates.Folders
{
    [TestClass]
    public class DeleteCandidateFoldersTests
        : CandidateFoldersTests
    {
        [TestMethod]
        public void TestDeletePrivateFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreatePrivateFolder(employer, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanDeleteFolder(employer, folder));
            _candidateFoldersCommand.DeleteFolder(employer, folder.Id);

            Assert.IsNull(_candidateFoldersQuery.GetFolder(employer, folder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod]
        public void TestDeleteSharedFolder()
        {
            var employer = CreateEmployer(1);
            var folder = CreateSharedFolder(employer, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanDeleteFolder(employer, folder));
            _candidateFoldersCommand.DeleteFolder(employer, folder.Id);

            Assert.IsNull(_candidateFoldersQuery.GetFolder(employer, folder.Id));
            Assert.AreEqual(0, _candidateFoldersQuery.GetPrivateFolders(employer).Count);
            Assert.AreEqual(0, _candidateFoldersQuery.GetSharedFolders(employer).Count);
            AssertFolders(employer, new CandidateFolder[0], _candidateFoldersQuery.GetFolders(employer));
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteShortlistFolder()
        {
            var employer = CreateEmployer(1);
            var folder = GetShortlistFolder(employer);

            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer, folder));
            _candidateFoldersCommand.DeleteFolder(employer, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteMobileFolder()
        {
            var employer = CreateEmployer(1);
            var folder = GetMobileFolder(employer);

            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer, folder));
            _candidateFoldersCommand.DeleteFolder(employer, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteOtherPrivateFolder()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            var folder = CreatePrivateFolder(employer1, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanDeleteFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer2, folder));
            _candidateFoldersCommand.DeleteFolder(employer2, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteOtherSharedFolder()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            var folder = CreateSharedFolder(employer1, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanDeleteFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer2, folder));
            _candidateFoldersCommand.DeleteFolder(employer2, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteOtherShortlistFolder()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            var folder = GetShortlistFolder(employer1);

            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer2, folder));
            _candidateFoldersCommand.DeleteFolder(employer2, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteOtherMobileFolder()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2);
            var folder = GetMobileFolder(employer1);

            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer2, folder));
            _candidateFoldersCommand.DeleteFolder(employer2, folder.Id);
        }

        [TestMethod, ExpectedException(typeof(CandidateFoldersPermissionsException))]
        public void TestDeleteOtherSharedFolderSameOrganisation()
        {
            var employer1 = CreateEmployer(1);
            var employer2 = CreateEmployer(2, employer1.Organisation);
            var folder = CreateSharedFolder(employer1, 1);

            Assert.IsTrue(_candidateFoldersCommand.CanDeleteFolder(employer1, folder));
            Assert.IsFalse(_candidateFoldersCommand.CanDeleteFolder(employer2, folder));
            _candidateFoldersCommand.DeleteFolder(employer2, folder.Id);
        }
    }
}