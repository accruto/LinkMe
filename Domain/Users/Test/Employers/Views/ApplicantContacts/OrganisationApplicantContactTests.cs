using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.ApplicantContacts
{
    [TestClass]
    public class OrganisationApplicantContactTests
        : ApplicantContactTests
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
    }
}