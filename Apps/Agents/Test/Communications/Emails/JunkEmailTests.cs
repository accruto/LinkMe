using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Communications.Emails.Commands;
using LinkMe.Apps.Agents.Communications.Emails.EmployerEmails;
using LinkMe.Apps.Agents.Communications.Emails.EmployerToMemberNotifications;
using LinkMe.Apps.Agents.Communications.Emails.MemberEmails;
using LinkMe.Apps.Agents.Communications.Emails.UserEmails;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Users.Employers.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Environment;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Communications.Emails
{
    [TestClass]
    public class JunkEmailTests
        : TestClass
    {
        #region Setup

        private static readonly Guid EmployerId = new Guid("7B0DA668-5C9B-4775-97D2-499A21742461"); // linkme
        private static readonly Guid MemberId = new Guid("50ECB0E7-7FA1-40BA-BF8D-4275993BAD76"); // Campbell

        private const string LiveEmailAddress = "testname.linkme@live.com.au";
        private const string GmailEmailAddress = "testname.linkme@gmail.com";
        private const string YahooEmailAddress = "testname.linkme@yahoo.com.au";

        private static IEmailsCommand _emailsCommand;

        private static Member _memberUser;
        private static Employer _employerUser;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev)
                Assert.Inconclusive("Must be run manually in UAT.");

            Container.Push();
            var smtpHost = Resolve<string>("linkme.communications.smtp.server");
            var smtpServer = new SmtpEmailClient(smtpHost);
            Container.Current.RegisterInstance<IEmailClient>(smtpServer);

            _emailsCommand = Resolve<IEmailsCommand>();
            _memberUser = Resolve<IMembersQuery>().GetMember(MemberId);
            _employerUser = Resolve<IEmployersQuery>().GetEmployer(EmployerId);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (RuntimeEnvironment.Environment == ApplicationEnvironment.Dev)
                return;

            Container.Pop();
        }

        #endregion

        #region PasswordReminderEmail

        [TestMethod]
        public void PasswordReminderEmailTestLive()
        {
            var email = CreatePasswordReminderEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void PasswordReminderEmailTestGmail()
        {
            var email = CreatePasswordReminderEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void PasswordReminderEmailTestYahoo()
        {
            var email = CreatePasswordReminderEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region ReactivationEmail

        [TestMethod]
        public void ReactivationEmailTestLive()
        {
            var email = CreateReactivationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ReactivationEmailTestGmail()
        {
            var email = CreateReactivationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ReactivationEmailTestYahoo()
        {
            var email = CreateReactivationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region ActivationEmail

        [TestMethod]
        public void ActivationEmailTestLive()
        {
            var email = CreateActivationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ActivationEmailTestGmail()
        {
            var email = CreateActivationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ActivationEmailTestYahoo()
        {
            var email = CreateActivationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region CandidateLookingConfirmationEmail

        [TestMethod]
        public void CandidateLookingConfirmationEmailTestLive()
        {
            var email = CreateCandidateLookingConfirmationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateLookingConfirmationEmailTestGmail()
        {
            var email = CreateCandidateLookingConfirmationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateLookingConfirmationEmailTestYahoo()
        {
            var email = CreateCandidateLookingConfirmationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region CandidatePassiveNotificationEmail

        [TestMethod]
        public void CandidatePassiveNotificationEmailTestLive()
        {
            var email = CreateCandidatePassiveNotificationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidatePassiveNotificationEmailTestGmail()
        {
            var email = CreateCandidatePassiveNotificationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidatePassiveNotificationEmailTestYahoo()
        {
            var email = CreateCandidatePassiveNotificationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region CandidateAvailableConfirmationEmail

        [TestMethod]
        public void CandidateAvailableConfirmationEmailTestLive()
        {
            var email = CreateCandidateAvailableConfirmationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateAvailableConfirmationEmailTestGmail()
        {
            var email = CreateCandidateAvailableConfirmationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateAvailableConfirmationEmailTestYahoo()
        {
            var email = CreateCandidateAvailableConfirmationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region CandidateLookingNotificationEmail

        [TestMethod]
        public void CandidateLookingNotificationEmailTestLive()
        {
            var email = CreateCandidateLookingNotificationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateLookingNotificationEmailTestGmail()
        {
            var email = CreateCandidateLookingNotificationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void CandidateLookingNotificationEmailTestYahoo()
        {
            var email = CreateCandidateLookingNotificationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region NewEmployerWelcomeEmail

        [TestMethod]
        public void NewEmployerWelcomeEmailTestLive()
        {
            var email = CreateNewEmployerWelcomeEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void NewEmployerWelcomeEmailTestGmail()
        {
            var email = CreateNewEmployerWelcomeEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void NewEmployerWelcomeEmailTestYahoo()
        {
            var email = CreateNewEmployerWelcomeEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region EmployerContactCandidateConfirmationEmail

        [TestMethod]
        public void EmployerContactCandidateConfirmationEmailTestLive()
        {
            var email = CreateEmployerContactCandidateConfirmationEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void EmployerContactCandidateConfirmationEmailTestGmail()
        {
            var email = CreateEmployerContactCandidateConfirmationEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void EmployerContactCandidateConfirmationEmailTestYahoo()
        {
            var email = CreateEmployerContactCandidateConfirmationEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region ContactCandidateEmail

        [TestMethod]
        public void ContactCandidateEmailTestLive()
        {
            var email = CreateContactCandidateEmail(LiveEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ContactCandidateEmailTestGmail()
        {
            var email = CreateContactCandidateEmail(GmailEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        public void ContactCandidateEmailTestYahoo()
        {
            var email = CreateContactCandidateEmail(YahooEmailAddress);
            bool ok = _emailsCommand.TrySend(email);
            Assert.IsTrue(ok);
        }

        #endregion

        #region Private Methods

        private PasswordReminderEmail CreatePasswordReminderEmail(string emailAddress)
        {
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            return new PasswordReminderEmail(_memberUser, "csallabank@linkme.com.au", "newPassword");
        }

        private ReactivationEmail CreateReactivationEmail(string emailAddress)
        {
            var emailVerification = new EmailVerification
                                   {
                                       CreatedTime = DateTime.Now,
                                       EmailAddress = emailAddress,
                                       Id = Guid.NewGuid(),
                                       UserId = _memberUser.Id,
                                       VerificationCode = "123"
                                   };
            return new ReactivationEmail(_memberUser, emailVerification);
        }

        private ActivationEmail CreateActivationEmail(string emailAddress)
        {
            var emailVerification = new EmailVerification
                                   {
                                       CreatedTime = DateTime.Now,
                                       EmailAddress = emailAddress,
                                       Id = Guid.NewGuid(),
                                       UserId = _memberUser.Id,
                                       VerificationCode = "123"
                                   };
            return new ActivationEmail(_memberUser, emailVerification);
        }

        private CandidateLookingConfirmationEmail CreateCandidateLookingConfirmationEmail(string emailAddress)
        {
            var email = new CandidateLookingConfirmationEmail(_memberUser);
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            email.To = _memberUser;
            return email;
        }

        private CandidatePassiveNotificationEmail CreateCandidatePassiveNotificationEmail(string emailAddress)
        {
            var email = new CandidatePassiveNotificationEmail(_memberUser);
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            email.To = _memberUser;
            return email;
        }

        private CandidateAvailableConfirmationEmail CreateCandidateAvailableConfirmationEmail(string emailAddress)
        {
            var email = new CandidateAvailableConfirmationEmail(_memberUser);
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            email.To = _memberUser;
            return email;
        }

        private CandidateLookingNotificationEmail CreateCandidateLookingNotificationEmail(string emailAddress)
        {
            var email = new CandidateLookingNotificationEmail(_memberUser);
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            email.To = _memberUser;
            return email;
        }

        private NewEmployerWelcomeEmail CreateNewEmployerWelcomeEmail(string emailAddress)
        {
            _employerUser.EmailAddress = new EmailAddress { Address = emailAddress };
            return new NewEmployerWelcomeEmail(_employerUser, emailAddress, "newPassword", 1234567);
        }

        private EmployerContactCandidateConfirmationEmail CreateEmployerContactCandidateConfirmationEmail(string emailAddress)
        {
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            return new EmployerContactCandidateConfirmationEmail(null, _employerUser, Guid.NewGuid(), "Joe Bloggs", "This is the subject",
                "Blah\nBlah blah\nBlah blah blah\nBlah blah blah blah\nBlah blah blah blah blah");
        }

        private ContactCandidateEmail CreateContactCandidateEmail(string emailAddress)
        {
            var view = new ProfessionalView(_memberUser, null, ProfessionalContactDegree.Contacted, true, false);
            var email = new ContactCandidateEmail(view, null, _employerUser, "This is the subject",
                "Blah\nBlah blah\nBlah blah blah\nBlah blah blah blah\nBlah blah blah blah blah");
            _memberUser.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = emailAddress } };
            email.To = _memberUser;
            return email;
        }

        #endregion
    }
}