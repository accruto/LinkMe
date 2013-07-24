using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using HtmlAgilityPack;
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
    public class ManageFoldersTests
        : FoldersTests
    {
        private const string FolderNameFormat = "My Folder {0}";
        private ReadOnlyUrl _newFolderUrl;
        private ReadOnlyUrl _baseFoldersUrl;
        private ReadOnlyUrl _flagListUrl;

        private const string PrivateFolderName = "PrivateFolder{0}";
        private const string SharedFolderName = "SharedFolder{0}";

        [TestInitialize]
        public void TestInitialize()
        {
            _flagListUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/flaglist");
            _baseFoldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/api/");
            _newFolderUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/api/new");
        }

        [TestMethod]
        public void TestNoFolders()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            // Check.

            AssertFolders(employer0, shortlistFolder0, new[] {GetMobileFolder(employer0)}, new CandidateFolder[0]);
            AssertFolders(employer1, shortlistFolder1, new[] {GetMobileFolder(employer1)}, new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestPrivateFoldersNoSharedFolders()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var folderIndex = 0;

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            // Create private folders for employer 0.

            var privateFolders0 = new List<CandidateFolder>();
            for (var index = 0; index < 2; ++index)
                privateFolders0.Add(CreatePrivateFolder(employer0, ++folderIndex));

            privateFolders0.Add(GetMobileFolder(employer0));

            // Create private folders for employer 1.

            var privateFolders1 = new List<CandidateFolder>();
            for (var index = 0; index < 3; ++index)
                privateFolders1.Add(CreatePrivateFolder(employer1, ++folderIndex));

            privateFolders1.Add(GetMobileFolder(employer1));

            // Check.

            AssertFolders(employer0, shortlistFolder0, privateFolders0, new CandidateFolder[0]);
            AssertFolders(employer1, shortlistFolder1, privateFolders1, new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestNoPrivateFoldersSharedFolders()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var folderIndex = 0;
            var sharedFolders = new List<CandidateFolder>();

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            // Create shared folders for employer 0.

            for (var index = 0; index < 2; ++index)
                sharedFolders.Add(CreateSharedFolder(employer0, ++folderIndex));

            // Create private folders for employer 1.

            for (var index = 0; index < 3; ++index)
                sharedFolders.Add(CreateSharedFolder(employer1, ++folderIndex));

            // Check.

            AssertFolders(employer0, shortlistFolder0, new[] {GetMobileFolder(employer0)}, sharedFolders);
            AssertFolders(employer1, shortlistFolder1, new[] {GetMobileFolder(employer1)}, sharedFolders);
        }

        [TestMethod]
        public void TestPrivateFoldersSharedFolders()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var folderIndex = 0;
            var sharedFolders = new List<CandidateFolder>();

            var shortlistFolder0 = _candidateFoldersQuery.GetShortlistFolder(employer0);
            var shortlistFolder1 = _candidateFoldersQuery.GetShortlistFolder(employer1);

            // Create private folders for employer 0.

            var privateFolders0 = new List<CandidateFolder>();
            for (var index = 0; index < 2; ++index)
                privateFolders0.Add(CreatePrivateFolder(employer0, ++folderIndex));

            privateFolders0.Add(GetMobileFolder(employer0));

            // Create private folders for employer 1.

            var privateFolders1 = new List<CandidateFolder>();
            for (var index = 0; index < 3; ++index)
                privateFolders1.Add(CreatePrivateFolder(employer1, ++folderIndex));

            privateFolders1.Add(GetMobileFolder(employer1));

            // Create shared folders for employer 0.

            for (var index = 0; index < 2; ++index)
                sharedFolders.Add(CreateSharedFolder(employer0, ++folderIndex));

            // Create private folders for employer 1.

            for (var index = 0; index < 3; ++index)
                sharedFolders.Add(CreateSharedFolder(employer1, ++folderIndex));

            // Check.

            AssertFolders(employer0, shortlistFolder0, privateFolders0, sharedFolders);
            AssertFolders(employer1, shortlistFolder1, privateFolders1, sharedFolders);
        }

        [TestMethod]
        public void TestAddFolder()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(GetFoldersUrl());

            // Add it, the ajax call refreshes the screen on a success.

            var name = string.Format(FolderNameFormat, 0);
            var folder = NewFolder(name, false).Folder;

            // Check.

            Get(GetFoldersUrl());
            AssertFolders(employer, _candidateFoldersQuery.GetShortlistFolder(employer), new[] { _candidateFoldersQuery.GetFolder(employer, folder.Id), GetMobileFolder(employer) }, new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestAddFolders()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(GetFoldersUrl());

            // Add it, the ajax call refreshes the screen on a success.

            var folders = new FolderModel[3];
            for (var index = 0; index < 3; ++index)
            {
                var name = string.Format(FolderNameFormat, index);
                folders[index] = NewFolder(name, false).Folder;
            }

            // Check.

            Get(GetFoldersUrl());

            var privateFolders = (from f in folders select _candidateFoldersQuery.GetFolder(employer, f.Id)).ToList();
            privateFolders.Add(GetMobileFolder(employer));

            AssertFolders(employer, _candidateFoldersQuery.GetShortlistFolder(employer), privateFolders.ToArray(), new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestRenameFolder()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(GetFoldersUrl());

            // Add it, the ajax call refreshes the screen on a success.

            var folders = new FolderModel[3];
            for (var index = 0; index < 3; ++index)
            {
                var name = string.Format(FolderNameFormat, index);
                folders[index] = NewFolder(name, false).Folder;
            }

            // Rename a folder.

            var newName = folders[1].Name + "1";
            RenameFolder(folders[1].Id, newName);
            folders[1] = new FolderModel {Id = folders[1].Id, Name = newName};

            Get(GetFoldersUrl());

            var privateFolders = (from f in folders select _candidateFoldersQuery.GetFolder(employer, f.Id)).ToList();
            privateFolders.Add(GetMobileFolder(employer));

            AssertFolders(employer, _candidateFoldersQuery.GetShortlistFolder(employer), privateFolders.ToArray(), new CandidateFolder[0]);
        }

        [TestMethod]
        public void TestRemoveFolder()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);

            Get(GetFoldersUrl());

            // Add some folders.

            var folders = new FolderModel[3];
            for (var index = 0; index < 3; ++index)
            {
                var name = string.Format(FolderNameFormat, index);
                folders[index] = NewFolder(name, false).Folder;
            }

            // Remove a folder.

            DeleteFolder(folders[1].Id);
            folders = folders.Take(1).Concat(folders.Skip(2).Take(1)).ToArray();

            Get(GetFoldersUrl());

            var privateFolders = (from f in folders select _candidateFoldersQuery.GetFolder(employer, f.Id)).ToList();
            privateFolders.Add(GetMobileFolder(employer));

            AssertFolders(employer, _candidateFoldersQuery.GetShortlistFolder(employer), privateFolders.ToArray(), new CandidateFolder[0]);
        }

        private CandidateFolder CreatePrivateFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(PrivateFolderName, index) };
            _candidateFoldersCommand.CreatePrivateFolder(employer, folder);
            return folder;
        }

        private CandidateFolder CreateSharedFolder(IEmployer employer, int index)
        {
            var folder = new CandidateFolder { Name = string.Format(SharedFolderName, index) };
            _candidateFoldersCommand.CreateSharedFolder(employer, folder);
            return folder;
        }

        private void AssertFolders(IUser employer, CandidateFolder shortlistFolder, ICollection<CandidateFolder> privateFolders, ICollection<CandidateFolder> sharedFolders)
        {
            LogIn(employer);
            Get(GetFoldersUrl());

            // Private folders.

            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='manage-folders']/div[position()=1]/div[@class='section-content']/table//tr");
            Assert.AreEqual(2 + privateFolders.Count, nodes.Count);

            // First one should be flag list.

            AssertFlagList(nodes[0]);

            // Next should be shortlist.

            AssertShortList(nodes[1], shortlistFolder);

            // The rest should be private folders.

            AssertPrivateFolders((from i in Enumerable.Range(2, nodes.Count - 2) select nodes[i]), employer as Employer, privateFolders);

            // Shared folders.

            nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='manage-folders']/div[position()=2]/div[@class='section-content']/table//tr");
            Assert.AreEqual(sharedFolders.Count, nodes == null ? 0 : nodes.Count);

            AssertSharedFolders(employer.Id, nodes, sharedFolders);
        }

        private void AssertSharedFolders(Guid employerId, IEnumerable<HtmlNode> nodes, IEnumerable<CandidateFolder> folders)
        {
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    var a = node.SelectSingleNode(".//a");
                    var name = a.InnerText;
                    var folder = (from f in folders where f.Name == name select f).Single();
                    Assert.AreEqual(GetFolderUrl(folder.Id).Path.ToLower(), a.Attributes["href"].Value.ToLower());
                    AssertRename(node, employerId == folder.RecruiterId);
                    AssertRemove(node, true, employerId == folder.RecruiterId);
                }
            }
        }

        private void AssertPrivateFolders(IEnumerable<HtmlNode> nodes, IEmployer employer, IEnumerable<CandidateFolder> folders)
        {
            foreach (var node in nodes)
            {
                var a = node.SelectSingleNode(".//a");
                var name = a.InnerText;
                var candidateFolder = (from f in folders where f.Name == name select f).Single();
                Assert.AreEqual(GetFolderUrl(candidateFolder.Id).Path.ToLower(), a.Attributes["href"].Value.ToLower());
                AssertRename(node, _candidateFoldersCommand.CanRenameFolder(employer, candidateFolder));
                AssertRemove(node, false, _candidateFoldersCommand.CanDeleteFolder(employer, candidateFolder));
            }
        }

        private void AssertShortList(HtmlNode node, CandidateFolder shortlistFolder)
        {
            var a = node.SelectSingleNode(".//a");
            Assert.AreEqual(string.IsNullOrEmpty(shortlistFolder.Name) ? "My shortlist" : shortlistFolder.Name, a.InnerText);
            Assert.AreEqual(GetFolderUrl(shortlistFolder.Id).Path.ToLower(), a.Attributes["href"].Value.ToLower());
            AssertRename(node, true);
            AssertRemove(node, false, false);
        }

        private void AssertFlagList(HtmlNode node)
        {
            var a = node.SelectSingleNode(".//a");
            Assert.AreEqual("Flagged candidates", a.InnerText);
            Assert.AreEqual(_flagListUrl.Path.ToLower(), a.Attributes["href"].Value.ToLower());
            AssertRename(node, false);
            AssertRemove(node, false, false);
        }

        private static void AssertRemove(HtmlNode node, bool isShared, bool isAllowed)
        {
            var a = isShared
                ? node.SelectSingleNode(".//a[@class='delete-action js_delete-folder js_Shared']")
                : node.SelectSingleNode(".//a[@class='delete-action js_delete-folder js_Private']");
            if (isAllowed)
            {
                Assert.IsNotNull(a);
                Assert.AreEqual("Remove", a.InnerText);
            }
            else
            {
                Assert.IsNull(a);
            }
        }

        private static void AssertRename(HtmlNode node, bool isAllowed)
        {
            var a = node.SelectSingleNode(".//a[@class='edit-action js_rename-folder']");
            if (isAllowed)
            {
                Assert.IsNotNull(a);
                Assert.AreEqual("Rename", a.InnerText);
            }
            else
            {
                Assert.IsNull(a);
            }
        }

        private JsonFolderModel NewFolder(string name, bool isShared, params Member[] members)
        {
            var parameters = new NameValueCollection { { "name", name }, { "isShared", isShared.ToString().ToLower() } };
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var response = Post(_newFolderUrl, parameters);
            return new JavaScriptSerializer().Deserialize<JsonFolderModel>(response);
        }

        private void RenameFolder(Guid folderId, string name)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/rename", new ReadOnlyQueryString("name", name));
            Post(url);
        }

        private void DeleteFolder(Guid folderId)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/delete");
            Post(url);
        }
    }
}