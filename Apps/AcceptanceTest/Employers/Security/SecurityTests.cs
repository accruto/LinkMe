using System;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Test.Users.Employers;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Candidates.Queries;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Security
{
    [TestClass]
    public class SecurityTests
        : WebTestClass
    {
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();
        private readonly ICandidateBlockListsQuery _candidateBlockListsQuery = Resolve<ICandidateBlockListsQuery>();
        private readonly ICandidateFoldersQuery _candidateFoldersQuery = Resolve<ICandidateFoldersQuery>();

        [TestMethod]
        public void TestSearch()
        {
            TestCanAccess("~/search/candidates", CreateEmployer(0, EmployerSubRole.Employer));
            TestCanAccess("~/search/candidates", CreateEmployer(1, EmployerSubRole.Recruiter));
            TestCanAccess("~/search/candidates", CreateMember(2));
            TestCanAccess("~/search/candidates", CreateAdministrator(3));
            TestCanAccess("~/search/candidates", CreateCustodian(4));
        }

        [TestMethod]
        public void TestFolders()
        {
            TestAccess("~/employers/candidates/folders");
        }

        [TestMethod]
        public void TestFolder()
        {
            var employer = CreateEmployer(0, EmployerSubRole.Employer);
            var path = "~/employers/candidates/folders/" + _candidateFoldersQuery.GetShortlistFolder(employer).Id;
            TestCanAccess(path, employer);

            employer = CreateEmployer(1, EmployerSubRole.Recruiter);
            path = "~/employers/candidates/folders/" + _candidateFoldersQuery.GetShortlistFolder(employer).Id;
            TestCanAccess(path, employer);

            TestCannotAccess(path, CreateMember(2));
            TestCannotAccess(path, CreateAdministrator(3));
            TestCannotAccess(path, CreateCustodian(4));
        }

        [TestMethod]
        public void TestFlagList()
        {
            TestAccess("~/employers/candidates/flaglist");
        }

        [TestMethod]
        public void TestBlockList()
        {
            var employer = CreateEmployer(0, EmployerSubRole.Employer);
            var path = "~/employers/candidates/blocklists/" + _candidateBlockListsQuery.GetPermanentBlockList(employer).BlockListType;
            TestCanAccess(path, employer);

            employer = CreateEmployer(1, EmployerSubRole.Recruiter);
            path = "~/employers/candidates/blocklists/" + _candidateBlockListsQuery.GetPermanentBlockList(employer).BlockListType;
            TestCanAccess(path, employer);

            TestCannotAccess(path, CreateMember(2));
            TestCannotAccess(path, CreateAdministrator(3));
            TestCannotAccess(path, CreateCustodian(4));
        }

        private void TestAccess(string path)
        {
            TestCanAccess(path, CreateEmployer(0, EmployerSubRole.Employer));
            TestCanAccess(path, CreateEmployer(1, EmployerSubRole.Recruiter));
            TestCannotAccess(path, CreateMember(2));
            TestCannotAccess(path, CreateAdministrator(3));
            TestCannotAccess(path, CreateCustodian(4));
        }

        private void TestCanAccess(string path, IUser user)
        {
            LogIn(user);
            var url = new ReadOnlyApplicationUrl(path);
            Get(url);
            AssertPath(url);
        }

        private void TestCannotAccess(string path, IRegisteredUser user)
        {
            LogIn(user);
            var url = new ReadOnlyApplicationUrl(path);
            Get(url);

            switch (user.UserType)
            {
                case UserType.Member:
                    AssertUrl(LoggedInMemberHomeUrl);
                    break;

                case UserType.Employer:
                    AssertUrl(LoggedInEmployerHomeUrl);
                    break;

                case UserType.Administrator:
                    AssertUrl(LoggedInAdministratorHomeUrl);
                    break;

                default:
                    AssertUrl(LoggedInCustodianHomeUrl);
                    break;
            }
        }

        private Employer CreateEmployer(int index, EmployerSubRole subRole)
        {
            var employer = _employerAccountsCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            if (employer.SubRole != subRole)
            {
                employer.SubRole = subRole;
                _employerAccountsCommand.UpdateEmployer(employer);
            }

            return employer;
        }

        private Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }

        private Administrator CreateAdministrator(int index)
        {
            return _administratorAccountsCommand.CreateTestAdministrator(index);
        }

        private Custodian CreateCustodian(int index)
        {
            return _custodianAccountsCommand.CreateTestCustodian(index, Guid.NewGuid());
        }
    }
}
