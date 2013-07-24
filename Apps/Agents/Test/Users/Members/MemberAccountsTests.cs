using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LinkMe.Apps.Agents.Security.Queries;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Accounts.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Candidates;
using LinkMe.Domain.Roles.Candidates.Queries;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users.Members
{
    [TestClass]
    public class MemberAccountsTests
        : TestClass
    {
        private class CreateMemberState
        {
            public string EmailAddress;
            public bool MemberAlreadyExists;
            public Exception Exception;
        }

        private const string EmailAddress1 = "member1@test.linkme.net.au";
        private const string EmailAddress2 = "member2@test.linkme.net.au";
        private const string EmailAddress3 = "member3@test.linkme.net.au";
        private const string FirstName1 = "Homer";
        private const string LastName1 = "Simpson";
        private const string FirstName2 = "Barney";
        private const string LastName2 = "Gumble";

        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly IUserAccountsQuery _userAccountsQuery = Resolve<IUserAccountsQuery>();
        private readonly ICandidatesQuery _candidatesQuery = Resolve<ICandidatesQuery>();
        private readonly ILoginCredentialsQuery _loginCredentialsQuery = Resolve<ILoginCredentialsQuery>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestCreateAccount()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            AssertAreEqual(member, _membersQuery.GetMember(member.GetBestEmailAddress().Address));
            AssertAreEqual(member, _membersQuery.GetMember(member.Id));
        }

        [TestMethod]
        public void TestGetInvalidAccount()
        {
            Assert.IsNull(_membersQuery.GetMember(Guid.NewGuid()));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestCreateDuplicateAccounts()
        {
            _memberAccountsCommand.CreateTestMember(EmailAddress1);
            _memberAccountsCommand.CreateTestMember(EmailAddress1);
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void TestUpdateDuplicateAccounts()
        {
            _memberAccountsCommand.CreateTestMember(EmailAddress1);
            var member = _memberAccountsCommand.CreateTestMember(EmailAddress2);
            member.EmailAddresses = new List<EmailAddress> { new EmailAddress { Address = EmailAddress1 } };
            _memberAccountsCommand.UpdateMember(member);
        }

        [TestMethod, ExpectedException(typeof(ValidationErrorsException))]
        public void TestLoginIdLength()
        {
            var emailAddress = "a@test.linkme.net.au";
            Assert.AreEqual(20, emailAddress.Length);
            while (emailAddress.Length <= 310)
            {
                // Should succeed.

                _memberAccountsCommand.CreateTestMember(emailAddress);
                emailAddress = new string('a', 10) + emailAddress;
            }

            Assert.AreEqual(320, emailAddress.Length);
            _memberAccountsCommand.CreateTestMember(emailAddress);

            // One more character and it should fail.

            emailAddress = 'a' + emailAddress;
            _memberAccountsCommand.CreateTestMember(emailAddress);
        }

        [TestMethod]
        public void TestGetMembers()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            var member2 = _memberAccountsCommand.CreateTestMember(2);
            var member3 = _memberAccountsCommand.CreateTestMember(3);
            var member4 = _memberAccountsCommand.CreateTestMember(4);

            AssertMembers();
            AssertMembers(member1.Id, member2.Id, member3.Id);
            AssertMembers(member2.Id, member3.Id);
            AssertMembers(member1.Id, member2.Id, member3.Id, member4.Id);
        }

        [TestMethod]
        public void TestGetMembersByName()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(EmailAddress1, FirstName1, LastName1);
            var member2 = _memberAccountsCommand.CreateTestMember(EmailAddress2, FirstName2, LastName2);
            var member3 = _memberAccountsCommand.CreateTestMember(EmailAddress3, FirstName1, LastName1);

            var members = _membersQuery.GetMembers(FirstName1 + " " + LastName1);
            Assert.AreEqual(2, members.Count);
            Assert.IsTrue((from m in members where m.Id == member1.Id select m).Any());
            Assert.IsTrue((from m in members where m.Id == member3.Id select m).Any());

            members = _membersQuery.GetMembers(FirstName2 + " " + LastName2);
            Assert.AreEqual(1, members.Count);
            Assert.IsTrue((from m in members where m.Id == member2.Id select m).Any());
        }

        [TestMethod]
        public void TestEthnicStatus()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            member = _membersQuery.GetMember(member.Id);
            Assert.AreEqual(default(EthnicStatus), member.EthnicStatus);

            member.EthnicStatus = EthnicStatus.Aboriginal;
            _memberAccountsCommand.UpdateMember(member);
            member = _membersQuery.GetMember(member.Id);
            Assert.AreEqual(EthnicStatus.Aboriginal, member.EthnicStatus);
        }

        [TestMethod]
        public void TestAddFirstDegreeContacts()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            Assert.IsTrue(_memberContactsQuery.GetFirstDegreeContacts(member1.Id).Count == 0);

            var member2 = _memberAccountsCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member1.Id, member2.Id);

            AssertFirstDegreeContacts(member1.Id, member2.Id);
        }

        [TestMethod]
        public void TestRemoveContacts()
        {
            var member1 = _memberAccountsCommand.CreateTestMember(1);
            Assert.IsTrue(_memberContactsQuery.GetFirstDegreeContacts(member1.Id).Count == 0);

            var member2 = _memberAccountsCommand.CreateTestMember(2);
            _networkingCommand.CreateFirstDegreeLink(member1.Id, member2.Id);

            AssertFirstDegreeContacts(member1.Id, member2.Id);

            _networkingCommand.DeleteFirstDegreeLink(member1.Id, member2.Id);
            Assert.IsTrue(_memberContactsQuery.GetFirstDegreeContacts(member1.Id).Count == 0);
            Assert.IsTrue(_memberContactsQuery.GetFirstDegreeContacts(member2.Id).Count == 0);
            Assert.IsFalse(_memberContactsQuery.AreFirstDegreeContacts(member1.Id, member2.Id));
            Assert.IsFalse(_memberContactsQuery.AreFirstDegreeContacts(member2.Id, member1.Id));
        }

        [TestMethod]
        public void TestUserType()
        {
            var member = _memberAccountsCommand.CreateTestMember(0);
            Assert.AreEqual(UserType.Member, member.UserType);
        }

        [TestMethod]
        public void TestDisableEnableUserAccount()
        {
            // Create a user and disable it.

            var member = _memberAccountsCommand.CreateTestMember(0);
            Assert.IsTrue(member.IsEnabled);

            var adminId = Guid.NewGuid();
            _userAccountsCommand.DisableUserAccount(member, adminId);

            member = _membersQuery.GetMember(member.Id);
            Assert.IsFalse(member.IsEnabled);

            // Check that the user event was logged.

            AssertDisablement(adminId, member.Id);

            // Enable the user.

            _userAccountsCommand.EnableUserAccount(member, adminId);
            member = _membersQuery.GetMember(member.Id);
            Assert.IsTrue(member.IsEnabled);

            // Check that the user event was logged.

            AssertEnablement(adminId, member.Id);
        }

        [TestMethod]
        public void TestDuplicateLoginId()
        {
            // This tests for bug 4136 by having two threads try to simulatenously create the same new user.

            var thread1 = new Thread(CreateMember);
            var thread2 = new Thread(CreateMember);

            var state1 = new CreateMemberState { EmailAddress = EmailAddress1 };
            var state2 = new CreateMemberState { EmailAddress = EmailAddress1 };

            thread1.Start(state1);
            thread2.Start(state2);

            thread1.Join();
            thread2.Join();

            ThrowThreadException(state1.Exception, 1);
            ThrowThreadException(state2.Exception, 2);

            Assert.IsTrue(state1.MemberAlreadyExists ^ state2.MemberAlreadyExists, "Member exists for thread 1: {0}, member exists for thread 2: {1}. One should be true and one should be false.", state1.MemberAlreadyExists, state2.MemberAlreadyExists);

            var member = _membersQuery.GetMember(EmailAddress1);
            Assert.AreEqual(EmailAddress1, member.EmailAddresses[0].Address);
            var credentials = _loginCredentialsQuery.GetCredentials(member.Id);
            Assert.AreEqual(EmailAddress1, credentials.LoginId);
        }

        private void AssertFirstDegreeContacts(Guid member1Id, Guid member2Id)
        {
            var firstDegreeContacts = _memberContactsQuery.GetFirstDegreeContacts(member1Id);
            Assert.AreEqual(1, firstDegreeContacts.Count);
            Assert.AreEqual(member2Id, firstDegreeContacts[0]);

            firstDegreeContacts = _memberContactsQuery.GetFirstDegreeContacts(member2Id);
            Assert.AreEqual(1, firstDegreeContacts.Count);
            Assert.AreEqual(member1Id, firstDegreeContacts[0]);

            Assert.IsTrue(_memberContactsQuery.AreFirstDegreeContacts(member1Id, member2Id));
            Assert.IsTrue(_memberContactsQuery.AreFirstDegreeContacts(member2Id, member1Id));
        }

        private void AssertDisablement(Guid actorId, Guid actionedUserId)
        {
            var events = _userAccountsQuery.GetDisablements(actionedUserId);
            Assert.IsTrue(events.Count > 0);
            var lastEvent = events[0];
            Assert.AreEqual(actorId, lastEvent.ActionedById);
            Assert.AreEqual(actionedUserId, lastEvent.UserId);
        }

        private void AssertEnablement(Guid actorId, Guid actionedUserId)
        {
            var events = _userAccountsQuery.GetEnablements(actionedUserId);
            Assert.IsTrue(events.Count > 0);
            var lastEvent = events[0];
            Assert.AreEqual(actorId, lastEvent.ActionedById);
            Assert.AreEqual(actionedUserId, lastEvent.UserId);
        }

        private void AssertMembers(params Guid[] ids)
        {
            var members = _membersQuery.GetMembers(ids);
            Assert.AreEqual(ids.Length, members.Count);
            foreach (var id in ids)
                Assert.IsTrue((from m in members where m.Id == id select m).Any());
        }

        private void AssertAreEqual(IMember expectedMember, IMember member)
        {
            // User

            Assert.IsNotNull(expectedMember);
            Assert.IsNotNull(member);
            Assert.AreEqual(expectedMember.IsEnabled, member.IsEnabled);
            Assert.AreEqual(expectedMember.IsActivated, member.IsActivated);
            Assert.AreEqual(expectedMember.FirstName, member.FirstName);
            Assert.AreEqual(expectedMember.Id, member.Id);
            Assert.AreEqual(expectedMember.LastName, member.LastName);

            // Member

            Assert.AreEqual(expectedMember.DateOfBirth, member.DateOfBirth);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.EmploymentVisibility, member.VisibilitySettings.Professional.EmploymentVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Professional.PublicVisibility, member.VisibilitySettings.Professional.PublicVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.FirstDegreeVisibility, member.VisibilitySettings.Personal.FirstDegreeVisibility);
            Assert.AreEqual(expectedMember.Gender, member.Gender);
            Assert.AreEqual(expectedMember.PhotoId, member.PhotoId);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.PublicVisibility, member.VisibilitySettings.Personal.PublicVisibility);
            Assert.AreEqual(expectedMember.VisibilitySettings.Personal.SecondDegreeVisibility, member.VisibilitySettings.Personal.SecondDegreeVisibility);

            if (expectedMember.EmailAddresses == null)
            {
                Assert.IsNull(member.EmailAddresses);
            }
            else
            {
                Assert.IsNotNull(member.EmailAddresses);
                Assert.AreEqual(expectedMember.EmailAddresses.Count, member.EmailAddresses.Count);
                for (var index = 0; index < expectedMember.EmailAddresses.Count; ++index)
                    Assert.AreEqual(expectedMember.EmailAddresses[index], member.EmailAddresses[index]);
            }

            Assert.AreEqual(expectedMember.GetPhoneNumber(PhoneNumberType.Mobile), member.GetPhoneNumber(PhoneNumberType.Mobile));
            Assert.AreEqual(expectedMember.GetPhoneNumber(PhoneNumberType.Home), member.GetPhoneNumber(PhoneNumberType.Home));
            Assert.AreEqual(expectedMember.GetPhoneNumber(PhoneNumberType.Work), member.GetPhoneNumber(PhoneNumberType.Work));

            Assert.AreEqual(expectedMember.Address.Id, member.Address.Id);
            Assert.AreEqual(expectedMember.Address.Line1, member.Address.Line1);
            Assert.AreEqual(expectedMember.Address.Line2, member.Address.Line2);
            Assert.AreEqual(expectedMember.Address.Location, member.Address.Location);

            AssertAreEqual(_candidatesQuery.GetCandidate(expectedMember.Id), _candidatesQuery.GetCandidate(member.Id));
        }

        private static void AssertAreEqual(ICandidate expectedCandidate, ICandidate candidate)
        {
            Assert.IsNotNull(expectedCandidate);
            Assert.IsNotNull(candidate);

            Assert.AreEqual(expectedCandidate.DesiredJobTitle, candidate.DesiredJobTitle);
            Assert.AreEqual(expectedCandidate.DesiredJobTypes, candidate.DesiredJobTypes);
            Assert.AreEqual(expectedCandidate.DesiredSalary, candidate.DesiredSalary);
            Assert.AreEqual(expectedCandidate.Id, candidate.Id);
            Assert.AreEqual(expectedCandidate.Status, candidate.Status);
            Assert.IsTrue(expectedCandidate.Industries.NullableSequenceEqual(candidate.Industries));
        }

        private void CreateMember(object stateObject)
        {
            var state = (CreateMemberState)stateObject;
            try
            {
                try
                {
                    _memberAccountsCommand.CreateTestMember(state.EmailAddress);
                }
                catch (DuplicateUserException)
                {
                    state.MemberAlreadyExists = true;
                }
            }
            catch (Exception ex)
            {
                state.Exception = ex;
            }
        }

        private static void ThrowThreadException(Exception ex, int thread)
        {
            if (ex == null)
                return;
            throw new ApplicationException("Exception on thread " + thread + ".", ex);
        }
    }
}
