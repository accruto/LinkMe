using System;
using LinkMe.Apps.Presentation.Domain.Users.Members;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Service;

namespace LinkMe.Web.UI.Controls.Networkers.OverlayPopups
{
    public partial class InviteRepresentative
        : LinkMeUserControl
    {
        private Member _invitee;
        private PersonalView _view;
        private readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();

        public const string HeadingFormat = "Invite {0} to represent me";

        public Member Invitee
        {
            get { return _invitee; }
            set { _invitee = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _view = _memberViewsQuery.GetPersonalView(LoggedInMemberId, _invitee);
            imgPhoto.ImageUrl = _view.GetPhotoUrlOrDefault().ToString();
        }

        protected string GetSendInvitationJavascript()
        {
            return @"function SendInvitation()
			    {
                    var msg = $('txtCustomMessage').value;
				    banishOverlayPopup();
				    populateOverlayPopup('" + GetUrlForPage<RepresentativePopupContents>()
                + "', '" + RepresentativePopupContents.InviteeIdParameter + "=" + _invitee.Id
                + "&" + RepresentativePopupContents.SendInvitationParameter + "=true&"
                + RepresentativePopupContents.MessageParameter + @"=' + msg);
			    }";
        }

        protected string GetInviteeFirstName()
        {
            return HtmlUtil.TextToHtml(_view.GetFirstNameDisplayText());
        }
    }
}