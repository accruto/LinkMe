using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers
{
    [TestClass]
    public class ViewMultipleCreditsUsageTests
        : ViewCreditsUsageTests
    {
        [TestMethod]
        public void TestMultipleAllocations()
        {
            var employer = CreateEmployer(true);

            // Exercise credits.

            var allocation0 = new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 2 };
            _allocationsCommand.CreateAllocation(allocation0);
            var member0 = CreateMember(0);
            var member1 = CreateMember(1);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member0));
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member1));

            var allocation1 = new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 2 };
            _allocationsCommand.CreateAllocation(allocation1);
            var member2 = CreateMember(2);
            var member3 = CreateMember(3);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member2));
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member3));

            var administrator = CreateAdministrator();
            LogIn(administrator);

            // Check who the employer has exercised credits on.

            AssertEmployerCreditUsage<ContactCredit>(employer, employer, true, member0, member1, member2, member3);

            // Check their credits.

            AssertEmployerCredits(employer, _allocationsQuery.GetAllocation(allocation0.Id), _allocationsQuery.GetAllocation(allocation1.Id));

            // Check the allocation.

            AssertEmployerCreditUsage(employer, allocation0, true, member0, member1);
            AssertEmployerCreditUsage(employer, allocation1, true, member2, member3);
        }

        [TestMethod]
        public void TestMultipleOrganisations()
        {
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid());
            var childOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, parentOrganisation, Guid.NewGuid());
            var employer = _employerAccountsCommand.CreateTestEmployer(0, childOrganisation);

            // Exercise credits.

            var allocation0 = new Allocation { OwnerId = childOrganisation.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1 };
            _allocationsCommand.CreateAllocation(allocation0);
            var member0 = CreateMember(0);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member0));

            var allocation1 = new Allocation { OwnerId = childOrganisation.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1 };
            _allocationsCommand.CreateAllocation(allocation1);
            var member1 = CreateMember(1);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member1));

            var allocation2 = new Allocation { OwnerId = parentOrganisation.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1 };
            _allocationsCommand.CreateAllocation(allocation2);
            var member2 = CreateMember(2);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member2));

            var allocation3 = new Allocation { OwnerId = parentOrganisation.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = 1 };
            _allocationsCommand.CreateAllocation(allocation3);
            var member3 = CreateMember(3);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetEmployerMemberView(employer, member3));

            var administrator = CreateAdministrator();
            LogIn(administrator);

            // Check who the employer has exercised credits on.

            AssertEmployerCreditUsage<ContactCredit>(employer, null, true, member0, member1, member2, member3);

            AssertEmployerCredits(employer);

            // Check the allocation.

            AssertEmployerCreditUsage(employer, allocation0, true, member0);
            AssertEmployerCreditUsage(employer, allocation1, true, member1);
            AssertEmployerCreditUsage(employer, allocation2, true, member2);
            AssertEmployerCreditUsage(employer, allocation3, true, member3);

            // Check organisation credits.

            AssertOrganisationCredits(childOrganisation, _allocationsQuery.GetAllocation(allocation0.Id), _allocationsQuery.GetAllocation(allocation1.Id));
            AssertOrganisationCredits(parentOrganisation, _allocationsQuery.GetAllocation(allocation2.Id), _allocationsQuery.GetAllocation(allocation3.Id));

            // Check who has used the organisation's credits.

            AssertOrganisationCreditUsage<ContactCredit>(childOrganisation, employer, true, member0, member1);
            AssertOrganisationCreditUsage<ContactCredit>(parentOrganisation, employer, true, member2, member3);

            // Check the allocations.

            AssertOrganisationCreditUsage(childOrganisation, employer, allocation0, true, member0);
            AssertOrganisationCreditUsage(childOrganisation, employer, allocation1, true, member1);
            AssertOrganisationCreditUsage(parentOrganisation, employer, allocation2, true, member2);
            AssertOrganisationCreditUsage(parentOrganisation, employer, allocation3, true, member3);
        }
    }
}
