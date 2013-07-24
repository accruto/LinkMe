using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Roles.JobAds;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.JobAds.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    [TestClass]
    public class ApplicantCreditExercisedEmailTests
        : CreditExercisedEmailTests<ApplicantCredit>
    {
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();
        private readonly IJobAdApplicationSubmissionsCommand _jobAdApplicationSubmissionsCommand = Resolve<IJobAdApplicationSubmissionsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private const int MaxCreditsPerJobAd = 20;

        [TestMethod]
        public void Test10ApplicantCredits()
        {
            TestCredits(10, 8);
        }

        [TestMethod]
        public void Test70ApplicantCredits()
        {
            TestCredits(70, 56);
        }

        [TestMethod]
        public void Test200ApplicantCredits()
        {
            TestCredits(200, 160);
        }

        [TestMethod]
        public void Test303ApplicantCredits()
        {
            TestCredits(303, 243);
        }

        [TestMethod]
        public void Test500ApplicantCredits()
        {
            TestCredits(500, 400);
        }

        [TestMethod]
        public void Test700ApplicantCredits()
        {
            TestCredits(700, 595);
        }

        [TestMethod]
        public void Test1000ApplicantCredits()
        {
            TestCredits(1000, 850);
        }

        [TestMethod]
        public void Test1200ApplicantCredits()
        {
            TestCredits(1200, 1080);
        }

        [TestMethod]
        public void TestSameApplicantSameJobAd()
        {
            const int quantity = 10;
            var employer = CreateEmployer(true, quantity);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            var member = _memberAccountsCommand.CreateTestMember(0);
            for (var index = 1; index <= quantity; ++index)
            {
                Submit(member.Id, jobAd);
                var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(quantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);
            }

            // No emails should have been sent.

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestSameApplicantDifferentJobAds()
        {
            const int quantity = 10;
            var employer = CreateEmployer(true, quantity);
            var member = _memberAccountsCommand.CreateTestMember(0);

            // Submit.

            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            for (var index = 1; index <= quantity; ++index)
            {
                Submit(member.Id, jobAd);
                var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(quantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);
            }

            // Submit again.

            jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            for (var index = 1; index <= quantity; ++index)
            {
                Submit(member.Id, jobAd);
                var allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(quantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity - 1, allocation.RemainingQuantity);
            }

            // No emails should have been sent.

            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestAllCreditsUsed()
        {
            const int initialQuantity = 10;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(true, quantity);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit even beyond limit.

            Allocation allocation;
            for (var index = 1; index <= 2 * initialQuantity; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                Submit(member.Id, jobAd);

                // Assert.

                if (quantity != 0)
                    --quantity;

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);

                if (index == initialQuantity * 0.8)
                    AssertLowCreditsEmail(null, employer);
                else if (index == initialQuantity)
                    AssertNoCreditsEmail(null, employer);
            }

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(0, allocation.RemainingQuantity);
        }

        [TestMethod]
        public void TestMaxJobAdCreditsUsed()
        {
            const int initialQuantity = 2 * MaxCreditsPerJobAd;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(true, quantity);
            var jobAd = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            Allocation allocation;
            for (var index = 0; index < 3 * MaxCreditsPerJobAd; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(index);
                Submit(member.Id, jobAd);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    --quantity;

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - MaxCreditsPerJobAd, allocation.RemainingQuantity);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestMaxJobAdCreditsUsedMultipleJobAds()
        {
            const int initialQuantity = 4 * MaxCreditsPerJobAd;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(true, quantity);
            var jobAd1 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            var jobAd2 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            Allocation allocation;
            for (var index = 0; index < 2 * MaxCreditsPerJobAd; ++index)
            {
                var member1 = _memberAccountsCommand.CreateTestMember(2 * index);
                var member2 = _memberAccountsCommand.CreateTestMember(2 * index + 1);
                Submit(member1.Id, jobAd1);
                Submit(member2.Id, jobAd2);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    quantity = quantity - 2;

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - 2 * MaxCreditsPerJobAd, allocation.RemainingQuantity);
            _emailServer.AssertNoEmailSent();
        }

        [TestMethod]
        public void TestMaxJobAdCreditsUsedOneApplicantMultipleJobAds()
        {
            const int initialQuantity = 4 * MaxCreditsPerJobAd;
            var quantity = initialQuantity;

            // Allocate some credits.

            var employer = CreateEmployer(true, quantity);
            var jobAd1 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };
            var jobAd2 = new JobAd { Id = Guid.NewGuid(), PosterId = employer.Id };

            // Submit.

            Allocation allocation;
            for (var index = 0; index < 2 * MaxCreditsPerJobAd; ++index)
            {
                var member = _memberAccountsCommand.CreateTestMember(2 * index);
                Submit(member.Id, jobAd1);
                Submit(member.Id, jobAd2);

                // Assert.

                if (index < MaxCreditsPerJobAd)
                    --quantity;

                allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
                Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
                Assert.AreEqual(quantity, allocation.RemainingQuantity);
            }

            allocation = _employerCreditsQuery.GetEffectiveActiveAllocation<ApplicantCredit>(employer);
            Assert.AreEqual(initialQuantity, allocation.InitialQuantity);
            Assert.AreEqual(initialQuantity - MaxCreditsPerJobAd, allocation.RemainingQuantity);
            _emailServer.AssertNoEmailSent();
        }

        private Employer CreateEmployer(bool allocateCredits, int? quantity)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            if (allocateCredits)
                _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id, InitialQuantity = quantity });
            return employer;
        }

        private void Submit(Guid memberId, IJobAd jobAd)
        {
            var application = new InternalApplication { ApplicantId = memberId };
            _jobAdApplicationSubmissionsCommand.CreateApplication(jobAd, application);
            _jobAdApplicationSubmissionsCommand.SubmitApplication(jobAd, application);
        }

        protected override void AssertLowCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            _emailServer.AssertNoEmailSent();
        }

        protected override void AssertNoCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            _emailServer.AssertNoEmailSent();
        }
    }
}