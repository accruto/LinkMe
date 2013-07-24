using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class VisaStatusTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.VisaStatusFlags = null;
            TestDisplay(criteria);
            criteria.VisaStatusFlags = VisaStatusFlags.Citizen | VisaStatusFlags.RestrictedWorkVisa;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestVisaStatus()
        {
            // Create members.

            var member0 = CreateMember(0, null);
            var member1 = CreateMember(1, VisaStatus.Citizen);
            var member2 = CreateMember(2, VisaStatus.NotApplicable);
            var member3 = CreateMember(3, VisaStatus.RestrictedWorkVisa);
            var member4 = CreateMember(4, VisaStatus.UnrestrictedWorkVisa);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no status.

            criteria.VisaStatusFlags = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4);

            // Citizen.

            criteria.VisaStatusFlags = VisaStatusFlags.Citizen;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // Restricted.

            criteria.VisaStatusFlags = VisaStatusFlags.RestrictedWorkVisa;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member3);

            // Unrestricted.

            criteria.VisaStatusFlags = VisaStatusFlags.UnrestrictedWorkVisa;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member4);

            // Some.

            criteria.VisaStatusFlags = VisaStatusFlags.Citizen | VisaStatusFlags.UnrestrictedWorkVisa;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member4);

            // All.

            criteria.VisaStatusFlags = VisaStatusFlags.All;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3, member4);
        }

        private Member CreateMember(int index, VisaStatus? visaStatus)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.VisaStatus = visaStatus;
            _candidatesCommand.UpdateCandidate(candidate);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}