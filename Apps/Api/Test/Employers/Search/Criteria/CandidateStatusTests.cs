using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Search.Criteria
{
    [TestClass]
    public class CandidateStatusTests
        : SearchTests
    {
        [TestMethod]
        public void TestStatus()
        {
            var activelyLooking = CreateMember(0, CandidateStatus.ActivelyLooking);
            var availableNow = CreateMember(1, CandidateStatus.AvailableNow);
            CreateMember(2, CandidateStatus.NotLooking);
            var openToOffers = CreateMember(3, CandidateStatus.OpenToOffers);
            var unspecified = CreateMember(4, CandidateStatus.Unspecified);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            var model = Search(criteria);
            AssertMembers(model, activelyLooking, availableNow, openToOffers, unspecified);

            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking;
            model = Search(criteria);
            AssertMembers(model, activelyLooking);

            criteria.CandidateStatusFlags = CandidateStatusFlags.AvailableNow;
            model = Search(criteria);
            AssertMembers(model, availableNow);

            criteria.CandidateStatusFlags = CandidateStatusFlags.OpenToOffers;
            model = Search(criteria);
            AssertMembers(model, openToOffers);

            criteria.CandidateStatusFlags = CandidateStatusFlags.Unspecified;
            model = Search(criteria);
            AssertMembers(model, unspecified);

            criteria.CandidateStatusFlags = CandidateStatusFlags.ActivelyLooking | CandidateStatusFlags.OpenToOffers;
            model = Search(criteria);
            AssertMembers(model, activelyLooking, openToOffers);

            criteria.CandidateStatusFlags = CandidateStatusFlags.All;
            model = Search(criteria);
            AssertMembers(model, activelyLooking, availableNow, openToOffers, unspecified);
        }

        private Member CreateMember(int index, CandidateStatus status)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.Status = status;
            _candidatesCommand.UpdateCandidate(candidate);

            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}
