using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.BlockLists
{
    [TestClass]
    public class ApiGetBlockListsTests
        : ApiBlockListsTests
    {
        private ReadOnlyUrl _blockListsUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _blockListsUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blockLists/api");
        }

        [TestMethod]
        public void TestSpecialBlockLists()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            var models = BlockLists();
            AssertModels(employer.Id, new Dictionary<Guid, int>(), models);
        }

        [TestMethod]
        public void TestSpecialBlockListsWithCandidates()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            var counts = new Dictionary<Guid, int>();

            var index = 0;
            var blockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[blockList.Id] = 2;

            LogIn(employer);
            var models = BlockLists();
            AssertModels(employer.Id, counts, models);
        }

        [TestMethod]
        public void TestAllBlockListsWithCandidates()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            LogIn(employer);

            var counts = new Dictionary<Guid, int>();

            // Temporary.

            var index = 0;

            var blockList = _candidateBlockListsQuery.GetTemporaryBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[blockList.Id] = 2;

            var models = BlockLists();
            AssertModels(employer.Id, counts, models);

            // Permanent.

            blockList = _candidateBlockListsQuery.GetPermanentBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, new[] { _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id, _memberAccountsCommand.CreateTestMember(++index).Id });
            counts[blockList.Id] = 3;

            models = BlockLists();
            AssertModels(employer.Id, counts, models);
        }

        private JsonBlockListsModel BlockLists()
        {
            var response = Post(_blockListsUrl);
            return new JavaScriptSerializer().Deserialize<JsonBlockListsModel>(response);
        }

        private static void AssertModels(Guid employerId, IDictionary<Guid, int> expectedCounts, JsonBlockListsModel model)
        {
            // Shortlist and flagged are always returned.

            AssertJsonSuccess(model);
            Assert.AreEqual(2, model.BlockLists.Count);

            // Temporary blockList should be first.

            var temporaryBlockList = new CandidateBlockList { RecruiterId = employerId, BlockListType = BlockListType.Temporary };
            AssertModel(temporaryBlockList, expectedCounts, model.BlockLists[0]);

            // Permanent blockList should be next.

            var permanentBlockList = new CandidateBlockList { RecruiterId = employerId, BlockListType = BlockListType.Permanent };
            AssertModel(permanentBlockList, expectedCounts, model.BlockLists[1]);
        }

        private static void AssertModel(CandidateBlockList blockList, IDictionary<Guid, int> expectedCounts, BlockListModel model)
        {
            Assert.AreEqual(blockList.BlockListType.ToString(), model.Type);

            int expectedCount;
            expectedCounts.TryGetValue(model.Id, out expectedCount);
            Assert.AreEqual(expectedCount, model.Count);
        }
    }
}
