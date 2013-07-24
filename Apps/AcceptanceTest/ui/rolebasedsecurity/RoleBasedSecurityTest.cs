using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Domain.Roles.Test.Recruiters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.ui.rolebasedsecurity
{
	[TestClass]
    public class RoleBasedSecurityTest : WebFormDataTestCase
	{
		[TestMethod]
		public void TestEmployer()
		{
		    var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
			LogIn(employer);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(LoggedInAdministratorHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);
		}

		[TestMethod]
		public void TestAdministrator()
		{
		    var administrator = _administratorAccountsCommand.CreateTestAdministrator(0);
			LogIn(administrator);
            AssertUrl(LoggedInAdministratorHomeUrl);

            // As the employer is the search candidates page you can actually get there as a non-employer.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInAdministratorHomeUrl);

            Get(LoggedInAdministratorHomeUrl);
            AssertUrl(LoggedInAdministratorHomeUrl);
		}

		[TestMethod]
		public void TestMember()
		{
		    var member = _memberAccountsCommand.CreateTestMember(0);
		    LogIn(member);
            AssertUrl(LoggedInMemberHomeUrl);

            // As the employer is the search candidates page you can actually get there as a non-employer.

            Get(LoggedInEmployerHomeUrl);
            AssertUrl(LoggedInEmployerHomeUrl);

            Get(LoggedInAdministratorHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);

            Get(LoggedInMemberHomeUrl);
            AssertUrl(LoggedInMemberHomeUrl);
		}
	}
}