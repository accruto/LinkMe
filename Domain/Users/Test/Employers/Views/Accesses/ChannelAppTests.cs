using System;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Test.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Users.Test.Employers.Views.Accesses
{
    [TestClass]
    public class ChannelAppsTests
        : AccessesTests
    {
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestChannelApp()
        {
            var employer = CreateEmployer(1);
            var member1 = CreateMember(employer, 1);
            var member2 = CreateMember(employer, 2);
            var member3 = CreateMember(employer, 3);
            var member4 = CreateMember(employer, 4);

            var channelApp1 = _channelsQuery.GetChannelApp(_channelsQuery.GetChannel("Web").Id, "Web");
            var channelApp2 = _channelsQuery.GetChannelApp(_channelsQuery.GetChannel("API").Id, "iOS");

            _employerMemberViewsCommand.CheckCanAccessMember(channelApp1, employer, member1, MemberAccessReason.Unlock);
            _employerMemberViewsCommand.AccessMember(channelApp1, employer, member1, MemberAccessReason.Unlock);
            _employerMemberViewsCommand.CheckCanAccessMember(channelApp2, employer, member2, MemberAccessReason.Unlock);
            _employerMemberViewsCommand.AccessMember(channelApp2, employer, member2, MemberAccessReason.Unlock);

            var views = new ProfessionalViews { member1, member3 };
            _employerMemberViewsCommand.AccessMembers(channelApp2, employer, views, MemberAccessReason.Unlock);

            _employerMemberViewsCommand.CheckCanAccessMember(channelApp1, employer, member4, MemberAccessReason.Unlock);
            _employerMemberViewsCommand.AccessMember(channelApp1, employer, member4, MemberAccessReason.Unlock);

            AssertMemberAccesses(employer.Id, member1.Id, channelApp1, channelApp2);
            AssertMemberAccesses(employer.Id, member2.Id, channelApp2);
            AssertMemberAccesses(employer.Id, member3.Id, channelApp2);
        }

        private void AssertMemberAccesses(Guid employerId, Guid memberId, params ChannelApp[] apps)
        {
            var accesses = _employerViewsRepository.GetMemberAccesses(employerId, memberId);
            Assert.AreEqual(apps.Length, accesses.Count);
            foreach (var app in apps)
            {
                var channelId = app.ChannelId;
                var appId = app.Id;
                Assert.IsTrue((from a in accesses where a.ChannelId == channelId && a.AppId == appId select a).Any());
            }
        }

        private ProfessionalView CreateMember(IEmployer employer, int index)
        {
            var member = _membersCommand.CreateTestMember(index);
            return _employerMemberViewsQuery.GetProfessionalView(employer, member);
        }

        private IEmployer CreateEmployer(int index)
        {
            var employer = _employersCommand.CreateTestEmployer(index, _organisationsCommand.CreateTestOrganisation(index));
            _allocationsCommand.CreateAllocation(new Allocation { CreditId = _creditsQuery.GetCredit<ContactCredit>().Id, InitialQuantity = null, OwnerId = employer.Id });
            return employer;
        }
    }
}
