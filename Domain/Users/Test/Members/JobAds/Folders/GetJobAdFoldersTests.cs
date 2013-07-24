using LinkMe.Domain.Users.Members.JobAds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.Folders
{
    [TestClass]
    public class GetJobAdFoldersTests
        : JobAdFoldersTests
    {
        [TestMethod]
        public void TestGetFolders()
        {
            var member = CreateMember(1);
            var folders = _jobAdFoldersQuery.GetFolders(member);

            // Assert.

            AssertFolders(folders);

            foreach (var folder in folders)
                Assert.AreEqual(folder.FolderType == FolderType.Private, _jobAdFoldersCommand.CanRenameFolder(member, folder));
        }

        [TestMethod]
        public void TestGetMobileFolder()
        {
            var member = CreateMember(1);
            var mobileFolder = _jobAdFoldersQuery.GetMobileFolder(member);
            AssertMobileFolder(mobileFolder);

            Assert.IsFalse(_jobAdFoldersCommand.CanRenameFolder(member, mobileFolder));
        }
    }
}