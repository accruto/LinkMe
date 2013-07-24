using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates.PhoneNumbers
{
    [TestClass]
    public class MemberAccessesTests
        : PhoneNumberTests
    {
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestAccess()
        {
            var member = CreateMember();
            var employer = CreateEmployer();
            Allocate(employer.Id, null);

            Assert.AreEqual(0, _employerViewsRepository.GetMemberAccesses(employer.Id, member.Id).Count);

            LogIn(employer);
            AssertJsonSuccess(PhoneNumbers(member.Id));

            var accesses = _employerViewsRepository.GetMemberAccesses(employer.Id, member.Id);
            Assert.AreEqual(1, accesses.Count);

            Assert.AreEqual(MemberAccessReason.PhoneNumberViewed, accesses[0].Reason);
            Assert.AreEqual(_channelsQuery.GetChannel("API").Id, accesses[0].ChannelId);
            Assert.AreEqual(_channelsQuery.GetChannelApp(_channelsQuery.GetChannel("API").Id, "iOS").Id, accesses[0].AppId);
        }
    }
}
