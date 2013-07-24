using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.JobAds.Credits
{
    [TestClass]
    public class ChildOrganisationCreditsTests
        : CreditsTests
    {
        protected override Employer CreateEmployer(bool allocate, int? jobAdCredits, int? applicantCredits)
        {
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, parentOrganisation, Guid.NewGuid());
            var employer = CreateEmployer(organisation);
            CreateAllocation(organisation.Id, allocate, jobAdCredits, applicantCredits);
            return employer;
        }
    }
}
