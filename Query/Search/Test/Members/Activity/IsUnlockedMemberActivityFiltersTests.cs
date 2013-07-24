using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class IsUnlockedMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly ICreditsRepository _creditsRepository = Resolve<ICreditsRepository>();

        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestMethod]
        public void TestFilterIsUnlocked()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id});

            // Member1 is unlocked.

            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member1), MemberAccessReason.PhoneNumberViewed);

            // Filter.

            TestFilter(employer, CreateIsUnlockedQuery, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockIsUnlocked()
        {
            var member = _membersCommand.CreateTestMember(1);
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 20, OwnerId = employer.Id });

            // Member1 is unlocked.

            _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, member), MemberAccessReason.PhoneNumberViewed);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateIsUnlockedQuery(true), new[] { member.Id });

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateIsUnlockedQuery(true), new[] { member.Id });
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new[] { isUnlockedMember.Id }, new[] { isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestLimitedCredits()
        {
            var employer = CreateEmployer(10);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new[] { isUnlockedMember.Id }, new[] { isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new[] { isUnlockedMember.Id }, new[] { isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestExpiredUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestExpiredLimitedCredits()
        {
            var employer = CreateEmployer(10);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestExpiredZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            ExpireAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestDeallocatedUnlimitedCredits()
        {
            var employer = CreateEmployer(null);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestDeallocatedLimitedCredits()
        {
            var employer = CreateEmployer(10);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        [TestMethod]
        public void TestDeallocatedZeroCredits()
        {
            // Need to allocate 1 which will be used on the has accessed member.

            var employer = CreateEmployer(1);
            var isUnlockedMember = CreateMember(employer, 0, true);
            var isNotUnlockedMember = CreateMember(employer, 1, false);

            DeallocateAllocations(employer.Id);
            TestFilter(employer, CreateIsUnlockedQuery, new[] { isUnlockedMember.Id, isNotUnlockedMember.Id }, new Guid[0], new[] { isUnlockedMember.Id, isNotUnlockedMember.Id });
        }

        private Employer CreateEmployer(int? quantity)
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer.Id, InitialQuantity = quantity });
            return employer;
        }

        private Employer CreateEmployer()
        {
            return _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
        }

        private static MemberSearchQuery CreateIsUnlockedQuery(bool? value)
        {
            return new MemberSearchQuery { IsUnlocked = value };
        }

        private Member CreateMember(IEmployer employer, int index, bool isUnlocked)
        {
            var member = _membersCommand.CreateTestMember(index);
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
    }
}
