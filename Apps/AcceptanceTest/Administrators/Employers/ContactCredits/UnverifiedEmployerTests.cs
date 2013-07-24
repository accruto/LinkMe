using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ContactCredits
{
    [TestClass]
    public class UnverifiedEmployerTests
        : ViewContactCreditsUsageTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer(false);
        }

        protected override ICreditOwner GetCreditOwner(Employer employer)
        {
            return employer;
        }
    }
}