using LinkMe.Apps.Asp.Mvc.Views;
using LinkMe.Domain.Users.Members.Affiliations;
using LinkMe.Domain.Users.Members.Affiliations.Affiliates;
using LinkMe.Web.Areas.Public.Models.Join;

namespace LinkMe.Web.Areas.Public.Views.Join.Verticals.Finsia
{
    public class CommunityPersonalDetails
        : ViewUserControl<PersonalDetailsModel>
    {
        protected bool GetIsMember(AffiliationItems items)
        {
            var finsiaItems = items as FinsiaAffiliationItems;
            return finsiaItems == null || string.IsNullOrEmpty(finsiaItems.MemberId)
                ? false
                : true;
        }

        protected string GetMemberId(AffiliationItems items)
        {
            var finsiaItems = items as FinsiaAffiliationItems;
            return finsiaItems == null || string.IsNullOrEmpty(finsiaItems.MemberId)
                ? null
                : finsiaItems.MemberId;
        }
    }
}
