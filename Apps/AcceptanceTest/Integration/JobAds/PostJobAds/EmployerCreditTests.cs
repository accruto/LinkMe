using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class EmployerCreditTests
        : PostJobAdCreditTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestVerifiedOrganisation(0));
            CreateAllocation(employer.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
