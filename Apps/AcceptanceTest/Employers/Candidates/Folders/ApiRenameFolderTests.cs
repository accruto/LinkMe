using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class ApiRenameFolderTests
        : ApiFoldersTests
    {
        private const string NewName = "Me new folder name";

        [TestMethod]
        public void TestRenamePrivateFolder()
        {
            TestRenameFolder(CreatePrivateFolder, false);
        }

        [TestMethod]
        public void TestRenameSharedFolder()
        {
            TestRenameFolder(CreatePrivateFolder, false);
        }

        [TestMethod]
        public void TestRenameShortlistFolder()
        {
            TestRenameFolder(GetShortlistFolder, false);
        }

        private void TestRenameFolder(Func<IEmployer, CandidateFolder> getFolder, bool isShared)
        {
            // Create a folder.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var folder = getFolder(employer);

            LogIn(employer);

            // Delete.

            var model = RenameFolder(folder.Id, NewName);

            // Assert.

            AssertJsonSuccess(model);
            AssertFolder(employer, NewName, isShared, _candidateFoldersQuery.GetFolder(employer, folder.Id));
        }

        private JsonResponseModel RenameFolder(Guid folderId, string name)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/rename", new ReadOnlyQueryString("name", name));
            return Deserialize<JsonResponseModel>(Post(url));
        }
    }
}
