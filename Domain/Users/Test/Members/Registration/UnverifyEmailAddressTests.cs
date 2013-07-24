using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Registration.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Members.Registration
{
    [TestClass]
    public class UnverifyEmailAddressTests
        : TestClass
    {
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmailVerificationsCommand _emailVerificationsCommand = Resolve<IEmailVerificationsCommand>();

        private const string SecondaryEmailAddress = "another@test.linkme.net.au";

        [TestInitialize]
        public void UnverifyEmailAddressTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestEmailAddress()
        {
            var member = _membersCommand.CreateTestMember(0);
            Assert.IsTrue(member.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[0].Address, true, member);

            // Unverify the address.

            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);

            // Assert.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsFalse(member.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[0].Address, false, member);
        }

        [TestMethod]
        public void TestPrimaryEmailAddress()
        {
            var member = _membersCommand.CreateTestMember(0);
            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress, IsVerified = true });
            _membersCommand.UpdateMember(member);

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[0].Address, true, member);

            // Unverify the address.

            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[0].Address, null);

            // Assert.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsFalse(member.EmailAddresses[0].IsVerified);
            Assert.IsTrue(member.EmailAddresses[1].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[1].Address, true, member);
        }

        [TestMethod]
        public void TestSecondaryEmailAddress()
        {
            var member = _membersCommand.CreateTestMember(0);
            member.EmailAddresses.Add(new EmailAddress { Address = SecondaryEmailAddress, IsVerified = true });
            _membersCommand.UpdateMember(member);

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[0].Address, true, member);

            // Unverify the address.

            _emailVerificationsCommand.UnverifyEmailAddress(member.EmailAddresses[1].Address, null);

            // Assert.

            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.EmailAddresses[0].IsVerified);
            Assert.IsFalse(member.EmailAddresses[1].IsVerified);
            AssertBestEmailAddress(member.EmailAddresses[0].Address, true, member);
        }

        [TestMethod]
        public void TestMultipleAddresses()
        {
            var member0 = _membersCommand.CreateTestMember(0);
            var member1 = _membersCommand.CreateTestMember(1);
            member1.EmailAddresses.Add(new EmailAddress { Address = member0.EmailAddresses[0].Address, IsVerified = true });
            _membersCommand.UpdateMember(member1);

            member0 = _membersQuery.GetMember(member0.Id);
            Assert.IsTrue(member0.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member0.EmailAddresses[0].Address, true, member0);

            member1 = _membersQuery.GetMember(member1.Id);
            Assert.IsTrue(member1.EmailAddresses[0].IsVerified);
            Assert.IsTrue(member1.EmailAddresses[1].IsVerified);
            AssertBestEmailAddress(member1.EmailAddresses[0].Address, true, member1);

            // Unverify the address.

            _emailVerificationsCommand.UnverifyEmailAddress(member0.EmailAddresses[0].Address, null);

            // Assert.

            member0 = _membersQuery.GetMember(member0.Id);
            Assert.IsFalse(member0.EmailAddresses[0].IsVerified);
            AssertBestEmailAddress(member0.EmailAddresses[0].Address, false, member0);

            member1 = _membersQuery.GetMember(member1.Id);
            Assert.IsTrue(member1.EmailAddresses[0].IsVerified);
            Assert.IsFalse(member1.EmailAddresses[1].IsVerified);
            AssertBestEmailAddress(member1.EmailAddresses[0].Address, true, member1);
        }

        private static void AssertBestEmailAddress(string expectedAddress, bool expectedIsVerified, IMember member)
        {
            var bestEmailAddress = member.GetBestEmailAddress();
            Assert.AreEqual(expectedAddress, bestEmailAddress.Address);
            Assert.AreEqual(expectedIsVerified, bestEmailAddress.IsVerified);
        }
    }
}
