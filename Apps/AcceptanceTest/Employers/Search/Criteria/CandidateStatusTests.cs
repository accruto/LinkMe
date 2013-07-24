using System;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class CandidateStatusTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.CandidateStatusFlags = null;
            TestDisplay(criteria);
            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking;
            TestDisplay(criteria);
            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers;
            TestDisplay(criteria);
            criteria.CandidateStatusFlags = CandidateStatusFlags.All;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestCandidateStatus()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var member0 = CreateMember(0, CandidateStatus.AvailableNow);
            var member1 = CreateMember(1, CandidateStatus.ActivelyLooking);
            var member2 = CreateMember(2, CandidateStatus.ActivelyLooking);
            var member3 = CreateMember(3, CandidateStatus.OpenToOffers);
            CreateMember(4, CandidateStatus.NotLooking);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // All.

            criteria.CandidateStatusFlags = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);

            // One.

            criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);

            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2);

            criteria.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member3);

            // Two.

            criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.OpenToOffers;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member3);

            criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow | CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);
        }

        [TestMethod]
        public void TestIsLooking()
        {
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

            // All members are looking.

            TestIsLooking(members);

            // Change to not looking.

            MakeNotLooking(members[1].Id);
            TestIsLooking(members[0], members[2], members[3]);

            // Only the first one is looking.

            MakeNotLooking(members[3].Id, members[2].Id);
            TestIsLooking(members[0]);

            MakeNotLooking(members[2].Id, members[3].Id);
            TestIsLooking(members[0]);

            MakeNotLooking(members[0].Id);
            TestIsLooking(new Member[0]);
        }

        private Member CreateMember(int index, CandidateStatus status)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.Status = status;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }

        private void MakeNotLooking(params Guid[] memberIds)
        {
            var candidates = _candidatesQuery.GetCandidates(memberIds);

            foreach (var candidate in candidates)
            {
                candidate.Status = CandidateStatus.NotLooking;
                _candidatesCommand.UpdateCandidate(candidate);
            }
        }

        private void TestIsLooking(params Member[] isLooking)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Ensure by default only looking members are returned.

            Get(GetSearchUrl(criteria));
            AssertMembers(isLooking.OrderBy(m => m.FirstName).ToArray());

            // Because not looking is ignored this is effectively the same as everyone.

            criteria.CandidateStatusFlags = CandidateStatusFlags.NotLooking;
            Get(GetSearchUrl(criteria));
            AssertMembers(isLooking.OrderBy(m => m.FirstName).ToArray());

            // Searching for all is the same as the default.

            criteria.CandidateStatusFlags = CandidateStatusFlags.All;
            Get(GetSearchUrl(criteria));
            AssertMembers(isLooking.OrderBy(m => m.FirstName).ToArray());
        }
    }
}