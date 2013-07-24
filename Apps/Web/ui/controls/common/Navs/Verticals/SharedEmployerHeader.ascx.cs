using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Common.Navs.Verticals
{
    public partial class SharedEmployerHeader
        : EmployerHeaderUserControl
    {
        private static readonly ICommunitiesQuery CommunitiesQuery = Container.Current.Resolve<ICommunitiesQuery>();

        protected bool CanPurchase()
        {
            var communityId = ActivityContext.Current.Community.Id;
            if (communityId != null)
            {
                var community = CommunitiesQuery.GetCommunity(communityId.Value);
                if (community.HasOrganisations && !community.OrganisationsCanSearchAllMembers)
                    return false;
            }

            return true;
        }
    }
}