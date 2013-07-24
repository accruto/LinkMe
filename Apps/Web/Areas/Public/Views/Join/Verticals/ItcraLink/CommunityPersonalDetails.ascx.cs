using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Web.Areas.Public.Models.Join;

namespace LinkMe.Web.Areas.Public.Views.Join.Verticals.ItcraLink
{
    public class CommunityPersonalDetails
        : ViewUserControl<PersonalDetailsModel>
    {
        protected ItcraLinkMemberStatus? GetStatus(AffiliationItems items)
        {
            var itcraLinkItems = items as ItcraLinkAffiliationItems;
            return itcraLinkItems == null ? null : itcraLinkItems.Status;
        }

        protected string GetMemberId(AffiliationItems items)
        {
            var itcraLinkItems = items as ItcraLinkAffiliationItems;
            return itcraLinkItems == null || string.IsNullOrEmpty(itcraLinkItems.MemberId)
                ? null
                : itcraLinkItems.MemberId;
        }
    }
}