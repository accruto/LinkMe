using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Integration.JobAds.PostJobAds
{
    [TestClass]
    public class ParentOrganisationCreditTests
        : PostJobAdCreditTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, parentOrganisation, Guid.NewGuid());
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            CreateAllocation(parentOrganisation.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
