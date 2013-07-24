using LinkMe.Domain.Contacts;
using LinkMe.Domain.Location;
using LinkMe.Domain.Location.Queries;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Organisations
{
    [TestClass]
    public class EmployerOrganisationsTests
        : TestClass
    {
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IOrganisationsQuery _organisationsQuery = Resolve<IOrganisationsQuery>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly ILocationQuery _locationQuery = Resolve<ILocationQuery>();

        private const string EmailAddress = "monty@test.linkme.net.au";
        private const string FirstName = "Monty";
        private const string LastName = "Burns";
        private const string PhoneNumber = "99999999";
        private const string OrganisationName = "Acme";
        private const string Country = "Australia";
        private const string Location = "Norlane VIC 3214";

        [TestMethod]
        public void TestAddress()
        {
            // Create organisation.

            var organisation = new Organisation
            {
                Name = OrganisationName,
                Address = new Address {Location = _locationQuery.ResolveLocation(_locationQuery.GetCountry(Country), Location)},
            };

            _organisationsCommand.CreateOrganisation(organisation);
            AssertOrganisation(organisation, _organisationsQuery.GetOrganisation(organisation.Id));

            // Create employers.

            var employer = new Employer
            {
                EmailAddress = new EmailAddress { Address = EmailAddress, IsVerified = true },
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = new PhoneNumber { Number = PhoneNumber },
                Organisation = organisation,
            };
            _employersCommand.CreateEmployer(employer);

            AssertOrganisation(organisation, _employersQuery.GetEmployer(employer.Id).Organisation);
        }

        private static void AssertOrganisation(IOrganisation expectedOrganisation, IOrganisation organisation)
        {
            Assert.AreEqual(expectedOrganisation.Name, organisation.Name);
            Assert.AreEqual(expectedOrganisation.Address, organisation.Address);
        }
    }
}
