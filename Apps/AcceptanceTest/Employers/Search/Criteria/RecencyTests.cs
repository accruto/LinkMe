using System;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Domain.Roles.Test.Candidates;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class RecencyTests
        : CriteriaTests
    {
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.Recency = null;
            TestDisplay(criteria);

            criteria.Recency = new TimeSpan(1, 0, 0, 0);
            TestDisplay(criteria);

            criteria.Recency = new TimeSpan(7, 0, 0, 0);
            TestDisplay(criteria);

            criteria.Recency = new TimeSpan(MemberSearchCriteria.DefaultRecency, 0, 0, 0);
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestRecency()
        {
            // Create members.

            var member0 = CreateMember(0, 2);
            var member1 = CreateMember(1, 9);
            var member2 = CreateMember(2, 19);
            var member3 = CreateMember(3, 300);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            // Do no recency.

            criteria.Recency = null;
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);

            // Today.

            criteria.Recency = new TimeSpan(1, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers();

            // 1 week.

            criteria.Recency = new TimeSpan(7, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0);

            // 2 weeks.

            criteria.Recency = new TimeSpan(14, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1);

            // 1 month.

            criteria.Recency = new TimeSpan(30, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // 2 months.

            criteria.Recency = new TimeSpan(61, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // 3 months.

            criteria.Recency = new TimeSpan(91, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // 6 months.

            criteria.Recency = new TimeSpan(183, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2);

            // 1 year.

            criteria.Recency = new TimeSpan(365, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);

            // 18 months.

            criteria.Recency = new TimeSpan(548, 0, 0, 0);
            Get(GetPartialSearchUrl(criteria));
            AssertMembers(member0, member1, member2, member3);
        }

        private Member CreateMember(int index, int days)
        {
            var lastUpdatedTime = DateTime.Now.AddDays(-1 * days);

            var member = CreateMember(index);
            member.CreatedTime = lastUpdatedTime;
            member.LastUpdatedTime = lastUpdatedTime;
            _membersRepository.UpdateMember(member);

            var candidate = _candidatesQuery.GetCandidate(member.Id);
            _candidateResumesCommand.AddTestResume(candidate);

            candidate.DesiredSalary = new Salary { UpperBound = days * 1000, Currency = Currency.AUD, Rate = SalaryRate.Year };
            candidate.LastUpdatedTime = lastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            // Use the repository directly so that the last updated time is not overridden by the command.

            var resume = _resumesQuery.GetResume(candidate.ResumeId.Value);
            resume.LastUpdatedTime = lastUpdatedTime;
            _resumesRepository.UpdateResume(resume);

            _memberSearchService.UpdateMember(member.Id);
            return member;
        }
    }
}