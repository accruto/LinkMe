using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Web.Areas.Public.Models.Join;

namespace LinkMe.Web.Areas.Public.Views.Join.Verticals.Aime
{
    public class CommunityPersonalDetails
        : ViewUserControl<PersonalDetailsModel>
    {
        protected AimeMemberStatus? GetStatus(AffiliationItems items)
        {
            var aimeItems = items as AimeAffiliationItems;
            return aimeItems == null ? null : aimeItems.Status;
        }
    }
}
