using System;
using LinkMe.Domain;
using LinkMe.Domain.Accounts.Commands;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Credits.Commands;
using LinkMe.Domain.Credits.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Employers.Views.Queries;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Reports.Users.Employers.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.Users.Employers
{
    [TestClass]
    public class ContactReportsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IMembersQuery _membersQuery = Resolve<IMembersQuery>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerMemberViewsQuery _employerMemberViewsQuery = Resolve<IEmployerMemberViewsQuery>();
        private readonly IEmployerMemberAccessReportsQuery _employerMemberAccessReportsQuery = Resolve<IEmployerMemberAccessReportsQuery>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly ICreditsQuery _creditsQuery = Resolve<ICreditsQuery>();
        private readonly IAllocationsCommand _allocationsCommand = Resolve<IAllocationsCommand>();
        private readonly IUserAccountsCommand _userAccountsCommand = Resolve<IUserAccountsCommand>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestGetMemberViews()
        {
            var channel = _channelsQuery.GetChannel("Web");
            var app = _channelsQuery.GetChannelApp(channel.Id, "Web");

            Assert.AreEqual(0, _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(channel, DayRange.Today).TotalViewings);

            // Make some viewings.

            var employer1 = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            var employer2 = _employersCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(2));
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var member3 = _membersCommand.CreateTestMember(3);

            _employerMemberViewsCommand.ViewMember(app, employer1, member1);
            Assert.AreEqual(1, _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(channel, DayRange.Today).TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer1, member2);
            Assert.AreEqual(2, _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(channel, DayRange.Today).TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer2, member1);
            Assert.AreEqual(3, _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(channel, DayRange.Today).TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer2, member3);
            Assert.AreEqual(4, _employerMemberAccessReportsQuery.GetEmployerMemberViewingReport(channel, DayRange.Today).TotalViewings);
        }

        [TestMethod]
        public void TestGetEmailMemberUnlockings()
        {
            var channel = _channelsQuery.GetChannel("Web");
            var app = _channelsQuery.GetChannelApp(channel.Id, "Web");

            Assert.AreEqual(0, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).MessagesSent);

            // Make some contacts.

            var employer1 = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer1.Id });
            var employer2 = _employersCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(2));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer2.Id });

            var member1 = _membersCommand.CreateTestMember(1);
            _userAccountsCommand.EnableUserAccount(member1, Guid.NewGuid());
            member1 = _membersQuery.GetMember(member1.Id);

            var member2 = _membersCommand.CreateTestMember(2);
            _userAccountsCommand.EnableUserAccount(member2, Guid.NewGuid());
            member2 = _membersQuery.GetMember(member2.Id);

            var member3 = _membersCommand.CreateTestMember(3);
            _userAccountsCommand.EnableUserAccount(member3, Guid.NewGuid());
            member3 = _membersQuery.GetMember(member3.Id);

            _employerMemberViewsCommand.AccessMember(app, employer1, _employerMemberViewsQuery.GetProfessionalView(employer1, member1), MemberAccessReason.MessageSent);
            Assert.AreEqual(1, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).MessagesSent);

            _employerMemberViewsCommand.AccessMember(app, employer1, _employerMemberViewsQuery.GetProfessionalView(employer1, member2), MemberAccessReason.MessageSent);
            Assert.AreEqual(2, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).MessagesSent);

            _employerMemberViewsCommand.AccessMember(app, employer2, _employerMemberViewsQuery.GetProfessionalView(employer2, member1), MemberAccessReason.MessageSent);
            Assert.AreEqual(3, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).MessagesSent);

            _employerMemberViewsCommand.AccessMember(app, employer2, _employerMemberViewsQuery.GetProfessionalView(employer2, member3), MemberAccessReason.MessageSent);
            Assert.AreEqual(4, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).MessagesSent);
        }

        [TestMethod]
        public void TestGetPhoneMemberUnlockings()
        {
            var channel = _channelsQuery.GetChannel("Web");
            var app = _channelsQuery.GetChannelApp(channel.Id, "Web");

            Assert.AreEqual(0, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).PhoneNumbersViewed);

            // Make some contacts.

            var employer1 = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));
            _allocationsCommand.CreateAllocation(new Allocation {CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer1.Id});
            var employer2 = _employersCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(2));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, OwnerId = employer2.Id });

            var member1 = _membersCommand.CreateTestMember(1);
            _userAccountsCommand.EnableUserAccount(member1, Guid.NewGuid());
            member1 = _membersQuery.GetMember(member1.Id);

            var member2 = _membersCommand.CreateTestMember(2);
            _userAccountsCommand.EnableUserAccount(member2, Guid.NewGuid());
            member2 = _membersQuery.GetMember(member2.Id);

            var member3 = _membersCommand.CreateTestMember(3);
            _userAccountsCommand.EnableUserAccount(member3, Guid.NewGuid());
            member3 = _membersQuery.GetMember(member3.Id);

            _employerMemberViewsCommand.AccessMember(app, employer1, _employerMemberViewsQuery.GetProfessionalView(employer1, member1), MemberAccessReason.PhoneNumberViewed);
            Assert.AreEqual(1, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).PhoneNumbersViewed);

            _employerMemberViewsCommand.AccessMember(app, employer1, _employerMemberViewsQuery.GetProfessionalView(employer1, member2), MemberAccessReason.PhoneNumberViewed);
            Assert.AreEqual(2, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).PhoneNumbersViewed);

            _employerMemberViewsCommand.AccessMember(app, employer2, _employerMemberViewsQuery.GetProfessionalView(employer2, member1), MemberAccessReason.PhoneNumberViewed);
            Assert.AreEqual(3, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).PhoneNumbersViewed);

            _employerMemberViewsCommand.AccessMember(app, employer2, _employerMemberViewsQuery.GetProfessionalView(employer2, member3), MemberAccessReason.PhoneNumberViewed);
            Assert.AreEqual(4, _employerMemberAccessReportsQuery.GetEmployerMemberAccessReport(channel, DayRange.Today).PhoneNumbersViewed);
        }
    }
}
