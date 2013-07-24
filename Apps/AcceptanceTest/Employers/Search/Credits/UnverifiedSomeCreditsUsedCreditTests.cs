using LinkMe.Domain.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Search.Credits
{
    [TestClass]
    public class UnverifiedSomeCreditsUsedCreditTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            var employer = CreateEmployer(false, 10);
            _employerCreditsCommand.ExerciseContactCredit(employer, _employerMemberViewsQuery.GetProfessionalView(employer, member));
            return employer;
        }

        protected override bool CanContact
        {
            get { return true; }
        }

        protected override bool HasUsedCredit
        {
            get { return true; }
        }

        protected override bool ShouldUseCredit
        {
            get { return false; }
        }
    }
}