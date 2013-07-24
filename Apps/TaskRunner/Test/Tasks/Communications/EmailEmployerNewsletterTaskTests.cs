using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Communications.Settings;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
    public class EmailEmployerNewsletterTaskTests
        : TaskTests
    {
        private const string Subject = "Your LinkMe performance and ranking: are you getting the most out of LinkMe?";

        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IUserAccountsQuery _userAccountsQuery = Resolve<IUserAccountsQuery>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();
        private readonly IRecruitersQuery _recruitersQuery = Resolve<IRecruitersQuery>();

        private readonly Category _category;

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        public EmailEmployerNewsletterTaskTests()
        {
            _category = _settingsQuery.GetCategory("EmployerUpdate");
        }

        [TestMethod]
        public void TestSendVerifiedEmails()
        {
            TestSendEmails(true);
        }

        [TestMethod]
        public void TestSendUnverifiedEmails()
        {
            TestSendEmails(false);
        }

        [TestMethod]
        public void TestDisabledVerifiedEmails()
        {
            TestDisabledEmails(true);
        }

        [TestMethod]
        public void TestDisabledUnverifiedEmails()
        {
            TestDisabledEmails(false);
        }

        [TestMethod]
        public void TestUnsubscribedVerifiedEmployers()
        {
            TestUnsubscribedEmployers(true);
        }

        [TestMethod]
        public void TestUnsubscribedUnverifiedEmployers()
        {
            TestUnsubscribedEmployers(false);
        }

        [TestMethod]
        public void TestUnsubscribedVerifiedOrganisations()
        {
            TestUnsubscribedOrganisations(true);
        }

        [TestMethod]
        public void TestUnsubscribedUnverifiedOrganisations()
        {
            TestUnsubscribedOrganisations(false);
        }

        [TestMethod]
        public void TestUnsubscribedParentOrganisations()
        {
            // Add some employers, unsubscribing every second one.

            var employers = CreateEmployers(true, true, UnsubscribeParentOrganisation);
            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        [TestMethod]
        public void TestVerifiedAllocations()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var index = 0;

            // Add some employers, with various allocations.

            IList<IEmployer> employers = new List<IEmployer>();

            // Employer allocations.

            var employer = CreateEmployer(ref index, true, credit, 100, null);
            employers.Add(employer);
            employer = CreateEmployer(ref index, true, credit, 100, 100);
            employers.Add(employer);
            employer = CreateEmployer(ref index, true, credit, 100, 0);
            employers.Add(employer);
            CreateEmployer(ref index, true, credit, -10, null);
            CreateEmployer(ref index, true, credit, -10, 100);
            CreateEmployer(ref index, true, credit, -10, 0);

            // Organisation allocations.

            employer = CreateOrganisationEmployer(ref index, true, credit, 100, null);
            employers.Add(employer);
            employer = CreateOrganisationEmployer(ref index, true, credit, 100, 100);
            employers.Add(employer);
            employer = CreateOrganisationEmployer(ref index, true, credit, 100, 0);
            employers.Add(employer);
            CreateOrganisationEmployer(ref index, true, credit, -10, null);
            CreateOrganisationEmployer(ref index, true, credit, -10, 100);
            CreateOrganisationEmployer(ref index, true, credit, -10, 0);

            // Parent organisation allocations.

            employer = CreateParentOrganisationEmployer(ref index, credit, 100, null);
            employers.Add(employer);
            employer = CreateParentOrganisationEmployer(ref index, credit, 100, 100);
            employers.Add(employer);
            employer = CreateParentOrganisationEmployer(ref index, credit, 100, 0);
            employers.Add(employer);
            CreateParentOrganisationEmployer(ref index, credit, -10, null);
            CreateParentOrganisationEmployer(ref index, credit, -10, 100);
            CreateParentOrganisationEmployer(ref index, credit, -10, 0);

            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        [TestMethod]
        public void TestUnverifiedAllocations()
        {
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            var index = 0;

            // Add some employers, with various allocations.

            IList<IEmployer> employers = new List<IEmployer>();

            // Employer allocations.

            var employer = CreateEmployer(ref index, false, credit, 100, null);
            employers.Add(employer);
            employer = CreateEmployer(ref index, false, credit, 100, 100);
            employers.Add(employer);
            employer = CreateEmployer(ref index, false, credit, 100, 0);
            employers.Add(employer);
            CreateEmployer(ref index, false, credit, -10, null);
            CreateEmployer(ref index, false, credit, -10, 100);
            CreateEmployer(ref index, false, credit, -10, 0);

            // Organisation allocations.

            employer = CreateOrganisationEmployer(ref index, false, credit, 100, null);
            employers.Add(employer);
            employer = CreateOrganisationEmployer(ref index, false, credit, 100, 100);
            employers.Add(employer);
            employer = CreateOrganisationEmployer(ref index, false, credit, 100, 0);
            employers.Add(employer);
            CreateOrganisationEmployer(ref index, false, credit, -10, null);
            CreateOrganisationEmployer(ref index, false, credit, -10, 100);
            CreateOrganisationEmployer(ref index, false, credit, -10, 0);

            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        private void TestSendEmails(bool verified)
        {
            var employers = CreateEmployers(verified, false, UpdateEmployer);
            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        private void TestDisabledEmails(bool verified)
        {
            var employers = CreateEmployers(verified, false, DisableEmployer);
            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        private void TestUnsubscribedEmployers(bool verified)
        {
            var employers = CreateEmployers(verified, false, UnsubscribeEmployer);
            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        private void TestUnsubscribedOrganisations(bool verified)
        {
            var employers = CreateEmployers(verified, false, UnsubscribeOrganisation);
            new EmailEmployerNewsletterTask(_emailsCommand, _employersQuery, _userAccountsQuery, _allocationsQuery, _recruitersQuery).ExecuteTask();
            AssertEmails(employers);
        }

        private IEmployer CreateEmployer(ref int index, bool verified, Credit credit, int days, int? quantity)
        {
            ++index;
            var organisation = verified ? _organisationsCommand.CreateTestVerifiedOrganisation(index) : _organisationsCommand.CreateTestOrganisation(index);
            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = credit.Id, ExpiryDate = DateTime.Now.AddDays(days), InitialQuantity = quantity, OwnerId = employer.Id});
            return employer;
        }

        private IEmployer CreateOrganisationEmployer(ref int index, bool verified, Credit credit, int days, int? quantity)
        {
            ++index;
            var organisation = verified ? _organisationsCommand.CreateTestVerifiedOrganisation(index) : _organisationsCommand.CreateTestOrganisation(index);
            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, ExpiryDate = DateTime.Now.AddDays(days), InitialQuantity = quantity, OwnerId = organisation.Id });
            return employer;
        }

        private IEmployer CreateParentOrganisationEmployer(ref int index, Credit credit, int days, int? quantity)
        {
            ++index;
            var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(2 * index);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(2 * index + 1, parentOrganisation, Guid.NewGuid());
            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, ExpiryDate = DateTime.Now.AddDays(days), InitialQuantity = quantity, OwnerId = parentOrganisation.Id });
            return employer;
        }

        private IEnumerable<IEmployer> CreateEmployers(bool verified, bool createParentOrganisation, Func<IEmployer, bool> updateEmployer)
        {
            const int count = 5;
            var employers = new List<IEmployer>();
            for (var index = 0; index < count; ++index)
            {
                var employer = CreateEmployer(index, verified, createParentOrganisation);
                if (index % 2 == 0)
                {
                    employers.Add(employer);
                }
                else
                {
                    if (updateEmployer(employer))
                        employers.Add(employer);
                }
            }

            return employers;
        }

        private static bool UpdateEmployer(IEmployer employer)
        {
            return true;
        }

        private bool DisableEmployer(IEmployer employer)
        {
            _userAccountsCommand.DisableUserAccount(employer, Guid.NewGuid());
            return false;
        }

        private bool UnsubscribeEmployer(IEmployer employer)
        {
            _settingsCommand.SetFrequency(employer.Id, _category.Id, Frequency.Never);
            return false;
        }

        private bool UnsubscribeOrganisation(IEmployer employer)
        {
            _settingsCommand.SetFrequency(employer.Organisation.Id, _category.Id, Frequency.Never);
            return false;
        }

        private bool UnsubscribeParentOrganisation(IEmployer employer)
        {
            var parentOrganisation = _organisationsQuery.GetRootOrganisation(employer.Organisation.Id);
            _settingsCommand.SetFrequency(parentOrganisation.Id, _category.Id, Frequency.Never);
            return false;
        }

        private void AssertEmails(IEnumerable<IEmployer> employers)
        {
            // Sort to get some consistency.

            var emails = _emailServer.AssertEmailsSent(employers.Count()).OrderBy(e => e.To[0].Address).ToList();
            var expectedEmployers = employers.OrderBy(e => e.EmailAddress.Address).ToList();

            for (var index = 0; index < emails.Count; ++index)
            {
                var email = emails[index];
                var expectedEmployer = expectedEmployers[index];

                Assert.AreEqual(expectedEmployer.EmailAddress.Address, email.To[0].Address);
                Assert.AreEqual(expectedEmployer.FullName, email.To[0].DisplayName);
                Assert.AreEqual(Subject, email.Subject);
            }
        }

        private IEmployer CreateEmployer(int index, bool verified, bool createParentOrganisation)
        {
            IOrganisation organisation;

            if (verified)
            {
                if (createParentOrganisation)
                {
                    var parentOrganisation = _organisationsCommand.CreateTestVerifiedOrganisation(2*index);
                    organisation = _organisationsCommand.CreateTestVerifiedOrganisation(2*index + 1, parentOrganisation,
                        Guid.NewGuid());
                }
                else
                {
                    organisation = _organisationsCommand.CreateTestVerifiedOrganisation(index);
                }
            }
            else
            {
                organisation = _organisationsCommand.CreateTestOrganisation(index);
            }

            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, ExpiryDate = DateTime.Now.AddDays(100), InitialQuantity = 100, OwnerId = employer.Id });
            return employer;
        }
    }
}
