using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ApplicantCredits
{
    [TestClass]
    public class VerifiedOrganisationTests
        : ViewApplicantCreditsUsageTests
    {
        protected override Employer CreateEmployer(Member member)
        {
            return CreateEmployer(true);
        }

        protected override ICreditOwner GetCreditOwner(Employer employer)
        {
            return employer.Organisation as ICreditOwner;
        }
    }
}