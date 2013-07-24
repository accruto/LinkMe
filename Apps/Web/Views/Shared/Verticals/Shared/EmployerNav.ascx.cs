using LinkMe.Domain.Roles.Affiliations.Communities.Queries;
using Microsoft.Practices.Unity;

namespace LinkMe.Web.Views.Shared.Verticals.Shared
{
    public class EmployerNav
        : Views.Shared.EmployerNav
    {
        [Dependency]
        public ICommunitiesQuery CommunitiesQuery { get; set; }

        protected bool CanPurchase()
        {
            var communityId = ActivityContext.Community.Id;
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
