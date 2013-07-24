using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers
{
    [TestClass]
    public class EmployersCommandTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IIndustriesQuery _industriesQuery = Resolve<IIndustriesQuery>();

        [TestInitialize]
        public void EmployersCommandTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestUpdateFirstName()
        {
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            AssertEmployer(employer, _employersQuery.GetEmployer(employer.Id));

            employer.FirstName = "Changed";
            _employersCommand.UpdateEmployer(employer);
            AssertEmployer(employer, _employersQuery.GetEmployer(employer.Id));
        }

        [TestMethod]
        public void TestUpdateIndustries()
        {
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            AssertEmployer(employer, _employersQuery.GetEmployer(employer.Id));

            var industries = _industriesQuery.GetIndustries();
            employer.Industries = new List<Industry> {industries[0], industries[1]};
            _employersCommand.UpdateEmployer(employer);
            AssertEmployer(employer, _employersQuery.GetEmployer(employer.Id));
        }

        private static void AssertEmployer(IEmployer expectedEmployer, IEmployer employer)
        {
            Assert.AreEqual(expectedEmployer.Id, employer.Id);
            Assert.AreEqual(expectedEmployer.EmailAddress, employer.EmailAddress);
            Assert.AreEqual(expectedEmployer.FirstName, employer.FirstName);
            Assert.AreEqual(expectedEmployer.LastName, employer.LastName);
            Assert.AreEqual(expectedEmployer.IsActivated, employer.IsActivated);
            Assert.AreEqual(expectedEmployer.IsEnabled, employer.IsEnabled);
            Assert.AreEqual(expectedEmployer.IsActivated, employer.IsActivated);
            Assert.AreEqual(expectedEmployer.Organisation.Id, employer.Organisation.Id);
            Assert.AreEqual(expectedEmployer.PhoneNumber, employer.PhoneNumber);
            AssertAreEqual(expectedEmployer.Industries, employer.Industries);
        }

        private static void AssertAreEqual(ICollection<Industry> expectedIndustries, ICollection<Industry> industries)
        {
            if (expectedIndustries == null)
            {
                Assert.IsTrue(industries == null || industries.Count == 0);
            }
            else
            {
                Assert.AreEqual(expectedIndustries.Count, industries.Count);
                foreach (var industry in expectedIndustries)
                {
                    var industryId = industry.Id;
                    Assert.IsTrue((from i in industries where i.Id == industryId select i).Any());
                }
            }
        }
    }
}