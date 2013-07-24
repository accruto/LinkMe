using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class ChildOrganisationOthersTests
        : OthersTests
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

        protected override IEmployer[] CreateOtherEmployers(IEmployer employer, bool canContact)
        {
            // Others in the same organisation can contact.

            return canContact
                       ? new IEmployer[]
                             {
                                 _employersCommand.CreateTestEmployer(2, employer.Organisation),
                             }
                       : new IEmployer[]
                             {
                                 _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0)),
                                 _employersCommand.CreateTestEmployer(3, _parentOrganisation),
                             };
        }
    }
}