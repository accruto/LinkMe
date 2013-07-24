using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.AcceptanceTest.Employers.Search
{
    [TestClass]
    public class SortOrderTests
        : SearchTests
    {
        private readonly ICandidateFlagListsQuery _candidateFlagListsQuery = Resolve<ICandidateFlagListsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

        private static readonly MemberSortOrder[] SortOrders = new[]
        {
            MemberSortOrder.Availability,
            MemberSortOrder.DateUpdated,
            MemberSortOrder.Distance,
            MemberSortOrder.FirstName,
            MemberSortOrder.Flagged,
            MemberSortOrder.Relevance,
            MemberSortOrder.Salary,
        };

        [TestMethod]
        public void TestDefaultSortOrder()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            Get(GetSearchUrl());
            _keywordsTextBox.Text = BusinessAnalyst;
            _searchButton.Click();

            AssertSortOrders(SortOrders, MemberSortOrder.Relevance, false);
        }

        [TestMethod]
        public void TestDateUpdated()
        {
            var member0 = CreateMember(0);
            var candidate0 = _candidatesQuery.GetCandidate(member0.Id);
            var resume0 = _resumesQuery.GetResume(candidate0.ResumeId.Value);
            UpdateMember(member0, candidate0, resume0, DateTime.Now.AddDays(-2));

            var member1 = CreateMember(1);
            var candidate1 = _candidatesQuery.GetCandidate(member1.Id);
            var resume1 = _resumesQuery.GetResume(candidate1.ResumeId.Value);
            UpdateMember(member1, candidate1, resume1, DateTime.Now.AddDays(-1));

            var employer = CreateEmployer();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            TestSortOrder(employer, criteria, MemberSortOrder.DateUpdated, member0, member1);
        }

        [TestMethod]
        public void TestFirstName()
        {
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            var employer = CreateEmployer();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            TestSortOrder(employer, criteria, MemberSortOrder.FirstName, member1, member0);
        }

        [TestMethod]
        public void TestFlagged()
        {
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);

            var employer = CreateEmployer();
            _candidateListsCommand.AddCandidateToFlagList(employer, _candidateFlagListsQuery.GetFlagList(employer), member1.Id);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            TestSortOrder(employer, criteria, MemberSortOrder.Flagged, member0, member1);
        }

        [TestMethod]
        public void TestDistance()
        {
            // Hallam is within 100 km of Melbourne.

            const string location0 = "Melbourne VIC 3000";
            const string location1 = "Hallam VIC 3803";

            var member0 = CreateMember(0);
            member0.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location0);
            _memberAccountsCommand.UpdateMember(member0);
            _memberSearchService.UpdateMember(member0.Id);

            var member1 = CreateMember(1);
            member1.Address.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location1);
            _memberAccountsCommand.UpdateMember(member1);
            _memberSearchService.UpdateMember(member1.Id);

            var employer = CreateEmployer();
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            criteria.Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry("Australia"), location0);
            criteria.Distance = 100;

            TestSortOrder(employer, criteria, MemberSortOrder.Distance, member1, member0);
        }

        [TestMethod]
        public void TestCurrentSearchSortOrder()
        {
            // Default.

            Get(GetSearchUrl(BusinessAnalyst));
            Get(GetResultsUrl());
            AssertSortOrders(SortOrders, MemberSortOrder.Relevance, false);

            // Change.

            Get(GetSearchUrl(BusinessAnalyst, MemberSortOrder.FirstName, true));
            Get(GetResultsUrl());
            AssertSortOrders(SortOrders, MemberSortOrder.FirstName, true);
        }

        private void TestSortOrder(IUser employer, MemberSearchCriteria criteria, MemberSortOrder sortOrder, params Member[] members)
        {
            // Give the employer some credits to see the mmebers.

            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });

            // Add members to folder.

            LogIn(employer);

            // Test ascending.

            Get(GetSearchUrl(criteria, sortOrder, true));
            AssertSortOrders(SortOrders, sortOrder, true);
            AssertMembers(true, members);

            // Test descending, order should be reversed.

            Get(GetSearchUrl(criteria, sortOrder, false));
            AssertSortOrders(SortOrders, sortOrder, false);
            AssertMembers(true, members.Reverse().ToArray());
        }

        private void UpdateMember(Member member, Candidate candidate, Resume resume, DateTime lastUpdatedTime)
        {
            member.CreatedTime = lastUpdatedTime;
            member.LastUpdatedTime = lastUpdatedTime;
            _membersRepository.UpdateMember(member);

            candidate.LastUpdatedTime = lastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            resume.LastUpdatedTime = DateTime.Now.AddDays(-2);
            _resumesRepository.UpdateResume(resume);

            _memberSearchService.UpdateMember(member.Id);
        }
    }
}