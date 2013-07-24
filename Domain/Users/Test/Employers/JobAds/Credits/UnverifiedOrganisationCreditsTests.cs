using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Credits
{
    [TestClass]
    public class UnverifiedOrganisationCreditsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer = CreateEmployer(organisation);
            CreateAllocation(organisation.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
