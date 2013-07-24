using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Unregistered.Autopeople
{
    public static class AutopeopleHelper
    {
        private static readonly ICommunitiesQuery _communitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();

        public static bool IsAutoPeople()
        {
            var id = ActivityContext.Current.Community.Id;
            if (id == null)
                return false;

            var community = _communitiesQuery.GetCommunity(id.Value);
            if (community == null)
                return false;

            // This is the hack.

            return community.Name == "Autopeople";
        }
    }
}
