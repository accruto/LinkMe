using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.JobAds.Folders
{
    [TestClass]
    public class RenameCandidateFoldersTests
        : JobAdFoldersTests
    {
        private const string NewName = "My new folder";

        [TestMethod]
        public void TestRenameFolder()
        {
            var member = CreateMember(1);
            var folder = GetFolder(member, 1);

            Assert.IsTrue(_jobAdFoldersCommand.CanRenameFolder(member, folder));
            _jobAdFoldersCommand.RenameFolder(member, folder, NewName);

            Assert.AreEqual(folder.Name, NewName);
            AssertFolder(member, folder, _jobAdFoldersQuery.GetFolder(member, folder.Id));
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestRenameFolderAlreadyExists()
        {
            var member = CreateMember(1);
            var folder1 = GetFolder(member, 1);
            var folder2 = GetFolder(member, 2);

            _jobAdFoldersCommand.RenameFolder(member, folder1, folder2.Name);
        }
    }
}