using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Asp.Json.Models;
using LinkMe.Apps.Asp.Test.Html;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Resumes.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates.FlagLists
{
    [TestClass]
    public class ApiCandidatesTests
        : ApiFlagListsTests
    {
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IResumesQuery _resumesQuery = Resolve<IResumesQuery>();
        private ReadOnlyUrl _searchUrl;
        private HtmlTextBoxTester _keywordsTextBox;
        private HtmlButtonTester _searchButton;

        [TestInitialize]
        public void TestInitialize()
        {
            ClearSearchIndexes();

            _searchUrl = new ReadOnlyApplicationUrl(true, "~/search/candidates");

            _keywordsTextBox = new HtmlTextBoxTester(Browser, "Keywords");
            _searchButton = new HtmlButtonTester(Browser, "search");
        }

        [TestMethod]
        public void TestAddCandidatesToFlagList()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and flagList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));

            // Log in and add candidates.

            LogIn(employer);
            var model = AddCandidates(members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, members);

            // Add again.

            model = AddCandidates(members);

            // Assert.

            AssertModel(3, model);
            AssertCandidates(employer, members);
        }

        [TestMethod]
        public void TestRemoveCandidatesFromFlagList()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and flagList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var flagList = GetFlagList(employer);
            _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id);

            // Log in and remove candidates.

            LogIn(employer);
            var model = RemoveCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, members[1]);

            // Remove again.

            model = RemoveCandidates(members[0], members[2]);

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, members[1]);
        }

        [TestMethod]
        public void TestRemoveAllCandidatesFromFlagList()
        {
            const int count = 3;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = _memberAccountsCommand.CreateTestMember(index);

            // Create employer and flagList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var flagList = GetFlagList(employer);
            _candidateListsCommand.AddCandidatesToFlagList(employer, flagList, from m in members select m.Id);

            // Log in and remove all candidates.

            LogIn(employer);
            var model = RemoveAllCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer);

            // Remove all again.

            model = RemoveAllCandidates();

            // Assert.

            AssertModel(0, model);
            AssertCandidates(employer);
        }

        [TestMethod]
        public void TestRemoveCurrentCandidatesFromFlagList()
        {
            const string keyWords = "business analyst";

            const int count = 3;
            var members = new Member[count];
            var candidates = new Candidate[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                candidates[index] = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidates[index]);
            }

            // Change so that they don't appear in search.

            var resume = _resumesQuery.GetResume(candidates[1].ResumeId.Value);
            resume.Jobs = new List<Job> {new Job {Title = "Architect"}};
            _candidateResumesCommand.UpdateResume(candidates[1], resume);

            // 0 + 1 are flagged, 0 + 2 in search.

            // Create employer and flagList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var flagList = GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[0].Id);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[1].Id);
            AssertCandidates(employer, members[0], members[1]);

            // Log in and search.

            LogIn(employer);
            _keywordsTextBox.Text = keyWords;
            _searchButton.Click();
            AssertSearchCandidates(members[0], members[2]);

            // Remove current candidates.
            
            var model = RemoveCurrentCandidates();

            // Assert that 1 is still in search but that 0 has been removed.

            AssertModel(1, model);
            AssertCandidates(employer, members[1]);

            // Search and remove all again.

            Get(_searchUrl);
            _keywordsTextBox.Text = keyWords;
            _searchButton.Click();
            AssertSearchCandidates(members[0], members[2]);

            model = RemoveCurrentCandidates();

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, members[1]);
        }

        [TestMethod]
        public void TestRemoveCurrentCandidatesFromFlagListWithNoCurrentSearch()
        {
            const string keyWords = "business analyst";

            const int count = 3;
            var members = new Member[count];
            var candidates = new Candidate[count];
            for (var index = 0; index < count; ++index)
            {
                members[index] = _memberAccountsCommand.CreateTestMember(index);
                candidates[index] = _candidatesQuery.GetCandidate(members[index].Id);
                _candidateResumesCommand.AddTestResume(candidates[index]);
            }

            // Change so that they don't appear in search.

            var resume = _resumesQuery.GetResume(candidates[1].ResumeId.Value);
            resume.Jobs = new List<Job> { new Job { Title = "Architect" } };
            _candidateResumesCommand.UpdateResume(candidates[1], resume);

            // 0 + 1 are flagged, 0 + 2 in search.

            // Create employer and flagList.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            var flagList = GetFlagList(employer);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[0].Id);
            _candidateListsCommand.AddCandidateToFlagList(employer, flagList, members[1].Id);
            AssertCandidates(employer, members[0], members[1]);

            // Log in but don't search.

            LogIn(employer);

            // Remove current candidates.

            var model = RemoveCurrentCandidates();

            // Assert that 0 + 1 are still flagged.

            AssertModel(2, model);
            AssertCandidates(employer, members[0], members[1]);

            // Search and remove.

            Get(_searchUrl);
            _keywordsTextBox.Text = keyWords;
            _searchButton.Click();
            AssertSearchCandidates(members[0], members[2]);

            model = RemoveCurrentCandidates();

            // Assert.

            AssertModel(1, model);
            AssertCandidates(employer, members[1]);
        }

        private void AssertSearchCandidates(params Member[] members)
        {
            var nodes = Browser.CurrentHtml.DocumentNode.SelectNodes("//div[@class='expanded_search-results']/div[position()=1]");
            Assert.AreEqual(members.Length, nodes.Count);
            foreach (var node in nodes)
                Assert.AreEqual(true, (from m in members where m.Id == new Guid(node.Attributes["data-memberid"].Value) select m).Any());
        }

        private JsonListCountModel AddCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseFlagListsUrl, "addcandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCandidates(params Member[] members)
        {
            var parameters = new NameValueCollection();
            if (members != null)
            {
                foreach (var member in members)
                    parameters.Add("candidateId", member.Id.ToString());
            }

            var url = new ReadOnlyApplicationUrl(_baseFlagListsUrl, "removecandidates");
            var response = Post(url, parameters);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveAllCandidates()
        {
            var url = new ReadOnlyApplicationUrl(_baseFlagListsUrl, "removeallcandidates");
            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }

        private JsonListCountModel RemoveCurrentCandidates()
        {
            var url = new ReadOnlyApplicationUrl(_baseFlagListsUrl, "removecurrentcandidates");
            var response = Post(url);
            return new JavaScriptSerializer().Deserialize<JsonListCountModel>(response);
        }
    }
}
