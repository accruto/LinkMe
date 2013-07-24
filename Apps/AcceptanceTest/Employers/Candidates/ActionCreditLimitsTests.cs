using System;
using LinkMe.Apps.Agents.Test.Users.Members;
using LinkMe.Apps.Agents.Users.Employers.Commands;
using LinkMe.Apps.Agents.Users.Members.Commands;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Employers.Candidates
{
    [TestClass]
    public abstract class ActionCreditLimitsTests
        : ApiTests
    {
        protected readonly IMemberAccountsCommand _memberAccountsCommand = Resolve<IMemberAccountsCommand>();
        private readonly IEmployerAccountsCommand _employerAccountsCommand = Resolve<IEmployerAccountsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();

        private const int DailyLimit = 300;
        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestInitialize]
        public void ActionCreditLimitsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestSomeCredits()
        {
            TestAction(new SomeCreditsCreditInfo(2 * DailyLimit, false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestUnlimitedCredits()
        {
            TestAction(new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Employer, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestSomeCreditsOrganisation()
        {
            TestAction(new SomeCreditsCreditInfo(2 * DailyLimit, false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        [TestMethod]
        public void TestUnlimitedCreditsOrganisation()
        {
            TestAction(new UnlimitedCreditsCreditInfo(false, false, CreditAllocation.Organisation, _employerAccountsCommand, _organisationsCommand, _creditsQuery, _allocationsCommand));
        }

        private void TestAction(CreditInfo creditInfo)
        {
            const int count = DailyLimit + 1;
            var members = new Member[count];
            for (var index = 0; index < count; ++index)
                members[index] = CreateMember(index);

            var employer = creditInfo.CreateEmployer(members);

            _emailServer.ClearEmails();
            if (employer != null)
                LogIn(employer);

            // Perform the action up to the limit.

            var reason = GetReason();
            for (var index = 0; index < DailyLimit; ++index)
                _employerMemberViewsCommand.AccessMember(_app, employer, _employerMemberViewsQuery.GetProfessionalView(employer, members[index]), reason);

            // Perform the action to push it over the limit.

            PerformAction(employer, members[DailyLimit]);
        }

        protected abstract MemberAccessReason GetReason();
        protected abstract void PerformAction(Employer employer, Member member);

        protected virtual Member CreateMember(int index)
        {
            return _memberAccountsCommand.CreateTestMember(index);
        }
    }
}
