using System;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Members.Jobs.Display.Web.Customized
{
    [TestClass]
    public abstract class CustomizedDisplayTests
        : DisplayTests
    {
        protected static readonly Guid OrganisationId = new Guid("703317d4-da51-49e8-a553-b9c94af70156");
        protected const string OrganisationName = "Database Consultants Australia (VIC)";

        protected override Domain.Contacts.Employer CreateEmployer()
        {
            // The id corresponds to an organisation (Database Consultants Australia (VIC)) that has customized styling.

            var organisation = new VerifiedOrganisation
            {
                Id = OrganisationId,
                Name = OrganisationName,
            };
            _organisationsCommand.CreateOrganisation(organisation);
            return _employerAccountsCommand.CreateTestEmployer(0, organisation);
        }
    }
}
