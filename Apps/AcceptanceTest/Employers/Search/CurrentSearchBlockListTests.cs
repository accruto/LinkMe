using System.Collections.Specialized;
using System.Linq;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class CurrentSearchBlockListTests
        : SearchTests
    {
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();

        private ReadOnlyUrl _blockUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _blockUrl = new ReadOnlyApplicationUrl(true, "~/employers/candidates/blockLists/api/blocktemporarycandidates");
        }

        [TestMethod]
        public void TestIsBlocked()
        {
            // Do a search.

            var employer = CreateEmployer();
            LogIn(employer);

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(members);

            // Do a partial search.

            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members);

            // Temporarily block.

            BlockTemporaryCandidates(members[0]);
            Get(GetBlockListUrl(_candidateBlockListsQuery.GetTemporaryBlockList(employer).BlockListType));
            AssertMembers(members[0]);

            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members.Skip(1).ToArray());

            BlockTemporaryCandidates(members[1], members[3]);
            Get(GetBlockListUrl(_candidateBlockListsQuery.GetTemporaryBlockList(employer).BlockListType));
            AssertMembers(members[0], members[1], members[3]);

            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members[2]);
        }

        [TestMethod]
        public void TestBecomesUnblocked()
        {
            // Do a search.

            var employer = CreateEmployer();
            LogIn(employer);

            const int count = 4;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(members);

            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members);

            // Temporarily block.

            BlockTemporaryCandidates(members[0], members[1]);
            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members[2], members[3]);

            // Simply going to the search page should not unblock.

            Get(GetSearchUrl());
            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members[2], members[3]);

            // Doing a search should unblock.

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();
            AssertMembers(members);

            Get(GetPartialSearchUrl(BusinessAnalyst));
            AssertMembers(members);
        }

        private void BlockTemporaryCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var model = Deserialize<JsonResponseModel>(Post(_blockUrl, parameters));
            AssertJsonSuccess(model);
        }
    }
}
