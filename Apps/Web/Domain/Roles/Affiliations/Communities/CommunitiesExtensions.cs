using System;
using LinkMe.Apps.Agents.Context;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Affiliations.Communities;
using LinkMe.Domain.Roles.Affiliations.Communities.Queries;

namespace LinkMe.Web.Domain.Roles.Affiliations.Communities
{
    public static class CommunitiesExtensions
    {
        public static Community GetCurrentCommunity(this ICommunitiesQuery communitiesQuery)
        {
            // Get the current community from the context.

            var id = ActivityContext.Current.Community.Id;
            return id != null ? communitiesQuery.GetCommunity(id.Value) : null;
        }

        public static bool CanSearchAllMembers(this Employer employer, Community community)
        {
            // If the community does not have any members then can.

            if (!community.HasMembers)
                return true;

            // If the community explicitly allows full search then can.

            if (community.OrganisationsCanSearchAllMembers)
                return true;

            // An anonymous recruiter cannot.

            if (employer == null)
                return false;

            // A recruiter with no affiliation to any community can.

            if (employer.Organisation.AffiliateId == null)
                return true;

            // A recruiter with an affiliation to another community can.

            if (employer.Organisation.AffiliateId.Value != community.Id)
                return true;

            // A recruiter with an affiliation to this community cannot.

            return false;
        }

        public static Guid? GetDefaultSearchCommunityId(this ICommunitiesQuery communitiesQuery, Employer employer)
        {
            // Check the current community.

            var community = communitiesQuery.GetCurrentCommunity();

            // If there is no current community then no community to search by.

            if (community == null)
                return null;

            // If they cannot search all members then restrict them to the current community.

            return employer.CanSearchAllMembers(community) ? (Guid?) null : community.Id;
        }
    }
}
