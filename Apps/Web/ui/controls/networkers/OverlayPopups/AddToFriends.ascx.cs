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
	public partial class AddToFriends
        : LinkMeUserControl
	{
		private Member _invitee;
        private PersonalView _view;
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();

	    public const string HeadingFormat = "Invite {0} to join my list of friends";

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
				    populateOverlayPopup('" + GetUrlForPage<InvitationPopupContents>()
                +  "', '" + InvitationPopupContents.InviteeIdParameter + "=" + _invitee.Id
                + "&" + InvitationPopupContents.SendInvitationParameter + "=true&"
                + InvitationPopupContents.MessageParameter + @"=' + msg);
			    }";
		}

		protected string GetInviteeFirstName()
		{
            return HtmlUtil.TextToHtml(_view.GetFirstNameDisplayText());
		}
	}
}