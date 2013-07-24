using System;
using System.Collections.Generic;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Commands;
using LinkMe.Apps.Agents.Users.Sessions.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users
{
    [TestClass]
    public class SessionsTests
        : TestClass
    {
        private readonly IUserSessionsQuery _userSessionsQuery = Resolve<IUserSessionsQuery>();
        private readonly IUserSessionsCommand _userSessionsCommand = Resolve<IUserSessionsCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestLastLogin()
        {
            var userId = Guid.NewGuid();
            Assert.IsNull(_userSessionsQuery.GetLastLoginTime(userId));

            // Admin login is not a real login.

            _userSessionsCommand.CreateUserLogin(new UserLogin {UserId = userId, AuthenticationStatus = AuthenticationStatus.AuthenticatedWithOverridePassword});
            Assert.IsNull(_userSessionsQuery.GetLastLoginTime(userId));

            _userSessionsCommand.CreateUserLogin(new UserLogin { UserId = userId, AuthenticationStatus = AuthenticationStatus.Authenticated });
            Assert.IsNotNull(_userSessionsQuery.GetLastLoginTime(userId));
        }

        [TestMethod]
        public void TestSession()
        {
            var userId = Guid.NewGuid();

            AssertActivities(_userSessionsQuery.GetUserActivity(userId));

            // Login.

            var now = DateTime.Now;
            var sessionId = Guid.NewGuid().ToString();
            const string ipAddress = "123.456.123.456";
            var login = new UserLogin
            {
                UserId = userId,
                SessionId = sessionId,
                Time = now,
                IpAddress = ipAddress,
                AuthenticationStatus = AuthenticationStatus.Authenticated
            };
            _userSessionsCommand.CreateUserLogin(login);

            AssertActivities(_userSessionsQuery.GetUserActivity(userId), login);

            // Logout.

            var logout = new UserLogout
            {
                UserId = userId,
                SessionId = sessionId,
                Time = now.AddMinutes(1),
                IpAddress = ipAddress,
            };
            _userSessionsCommand.CreateUserLogout(logout);

            AssertActivities(_userSessionsQuery.GetUserActivity(userId), login, logout);

            // End session.

            var sessionEnd = new UserSessionEnd
            {
                UserId = userId,
                SessionId = sessionId,
                Time = now.AddMinutes(2),
                IpAddress = ipAddress,
            };
            _userSessionsCommand.CreateUserSessionEnd(sessionEnd);

            AssertActivities(_userSessionsQuery.GetUserActivity(userId), login, logout, sessionEnd);
        }

        private static void AssertActivities(IList<UserSessionActivity> activities, params UserSessionActivity[] expectedActivities)
        {
            Assert.AreEqual(expectedActivities.Length, activities.Count);
            for (var index = 0; index < activities.Count; ++index)
                AssertActivity(activities[index], expectedActivities[index]);
        }

        private static void AssertActivity(UserSessionActivity activity, UserSessionActivity expectedActivity)
        {
            Assert.IsInstanceOfType(activity, expectedActivity.GetType());
            Assert.AreEqual(expectedActivity.Id, activity.Id);
            Assert.AreEqual(expectedActivity.UserId, activity.UserId);
            Assert.AreEqual(expectedActivity.SessionId, activity.SessionId);
            Assert.AreEqual(expectedActivity.IpAddress, activity.IpAddress);

            if (expectedActivity is UserLogin)
                Assert.AreEqual(((UserLogin)expectedActivity).AuthenticationStatus, ((UserLogin)activity).AuthenticationStatus);
        }
    }
}