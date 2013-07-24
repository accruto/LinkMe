using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.JobAdCredits
{
    [TestClass]
    public class VerifiedOrganisationTests
        : ViewJobAdCreditsUsageTests
    {
        protected override Employer CreateEmployer()
        {
            return CreateEmployer(true);
        }

        protected override ICreditOwner GetCreditOwner(Employer employer)
        {
            return employer.Organisation as ICreditOwner;
        }
    }
}