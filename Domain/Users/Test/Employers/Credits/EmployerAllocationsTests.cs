using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public class EmployerAllocationsTests
        : TestClass
    {
        private readonly IEmployerAllocationsCommand _employerAllocationsCommand = Resolve<IEmployerAllocationsCommand>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUnlimitedContactCredits()
        {
            var employer = CreateEmployer();
            AllocateContactCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ContactCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestLimitedContactCredits()
        {
            var employer = CreateEmployer();
            AllocateContactCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ContactCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestUnlimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            AllocateJobAdCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(2));
        }

        [TestMethod]
        public void TestLimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            AllocateJobAdCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(2));
        }

        [TestMethod]
        public void TestUnlimitedJobAdCreditsExistingLimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 50, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocations<ApplicantCredit>(employer.Id, 50, DateTime.Now.Date.AddMonths(1), null, DateTime.Now.Date.AddYears(1).AddMonths(2));
        }

        [TestMethod]
        public void TestLimitedJobAdCreditsExistingLimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 50, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, 50, DateTime.Now.Date.AddMonths(1));
        }

        [TestMethod]
        public void TestUnlimitedJobAdCreditsExistingUnlimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddYears(1).AddMonths(3), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(3));
        }

        [TestMethod]
        public void TestLimitedJobAdCreditsExistingUnlimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddYears(1).AddMonths(3), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(3));
        }

        [TestMethod]
        public void TestUnlimitedJobAdCreditsExistingUnlimitedBadExpiryApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddMonths(3), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocations<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddMonths(3), null, DateTime.Now.Date.AddYears(1).AddMonths(2));
        }

        [TestMethod]
        public void TestLimitedJobAdCreditsExistingUnlimitedBadExpiryApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddMonths(3), CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocations<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddMonths(3), null, DateTime.Now.Date.AddYears(1).AddMonths(2));
        }

        [TestMethod]
        public void TestUnlimitedJobAdCreditsExistingUnlimitedNoExpiryApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = null, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, null);
        }

        [TestMethod]
        public void TestLimitedJobAdCreditsExistingUnlimitedNoExpiryApplicantCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = null, CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id });
            AllocateJobAdCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<JobAdCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<ApplicantCredit>(employer.Id, null, null);
        }

        [TestMethod]
        public void TestUnlimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            AllocateApplicantCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestLimitedApplicantCredits()
        {
            var employer = CreateEmployer();
            AllocateApplicantCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestUnlimitedApplicantCreditsExistingLimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 50, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocations<JobAdCredit>(employer.Id, 50, DateTime.Now.Date.AddMonths(1), null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestLimitedApplicantCreditsExistingLimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 50, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, 50, DateTime.Now.Date.AddMonths(1));
        }

        [TestMethod]
        public void TestUnlimitedApplicantCreditsExistingUnlimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddYears(1).AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(1));
        }

        [TestMethod]
        public void TestLimitedApplicantCreditsExistingUnlimitedJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddYears(1).AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1).AddMonths(1));
        }

        [TestMethod]
        public void TestUnlimitedApplicantCreditsExistingUnlimitedBadExpiryJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocations<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddMonths(1), null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestLimitedApplicantCreditsExistingUnlimitedBadExpiryJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = DateTime.Now.AddMonths(1), CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocations<JobAdCredit>(employer.Id, null, DateTime.Now.Date.AddMonths(1), null, DateTime.Now.Date.AddYears(1));
        }

        [TestMethod]
        public void TestUnlimitedApplicantCreditsExistingUnlimitedNoExpiryJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = null, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, null, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, null, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, null);
        }

        [TestMethod]
        public void TestLimitedApplicantCreditsExistingUnlimitedNoExpiryJobAdCredits()
        {
            var employer = CreateEmployer();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, ExpiryDate = null, CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id });
            AllocateApplicantCredits(employer.Id, 100, DateTime.Now.Date.AddYears(1));

            AssertAllocation<ApplicantCredit>(employer.Id, 100, DateTime.Now.Date.AddYears(1));
            AssertAllocation<JobAdCredit>(employer.Id, null, null);
        }

        private void AllocateContactCredits(Guid ownerId, int? quantity, DateTime expiryTime)
        {
            _employerAllocationsCommand.CreateAllocation(new Allocation
            {
                CreditId = _creditsQuery.GetCredit<ContactCredit>().Id,
                OwnerId = ownerId,
                InitialQuantity = quantity,
                ExpiryDate = expiryTime
            });
        }

        private void AllocateJobAdCredits(Guid ownerId, int? quantity, DateTime expiryTime)
        {
            _employerAllocationsCommand.CreateAllocation(new Allocation
            {
                CreditId = _creditsQuery.GetCredit<JobAdCredit>().Id,
                OwnerId = ownerId,
                InitialQuantity = quantity,
                ExpiryDate = expiryTime
            });
        }

        private void AllocateApplicantCredits(Guid ownerId, int? quantity, DateTime expiryTime)
        {
            _employerAllocationsCommand.CreateAllocation(new Allocation
            {
                CreditId = _creditsQuery.GetCredit<ApplicantCredit>().Id,
                OwnerId = ownerId,
                InitialQuantity = quantity,
                ExpiryDate = expiryTime
            });
        }

        private void AssertAllocation<TCredit>(Guid ownerId, int? quantity, DateTime? expiryTime)
            where TCredit : Credit
        {
            var allocations = _allocationsQuery.GetActiveAllocations<TCredit>(ownerId);
            Assert.AreEqual(1, allocations.Count);
            AssertAllocation<TCredit>(quantity, expiryTime, allocations);
        }

        private void AssertAllocations<TCredit>(Guid ownerId, int? quantity1, DateTime? expiryTime1, int? quantity2, DateTime? expiryTime2)
            where TCredit : Credit
        {
            var allocations = _allocationsQuery.GetActiveAllocations<TCredit>(ownerId);
            Assert.AreEqual(2, allocations.Count);
            AssertAllocation<TCredit>(quantity1, expiryTime1, allocations);
            AssertAllocation<TCredit>(quantity2, expiryTime2, allocations);
        }

        private void AssertAllocation<TCredit>(int? quantity, DateTime? expiryDate, IEnumerable<Allocation> allocations)
            where TCredit : Credit
        {
            Assert.IsTrue((from a in allocations
                           where a.CreditId == _creditsQuery.GetCredit<TCredit>().Id
                           && a.ExpiryDate == expiryDate
                           && a.InitialQuantity == quantity
                           select a).Any());
        }

        private Employer CreateEmployer()
        {
            return _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }
    }
}
