using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Contacts
{
    [TestClass]
    public class ChildOrganisationContactTests
        : ContactTests
    {
        private VerifiedOrganisation _parentOrganisation;

        protected override IEmployer CreateEmployer()
        {
            var administratorId = Guid.NewGuid();
            _parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administratorId);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, _parentOrganisation, administratorId);
            return _employersCommand.CreateTestEmployer(0, organisation);
        }

        protected override void AllocateCredits<T>(IEmployer employer, int? quantity)
        {
            // Allocate to the child organisation.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Organisation.Id, CreditId = _creditsQuery.GetCredit<T>().Id, InitialQuantity = quantity });
        }
    }
}