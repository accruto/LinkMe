using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Communications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Subscribers.Products
{
    public abstract class CreditExercisedEmailTests<TCredit>
        : ProductEmailTests
        where TCredit : Credit
    {
        protected static readonly EmailRecipient Return = new EmailRecipient("do_not_reply@test.linkme.net.au", "LinkMe");
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        protected readonly IAllocationsQuery _allocationsQuery = Resolve<IAllocationsQuery>();

        [TestMethod]
        public void TestUnverifiedEmployerCredits()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            TestCredits(employer.Id, 10, 8, null, employer);
        }

        [TestMethod]
        public void TestUnverifiedOrganisationCredits()
        {
            var organisation = _organisationsCommand.CreateTestOrganisation(0);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, organisation);
            TestCredits(organisation.Id, 10, 8, null, employer1, employer2);
        }

        [TestMethod]
        public void TestVerifiedEmployerCredits()
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer = _employerAccountsCommand.CreateTestEmployer(0, organisation);
            TestCredits(employer.Id, 10, 8, null, employer);
        }

        [TestMethod]
        public void TestVerifiedOrganisationCredits()
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, organisation);
            TestCredits(organisation.Id, 10, 8, null, employer1, employer2);
        }

        [TestMethod]
        public void TestVerifiedOrganisationWithContactCredits()
        {
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0);
            _employerAccountsCommand.CreateTestEmployer(1, organisation);
            _employerAccountsCommand.CreateTestEmployer(2, organisation);
            var employer3 = _employerAccountsCommand.CreateTestEmployer(3, organisation);

            organisation.ContactDetails = new ContactDetails { EmailAddress = employer3.EmailAddress.Address, FirstName = employer3.FirstName, LastName = employer3.LastName };
            _organisationsCommand.UpdateOrganisation(organisation);

            TestCredits(organisation.Id, 10, 8, null, employer3);
        }

        [TestMethod]
        public void TestVerifiedOrganisationWithAccountManagerCredits()
        {
            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation(0, null, administrator.Id);

            var employer1 = _employerAccountsCommand.CreateTestEmployer(1, organisation);
            var employer2 = _employerAccountsCommand.CreateTestEmployer(2, organisation);
            TestCredits(organisation.Id, 10, 8, administrator, employer1, employer2);
        }

        [TestMethod]
        public void TestMultipleAllocations()
        {
            var credit = _creditsQuery.GetCredit<TCredit>();
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));

            const int quantity = 10;
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = credit.Id, InitialQuantity = quantity });
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = credit.Id, InitialQuantity = quantity });

            // The first allocation should not trigger an email.

            for (var index = 1; index <= quantity; ++index)
            {
                _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(employer.Id), true, employer.Id, Guid.NewGuid(), null);
                _emailServer.AssertNoEmailSent();
            }

            var allocations = _allocationsQuery.GetActiveAllocations(employer.Id);
            Assert.AreEqual(2, allocations.Count);

            var allocation = (from a in allocations where a.RemainingQuantity == 0 select a).Single();
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(0, allocation.RemainingQuantity);

            allocation = (from a in allocations where a.RemainingQuantity != 0 select a).Single();
            Assert.AreEqual(quantity, allocation.InitialQuantity);
            Assert.AreEqual(quantity, allocation.RemainingQuantity);

            TestCredits(employer.Id, credit, quantity, (int)(quantity * 0.8), null, employer);

            allocations = _allocationsQuery.GetActiveAllocations(employer.Id);
            Assert.AreEqual(2, allocations.Count);
            Assert.AreEqual(quantity, allocations[0].InitialQuantity);
            Assert.AreEqual(0, allocations[0].RemainingQuantity);
            Assert.AreEqual(quantity, allocations[1].InitialQuantity);
            Assert.AreEqual(0, allocations[1].RemainingQuantity);
        }

        protected void TestCredits(int initialQuantity, int lowCreditsThreshold)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(0));
            TestCredits(employer.Id, initialQuantity, lowCreditsThreshold, null, employer);
        }

        protected void TestCredits(Guid ownerId, int initialQuantity, int lowCreditsThreshold, ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            var credit = _creditsQuery.GetCredit<TCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = ownerId, CreditId = credit.Id, InitialQuantity = initialQuantity });
            TestCredits(ownerId, credit, initialQuantity, lowCreditsThreshold, accountManager, recipients);
        }

        private void TestCredits(Guid ownerId, TCredit credit, int quantity, int lowCreditsThreshold, ICommunicationUser accountManager, params ICommunicationUser[] recipients)
        {
            // Should get low and no credits emails.

            for (var index = 1; index <= quantity; ++index)
            {
                // Exercise the credit.

                _exercisedCreditsCommand.ExerciseCredit(credit.Id, new HierarchyPath(ownerId), true, ownerId, Guid.NewGuid(), null);

                // Check for an email.

                if (index == lowCreditsThreshold)
                    AssertLowCreditsEmail(accountManager, recipients);
                else if (index == quantity)
                    AssertNoCreditsEmail(accountManager, recipients);
                else
                    _emailServer.AssertNoEmailSent();
            }
        }

        protected abstract void AssertNoCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients);
        protected abstract void AssertLowCreditsEmail(ICommunicationUser accountManager, params ICommunicationUser[] recipients);

        protected void AssertCreditsEmail(ICommunicationUser accountManager, ICommunicationUser[] recipients, string subject, string contains)
        {
            var emails = _emailServer.AssertEmailsSent(recipients.Length);
            foreach (var recipient in recipients)
            {
                var emailAddress = recipient.EmailAddress;
                var email = (from e in emails where e.To[0].Address == emailAddress select e).Single();
                email.AssertAddresses(Return, Return, recipient, null, accountManager);
                email.AssertSubject(subject);
                email.AssertHtmlViewContains(contains);
            }
        }
    }
}