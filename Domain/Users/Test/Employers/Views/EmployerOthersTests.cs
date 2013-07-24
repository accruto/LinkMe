using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class EmployerOthersTests
        : OthersTests
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

        protected override IEmployer[] CreateOtherEmployers(IEmployer employer, bool canAccess)
        {
            // No-one else can access, even if they are in the same organisation.

            return canAccess
                       ? new IEmployer[0]
                       : new IEmployer[]
                             {
                                 _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0)),
                                 _employersCommand.CreateTestEmployer(2, employer.Organisation),
                             };
        }
    }
}