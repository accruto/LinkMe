using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Resumes;
using LinkMe.Query.Members;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IMembersRepository=LinkMe.Domain.Users.Members.IMembersRepository;

namespace LinkMe.Apps.Api.Test.Employers.Search
{
    [TestClass]
    public class SortOrderTests
        : SearchTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IMembersRepository _membersRepository = Resolve<IMembersRepository>();
        private readonly ICandidatesRepository _candidatesRepository = Resolve<ICandidatesRepository>();
        private readonly IResumesRepository _resumesRepository = Resolve<IResumesRepository>();

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

            // Give the employer some credits to see the mmebers.

            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id });
            LogIn(employer);

            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);
            TestSortOrder(criteria, MemberSortOrder.DateUpdated, member1, member0);
        }

        private void TestSortOrder(MemberSearchCriteria criteria, MemberSortOrder sortOrder, params Member[] members)
        {
            // Test ascending.

            var model = Search(criteria, sortOrder);
            AssertMembers(model, true, members);
        }

        private void UpdateMember(Member member, Candidate candidate, Resume resume, DateTime lastUpdatedTime)
        {
            member.CreatedTime = lastUpdatedTime;
            member.LastUpdatedTime = lastUpdatedTime;
            _membersRepository.UpdateMember(member);

            candidate.LastUpdatedTime = lastUpdatedTime;
            _candidatesRepository.UpdateCandidate(candidate);

            resume.LastUpdatedTime = lastUpdatedTime;
            _resumesRepository.UpdateResume(resume);

            _memberSearchService.UpdateMember(member.Id);
        }
    }
}