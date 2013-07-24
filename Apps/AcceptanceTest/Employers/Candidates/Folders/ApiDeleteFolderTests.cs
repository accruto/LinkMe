using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class ApiDeleteFolderTests
        : ApiFoldersTests
    {
        [TestMethod]
        public void TestDeletePrivateFolder()
        {
            TestDeleteFolder(false);
        }

        [TestMethod]
        public void TestDeleteSharedFolder()
        {
            TestDeleteFolder(true);
        }

        [TestMethod]
        public void TestDeleteShortlistFolder()
        {
            TestTryDeleteFolder();
        }

        [TestMethod]
        public void TestDeleteFlaggedFolder()
        {
            TestTryDeleteFolder();
        }

        private void TestDeleteFolder(bool isShared)
        {
            // Create a folder.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            var folder = new CandidateFolder { Name = string.Format(FolderNameFormat, 0) };
            if (isShared)
                _candidateFoldersCommand.CreateSharedFolder(employer, folder);
            else
                _candidateFoldersCommand.CreatePrivateFolder(employer, folder);

            LogIn(employer);

            // Delete.

            var model = DeleteFolder(folder.Id);

            // Assert.

            AssertJsonSuccess(model);
            Assert.IsNull(_candidateFoldersQuery.GetFolder(employer, folder.Id));
        }

        private void TestTryDeleteFolder()
        {
            // Create a folder.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);

            LogIn(employer);

            // Delete.

            var model = DeleteFolder(folder.Id);

            // Assert.

            AssertJsonError(model, null, "301", "You do not have sufficient permissions.");
            Assert.IsNotNull(_candidateFoldersQuery.GetFolder(employer, folder.Id));
        }

        private JsonResponseModel DeleteFolder(Guid folderId)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/delete");
            return Deserialize<JsonResponseModel>(Post(url));
        }
    }
}
