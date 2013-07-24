using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
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
    public class ApiCandidatesTests
        : ApiFoldersTests
    {
        [TestMethod]
        public void TestAddCandidatesToPrivateFolder()
        {
            TestAddCandidatesToFolder(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestAddCandidatesToSharedFolder()
        {
            TestAddCandidatesToFolder(CreateSharedFolder);
        }

        [TestMethod]
        public void TestAddCandidatesToShortlistFolder()
        {
            TestAddCandidatesToFolder(GetShortlistFolder);
        }

        [TestMethod]
        public void TestAddCandidatesToMobileFolder()
        {
            TestAddCandidatesToFolder(GetMobileFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromPrivateFolder()
        {
            TestRemoveCandidatesFromFolder(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromSharedFolder()
        {
            TestRemoveCandidatesFromFolder(CreateSharedFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromShortlistFolder()
        {
            TestRemoveCandidatesFromFolder(GetShortlistFolder);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromMobileFolder()
        {
            TestRemoveCandidatesFromFolder(GetMobileFolder);
        }

        [TestMethod]
        public void TestCannotAddToOtherPrivateFolder()
        {
            TestCannotAddCandidatesToOtherFolder(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestCannotAddToOtherSharedFolder()
        {
            TestCannotAddCandidatesToOtherFolder(CreateSharedFolder);
        }

        [TestMethod]
        public void TestCannotAddToOtherShortlistFolder()
        {
            TestCannotAddCandidatesToOtherFolder(GetShortlistFolder);
        }

        [TestMethod]
        public void TestCannotAddToOtherMobileFolder()
        {
            TestCannotAddCandidatesToOtherFolder(GetMobileFolder);
        }

        [TestMethod]
        public void TestAddToOtherSharedFolderSameOrganisation()
        {
            TestAddCandidatesToOtherFolder(CreateSharedFolder);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherPrivateFolder()
        {
            TestCannotRemoveCandidatesFromOtherFolder(CreatePrivateFolder);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherSharedFolder()
        {
            TestCannotRemoveCandidatesFromOtherFolder(CreateSharedFolder);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherShortlistFolder()
        {
            TestCannotRemoveCandidatesFromOtherFolder(GetShortlistFolder);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherMobileFolder()
        {
            TestCannotRemoveCandidatesFromOtherFolder(GetMobileFolder);
        }

        [TestMethod]
        public void TestRemoveFromOtherSharedFolderSameOrganisation()
        {
            TestRemoveCandidatesFromOtherFolder(CreateSharedFolder);
        }

        private void TestAddCandidatesToFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var folder = getFolder(employer);

            // Log in and add candidates.

            LogIn(employer);
            var model = AddCandidates(folder.Id, members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, folder.Id, members);

            // Add again.

            model = AddCandidates(folder.Id, members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, folder.Id, members);
        }

        private void TestAddCandidatesToOtherFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, employer1.Organisation);
            var folder = getFolder(employer2);

            // Log in and add candidates.

            LogIn(employer1);
            var model = AddCandidates(folder.Id, members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer1, folder.Id, members);
            AssertCandidates(employer2, folder.Id, members);
        }

        private void TestCannotAddCandidatesToOtherFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestVerifiedOrganisation(2));
            var folder = getFolder(employer2);

            // Log in and add candidates.

            LogIn(employer1);
            var model = AddCandidates(HttpStatusCode.NotFound, folder.Id, members);

            // Assert.

            AssertJsonError(model, null, "400", "The folder cannot be found.");
            AssertCandidates(employer1, folder.Id);
            AssertCandidates(employer2, folder.Id);
        }

        private void TestRemoveCandidatesFromFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var folder = getFolder(employer);
            _candidateListsCommand.AddCandidatesToFolder(employer, folder, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = RemoveCandidates(folder.Id, members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, folder.Id, members[1]);

            // Remove again.

            model = RemoveCandidates(folder.Id, members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, folder.Id, members[1]);
        }

        private void TestCannotRemoveCandidatesFromOtherFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var folder = getFolder(employer2);
            _candidateListsCommand.AddCandidatesToFolder(employer2, folder, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer1);
            var model = RemoveCandidates(HttpStatusCode.NotFound, folder.Id, members[0], members[2]);

            // Assert.

            AssertJsonError(model, null, "400", "The folder cannot be found.");
            AssertCandidates(employer2, folder.Id, members);
        }

        private void TestRemoveCandidatesFromOtherFolder(Func<IEmployer, CandidateFolder> getFolder)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and folder.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, employer1.Organisation);
            var folder = getFolder(employer2);
            _candidateListsCommand.AddCandidatesToFolder(employer2, folder, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer1);
            var model = RemoveCandidates(folder.Id, members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer1, folder.Id, members[1]);
            AssertCandidates(employer2, folder.Id, members[1]);
        }

        private JsonListCountModel AddCandidates(Guid folderId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/addcandidates");
            var response = Post(url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel AddCandidates(HttpStatusCode expectedStatusCode, Guid folderId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/addcandidates");
            var response = Post(expectedStatusCode, url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCandidates(Guid folderId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/removecandidates");
            var response = Post(url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCandidates(HttpStatusCode expectedStatusCode, Guid folderId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseFoldersUrl, folderId + "/removecandidates");
            var response = Post(expectedStatusCode, url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private static NameValueCollection GetParameters(IEnumerable<Member> members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }
            return parameters;
        }
    }
}
