using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Utility.Test;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.Folders
{
    [TestClass]
    public class ApiGetFoldersTests
        : ApiFoldersTests
    {
        private ReadOnlyUrl _foldersUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _foldersUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/folders/api");
        }

        [TestMethod]
        public void TestSpecialFolders()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            AssertModels(employer.Id, new CandidateFolder[0], new Dictionary<Guid, int>(), Folders());
        }

        [TestMethod]
        public void TestSpecialFoldersWithCandidates()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            var counts = new Dictionary<Guid, int>();

            var index = 0;
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, new[] {_memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id});
            counts[folder.Id] = 2;

            LogIn(employer);
            AssertModels(employer.Id, new CandidateFolder[0], counts, Folders());
        }

        [TestMethod]
        public void TestAllFoldersWithCandidates()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            IList<CandidateFolder> folders = new List<CandidateFolder>();
            var counts = new Dictionary<Guid, int>();

            // Shortlist.

            var index = 0;
            var folder = _candidateFoldersQuery.GetShortlistFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[folder.Id] = 4;
            AssertModels(employer.Id, folders, counts, Folders());

            // Mobile.

            folder = _candidateFoldersQuery.GetMobileFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[folder.Id] = 3;
            AssertModels(employer.Id, folders, counts, Folders());

            // Private 1.

            var folder0 = CreatePrivateFolder(employer, 0);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder0, new[] { _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[folder0.Id] = 1;

            // Private 2.

            TestUtils.SleepForDifferentSqlTimestamp();

            var folder1 = CreatePrivateFolder(employer, 1);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder1, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[folder1.Id] = 2;

            // The order is determined by when the last candidate is added so folder1 will appear before folder0.

            folders.Add(folder1);
            folders.Add(folder0);
            AssertModels(employer.Id, folders, counts, Folders());

            // Shared 1.

            folder = CreateSharedFolder(employer, 0);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            folders.Add(folder);
            counts[folder.Id] = 3;

            AssertModels(employer.Id, folders, counts, Folders());
        }

        private JsonFoldersModel Folders()
        {
            var response = Post(_foldersUrl);
            return new JavaScriptSerializer().Deserialize<JsonFoldersModel>(response);
        }

        private static void AssertModels(Guid employerId, ICollection<CandidateFolder> expectedFolders, IDictionary<Guid, int> expectedCounts, JsonFoldersModel model)
        {
            AssertModels(employerId, null, expectedFolders, expectedCounts, model);
        }

        private static void AssertModels(Guid employerId, string shortlistFolderName, ICollection<CandidateFolder> expectedFolders, IDictionary<Guid, int> expectedCounts, JsonFoldersModel model)
        {
            // Shortlist, flagged and mobile folders are always returned.

            AssertJsonSuccess(model);
            Assert.AreEqual(expectedFolders.Count + 3, model.Folders.Count);

            // Flagged folder should be first.

            var flagList = new CandidateFlagList { RecruiterId = employerId, FlagListType = FlagListType.Flagged };
            AssertModel(flagList, expectedCounts, false, false, model.Folders[0]);

            // Shortlist folder should be next.

            var shortlistFolder = new CandidateFolder { RecruiterId = employerId, FolderType = FolderType.Shortlist, Name = shortlistFolderName };
            AssertModel(shortlistFolder, expectedCounts, true, false, model.Folders[1]);

            // Mobile folder should be next.

            var mobileFolder = new CandidateFolder { RecruiterId = employerId, FolderType = FolderType.Mobile };
            AssertModel(mobileFolder, expectedCounts, false, false, model.Folders[2]);

            // Look for other folders.

            var index = 3;
            foreach (var expectedFolder in expectedFolders)
                AssertModel(expectedFolder, expectedCounts, true, true, model.Folders[index++]);
        }

        private static void AssertModel(CandidateFolder folder, IDictionary<Guid, int> expectedCounts, bool expectedCanRename, bool expectedCanDelete, FolderModel model)
        {
            Assert.AreEqual(folder.Name, model.Name);
            Assert.AreEqual(folder.FolderType.ToString(), model.Type);
            Assert.AreEqual(expectedCanRename, model.CanRename);
            Assert.AreEqual(expectedCanDelete, model.CanDelete);

            int expectedCount;
            expectedCounts.TryGetValue(model.Id, out expectedCount);
            Assert.AreEqual(expectedCount, model.Count);
        }

        private static void AssertModel(CandidateFlagList flagList, IDictionary<Guid, int> expectedCounts, bool expectedCanRename, bool expectedCanDelete, FolderModel model)
        {
            Assert.AreEqual(flagList.Name, model.Name);
            Assert.AreEqual(flagList.FlagListType.ToString(), model.Type);
            Assert.AreEqual(expectedCanRename, model.CanRename);
            Assert.AreEqual(expectedCanDelete, model.CanDelete);

            int expectedCount;
            expectedCounts.TryGetValue(model.Id, out expectedCount);
            Assert.AreEqual(expectedCount, model.Count);
        }
    }
}
