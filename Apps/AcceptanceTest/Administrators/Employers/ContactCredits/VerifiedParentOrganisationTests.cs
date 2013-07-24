using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Employers.ContactCredits
{
    [TestClass]
    public class VerifiedParentOrganisationTests
        : ViewContactCreditsUsageTests
    {
        private Organisation _parentOrganisation;

        protected override Employer CreateEmployer(Member member)
        {
            _parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, Guid.NewGuid());
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(1, _parentOrganisation, Guid.NewGuid());
            return _employerAccountsCommand.CreateTestEmployer(1, organisation);
        }

        protected override ICreditOwner GetCreditOwner(Employer employer)
        {
            return _parentOrganisation;
        }
    }
}
