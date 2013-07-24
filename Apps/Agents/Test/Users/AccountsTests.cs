using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Agents.Test.Users
{
    [TestClass]
    public class AccountsTests
        : TestClass
    {
        private const string EmailAddress = "homer@test.linkme.net.au";

        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void CreateMemberEmployerSameLoginId()
        {
            _memberAccountsCommand.CreateTestMember(EmailAddress);
            _employerAccountsCommand.CreateTestEmployer(EmailAddress, _organisationsCommand.CreateTestOrganisation(0));
        }

        [TestMethod, ExpectedException(typeof(DuplicateUserException))]
        public void CreateEmployerMemberSameLoginId()
        {
            _employerAccountsCommand.CreateTestEmployer(EmailAddress, _organisationsCommand.CreateTestOrganisation(0));
            _memberAccountsCommand.CreateTestMember(EmailAddress);
        }
    }
}
