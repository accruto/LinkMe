using System.Collections.Specialized;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class ApiNewFolderTests
        : ApiFoldersTests
    {
        private ReadOnlyUrl _newFolderUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _newFolderUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/api/new");
        }

        [TestMethod]
        public void TestNewPrivateFolderNoCandidates()
        {
            TestNewFolder(false, 0);
        }

        [TestMethod]
        public void TestNewSharedFolderNoCandidates()
        {
            TestNewFolder(true, 0);
        }

        [TestMethod]
        public void TestNewPrivateFolderCandidates()
        {
            TestNewFolder(false, 3);
        }

        [TestMethod]
        public void TestNewSharedFolderCandidates()
        {
            TestNewFolder(true, 3);
        }

        [TestMethod]
        public void TestNewPrivateFolderSameName()
        {
            TestNewFolderSameName(false);
        }

        [TestMethod]
        public void TestNewSharedFolderSameName()
        {
            TestNewFolderSameName(true);
        }

        private void TestNewFolderSameName(bool isShared)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            // Create.

            var folderName = string.Format(FolderNameFormat, 0);
            NewFolder(folderName, isShared);

            // Create with same name.

            var model = NewFolder(folderName, isShared);
            AssertJsonError(model, "Name", "The name is already being used.");
        }

        private void TestNewFolder(bool isShared, int candidates)
        {
            // Create members.

            var members = new Member[candidates];
            for (var index = 0; index < candidates; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            // Create folder.

            var folderName = string.Format(FolderNameFormat, 0);
            var model = NewFolder(folderName, isShared, members);

            // Assert.

            AssertModel(folderName, isShared ? FolderType.Shared : FolderType.Private, candidates, model);
            AssertFolder(employer, folderName, isShared, _candidateFoldersQuery.GetFolder(employer, model.Folder.Id));
            AssertCandidates(employer, model.Folder.Id, members);
        }

        private JsonFolderModel NewFolder(string name, bool isShared, params Member[] members)
        {
            var parameters = GetParameters(name, isShared);
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var response = Post(_newFolderUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonFolderModel>(response);
        }

        private static NameValueCollection GetParameters(string name, bool isShared)
        {
            return new NameValueCollection
            {
                {"name", name},
                {"isShared", isShared.ToString().ToLower()}
            };
        }

        private static void AssertModel(string name, FolderType folderType, int candidates, JsonFolderModel model)
        {
            AssertJsonSuccess(model);
            Assert.AreEqual(name, model.Folder.Name);
            Assert.AreEqual(folderType.ToString(), model.Folder.Type);
            Assert.AreEqual(candidates, model.Folder.Count);
        }
    }
}
