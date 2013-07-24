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

namespace LinkMe.AcceptanceTest.Employers.Candidates.BlockLists
{
    [TestClass]
    public class ApiCandidatesTests
        : ApiBlockListsTests
    {
        [TestMethod]
        public void TestAddCandidatesToTemporaryBlockList()
        {
            TestAddCandidatesToBlockList(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestAddCandidatesToPermanentBlockList()
        {
            TestAddCandidatesToBlockList(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromTemporaryBlockList()
        {
            TestRemoveCandidatesFromBlockList(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromPermanentBlockList()
        {
            TestRemoveCandidatesFromBlockList(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestCannotAddToOtherTemporaryBlockList()
        {
            TestCannotAddCandidatesToOtherBlockList(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestCannotAddToOtherPermanentBlockList()
        {
            TestCannotAddCandidatesToOtherBlockList(GetPermanentBlockList);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherTemporaryBlockList()
        {
            TestCannotRemoveCandidatesFromOtherBlockList(GetTemporaryBlockList);
        }

        [TestMethod]
        public void TestCannotRemoveFromOtherPermanentBlockList()
        {
            TestCannotRemoveCandidatesFromOtherBlockList(GetPermanentBlockList);
        }

        private void TestAddCandidatesToBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = getBlockList(employer);

            // Log in and add candidates.

            LogIn(employer);
            var model = AddCandidates(blockList.Id, members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, blockList.Id, members);

            // Add again.

            model = AddCandidates(blockList.Id, members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, blockList.Id, members);
        }

        private void TestCannotAddCandidatesToOtherBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestVerifiedOrganisation(2));
            var blockList = getBlockList(employer2);

            // Log in and add candidates.

            LogIn(employer1);
            var model = AddCandidates(HttpStatusCode.NotFound, blockList.Id, members);

            // Assert.

            AssertJsonError(model, null, "400", "The blocklist cannot be found.");
            AssertCandidates(employer1, blockList.Id);
            AssertCandidates(employer2, blockList.Id);
        }

        private void TestRemoveCandidatesFromBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = getBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = RemoveCandidates(blockList.Id, members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);

            // Remove again.

            model = RemoveCandidates(blockList.Id, members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);
        }

        private void TestCannotRemoveCandidatesFromOtherBlockList(Func<IEmployer, CandidateBlockList> getBlockList)
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer1 = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var employer2 = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestVerifiedOrganisation(1));
            var blockList = getBlockList(employer2);
            _candidateListsCommand.AddCandidatesToBlockList(employer2, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer1);
            var model = RemoveCandidates(HttpStatusCode.NotFound, blockList.Id, members[0], members[2]);

            // Assert.

            AssertJsonError(model, null, "400", "The blocklist cannot be found.");
            AssertCandidates(employer2, blockList.Id, members);
        }

        private JsonListCountModel AddCandidates(Guid blockListId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListId + "/addcandidates");
            var response = Post(url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel AddCandidates(HttpStatusCode expectedStatusCode, Guid blockListId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListId + "/addcandidates");
            var response = Post(expectedStatusCode, url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCandidates(Guid blockListId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListId + "/removecandidates");
            var response = Post(url, GetParameters(members));
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCandidates(HttpStatusCode expectedStatusCode, Guid blockListId, params Member[] members)
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, blockListId + "/removecandidates");
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
