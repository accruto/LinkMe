using System;
using LinkMe.Domain.Roles.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.JobAds.MaintainJobAds.NewJobAdFlow
{
    [TestClass]
    public class CustomizedNewJobAdFlowTests
        : EmployerNewJobAdFlowTests
    {
        // The id corresponds to an organisation (Database Consultants Australia (VIC)) that has customized styling.

        private static readonly Guid OrganisationId = new Guid("703317d4-da51-49e8-a553-b9c94af70156");
        private const string OrganisationName = "Database Consultants Australia (VIC)";

        protected override int? GetEmployerCredits()
        {
            // Even though they have 0 credits they are still not show the preview options.

            return 0;
        }

        protected override bool ShouldFeaturePacksShow
        {
            get { return false; }
        }

        protected override Organisation CreateOrganisation()
        {
            var organisation = new VerifiedOrganisation
            {
                Id = OrganisationId,
                Name = OrganisationName,
            };
            _organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }
    }
}
