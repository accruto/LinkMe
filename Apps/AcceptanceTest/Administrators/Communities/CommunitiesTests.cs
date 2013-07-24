using LinkMe.Apps.Agents.Test.Communities;
using LinkMe.Apps.Agents.Test.Users.Custodians;
using LinkMe.Apps.Agents.Users.Administrators.Commands;
using LinkMe.Apps.Agents.Test.Users.Administrators;
using LinkMe.Apps.Agents.Users.Custodians.Commands;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Commands;
using LinkMe.Domain.Roles.Affiliations.Verticals.Queries;
using LinkMe.Framework.Content;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkMe.AcceptanceTest.Administrators.Communities
{
    [TestClass]
    public abstract class CommunitiesTests
        : WebTestClass
    {
        protected readonly ICommunitiesCommand _communitiesCommand = Resolve<ICommunitiesCommand>();
        protected readonly IVerticalsQuery _verticalsQuery = Resolve<IVerticalsQuery>();
        protected readonly IVerticalsCommand _verticalsCommand = Resolve<IVerticalsCommand>();
        protected readonly IContentEngine _contentEngine = Resolve<IContentEngine>();
        protected readonly IAdministratorAccountsCommand _administratorAccountsCommand = Resolve<IAdministratorAccountsCommand>();
        private readonly ICustodianAccountsCommand _custodianAccountsCommand = Resolve<ICustodianAccountsCommand>();

        private ReadOnlyUrl _communitiesUrl;
        private ReadOnlyUrl _custodiansUrl;

        [TestInitialize]
        public void CommunitiesTestsInitialize()
        {
            _communitiesUrl = new ReadOnlyApplicationUrl("~/administrators/communities");
            _custodiansUrl = new ReadOnlyApplicationUrl("~/administrators/custodians");
        }

        protected Community CreateCommunity()
        {
            return TestCommunity.TheNursingCentre.CreateTestCommunity(_communitiesCommand, _verticalsCommand, _contentEngine);
        }

        protected Administrator CreateAdministrator(int index)
        {
            return _administratorAccountsCommand.CreateTestAdministrator(index);
        }

        protected Custodian CreateCustodian(int index, Community community)
        {
            return _custodianAccountsCommand.CreateTestCustodian(index, community.Id);
        }

        protected ReadOnlyUrl GetCommunityUrl(Community community)
        {
            var url = (_communitiesUrl.AbsoluteUri + "/")
                .AddUrlSegments(community.Id.ToString());
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCustodiansUrl(Community community)
        {
            var url = (_communitiesUrl.AbsoluteUri + "/")
                .AddUrlSegments(community.Id + "/custodians");
            return new ReadOnlyApplicationUrl(true, url.ToLower());
        }

        protected ReadOnlyUrl GetNewCustodianUrl(Community community)
        {
            var url = (_communitiesUrl.AbsoluteUri + "/")
                .AddUrlSegments(community.Id + "/custodians/new");
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCustodianUrl(Custodian custodian)
        {
            var url = (_custodiansUrl.AbsoluteUri + "/")
                .AddUrlSegments(custodian.Id.ToString());
            return new ReadOnlyApplicationUrl(url.ToLower());
        }

        protected ReadOnlyUrl GetCommunitiesUrl()
        {
            return _communitiesUrl;
        }
    }
}
