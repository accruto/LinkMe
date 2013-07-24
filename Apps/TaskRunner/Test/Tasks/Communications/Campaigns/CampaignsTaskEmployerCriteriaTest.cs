using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Criterias;
using LinkMe.Domain.Industries;
using LinkMe.Domain.Industries.Queries;
using LinkMe.Domain.Roles.Communications.Campaigns;
using LinkMe.Domain.Roles.Recruiters;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Framework.Utility;
using LinkMe.Query.Search.Employers;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications.Campaigns
{
    [TestClass]
    public class CampaignsTaskEmployerCriteriaTest
        : CampaignsTaskTests
    {
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();

        private const string OrganisationName = "Organisation{0}";

        [TestMethod]
        public void TestDefaultCriteria()
        {
            var index = 0;
            var criteria = new OrganisationEmployerSearchCriteria();

            // No employers.

            TestSearch(++index, criteria, new Employer[0]);

            // Create some employers.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);

            // Default criteria should return everyone.

            criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(++index, criteria, employers);
        }

        [TestMethod]
        public void TestEmployersRecruitersCriteria()
        {
            var index = 0;

            // Create some employers and recruiters.

            var employers = CreateEmployers(0, 6, EmployerSubRole.Employer);
            var recruiters = CreateEmployers(6, 8, EmployerSubRole.Recruiter);

            // Default criteria should return everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(++index, criteria, employers.Concat(recruiters));

            // Only employers.

            criteria = new OrganisationEmployerSearchCriteria {Employers = true, Recruiters = false};
            TestSearch(++index, criteria, employers);

            // Only recruiters.

            criteria = new OrganisationEmployerSearchCriteria {Employers = false, Recruiters = true};
            TestSearch(++index, criteria, recruiters);

            // Neither.

            criteria = new OrganisationEmployerSearchCriteria {Employers = false, Recruiters = false};
            TestSearch(++index, criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestIndustriesCriteria()
        {
            var index = 0;
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

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2).Concat(employers));

            // Industry 0

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[0].Id}};
            TestSearch(++index, criteria, employers0.Concat(employers01));

            // Industry 1

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[1].Id}};
            TestSearch(++index, criteria, employers1.Concat(employers01));

            // Industry 2

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[2].Id}};
            TestSearch(++index, criteria, employers2);

            // Industry 0 & 1

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[1].Id}};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers01));

            // Industry 0 & 2

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[2].Id}};
            TestSearch(++index, criteria, employers0.Concat(employers01).Concat(employers2));

            // Industry 1 & 2

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[1].Id, industries[2].Id}};
            TestSearch(++index, criteria, employers1.Concat(employers01).Concat(employers2));

            // Industry 0, 1 & 2

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = new[] {industries[0].Id, industries[1].Id, industries[2].Id}};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2));

            // All

            criteria = new OrganisationEmployerSearchCriteria {IndustryIds = industries.Select(i => i.Id).ToArray()};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers01).Concat(employers2));
        }

        [TestMethod]
        public void TestOrganisationalUnitCriteria()
        {
            var index = 0;

            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator);
            var organisation1 = CreateOrganisation(1, false, administrator);
            var organisation2 = CreateOrganisation(2, true, administrator);
            var organisation3 = CreateOrganisation(3, true, administrator);
            var organisation4 = CreateOrganisation(4, true, administrator);

            // Create some employers.

            var employers0 = CreateEmployers(0, 6, organisation0);
            var employers1 = CreateEmployers(6, 8, organisation1);
            var employers2 = CreateEmployers(14, 4, organisation2);
            var employers3 = CreateEmployers(18, 2, organisation3);
            var employers4 = CreateEmployers(20, 5, organisation4);

            // Empty criteria should return everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            // Unverified.

            criteria = new OrganisationEmployerSearchCriteria {VerifiedOrganisations = false, UnverifiedOrganisations = true};
            TestSearch(++index, criteria, employers0.Concat(employers1));

            // Verified.

            criteria = new OrganisationEmployerSearchCriteria {VerifiedOrganisations = true, UnverifiedOrganisations = false};
            TestSearch(++index, criteria, employers2.Concat(employers3).Concat(employers4));

            // Neither.

            criteria = new OrganisationEmployerSearchCriteria {VerifiedOrganisations = false, UnverifiedOrganisations = false};
            TestSearch(++index, criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestLoginsCriteria()
        {
            var index = 0;

            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator);
            var organisation1 = CreateOrganisation(1, false, administrator);
            var organisation2 = CreateOrganisation(2, true, administrator);
            var organisation3 = CreateOrganisation(3, true, administrator);
            var organisation4 = CreateOrganisation(4, true, administrator);

            // Create some employers.

            var employers0 = CreateEmployers(0, 6, organisation0);
            var employers1 = CreateEmployers(6, 8, organisation1);
            var employers2 = CreateEmployers(14, 4, organisation2);
            var employers3 = CreateEmployers(18, 2, organisation3);
            var employers4 = CreateEmployers(20, 5, organisation4);

            // Empty criteria should return everyone.

            var criteria = new OrganisationEmployerSearchCriteria();
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            // Minimum logins.

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 2};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 3};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 4};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 5};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 6};
            TestSearch(++index, criteria, employers0.Concat(employers1));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 7};
            TestSearch(++index, criteria, employers1);

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 8};
            TestSearch(++index, criteria, employers1);

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 9};
            TestSearch(++index, criteria, new Employer[0]);

            // Maximum logins.

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = null, MaximumLogins = 9};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 8};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 7};
            TestSearch(++index, criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 6};
            TestSearch(++index, criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 5};
            TestSearch(++index, criteria, employers2.Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 4};
            TestSearch(++index, criteria, employers2.Concat(employers3));

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 3};
            TestSearch(++index, criteria, employers3);

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 2};
            TestSearch(++index, criteria, employers3);

            criteria = new OrganisationEmployerSearchCriteria {MaximumLogins = 1};
            TestSearch(++index, criteria, new Employer[0]);

            // Both.

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 0, MaximumLogins = 9};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 1, MaximumLogins = 8};
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 2, MaximumLogins = 7};
            TestSearch(++index, criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 3, MaximumLogins = 6};
            TestSearch(++index, criteria, employers0.Concat(employers2).Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 4, MaximumLogins = 5};
            TestSearch(++index, criteria, employers2.Concat(employers4));

            criteria = new OrganisationEmployerSearchCriteria {MinimumLogins = 5, MaximumLogins = 4};
            TestSearch(++index, criteria, new Employer[0]);
        }

        [TestMethod]
        public void TestCombinationCriteria()
        {
            var index = 0;
            var industries = GetIndustries();

            // Create some organisations.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(1);
            var organisation0 = CreateOrganisation(0, false, administrator);
            var organisation1 = CreateOrganisation(1, true, administrator);

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
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers5).Concat(employers6).Concat(employers7));

            // Employers.

            criteria = new OrganisationEmployerSearchCriteria {Employers = true, Recruiters = false};
            TestSearch(++index, criteria, employers0.Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers5).Concat(employers6));

            // Recruiters.

            criteria = new OrganisationEmployerSearchCriteria {Employers = false, Recruiters = true};
            TestSearch(++index, criteria, employers1.Concat(employers7));

            // Industy 0.

            criteria = new OrganisationEmployerSearchCriteria { IndustryIds = new[] { industries[0].Id } };
            TestSearch(++index, criteria, employers2.Concat(employers6));

            // Industy 2.

            criteria = new OrganisationEmployerSearchCriteria { IndustryIds = new[] { industries[1].Id } };
            TestSearch(++index, criteria, employers3.Concat(employers7));

            // Unverified.

            criteria = new OrganisationEmployerSearchCriteria { VerifiedOrganisations = false };
            TestSearch(++index, criteria, employers0.Concat(employers1).Concat(employers2).Concat(employers3).Concat(employers4).Concat(employers6));

            // Verified.

            criteria = new OrganisationEmployerSearchCriteria { UnverifiedOrganisations = false };
            TestSearch(++index, criteria, employers5.Concat(employers7));

            // Pick out 6.

            criteria = new OrganisationEmployerSearchCriteria { Recruiters = false, VerifiedOrganisations = false, IndustryIds = new[] { industries[0].Id }, MinimumLogins = 12 };
            TestSearch(++index, criteria, employers6);

            // Pick out 7.

            criteria = new OrganisationEmployerSearchCriteria { Employers = false, UnverifiedOrganisations = false, IndustryIds = new[] { industries[1].Id }, MinimumLogins = 12 };
            TestSearch(++index, criteria, employers7);
        }

        private static IList<Industry> GetIndustries()
        {
            return Resolve<IIndustriesQuery>().GetIndustries();
        }

        private IOrganisation CreateOrganisation(int index, bool isVerified, IHasId<Guid> administrator)
        {
            if (isVerified)
            {
                var organisationalUnit = new VerifiedOrganisation {Name = string.Format(OrganisationName, index), VerifiedById = administrator.Id, AccountManagerId = administrator.Id};
                _organisationsCommand.CreateOrganisation(organisationalUnit);
                return organisationalUnit;
            }
            
            var organisation = new Organisation { Name = string.Format(OrganisationName, index) };
            _organisationsCommand.CreateOrganisation(organisation);
            return organisation;
        }

        private void TestSearch(int index, Criteria criteria, IEnumerable<Employer> expected)
        {
            // Create a campaign with the criteria.

            Campaign campaign;
            Template template;
            CreateCampaign(index, CampaignCategory.Employer, null, CampaignStatus.Activated, out campaign, out template);
            _campaignsCommand.UpdateCriteria(campaign.Id, criteria);

            // Run the task.

            new CampaignsTask().ExecuteTask();
            var emails = _emailServer.AssertEmailsSent(expected.Count());
            for (var employerIndex = 0; employerIndex < emails.Length; ++employerIndex)
                Assert.AreEqual(expected.ElementAt(employerIndex).EmailAddress.Address, emails[employerIndex].To[0].Address);
        }
    }
}