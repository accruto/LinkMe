using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class IndigenousTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.EthnicStatus = null;
            TestDisplay(criteria);
            criteria.EthnicStatus = EthnicStatus.Aboriginal;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestIndigenousStatus()
        {
            // Create members.

            var member0 = CreateMember(0, default(EthnicStatus));
            var member1 = CreateMember(1, EthnicStatus.Aboriginal);
            var member2 = CreateMember(2, EthnicStatus.TorresIslander);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no status.

            criteria.EthnicStatus = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // Aboriginal.

            criteria.EthnicStatus = EthnicStatus.Aboriginal;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1);

            // Torres Islander.

            criteria.EthnicStatus = EthnicStatus.TorresIslander;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member2);
        }

        private Member CreateMember(int index, EthnicStatus ethnicStatus)
        {
            var member = CreateMember(index);
            member.EthnicStatus = ethnicStatus;
            _memberAccountsCommand.UpdateMember(member);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);
            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}