﻿using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Contacts
{
    [TestClass]
    public class EmployerContactTests
        : ContactTests
    {
        protected override IEmployer CreateEmployer()
        {
            return _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected override void AllocateCredits<T>(IEmployer employer, int? quantity)
        {
            // Allocate directly to the employer.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<T>().Id, InitialQuantity = quantity });
        }
    }
}