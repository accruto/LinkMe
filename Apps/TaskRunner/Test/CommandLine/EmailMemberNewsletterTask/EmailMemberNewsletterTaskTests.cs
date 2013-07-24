using System.IO;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Test.Communications.Mocks;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.TaskRunner.Test.CommandLine.EmailMemberNewsletterTask
{
    [TestClass]
    public class EmailMemberNewsletterTaskTests
        : CommandLineTests
    {
        private const string EmailFormat = "test{0}@test.linkme.net.au";
        private const string LoginIdFormat = "test{0}";

        private string _fileName;
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        protected override string GetTask()
        {
            return "EmailMemberNewsletterTask";
        }

        protected override string GetTaskArgs()
        {
            return "\"" + _fileName + "\"";
        }

        [TestMethod]
        public void TestFile()
        {
            try
            {
                const int count = 3;

                // Create some members.

                var members = new Member[count];
                for (var index = 0; index < count; ++index)
                    members[index] = CreateMember(index);

                // Put their emails into a file.

                _fileName = Path.GetTempFileName();
                using (var writer = new StreamWriter(_fileName))
                {
                    for (var index = 0; index < count; ++index)
                        writer.WriteLine(string.Format(EmailFormat, index));

                    // Put in a non-member who should not get an email.

                    writer.WriteLine(string.Format(EmailFormat, 100));

                    // Create some other accounts who should not get an email.

                    var administrator = CreateAdministrator(101);
                    writer.WriteLine(administrator.EmailAddress);
                    var employer = CreateEmployer(102);
                    writer.WriteLine(employer.EmailAddress);
                }

                // Execute.

                Execute(true);
                AssertEmails(members);

                // Using a file should bypass all checks for sending so try again.

                Execute(true);
                AssertEmails(members);
            }
            finally
            {
                // Remove the temporary file.

                if (File.Exists(_fileName))
                    File.Delete(_fileName);
            }
        }

        [TestMethod]
        public void TestFileToInactiveUser()
        {
            try
            {
                const int count = 3;

                // Create some members.

                var members = new Member[count];
                for (var index = 0; index < count; ++index)
                {
                    members[index] = CreateMember(index);
                }

                // Now create some disabled members

                for (var index = count; index < count * 2; ++index)
                {
                    var member = CreateMember(index);
                    _emailVerificationsCommand.UnverifyEmailAddress(member.GetPrimaryEmailAddress().Address, "reason");
                }

                // Put their emails into a file.

                _fileName = Path.GetTempFileName();
                using (var writer = new StreamWriter(_fileName))
                {
                    for (var index = 0; index < count * 2; ++index)
                        writer.WriteLine(string.Format(EmailFormat, index));

                    // Put in a non-member who should not get an email.

                    writer.WriteLine(string.Format(EmailFormat, 100));

                    // Create some other accounts who should not get an email.

                    var administrator = CreateAdministrator(101);
                    writer.WriteLine(administrator.EmailAddress);
                    var employer = CreateEmployer(102);
                    writer.WriteLine(employer.EmailAddress);
                }

                // Execute.

                Execute(true);
                AssertEmails(members);

                // Using a file should bypass all checks for sending so try again.

                Execute(true);
                AssertEmails(members);
            }
            finally
            {
                // Remove the temporary file.

                if (File.Exists(_fileName))
                    File.Delete(_fileName);
            }
        }

        private void AssertEmails(Member[] members)
        {
            var emails = _emailServer.AssertEmailsSent(members.Length);
            for (var index = 0; index < emails.Length; ++index)
                Assert.AreEqual(members[index].GetBestEmailAddress().Address, emails[index].To[0].Address);
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(string.Format(EmailFormat, index));
        }

        private Administrator CreateAdministrator(int index)
        {
            return _administratorAccountsCommand.CreateTestAdministrator(string.Format(LoginIdFormat, index));
        }

        private Employer CreateEmployer(int index)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(string.Format(LoginIdFormat, index), _organisationsCommand.CreateTestOrganisation(0));
            _allocationsCommand.CreateAllocation(new Allocation { OwnerId = employer.Id, CreditId = _creditsQuery.GetCredit<ContactCredit>().Id });
            return employer;
        }
    }
}