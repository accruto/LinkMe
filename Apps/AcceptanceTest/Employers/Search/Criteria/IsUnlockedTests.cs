using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Query.Search.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Criteria
{
    [TestClass]
    public class IsUnlockedTests
        : CriteriaTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly ICreditsRepository _creditsRepository = Resolve<ICreditsRepository>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        protected override void TestDisplay()
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.IsUnlocked = null;
            TestDisplay(criteria);
            criteria.IsUnlocked = false;
            TestDisplay(criteria);
            criteria.IsUnlocked = true;
            TestDisplay(criteria);
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestIsUnlocked(new[] { isUnlockedMember }, new[] { isNotUnlockedMember });
        }

        [TestMethod]
        public void TestLimitedCredits()
        {
            var employer = CreateEmployer(10);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestIsUnlocked(new[] { isUnlockedMember }, new[] { isNotUnlockedMember });
        }

        [TestMethod]
        public void TestZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestIsUnlocked(new[] { isUnlockedMember }, new[] { isNotUnlockedMember });
        }

        [TestMethod]
        public void TestExpiredUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        [TestMethod]
        public void TestExpiredLimitedCredits()
        {
            var employer = CreateEmployer(10);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        [TestMethod]
        public void TestExpiredZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        [TestMethod]
        public void TestDeallocatedUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        [TestMethod]
        public void TestDeallocatedLimitedCredits()
        {
            var employer = CreateEmployer(10);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        [TestMethod]
        public void TestDeallocatedZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            LogIn(employer);

            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestIsUnlocked(new Member[0], new[] { isUnlockedMember, isNotUnlockedMember });
        }

        private Employer CreateEmployer(int? quantity)
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id, InitialQuantity = quantity });
            return employer;
        }

        private Member CreateMember(IEmployer employer, int index, bool isUnlocked)
        {
            var member = CreateMember(index);
            if (isUnlocked)
            {
                var view = _employerMemberViewsQuery.GetProfessionalView(employer, member);
                _employerMemberViewsCommand.AccessMember(_app, employer, view, MemberAccessReason.Unlock);
            }

            return member;
        }

        private void ExpireAllocations(Guid employerId)
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(employerId);
            foreach (var allocation in allocations)
            {
                allocation.CreatedTime = DateTime.Now.AddDays(-10);
                allocation.ExpiryDate = DateTime.Now.AddDays(-5).Date;
                _creditsRepository.UpdateAllocation(allocation);
            }
        }

        private void DeallocateAllocations(Guid employerId)
        {
            var allocations = _allocationsQuery.GetAllocationsByOwnerId(employerId);
            foreach (var allocation in allocations)
            {
                allocation.CreatedTime = DateTime.Now.AddDays(-10);
                allocation.DeallocatedTime = DateTime.Now.AddDays(-5).Date;
                _creditsRepository.UpdateAllocation(allocation);
            }
        }

        private void TestIsUnlocked(IEnumerable<Member> isUnlocked, IEnumerable<Member> isNotUnlocked)
        {
            var criteria = new MemberSearchCriteria();
            criteria.SetKeywords(BusinessAnalyst);

            criteria.IsUnlocked = null;
            Get(GetSearchUrl(criteria));
            AssertMembers(isUnlocked.Concat(isNotUnlocked).OrderBy(m => m.FirstName).ToArray());

            criteria.IsUnlocked = false;
            Get(GetSearchUrl(criteria));
            AssertMembers(isNotUnlocked.OrderBy(m => m.FirstName).ToArray());

            criteria.IsUnlocked = true;
            Get(GetSearchUrl(criteria));
            AssertMembers(isUnlocked.OrderBy(m => m.FirstName).ToArray());
        }
    }
}
