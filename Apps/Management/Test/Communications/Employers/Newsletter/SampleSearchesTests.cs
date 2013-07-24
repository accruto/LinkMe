using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain;
using LinkMe.Domain.Roles.Candidates.Commands;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using LinkMe.Query.Search.Members.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Management.Test.Communications.Employers.Newsletter
{
    [TestClass]
    public class SampleSearchesTests
        : NewsletterTests
    {
        protected const string BusinessAnalyst = "business analyst";
        private const string SavedSearchName = "My saved search";
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly ICandidatesCommand _candidatesCommand = Resolve<ICandidatesCommand>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ICandidateResumesCommand _candidateResumesCommand = Resolve<ICandidateResumesCommand>();
        private readonly IMemberSearchesCommand _memberSearchesCommand = Resolve<IMemberSearchesCommand>();

        private enum Sample
        {
            ImmediatelyAvailable = 3,
            ActivelyLooking = 4,
            Indigenous = 5,
        }

        [TestInitialize]
        public void TestInitialize()
        {
            ClearCache();
        }

        [TestMethod]
        public void TestStatuses()
        {
            var employer = CreateEmployer(0);

            // Available now.

            var memberIndex = 0;
            const int availableNow = 4;
            for (var index = 0; index < availableNow; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(memberIndex++);
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                candidate.Status = CandidateStatus.AvailableNow;
                _candidatesCommand.UpdateCandidate(candidate);
            }

            // Actively looking.

            const int activelyLooking = 3;
            for (var index = 0; index < activelyLooking; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(memberIndex++);
                var candidate = _candidatesCommand.GetCandidate(member.Id);
                candidate.Status = CandidateStatus.ActivelyLooking;
                _candidatesCommand.UpdateCandidate(candidate);
            }

            // Indigenous.

            const int aboriginal = 2;
            for (var index = 0; index < aboriginal; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(memberIndex++);
                member.EthnicStatus = EthnicStatus.Aboriginal;
                _memberAccountsCommand.UpdateMember(member);
            }

            const int torreIslander = 3;
            for (var index = 0; index < torreIslander; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(memberIndex++);
                member.EthnicStatus = EthnicStatus.TorresIslander;
                _memberAccountsCommand.UpdateMember(member);
            }

            GetNewsletterUrl(employer.Id);

            var document = GetDocument();
            Assert.AreEqual(availableNow, GetSampleResults(document, Sample.ImmediatelyAvailable));
            Assert.AreEqual(activelyLooking + aboriginal + torreIslander, GetSampleResults(document, Sample.ActivelyLooking));
            Assert.AreEqual(aboriginal + torreIslander, GetSampleResults(document, Sample.Indigenous));
        }

        [TestMethod]
        public void TestSavedSearch()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var employer = CreateEmployer(0);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearch(employer, new MemberSearch { Name = SavedSearchName, Criteria = criteria });

            GetNewsletterUrl(employer.Id);

            var document = GetDocument();
            var previousSearches = GetPreviousSearches(document);
            Assert.AreEqual(1, previousSearches.Count);
            Assert.AreEqual("Keywords: " + BusinessAnalyst, previousSearches[0].Item1);
        }

        [TestMethod]
        public void TestPreviousSearch()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var employer = CreateEmployer(0);
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearchExecution(new MemberSearchExecution { SearcherId = employer.Id, Criteria = criteria, Results = new MemberSearchResults()});

            GetNewsletterUrl(employer.Id);

            var document = GetDocument();
            var previousSearches = GetPreviousSearches(document);
            Assert.AreEqual(1, previousSearches.Count);
            Assert.AreEqual("Keywords: " + BusinessAnalyst, previousSearches[0].Item1);
        }

        [TestMethod]
        public void TestOtherSavedSearch()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearch(employer1, new MemberSearch { Name = SavedSearchName, Criteria = criteria });

            GetNewsletterUrl(employer0.Id);

            var document = GetDocument();
            var previousSearches = GetPreviousSearches(document);
            Assert.AreEqual(1, previousSearches.Count);
            Assert.AreEqual("Keywords: " + BusinessAnalyst, previousSearches[0].Item1);
        }

        [TestMethod]
        public void TestOtherPreviousSearch()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearchExecution(new MemberSearchExecution { SearcherId = employer1.Id, Criteria = criteria, Results = new MemberSearchResults() });

            GetNewsletterUrl(employer0.Id);

            var document = GetDocument();
            var previousSearches = GetPreviousSearches(document);
            Assert.AreEqual(1, previousSearches.Count);
            Assert.AreEqual("Keywords: " + BusinessAnalyst, previousSearches[0].Item1);
        }

        [TestMethod]
        public void TestMultiplePreviousSearches()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer0 = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearchExecution(new MemberSearchExecution { SearcherId = employer0.Id, Criteria = criteria, Results = new MemberSearchResults() });

            criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            _memberSearchesCommand.CreateMemberSearchExecution(new MemberSearchExecution { SearcherId = employer1.Id, Criteria = criteria, Results = new MemberSearchResults() });

            GetNewsletterUrl(employer0.Id);

            var document = GetDocument();
            var previousSearches = GetPreviousSearches(document);
            Assert.AreEqual(1, previousSearches.Count);
            Assert.AreEqual("Keywords: " + BusinessAnalyst, previousSearches[0].Item1);
        }

        private static IList<Tuple<string, int>> GetPreviousSearches(HtmlDocument document)
        {
            var searches = new List<Tuple<string, int>>();

            foreach (var tr in document.DocumentNode.SelectNodes("//table[@id='previous']/tr[position()>=3]"))
            {
                var criteria = tr.SelectSingleNode("td[position()=1]/span").InnerText;
                var results = int.Parse(tr.SelectSingleNode("td[position()=2]/span").InnerText);
                searches.Add(new Tuple<string, int>(criteria, results));
            }

            return searches;
        }

        private HtmlDocument GetDocument()
        {
            var document = new HtmlDocument();
            document.LoadHtml(Browser.CurrentPageText);
            return document;
        }

        private static int GetSampleResults(HtmlDocument document, Sample sample)
        {
            return int.Parse(document.DocumentNode.SelectSingleNode("//table[@id='samples']/tr[position()=" + (int)sample + "]/td[position()=2]/span").InnerText);
        }
    }
}
