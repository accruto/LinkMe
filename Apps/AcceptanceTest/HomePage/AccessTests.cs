using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.HomePage
{
    [TestClass]
    public class AccessTests
        : WebTestClass
    {
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private ReadOnlyUrl _employerHomeUrl;

        [TestInitialize]
        public void TestInitialize()
        {
            _employerHomeUrl = new ReadOnlyApplicationUrl("~/employers");
        }

        [TestMethod]
        public void TestMemberAccessHome()
        {
            TestMemberAccess(HomeUrl);
        }

        [TestMethod]
        public void TestEmployerAccessHome()
        {
            TestEmployerAccess(HomeUrl);
        }

        [TestMethod]
        public void TestMemberAccessEmployerHome()
        {
            TestMemberAccess(_employerHomeUrl);
        }

        [TestMethod]
        public void TestEmployerAccessEmployerHome()
        {
            TestEmployerAccess(_employerHomeUrl);
        }

        private void TestMemberAccess(ReadOnlyUrl url)
        {
            // Try to access as member, should be redirected to member home.

            var member = _memberAccountsCommand.CreateTestMember(0);
            LogIn(member);
            Get(url);
            AssertUrl(LoggedInMemberHomeUrl);
        }

        private void TestEmployerAccess(ReadOnlyUrl url)
        {
            // Try to access as employer, should be redirected to employer home.

            var employer = _employerAccountsCommand.CreateTestEmployer(0, _organisationsCommand.CreateTestOrganisation(0));
            LogIn(employer);
            Get(url);
            AssertUrl(LoggedInEmployerHomeUrl);
        }
    }
}
