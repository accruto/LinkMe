using LinkMe.Domain;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Reports.DailyReports.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Reports.Test.DailyReports
{
    [TestClass]
    public class DailyReportsTests
        : TestClass
    {
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IDailyReportsQuery _dailyReportsQuery = Resolve<IDailyReportsQuery>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestMemberViewsWithLinkMe()
        {
            // It used to be the case that we filtered out LinkMe employers from these reports but given the
            // changes in the company etc we will include them from now on because it makes things a whole lot
            // easier, and stats from LinkMe should not be significant now.

            var organisation = _organisationsCommand.CreateTestVerifiedOrganisation("LinkMe");
            var employer1 = _employersCommand.CreateTestEmployer(0, organisation);

            var employer2 = _employersCommand.CreateTestEmployer(2, _organisationsCommand.CreateTestOrganisation(2));
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var member3 = _membersCommand.CreateTestMember(3);

            // Make some viewings.

            var channel = _channelsQuery.GetChannel("Web");
            var app = _channelsQuery.GetChannelApp(channel.Id, "Web");

            _employerMemberViewsCommand.ViewMember(app, employer1, member1);
            Assert.AreEqual(1, _dailyReportsQuery.GetDailyReport(DayRange.Today).MemberViewingReports["Web"].TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer2, member1);
            Assert.AreEqual(2, _dailyReportsQuery.GetDailyReport(DayRange.Today).MemberViewingReports["Web"].TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer1, member2);
            Assert.AreEqual(3, _dailyReportsQuery.GetDailyReport(DayRange.Today).MemberViewingReports["Web"].TotalViewings);

            _employerMemberViewsCommand.ViewMember(app, employer2, member3);
            Assert.AreEqual(4, _dailyReportsQuery.GetDailyReport(DayRange.Today).MemberViewingReports["Web"].TotalViewings);
        }
    }
}
