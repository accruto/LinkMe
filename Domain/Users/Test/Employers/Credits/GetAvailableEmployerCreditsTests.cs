using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Credits
{
    [TestClass]
    public class GetAvailableEmployerCreditsTests
        : TestClass
    {
        private readonly IDbConnectionFactory _connectionFactory = Resolve<IDbConnectionFactory>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        [TestInitialize]
        public void GetAvailableEmployerCreditsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestNoAllocations()
        {
            var employer = CreateEmployer(false);
            TestAvailableCredits(employer.Id, 0);
        }

        [TestMethod]
        public void TestUnlimited()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimited()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 100);
        }

        [TestMethod]
        public void TestLimitedUnlimited()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedLimited()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 200, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 300);
        }

        [TestMethod]
        public void TestUnlimitedUnverified()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedUnverified()
        {
            var employer = CreateEmployer(false);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 100);
        }

        [TestMethod]
        public void TestUnlimitedVerified()
        {
            var employer = CreateEmployer(true);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedVerified()
        {
            var employer = CreateEmployer(true);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 100);
        }

        [TestMethod]
        public void TestUnlimitedParentVerified()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedParentVerified()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 100);
        }

        [TestMethod]
        public void TestLimitedUnlimitedVerified()
        {
            var employer = CreateEmployer(true);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedLimitedVerified()
        {
            var employer = CreateEmployer(true);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = 200, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 300);
        }

        [TestMethod]
        public void TestUnlimitedLimitedVerified()
        {
            var employer = CreateEmployer(true);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedUnlimitedParentVerified()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        [TestMethod]
        public void TestLimitedLimitedParentVerified()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, InitialQuantity = 200, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, 300);
        }

        [TestMethod]
        public void TestUnlimitedLimitedParentVerified()
        {
            var parentOrganisation = CreateOrganisation(0, true);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, InitialQuantity = null, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = parentOrganisation.Id, InitialQuantity = 100, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            TestAvailableCredits(employer.Id, null);
        }

        private void TestAvailableCredits(Guid employerId, int? expectedCredits)
        {
            using (var dc = new CreditsDataContext(_connectionFactory.CreateConnection()))
            {
                Assert.AreEqual(expectedCredits, dc.GetAvailableEmployerCredits(employerId, _creditsQuery.GetCredit<ContactCredit>().Id));
            }
        }

        private Employer CreateEmployer(bool verified)
        {
            return CreateEmployer(CreateOrganisation(0, verified));
        }

        private Organisation CreateOrganisation(int index, bool verified)
        {
            return verified
                ? _organisationsCommand.CreateTestVerifiedOrganisation(index)
                : _organisationsCommand.CreateTestOrganisation(index);
        }

        private Employer CreateEmployer(IOrganisation organisation)
        {
            return _employersCommand.CreateTestEmployer(0, organisation);
        }
    }
}
