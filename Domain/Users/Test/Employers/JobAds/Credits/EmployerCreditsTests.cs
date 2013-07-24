using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Credits
{
    [TestClass]
    public class EmployerCreditsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var employer = CreateEmployer(_organisationsCommand.CreateTestVerifiedOrganisation(0));
            CreateAllocation(employer.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
