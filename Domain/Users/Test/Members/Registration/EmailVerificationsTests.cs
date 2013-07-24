using System;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Roles.Registration.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Registration
{
    [TestClass]
    public class EmailVerificationTests
        : TestClass
    {
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();
        private readonly IEmailVerificationsQuery _emailVerificationsQuery = Resolve<IEmailVerificationsQuery>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();

        [TestInitialize]
        public void EmailVerificationTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestGetEmailVerificationById()
        {
            var member = _membersCommand.CreateTestMember(1);

            // Create.

            var expectedEmailVerification = new EmailVerification { EmailAddress = member.GetBestEmailAddress().Address, UserId = member.Id };
            _emailVerificationsCommand.CreateEmailVerification(expectedEmailVerification);

            // Get.

            var emailVerification = _emailVerificationsQuery.GetEmailVerification(member.Id, member.GetBestEmailAddress().Address);
            AssertAreEqual(expectedEmailVerification, emailVerification);
        }

        [TestMethod]
        public void TestGetEmailVerificationByVerificationCode()
        {
            var member = _membersCommand.CreateTestMember(1);

            // Create.

            var expectedEmailVerification = new EmailVerification { EmailAddress = member.GetBestEmailAddress().Address, UserId = member.Id };
            _emailVerificationsCommand.CreateEmailVerification(expectedEmailVerification);

            // Get.

            var emailVerification = _emailVerificationsQuery.GetEmailVerification(expectedEmailVerification.VerificationCode);
            AssertAreEqual(expectedEmailVerification, emailVerification);
        }

        [TestMethod]
        public void TestDeleteEmailVerification()
        {
            var member = _membersCommand.CreateTestMember(1);

            var expectedEmailVerification = new EmailVerification { EmailAddress = member.GetBestEmailAddress().Address, UserId = member.Id };
            _emailVerificationsCommand.CreateEmailVerification(expectedEmailVerification);
            AssertAreEqual(expectedEmailVerification, _emailVerificationsQuery.GetEmailVerification(expectedEmailVerification.VerificationCode));

            // Delete it.

            _emailVerificationsCommand.DeleteEmailVerification(expectedEmailVerification.Id);
            Assert.IsNull(_emailVerificationsQuery.GetEmailVerification(expectedEmailVerification.VerificationCode));
        }

        private static void AssertAreEqual(EmailVerification expectedEmailVerification, EmailVerification emailVerification)
        {
            // Check properties first.

            Assert.AreNotEqual(Guid.Empty, emailVerification.Id);
            Assert.AreNotEqual(Guid.Empty, emailVerification.UserId);
            Assert.IsFalse(string.IsNullOrEmpty(emailVerification.VerificationCode));
            Assert.AreNotEqual(DateTime.MinValue, emailVerification.CreatedTime);
            Assert.IsFalse(string.IsNullOrEmpty(emailVerification.EmailAddress));

            // Check are equal.

            Assert.AreEqual(expectedEmailVerification.Id, emailVerification.Id);
            Assert.AreEqual(expectedEmailVerification.UserId, emailVerification.UserId);
            Assert.AreEqual(expectedEmailVerification.VerificationCode, emailVerification.VerificationCode);
            Assert.AreEqual(expectedEmailVerification.EmailAddress, emailVerification.EmailAddress);
        }
    }
}