using System.Web.UI;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Web.Service;

namespace LinkMe.Web.UI.Controls.Networkers
{
    internal class ContactsListRepresentativeFactory
        : IExtraContactDetailsFactory
    {
        public Control GetExtraActions(TemplateControl parent)
        {
            var control = (ContactsListRepresentativeActions)parent.LoadControl("~/ui/controls/networkers/ContactsListRepresentativeActions.ascx");
            return control;
        }

        public Control GetExtraDescription(TemplateControl parent)
        {
            return null;
        }
    }

    public partial class ContactsListRepresentativeActions
        : LinkMeUserControl, IExtraContactDetailsControl
    {
        public void DisplayContact(PersonalView view, string itemSuffix)
        {
            lnkInviteRepresentative.NavigateUrl = "javascript:populateOverlayPopup('"
                + GetUrlForPage<RepresentativePopupContents>() + "', '"
                + RepresentativePopupContents.InviteeIdParameter + "=" + view.Id + "');";
        }
    }
}