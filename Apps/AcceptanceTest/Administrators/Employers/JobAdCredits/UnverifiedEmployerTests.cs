using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.JobAdCredits
{
    [TestClass]
    public class UnverifiedEmployerTests
        : ViewJobAdCreditsUsageTests
    {
        protected override Employer CreateEmployer()
        {
            return CreateEmployer(false);
        }

        protected override ICreditOwner GetCreditOwner(Employer employer)
        {
            return employer;
        }
    }
}