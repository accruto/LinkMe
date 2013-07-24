using System;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Roles.Test.Recruiters;
using LinkMe.Domain.Users.Test.Employers;
using LinkMe.Domain.Users.Test.Members;
using LinkMe.Query.Members;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Query.Search.Test.Members.Activity
{
    [TestClass]
    public class HasViewedMemberActivityFiltersTests
        : MemberActivityFiltersTests
    {
        private readonly ChannelApp _app = new ChannelApp { ChannelId = Guid.NewGuid(), Id = Guid.NewGuid() };

        [TestMethod]
        public void TestFilterHasViewed()
        {
            var member1 = _membersCommand.CreateTestMember(1);
            var member2 = _membersCommand.CreateTestMember(2);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member1 has been viewed.

            _employerMemberViewsCommand.ViewMember(_app, employer, member1);

            // Filter.

            TestFilter(employer, CreateHasViewedQuery, new[] { member1.Id, member2.Id }, new[] { member1.Id }, new[] { member2.Id });
        }

        [TestMethod]
        public void TestFilterBlockHasViewed()
        {
            var member = _membersCommand.CreateTestMember(1);
            var employer = _employersCommand.CreateTestEmployer(1, _organisationsCommand.CreateTestOrganisation(1));

            // Member has been viewed.

            _employerMemberViewsCommand.ViewMember(_app, employer, member);

            // Filter.

            TestFilter(new[] { member.Id }, employer, CreateHasViewedQuery(true), new[] { member.Id });

            // Block.

            _candidateListsCommand.AddCandidateToBlockList(employer, _candidateBlockListsQuery.GetTemporaryBlockList(employer), member.Id);
            TestFilter(new Guid[0], employer, CreateHasViewedQuery(true), new[] { member.Id });
        }

        private static MemberSearchQuery CreateHasViewedQuery(bool? value)
        {
            return new MemberSearchQuery { HasViewed = value };
        }
    }
}
