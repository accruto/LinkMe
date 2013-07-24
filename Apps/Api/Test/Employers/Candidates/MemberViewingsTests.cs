using LinkMe.Domain.Channels.Queries;
using LinkMe.Domain.Users.Employers.Views;
using LinkMe.Framework.Utility.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Apps.Api.Test.Employers.Candidates
{
    [TestClass]
    public class MemberAccessesTests
        : CandidateTests
    {
        private readonly IEmployerViewsRepository _employerViewsRepository = Resolve<IEmployerViewsRepository>();
        private readonly IChannelsQuery _channelsQuery = Resolve<IChannelsQuery>();

        [TestMethod]
        public void TestAccess()
        {
            var member = CreateMember(0);
            var employer = CreateEmployer(0);

            Assert.AreEqual(0, _employerViewsRepository.GetMemberViewings(employer.Id, member.Id).Count);

            LogIn(employer);
            AssertJsonSuccess(Candidate(member.Id));

            var viewings = _employerViewsRepository.GetMemberViewings(employer.Id, member.Id);
            Assert.AreEqual(1, viewings.Count);

            Assert.AreEqual(_channelsQuery.GetChannel("API").Id, viewings[0].ChannelId);
            Assert.AreEqual(_channelsQuery.GetChannelApp(_channelsQuery.GetChannel("API").Id, "iOS").Id, viewings[0].AppId);
        }
    }
}