using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Communications.Settings.Commands;
using LinkMe.Domain.Roles.Communications.Settings.Queries;
using LinkMe.Domain.Roles.Recruiters.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Administrators.Queries;
using LinkMe.Domain.Users.Employers.Credits.Queries;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Framework.Utility.Sql;
using LinkMe.TaskRunner.Tasks.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.Tasks.Communications
{
    [TestClass]
    public class ExpiringCreditsEmailTaskTest
        : TaskTests
    {
        private const string EmailAddressFormat = "contact{0}@test.linkme.net.au";
        private const string FirstNameFormat = "Monty{0}";
        private const string LastNameFormat = "Burns{0}";
        private readonly ISettingsQuery _settingsQuery = Resolve<ISettingsQuery>();
        private readonly ISettingsCommand _settingsCommand = Resolve<ISettingsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IEmployerCreditsQuery _employerCreditsQuery = Resolve<IEmployerCreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployersQuery _employersQuery = Resolve<IEmployersQuery>();
        private readonly IEmailsCommand _emailsCommand = Resolve<IEmailsCommand>();
        private readonly IRecruitersQuery _recruitersQuery = Resolve<IRecruitersQuery>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly IAdministratorsQuery _administratorsQuery = Resolve<IAdministratorsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestEmployerLimitedApplicantCredits()
        {
            Test<ApplicantCredit>(CreateEmployer, 10);
        }

        [TestMethod]
        public void TestOrganisationLimitedApplicantCredits()
        {
            Test<ApplicantCredit>(CreateOrganisation, 10);
        }

        [TestMethod]
        public void TestVerifiedOrganisationLimitedApplicantCredits()
        {
            Test<ApplicantCredit>(CreateVerifiedOrganisation, 10);
        }

        [TestMethod]
        public void TestEmployerZeroApplicantCredits()
        {
            Test<ApplicantCredit>(CreateEmployer, 0);
        }

        [TestMethod]
        public void TestOrganisationZeroApplicantCredits()
        {
            Test<ApplicantCredit>(CreateOrganisation, 0);
        }

        [TestMethod]
        public void TestVerifiedOrganisationZeroApplicantCredits()
        {
            Test<ApplicantCredit>(CreateVerifiedOrganisation, 0);
        }

        [TestMethod]
        public void TestEmployerLimitedContactCredits()
        {
            Test<ContactCredit>(CreateEmployer, 10);
        }

        [TestMethod]
        public void TestOrganisationLimitedContactCredits()
        {
            Test<ContactCredit>(CreateOrganisation, 10);
        }

        [TestMethod]
        public void TestVerifiedOrganisationLimitedContactCredits()
        {
            Test<ContactCredit>(CreateVerifiedOrganisation, 10);
        }

        [TestMethod]
        public void TestEmployerZeroContactCredits()
        {
            Test<ContactCredit>(CreateEmployer, 0);
        }

        [TestMethod]
        public void TestOrganisationZeroContactCredits()
        {
            Test<ContactCredit>(CreateOrganisation, 0);
        }

        [TestMethod]
        public void TestVerifiedOrganisationZeroContactCredits()
        {
            Test<ContactCredit>(CreateVerifiedOrganisation, 0);
        }

        [TestMethod]
        public void TestVerifiedOrganisationUnlimitedContactCredits()
        {
            Test<ContactCredit>(CreateVerifiedOrganisation, null);
        }

        [TestMethod]
        public void TestEmployerLimitedJobAdCredits()
        {
            Test<JobAdCredit>(CreateEmployer, 10);
        }

        [TestMethod]
        public void TestOrganisationLimitedJobAdCredits()
        {
            Test<JobAdCredit>(CreateOrganisation, 10);
        }

        [TestMethod]
        public void TestVerifiedOrganisationLimitedJobAdCredits()
        {
            Test<JobAdCredit>(CreateVerifiedOrganisation, 10);
        }

        [TestMethod]
        public void TestEmployerZeroJobAdCredits()
        {
            Test<JobAdCredit>(CreateEmployer, 0);
        }

        [TestMethod]
        public void TestOrganisationZeroJobAdCredits()
        {
            Test<JobAdCredit>(CreateOrganisation, 0);
        }

        [TestMethod]
        public void TestVerifiedOrganisationZeroJobAdCredits()
        {
            Test<JobAdCredit>(CreateVerifiedOrganisation, 0);
        }

        [TestMethod]
        public void TestEmployerMultipleAllocations()
        {
            TestMultipleAllocations<ContactCredit>(CreateEmployer);
        }

        [TestMethod]
        public void TestOrganisationMultipleAllocations()
        {
            TestMultipleAllocations<ContactCredit>(CreateOrganisation);
        }

        [TestMethod]
        public void TestVerifiedOrganisationMultipleAllocations()
        {
            TestMultipleAllocations<ContactCredit>(CreateVerifiedOrganisation);
        }

        private void Test<TCredit>(Func<int, Guid, DateTime, int?, Employer> createRecipient, int? quantity)
            where TCredit : Credit
        {
            var today = DateTime.Now.Date;
            var index = 0;
            var expected = new List<Tuple<Employer, int>>();
            var credit = _creditsQuery.GetCredit<TCredit>();

            // No recipients.

            ExecuteTask();
            _emailServer.AssertNoEmailSent();

            // Expire in 1 day.

            var period = 1;
            if (quantity != 0)
            {
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
            }
            else
            {
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);
            }

            // Expire in 7 days.

            period = 7;
            if (quantity != 0)
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
            else
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);

            // Expire in 30 days.

            period = 30;
            if (quantity != 0)
            {
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
                Add(expected, period, createRecipient(++index, credit.Id, today.AddDays(period), quantity));
            }
            else
            {
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);
                createRecipient(++index, credit.Id, today.AddDays(period), quantity);
            }

            // Expire in 32 days.

            period = 32;
            createRecipient(++index, credit.Id, today.AddDays(period), quantity);
            createRecipient(++index, credit.Id, today.AddDays(period), quantity);

            // Execute the task.

            ExecuteTask();
            AssertEmails<TCredit>(expected, quantity);

            // Execute again, should get no-one.

            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private void TestMultipleAllocations<TCredit>(Func<int, Employer> createRecipient)
            where TCredit : Credit
        {
            // Emails should only go out when the last allocation is about to expire.

            var index = 0;
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 30, 10);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 30, 1);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 60, 30);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 60, 1);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 7, 2, 1);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 30, 7, 1);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 60, 30);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 30, -10);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 7, 4, -120);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, true, 1, -7, -364);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 1, null);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 7, null);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 30, null);
            TestMultipleAllocations<TCredit>(createRecipient, ref index, false, 1, 7, 30, null);
        }

        private void TestMultipleAllocations<TCredit>(Func<int, Employer> createRecipient, ref int index, bool expectEmail, params int?[] periods)
            where TCredit : Credit
        {
            var expected = new List<Tuple<Employer, int>>();
            var recipient = createRecipient(++index);
            const int quantity = 100;

            // Allocate.

            var now = DateTime.Now;
            foreach (var period in periods)
            {
                _allocationsCommand.CreateAllocation(
                    new Allocation
                    {
                        OwnerId = recipient.Id,
                        CreditId = _creditsQuery.GetCredit<TCredit>().Id,
                        ExpiryDate = period == null ? (DateTime?)null : now.Date.AddDays(period.Value),
                        InitialQuantity = quantity
                    });
            }

            if (expectEmail)
                Add(expected, periods[0].Value, recipient);

            // Execute the task.

            ExecuteTask();
            AssertEmails<TCredit>(expected, quantity);

            // Execute again, should get no-one.

            ExecuteTask();
            _emailServer.AssertNoEmailSent();
        }

        private void ExecuteTask()
        {
            new ExpiringCreditsEmailTask(
                _emailsCommand,
                _employerCreditsQuery,
                _employersQuery,
                _organisationsQuery,
                _recruitersQuery,
                _administratorsQuery).ExecuteTask();
        }

        private static void Add<TRecipient>(ICollection<Tuple<Employer, int>> expected, int period, TRecipient recipient)
            where TRecipient : Employer
        {
            expected.Add(new Tuple<Employer, int>(recipient, period));
        }

        private void AssertEmails<TCredit>(ICollection<Tuple<Employer, int>> expected, int? quantity)
        {
            var emails = _emailServer.AssertEmailsSent(expected.Count).OrderBy(e => e.To[0].Address).ToList();
            var expectedEmails = expected.OrderBy(e => e.Item1.EmailAddress.Address).ToList();
            for (var emailIndex = 0; emailIndex < emails.Count; ++emailIndex)
                AssertEmail<TCredit>(expectedEmails[emailIndex].Item1, emails[emailIndex], quantity);
        }

        private static void AssertEmail<TCredit>(ICommunicationUser expected, MockEmail email, int? quantity)
        {
            email.AssertAddresses(Return, Return, expected);
            email.AssertSubject(GetSubject<TCredit>(quantity));
            email.AssertHtmlViewContains(GetBodySnippet<TCredit>(quantity));
        }

        private static string GetSubject<TCredit>(int? quantity)
        {
            if (typeof(TCredit) == typeof(ContactCredit))
                return quantity == null
                    ? "Your unlimited LinkMe account will expire in one month"
                    : "You have unused LinkMe contact credits";
            return typeof(TCredit) == typeof(ApplicantCredit)
                ? "You have unused LinkMe applicant credits"
                : "You have unused LinkMe job ad credits";
        }

        private static string GetBodySnippet<TCredit>(int? quantity)
        {
            if (typeof(TCredit) == typeof(ContactCredit))
                return quantity == null
                    ? "To continue uninterrupted access"
                    : "To use the remaining contact credits";
            return typeof(TCredit) == typeof(ApplicantCredit)
                ? "To use the remaining applicant credits"
                : "To use the remaining job ad credits";
        }

        private Employer CreateEmployer(int index, Guid creditId, DateTime expiryDate, int? quantity)
        {
            return CreateEmployer(index, creditId, expiryDate, quantity, null);
        }

        private Employer CreateEmployer(int index, Guid creditId, DateTime expiryDate, int? quantity, DateTime? lastSentTime)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));

            // Allocate credits to the employer.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = creditId, ExpiryDate = expiryDate, InitialQuantity = quantity });

            if (lastSentTime != null)
            {
                var definition = _settingsQuery.GetDefinition(typeof(ExpiringCreditsEmail).Name);
                _settingsCommand.SetLastSentTime(employer.Id, definition.Id, lastSentTime);
            }

            return employer;
        }

        private Employer CreateEmployer(int index)
        {
            return _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }

        private Employer CreateOrganisation(int index, Guid creditId, DateTime expiryDate, int? quantity)
        {
            return CreateOrganisation(index, creditId, expiryDate, quantity, null);
        }

        private Employer CreateOrganisation(int index, Guid creditId, DateTime expiryDate, int? quantity, DateTime? lastSentTime)
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(index);
            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            // Allocate credits to the organisation.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = creditId, ExpiryDate = expiryDate, InitialQuantity = quantity });

            if (lastSentTime != null)
            {
                var definition = _settingsQuery.GetDefinition(typeof(ExpiringCreditsEmail).Name);
                _settingsCommand.SetLastSentTime(organisation.Id, definition.Id, lastSentTime);
            }

            return new Employer
            {
                Id = organisation.Id,
                EmailAddress = employer.EmailAddress,
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                IsEnabled = true,
            };
        }

        private Employer CreateOrganisation(int index)
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(index);
            var employer = _employerAccountsCommand.CreateTestEmployer(index, organisation);

            return new Employer
            {
                Id = organisation.Id,
                EmailAddress = employer.EmailAddress,
                FirstName = employer.FirstName,
                LastName = employer.LastName,
                IsEnabled = true,
            };
        }

        private Employer CreateVerifiedOrganisation(int index, Guid creditId, DateTime expiryDate, int? quantity)
        {
            return CreateVerifiedOrganisation(index, creditId, expiryDate, quantity, null);
        }

        private Employer CreateVerifiedOrganisation(int index, Guid creditId, DateTime expiryDate, int? quantity, DateTime? lastSentTime)
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(index);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(index, null, administrator.Id);

            // Create some contact details for the organisation.

            organisation.ContactDetails = new ContactDetails
            {
                EmailAddress = string.Format(EmailAddressFormat, index),
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
            _organisationsCommand.UpdateOrganisation(organisation);

            // Allocate to the organisation.

            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = organisation.Id, CreditId = creditId, ExpiryDate = expiryDate, InitialQuantity = quantity });

            if (lastSentTime != null)
            {
                var definition = _settingsQuery.GetDefinition(typeof(ExpiringContactCreditsEmail).Name);
                _settingsCommand.SetLastSentTime(organisation.Id, definition.Id, lastSentTime);
            }

            return new Employer
            {
                Id = organisation.Id,
                EmailAddress = new EmailAddress { Address = organisation.ContactDetails.EmailAddress, IsVerified = true },
                FirstName = organisation.ContactDetails.FirstName,
                LastName = organisation.ContactDetails.LastName,
                IsEnabled = true,
            };
        }

        private Employer CreateVerifiedOrganisation(int index)
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(index);

            // Create some contact details for the organisation.

            organisation.ContactDetails = new ContactDetails
            {
                EmailAddress = string.Format(EmailAddressFormat, index),
                FirstName = string.Format(FirstNameFormat, index),
                LastName = string.Format(LastNameFormat, index),
            };
            _organisationsCommand.UpdateOrganisation(organisation);

            return new Employer
            {
                Id = organisation.Id,
                EmailAddress = new EmailAddress { Address = organisation.ContactDetails.EmailAddress, IsVerified = true },
                FirstName = organisation.ContactDetails.FirstName,
                LastName = organisation.ContactDetails.LastName,
                IsEnabled = true,
            };
        }
    }
}
