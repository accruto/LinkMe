using System;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters.Commands;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Test.Data;
using LinkMe.Domain.Users.Employers.Commands;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Employers.Views.Commands;
using LinkMe.Domain.Users.Members.Commands;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views
{
    [TestClass]
    public class ChannelAppsTests
        : TestClass
    {
        private readonly IEmployerMemberViewsCommand _employerMemberViewsCommand = Resolve<IEmployerMemberViewsCommand>();
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IEmployersCommand _employersCommand = Resolve<IEmployersCommand>();
        private readonly IOrganisationsCommand _organisationsCommand = Resolve<IOrganisationsCommand>();
        private readonly IMembersCommand _membersCommand = Resolve<IMembersCommand>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestInitialize]
        public void ChannelAppsTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        [TestMethod]
        public void TestChannelApp()
        {
            var member1 = CreateMember(1);
            var member2 = CreateMember(2);
            var member3 = CreateMember(3);
            var employer = CreateEmployer(1);

            var channelApp1 = _channelsQuery.GetChannelApp(_channelsQuery.GetChannel("Web").Id, "Web");
            var channelApp2 = _channelsQuery.GetChannelApp(_channelsQuery.GetChannel("API").Id, "iOS");

            _employerMemberViewsCommand.ViewMember(channelApp1, employer, member1);
            _employerMemberViewsCommand.ViewMember(channelApp2, employer, member1);
            _employerMemberViewsCommand.ViewMember(channelApp2, employer, member2);
            _employerMemberViewsCommand.ViewMember(channelApp2, employer, member3);

            AssertMemberViewings(employer.Id, member1.Id, channelApp1, channelApp2);
            AssertMemberViewings(employer.Id, member2.Id, channelApp2);
            AssertMemberViewings(employer.Id, member3.Id, channelApp2);
        }

        private void AssertMemberViewings(Guid employerId, Guid memberId, params ChannelApp[] apps)
        {
            var viewings = _employerViewsRepository.GetMemberViewings(employerId, memberId);
            Assert.AreEqual(apps.Length, viewings.Count);
            foreach (var app in apps)
            {
                var channelId = app.ChannelId;
                var appId = app.Id;
                Assert.IsTrue((from v in viewings where v.ChannelId == channelId && v.AppId == appId select v).Any());
            }
        }

        private Member CreateMember(int index)
        {
            return _membersCommand.CreateTestMember(index);
        }

        private IEmployer CreateEmployer(int index)
        {
            return _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
        }
    }
}
