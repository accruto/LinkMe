using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.BlockLists
{
    [TestClass]
    public class ApiPermanentCandidatesTests
        : ApiBlockListsTests
    {
        [TestMethod]
        public void TestBlockPermanentCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetPermanentBlockList(employer);

            // Log in and add a candidate.

            LogIn(employer);

            var model = BlockPermanentCandidates(members[0]);
            AssertJsonSuccess(model);
            AssertCandidates(employer, blockList.Id, members[0]);

            // Add more.

            model = BlockPermanentCandidates(members[1], members[2]);
            AssertModel(3, model);
            AssertCandidates(employer, blockList.Id, members);

            // Add again.

            model = BlockPermanentCandidates(members);

            // Assert.

            AssertJsonSuccess(model);
            AssertCandidates(employer, blockList.Id, members);
        }

        [TestMethod]
        public void TestUnblockPermanentCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetPermanentBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = UnblockPermanentCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);

            // Remove again.

            model = UnblockPermanentCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);
        }

        [TestMethod]
        public void TestUnblockAllPermanentCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetPermanentBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = UnblockAllPermanentCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer, blockList.Id);

            // Remove again.

            model = UnblockAllPermanentCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer, blockList.Id);
        }

        private JsonListCountModel BlockPermanentCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "blockpermanentcandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel UnblockPermanentCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "unblockpermanentcandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel UnblockAllPermanentCandidates()
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "unblockallpermanentcandidates");

            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }
    }
}
