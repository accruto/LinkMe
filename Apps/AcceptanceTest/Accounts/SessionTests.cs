using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using LinkMe.Apps.Agents.Security.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Apps.Agents.Users.Sessions;
using LinkMe.Apps.Agents.Users.Sessions.Queries;
using LinkMe.Apps.Utility.Test;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Affiliations.Communities;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Accounts
{
    [TestClass]
    public class SessionTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IUserSessionsQuery _userSessionsQuery = Resolve<IUserSessionsQuery>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        [TestInitialize]
        public void TestInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Log in.

            Get(HomeUrl);
            SubmitLogIn(member);
            var login = AssertActivity(_userSessionsQuery.GetUserActivity(member.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();

            var logout = new UserLogout { UserId = member.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(member.Id), GetExpectedActivities(login, logout));
        }

        [TestMethod]
        public void TestEmployer()
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Log in.

            Get(EmployerHomeUrl);
            SubmitLogIn(employer);

            var login = AssertActivity(_userSessionsQuery.GetUserActivity(employer.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();

            var logout = new UserLogout { UserId = employer.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(employer.Id), GetExpectedActivities(login, logout));
        }

        [TestMethod]
        public void TestAdministrator()
        {
            // Create the administrator.

            var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);

            // Log in.

            Get(HomeUrl);
            SubmitLogIn(administrator);
            var login = AssertActivity(_userSessionsQuery.GetUserActivity(administrator.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();

            var logout = new UserLogout { UserId = administrator.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(administrator.Id), GetExpectedActivities(login, logout));
        }

        [TestMethod]
        public void TestCustodian()
        {
            // Create the custodian.

            var community = _communitiesCommand.CreateTestCommunity(0);
            var custodian = _custodianAccountsCommand.CreateTestCustodian(0, community.Id);

            // Log in.

            Get(HomeUrl);
            SubmitLogIn(custodian);
            var login = AssertActivity(_userSessionsQuery.GetUserActivity(custodian.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();

            var logout = new UserLogout { UserId = custodian.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(custodian.Id), GetExpectedActivities(login, logout));
        }

        [TestMethod]
        public void TestRememberMeMember()
        {
            // Create the member.

            var member = _memberAccountsCommand.CreateTestMember(0);

            // Log in.

            Get(HomeUrl);
            SubmitLogIn(member, true);
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");
            var login = AssertActivity(_userSessionsQuery.GetUserActivity(member.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();
            var logout = new UserLogout { UserId = member.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(member.Id), GetExpectedActivities(login, logout));

            // Set the cookies and try to access the home page.

            Browser.Cookies = new CookieContainer();
            var uri = new Uri(new ReadOnlyApplicationUrl("/").AbsoluteUri);
            Browser.Cookies.Add(uri, new Cookie("user_cookie", userCookieValue));
            Browser.Cookies.Add(uri, new Cookie("passwordCookie", passwordCookieValue));
            Get(HomeUrl);

            var activities = _userSessionsQuery.GetUserActivity(member.Id);
            Assert.AreEqual(3, activities.Count);
            AssertActivities(activities.Take(2).ToList(), GetExpectedActivities(login, logout));

            // Last activity is automatic log in.

            var activity = activities[2];
            Assert.IsInstanceOfType(activity, typeof(UserLogin));
            Assert.AreEqual(member.Id, activity.UserId);
            Assert.AreNotEqual(login.SessionId, activity.SessionId);
            Assert.AreEqual(login.IpAddress, activity.IpAddress);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedAutomatically, ((UserLogin)activity).AuthenticationStatus);
        }

        [TestMethod]
        public void TestRememberMeEmployer()
        {
            // Create the member.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));

            // Log in.

            Get(EmployerHomeUrl);
            SubmitLogIn(employer, true);
            var userCookieValue = GetCookieValue("user_cookie");
            var passwordCookieValue = GetCookieValue("passwordCookie");
            var login = AssertActivity(_userSessionsQuery.GetUserActivity(employer.Id), AuthenticationStatus.Authenticated);

            // Logout.

            TestUtils.SleepForDifferentSqlTimestamp();
            LogOut();
            var logout = new UserLogout { UserId = employer.Id };
            AssertActivities(_userSessionsQuery.GetUserActivity(employer.Id), GetExpectedActivities(login, logout));

            // Set the cookies and try to access the home page.

            Browser.Cookies = new CookieContainer();
            var uri = new Uri(new ReadOnlyApplicationUrl("/").AbsoluteUri);
            Browser.Cookies.Add(uri, new Cookie("user_cookie", userCookieValue));
            Browser.Cookies.Add(uri, new Cookie("passwordCookie", passwordCookieValue));
            Get(EmployerHomeUrl);

            var activities = _userSessionsQuery.GetUserActivity(employer.Id);
            Assert.AreEqual(3, activities.Count);
            AssertActivities(activities.Take(2).ToList(), GetExpectedActivities(login, logout));

            // Last activity is automatic log in.

            var activity = activities[2];
            Assert.IsInstanceOfType(activity, typeof(UserLogin));
            Assert.AreEqual(employer.Id, activity.UserId);
            Assert.AreNotEqual(login.SessionId, activity.SessionId);
            Assert.AreEqual(login.IpAddress, activity.IpAddress);
            Assert.AreEqual(AuthenticationStatus.AuthenticatedAutomatically, ((UserLogin)activity).AuthenticationStatus);
        }

        private static UserSessionActivity[] GetExpectedActivities(UserSessionActivity login, params UserSessionActivity[] activities)
        {
            return new[] { login }.Concat(from a in activities select GetExpectedActivity(login, a)).ToArray();
        }

        private static UserSessionActivity GetExpectedActivity(UserSessionActivity login, UserSessionActivity activity)
        {
            activity.SessionId = login.SessionId;
            activity.IpAddress = login.IpAddress;
            return activity;
        }

        private static UserLogin AssertActivity(IList<UserSessionActivity> activities, AuthenticationStatus status)
        {
            Assert.AreEqual(1, activities.Count);
            Assert.IsInstanceOfType(activities[0], typeof(UserLogin));
            Assert.AreEqual(status, ((UserLogin)activities[0]).AuthenticationStatus);
            return (UserLogin)activities[0];
        }

        private static void AssertActivities(IList<UserSessionActivity> activities, IList<UserSessionActivity> expectedActivities)
        {
            Assert.AreEqual(expectedActivities.Count, activities.Count);
            for (var index = 0; index < expectedActivities.Count; ++index)
                AssertActivity(activities[index], expectedActivities[index]);
        }

        private static void AssertActivity(UserSessionActivity activity, UserSessionActivity expectedActivity)
        {
            Assert.IsInstanceOfType(activity, expectedActivity.GetType());
            Assert.AreEqual(expectedActivity.UserId, activity.UserId);
            Assert.AreEqual(expectedActivity.SessionId, activity.SessionId);
            Assert.AreEqual(expectedActivity.IpAddress, activity.IpAddress);

            if (expectedActivity is UserLogin)
                Assert.AreEqual(((UserLogin)expectedActivity).AuthenticationStatus, ((UserLogin)activity).AuthenticationStatus);
        }

        private string GetCookieValue(string name)
        {
            var cookies = Browser.Cookies.GetCookies(Browser.CurrentUrl);
            var cookie = cookies[name];
            return cookie == null ? null : cookie.Value;
        }
    }
}
