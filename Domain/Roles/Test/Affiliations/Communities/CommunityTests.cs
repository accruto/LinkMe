using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Domain.Test.Data;
using LinkMe.Framework.Utility.Sql;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.Domain.Roles.Test.Affiliations.Communities
{
    [TestClass]
    public abstract class CommunityTests
        : TestClass
    {
        protected readonly ICommunitiesQuery _communitiesQuery = Resolve<ICommunitiesQuery>();
        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();

        protected const string CommunityNameFormat = "Community{0}";
        protected const string CommunityShortNameFormat = "Comm{0}";

        [TestInitialize]
        public void CommunityTestsInitialize()
        {
            Resolve<IDbConnectionFactory>().DeleteAllTestData();
        }

        protected Community CreateCommunity(int index)
        {
            var community = new Community
            {
                Name = string.Format(CommunityNameFormat, index),
                ShortName = string.Format(CommunityShortNameFormat, index),
            };
            _communitiesCommand.CreateCommunity(community);
            return community;
        }
    }
}
