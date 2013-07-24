using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class OrganisationCreditTests
        : PostJobAdCreditTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            CreateAllocation(organisation.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
