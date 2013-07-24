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
    public class ApiTemporaryCandidatesTests
        : ApiBlockListsTests
    {
        [TestMethod]
        public void TestBlockTemporaryCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetTemporaryBlockList(employer);

            // Log in and add a candidate.

            LogIn(employer);

            var model = BlockTemporaryCandidates(members[0]);
            AssertJsonSuccess(model);
            AssertCandidates(employer, blockList.Id, members[0]);

            // Add more.

            model = BlockTemporaryCandidates(members[1], members[2]);
            AssertModel(3, model);
            AssertCandidates(employer, blockList.Id, members);

            // Add again.

            model = BlockTemporaryCandidates(members);

            // Assert.

            AssertJsonSuccess(model);
            AssertCandidates(employer, blockList.Id, members);
        }

        [TestMethod]
        public void TestUnblockTemporaryCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetTemporaryBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = UnblockTemporaryCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);

            // Remove again.

            model = UnblockTemporaryCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, blockList.Id, members[1]);
        }

        [TestMethod]
        public void TestUnblockAllTemporaryCandidates()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and blockList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var blockList = GetTemporaryBlockList(employer);
            _candidateListsCommand.AddCandidatesToBlockList(employer, blockList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = UnblockAllTemporaryCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer, blockList.Id);

            // Remove again.

            model = UnblockAllTemporaryCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer, blockList.Id);
        }

        private JsonListCountModel BlockTemporaryCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "blocktemporarycandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel UnblockTemporaryCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "unblocktemporarycandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel UnblockAllTemporaryCandidates()
        {
            var url = new ReadOnlyApplicationUrl(_baseBlockListsUrl, "unblockalltemporarycandidates");

            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }
    }
}
