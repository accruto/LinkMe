using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class OrganisationOthersTests
        : OthersTests
    {
        protected override IEmployer CreateEmployer()
        {
            return _employersCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
        }

        protected override void AllocateCredits<T>(IEmployer employer, int? quantity)
        {
            // Allocate to the employer's organisation.

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
                             };
        }
    }
}