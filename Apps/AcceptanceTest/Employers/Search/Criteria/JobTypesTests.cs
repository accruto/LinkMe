using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class JobTypesTests
        : CriteriaTests
    {
        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.JobTypes = JobTypes.PartTime;
            TestDisplay(criteria);
            criteria.JobTypes = JobTypes.PartTime | JobTypes.Contract;
            TestDisplay(criteria);
            criteria.JobTypes = JobTypes.All;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestJobTypes()
        {
            var employer = CreateEmployer();
            LogIn(employer);

            var member0 = CreateMember(0, JobTypes.FullTime);
            var member1 = CreateMember(1, JobTypes.PartTime);
            var member2 = CreateMember(2, JobTypes.FullTime | JobTypes.Contract);
            var member3 = CreateMember(3, JobTypes.All);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // All.

            criteria.JobTypes = JobTypes.All;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);

            // One.

            criteria.JobTypes = JobTypes.FullTime;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member2, member3);

            criteria.JobTypes = JobTypes.Contract;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member2, member3);

            // Two.

            criteria.JobTypes = JobTypes.FullTime | JobTypes.PartTime;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);

            criteria.JobTypes = JobTypes.FullTime | JobTypes.Contract;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member2, member3);

            criteria.JobTypes = JobTypes.PartTime | JobTypes.Contract;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member1, member2, member3);
        }

        private Member CreateMember(int index, JobTypes jobTypes)
        {
            var member = CreateMember(index);
            var candidate = _candidatesQuery.GetCandidate(member.Id);
            candidate.DesiredJobTypes = jobTypes;
            _candidatesCommand.UpdateCandidate(candidate);
            return member;
        }
    }
}
