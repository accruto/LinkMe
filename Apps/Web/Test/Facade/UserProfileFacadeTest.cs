using System;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Security;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Networking.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Members.Contacts.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Web.Test.Facade
{
    [TestClass]
    public class UserProfileFacadeTest
        : TestClass
    {
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly INetworkingCommand _networkingCommand = Resolve<INetworkingCommand>();
        private readonly IMemberContactsQuery _memberContactsQuery = Resolve<IMemberContactsQuery>();
        private readonly IMemberViewsQuery _memberViewsQuery = Resolve<IMemberViewsQuery>();
        private readonly ILoginAuthenticationCommand _loginAuthenticationCommand = Resolve<ILoginAuthenticationCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestContactAlreadyInNetwork()
        {
            var contactOne = _memberAccountsCommand.CreateTestMember("abc1@abc.com", "Sideshow", "Bob");
            var contactTwo = _memberAccountsCommand.CreateTestMember("abc2@abc.com", "John", "Doe");
            var contactThree = _memberAccountsCommand.CreateTestMember("abc5@abc.com", "Rambo", "Doe");

            _networkingCommand.CreateFirstDegreeLink(contactOne.Id, contactTwo.Id);

            var contactInNetwork = GetNameOfContactUserIdInNetwork(contactOne.Id, contactTwo.GetBestEmailAddress().Address);
            var contactNotInNetwork = GetNameOfContactUserIdInNetwork(contactOne.Id, contactThree.GetBestEmailAddress().Address);

            Assert.AreEqual(contactTwo.FullName, contactInNetwork);
            Assert.AreEqual("", contactNotInNetwork);
        }

        [TestMethod]
        public void TestCreateUser()
        {
            // Create a member account.

            const string userId = "abc@abc.com";
            _memberAccountsCommand.CreateTestMember(userId, false);

            // Authenticate the user, who is deactivated when first created.

            var credentials = new LoginCredentials {LoginId = userId, PasswordHash = LoginCredentials.HashToString("password")};
            Assert.AreEqual(AuthenticationStatus.Deactivated, _loginAuthenticationCommand.AuthenticateUser(credentials).Status);

            var profile = _membersQuery.GetMember(userId);
            Assert.IsNotNull(profile);
        }

        private string GetNameOfContactUserIdInNetwork(Guid memberId, string userId)
        {
            var contactIds = _memberContactsQuery.GetFirstDegreeContacts(memberId);
            var views = _memberViewsQuery.GetPersonalViews(memberId, contactIds);
            foreach (var contactId in contactIds)
            {
                if (views[contactId].GetBestEmailAddress().Address == userId)
                    return views[contactId].FullName;
            }

            return "";
        }
    }
}
