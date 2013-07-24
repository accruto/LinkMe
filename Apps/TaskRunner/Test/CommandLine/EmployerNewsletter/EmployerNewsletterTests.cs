using System;
using System.Linq;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.EmployerNewsletter
{
    [TestClass]
    public class EmployerNewsletterTests
        : CommandLineTests
    {
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();

        protected override string GetTaskGroup()
        {
            return "EmployerNewsletter";
        }

        [TestMethod]
        public void TestSend()
        {
            const int count = 3;

            // Create some employers.

            var employers = new IEmployer[count];
            for (var index = 0; index < count; ++index)
                employers[index] = CreateEmployer(index);

            // Execute.

            Execute(true);
            AssertEmails(employers);
        }

        private void AssertEmails(IEmployer[] members)
        {
            var emails = _emailServer.AssertEmailsSent(members.Length).OrderBy(e => e.To[0].Address).ToList();
            for (var index = 0; index < emails.Count; ++index)
                Assert.AreEqual(members[index].EmailAddress.Address, emails[index].To[0].Address);
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            var credit = _creditsQuery.GetCredit<ContactCredit>();
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = credit.Id, ExpiryDate = DateTime.Now.AddDays(100), InitialQuantity = 100, OwnerId = employer.Id });
            return employer;
        }
    }
}