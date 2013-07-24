using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Communications.Campaigns.Commands;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Query.Search.Employers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Campaigns.Employers
{
    [TestClass]
    public class SearchCampaignUsersTests
        : TestClass
    {
        private const string UserIdFormat = "employer{0}";
        private const string OrganisationName = "Organisation{0}";
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICampaignCriteriaCommand _campaignCriteriaCommand = Resolve<ICampaignCriteriaCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestDefaultCriteria()
        {
            var criteria = new OrganisationEmployerSearchCriteria();

            // No employers.

            TestSearch(criteria, new Employer[0]);

            // Create some employers.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);

            // Default criteria should return everyone.

            TestSearch(criteria, employers);
        }

        [TestMethod]
        public void TestEmployersRecruitersCriteria()
        {
            var criteria = new OrganisationEmployerSearchCriteria();

            // Create some employers and recruiters.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);
            var recruiters = CreateEmployers(6, 8, EmployerSubRole.Recruiter);

            // Default criteria should return everyone.

            TestSearch(criteria, employers.Concat(recruiters));

            // Only employers.

            criteria.Employers = true;
            criteria.Recruiters = false;
            TestSearch(criteria, employers);

            // Only recruiters.

            criteria.Employers = false;
            criteria.Recruiters = true;
            TestSearch(criteria, recruiters);

            // Neither.

            criteria.Employers = false;
            criteria.Recruiters = false;
            TestSearch(criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestIndustriesCriteria()
        {
            var criteria = new OrganisationEmployerSearchCriteria();
            var industries = GetIndustries();

            // Create some employers in one industry.

            var employers0 = CreateEmployers(0, 6, industries[0]);

            // Create some employers in another industry.

            var employers1 = CreateEmployers(6, 8, industries[1]);

            // Create some employers in both.

            var employers01 = CreateEmployers(14, 4, industries[0], industries[1]);

            // Create some employers in neither.

            var employers2 = CreateEmployers(18, 2, industries[2]);

            // Create some employers in none.

            var employers = CreateEmployers(20, 5, EmployerSubRole.Employer);

            // Default criteria should return everyone.

            TestSearch(criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2).Concat(employers));

            // Industry 0

            criteria.IndustryIds = new[] { industries[0].Id };
            TestSearch(criteria, employers0.Concat(employers01));

            // Industry 1

            criteria.IndustryIds = new[] { industries[1].Id };
            TestSearch(criteria, employers1.Concat(employers01));

            // Industry 2

            criteria.IndustryIds = new[] { industries[2].Id };
            TestSearch(criteria, employers2);

            // Industry 0 & 1

            criteria.IndustryIds = new[] { industries[0].Id, industries[1].Id };
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers01));

            // Industry 0 & 2

            criteria.IndustryIds = new[] { industries[0].Id, industries[2].Id };
            TestSearch(criteria, employers0.Concat(employers01).Concat(employers2));

            // Industry 1 & 2

            criteria.IndustryIds = new[] { industries[1].Id, industries[2].Id };
            TestSearch(criteria, employers1.Concat(employers01).Concat(employers2));

            // Industry 0, 1 & 2

            criteria.IndustryIds = new[] { industries[0].Id, industries[1].Id, industries[2].Id };
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2));

            // All

            criteria.IndustryIds = industries.Select(i => i.Id).ToArray();
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2));
        }

        [TestMethod]
        public void TestOrganisationalUnitCriteria()
        {
            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator.Id);
            var organisation1 = CreateOrganisation(1, false, administrator.Id);
            var organisation2 = CreateOrganisation(2, true, administrator.Id);
            var organisation3 = CreateOrganisation(3, true, administrator.Id);
            var organisation4 = CreateOrganisation(4, true, administrator.Id);

            // Create some employers.

            var employers0 = CreateEmployers(0, 6, organisation0);
            var employers1 = CreateEmployers(6, 8, organisation1);
            var employers2 = CreateEmployers(14, 4, organisation2);
            var employers3 = CreateEmployers(18, 2, organisation3);
            var employers4 = CreateEmployers(20, 5, organisation4);

            // Empty criteria should return everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            // Unverified.

            criteria.VerifiedOrganisations = false;
            criteria.UnverifiedOrganisations = true;
            TestSearch(criteria, employers0.Concat(employers1));

            // Verified.

            criteria.VerifiedOrganisations = true;
            criteria.UnverifiedOrganisations = false;
            TestSearch(criteria, employers2.Concat(employers3).Concat(employers4));

            // Neither.

            criteria.VerifiedOrganisations = false;
            criteria.UnverifiedOrganisations = false;
            TestSearch(criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestLoginsCriteria()
        {
            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator.Id);
            var organisation1 = CreateOrganisation(1, false, administrator.Id);
            var organisation2 = CreateOrganisation(2, true, administrator.Id);
            var organisation3 = CreateOrganisation(3, true, administrator.Id);
            var organisation4 = CreateOrganisation(4, true, administrator.Id);

            // Create some employers.

            var employers0 = CreateEmployers(0, 6, organisation0);
            var employers1 = CreateEmployers(6, 8, organisation1);
            var employers2 = CreateEmployers(14, 4, organisation2);
            var employers3 = CreateEmployers(18, 2, organisation3);
            var employers4 = CreateEmployers(20, 5, organisation4);

            // Empty criteria should return everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            // Minimum logins.

            criteria.MinimumLogins = 2;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MinimumLogins = 3;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers4));

            criteria.MinimumLogins = 4;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers4));

            criteria.MinimumLogins = 5;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers4));

            criteria.MinimumLogins = 6;
            TestSearch(criteria, employers0.Concat(employers1));

            criteria.MinimumLogins = 7;
            TestSearch(criteria, employers1);

            criteria.MinimumLogins = 8;
            TestSearch(criteria, employers1);

            criteria.MinimumLogins = 9;
            TestSearch(criteria, new Employer[0]);

            // Maximum logins.

            criteria.MinimumLogins = null;
            criteria.MaximumLogins = 9;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MaximumLogins = 8;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MaximumLogins = 7;
            TestSearch(criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MaximumLogins = 6;
            TestSearch(criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MaximumLogins = 5;
            TestSearch(criteria, employers2.Concat(employers3).Concat(employers4));

            criteria.MaximumLogins = 4;
            TestSearch(criteria, employers2.Concat(employers3));

            criteria.MaximumLogins = 3;
            TestSearch(criteria, employers3);

            criteria.MaximumLogins = 2;
            TestSearch(criteria, employers3);

            criteria.MaximumLogins = 1;
            TestSearch(criteria, new Employer[0]);

            // Both.

            criteria.MinimumLogins = 0;
            criteria.MaximumLogins = 9;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MinimumLogins = 1;
            criteria.MaximumLogins = 8;
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MinimumLogins = 2;
            criteria.MaximumLogins = 7;
            TestSearch(criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria.MinimumLogins = 3;
            criteria.MaximumLogins = 6;
            TestSearch(criteria, employers0.Concat(employers2).Concat(employers4));

            criteria.MinimumLogins = 4;
            criteria.MaximumLogins = 5;
            TestSearch(criteria, employers2.Concat(employers4));

            criteria.MinimumLogins = 5;
            criteria.MaximumLogins = 4;
            TestSearch(criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestCombinationCriteria()
        {
            var industries = GetIndustries();

            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator.Id);
            var organisation1 = CreateOrganisation(1, true, administrator.Id);

            // Create some employers.

            var employers0 = CreateEmployers(0, 1, EmployerSubRole.Employer);
            var employers1 = CreateEmployers(1, 2, EmployerSubRole.Recruiter);
            var employers2 = CreateEmployers(3, 3, industries[0]);
            var employers3 = CreateEmployers(6, 4, industries[1]);
            var employers4 = CreateEmployers(10, 5, organisation0);
            var employers5 = CreateEmployers(15, 6, organisation1);
            var employers6 = CreateEmployers(21, 7, EmployerSubRole.Employer, organisation0, industries[0]);
            var employers7 = CreateEmployers(28, 8, EmployerSubRole.Recruiter, organisation1, industries[1]);

            // Everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers5).Concat(employers6).Concat(employers7));

            // Employers.

            criteria.Employers = true;
            criteria.Recruiters = false;
            TestSearch(criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers5).Concat(employers6));

            // Recruiters.

            criteria.Employers = false;
            criteria.Recruiters = true;
            TestSearch(criteria, employers1.Concat(employers7));

            // Industy 0.

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[0].Id}};
            TestSearch(criteria, employers2.Concat(employers6));

            // Industy 2.

            criteria = new OrganisationEmployerSearchCriteria { IndustryIds = new[] { industries[1].Id } };
            TestSearch(criteria, employers3.Concat(employers7));

            // Unverified.

            criteria = new OrganisationEmployerSearchCriteria { VerifiedOrganisations = false };
            TestSearch(criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers6));

            // Verified.

            criteria = new OrganisationEmployerSearchCriteria { UnverifiedOrganisations = false };
            TestSearch(criteria, employers5.Concat(employers7));

            // Pick out 6.

            criteria = new OrganisationEmployerSearchCriteria { Recruiters = false, VerifiedOrganisations = false, IndustryIds = new[] { industries[0].Id }, MinimumLogins = 12 };
            TestSearch(criteria, employers6);

            // Pick out 7.

            criteria = new OrganisationEmployerSearchCriteria { Employers = false, UnverifiedOrganisations = false, IndustryIds = new[] { industries[1].Id }, MinimumLogins = 12 };
            TestSearch(criteria, employers7);
        }

        private Organisation CreateOrganisation(int index, bool isVerified, Guid administratorId)
        {
            if (isVerified)
            {
                var verifiedOrganisation = new VerifiedOrganisation { Name = string.Format(OrganisationName, index), VerifiedById = administratorId, AccountManagerId = administratorId };
                _organisationsCommand.CreateOrganisation(verifiedOrganisation);
                return verifiedOrganisation;
            }
            
            var organisation = new Organisation { Name = string.Format(OrganisationName, index) };
            _organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }

        private static IList<Industry> GetIndustries()
        {
            return Resolve<IIndustriesQuery>().GetIndustries();
        }

        private void TestSearch(Criteria criteria, IEnumerable<Employer> expected)
        {
            var users = _campaignCriteriaCommand.Match(CampaignCategory.Employer, criteria);
            Assert.AreEqual(expected.Count(), users.Count);

            // Order by email address.

            expected = expected.OrderBy(e => e.EmailAddress.Address);
            for (var index = 0; index < users.Count; ++index)
                Assert.AreEqual(expected.ElementAt(index).Id, users.ElementAt(index).Id);
        }

        private IEnumerable<Employer> CreateEmployers(int start, int count, EmployerSubRole subRole, IOrganisation organisation, params Industry[] industries)
        {
            var employers = new List<Employer>();
            for (var index = start; index < start + count; ++index)
                employers.Add(CreateEmployer(index, subRole, organisation, industries));
            return employers;
        }

        private IEnumerable<Employer> CreateEmployers(int start, int count, EmployerSubRole subRole)
        {
            return CreateEmployers(start, count, subRole, null, null);
        }

        private IEnumerable<Employer> CreateEmployers(int start, int count, params Industry[] industries)
        {
            return CreateEmployers(start, count, EmployerSubRole.Employer, null, industries);
        }

        private IEnumerable<Employer> CreateEmployers(int start, int count, IOrganisation organisation)
        {
            return CreateEmployers(start, count, EmployerSubRole.Employer, organisation, null);
        }

        private Employer CreateEmployer(int index, EmployerSubRole subRole, IOrganisation organisation, params Industry[] industries)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(string.Format(UserIdFormat, index), _organisationsCommand.CreateTestOrganisation(0));
            employer.SubRole = subRole;
            if (organisation != null)
                employer.Organisation = organisation;
            if (industries != null)
                employer.Industries = industries.ToList();
            _employerAccountsCommand.UpdateEmployer(employer);

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, ExpiryDate = DateTime.Now.AddDays(100), InitialQuantity = 100});
            return employer;
        }
    }
}