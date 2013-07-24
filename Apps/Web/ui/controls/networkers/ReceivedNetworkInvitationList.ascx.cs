using System;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Utility.Urls;
using LinkMe.Utility.Validation;
using LinkMe.Web.Domain.Roles.Networking;
using LinkMe.Web.Domain.Users.Members;
using LinkMe.Web.Helpers;
using LinkMe.Web.Members.Friends;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class ReceivedNetworkInvitationList : LinkMeUserControl
    {
        public const int MaxInvitationListLength = 15;
        public const string NoPendingInvitations = "You do not have any pending invitations.";

        private static readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
        private static readonly IMemberFriendsCommand _memberFriendsCommand = Container.Current.Resolve<IMemberFriendsCommand>();
        private static readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private PersonalViews _views;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitialiseInvitations();
        }

        protected void rptInvitations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var invitationId = new Guid((string)e.CommandArgument);
            var invitation = _memberFriendsQuery.GetFriendInvitation(invitationId);
            if (invitation == null)
            {
                LinkMePage.AddError(ValidationErrorMessages.INVALID_INVITATION);
                return;
            }

            var inviter = _membersQuery.GetMember(invitation.InviterId);

            switch (e.CommandName)
            {
                case "AcceptInvitation":
                    try
                    {
                        _memberFriendsCommand.AcceptInvitation(LoggedInMember.Id, invitation);
                        var successMsg = invitation.GetInvitationAcceptedHtml(inviter);
                        LinkMePage.AddConfirm(successMsg, false);
                    }
                    catch (UserException ex)
                    {
                        LinkMePage.AddError(ex);
                    }
                    break;

                case "IgnoreInvitation":
                    _memberFriendsCommand.RejectInvitation(invitation);
                    LinkMePage.AddConfirm(string.Format(ValidationInfoMessages.INVITE_IGNORED, inviter.FullName.MakeNamePossessive()));
                    break;
            }

            InitialiseInvitations(); // Re-initialise - just hiding items casuses bug 8772
        }

        protected void rptRepresentativeInvitations_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var invitationId = new Guid((string)e.CommandArgument);
            var invitation = _memberFriendsQuery.GetRepresentativeInvitation(invitationId);
            if (invitation == null)
            {
                LinkMePage.AddError(ValidationErrorMessages.INVALID_INVITATION);
                return;
            }

            var inviter = _membersQuery.GetMember(invitation.InviterId);

            switch (e.CommandName)
            {
                case "AcceptInvitation":
                    try
                    {
                        _memberFriendsCommand.AcceptInvitation(LoggedInMember.Id, invitation);
                        var successMsg = invitation.GetInvitationAcceptedHtml(inviter);
                        LinkMePage.AddConfirm(successMsg, false);
                    }
                    catch (UserException ex)
                    {
                        LinkMePage.AddError(ex);
                    }
                    break;

                case "IgnoreInvitation":
                    _memberFriendsCommand.RejectInvitation(invitation);
                    LinkMePage.AddConfirm(string.Format(ValidationInfoMessages.INVITE_IGNORED, inviter.FullName.MakeNamePossessive()));
                    break;
            }

            InitialiseInvitations(); // Re-initialise - just hiding items casuses bug 8772
        }

        private void InitialiseInvitations()
        {
            var friendInvitations = _memberFriendsQuery.GetFriendInvitations(LoggedInMember.Id, LoggedInMember.GetBestEmailAddress().Address);
            var representativeInvitations = _memberFriendsQuery.GetRepresentativeInvitations(LoggedInMember.Id, LoggedInMember.GetBestEmailAddress().Address);

            var allIds = (from i in friendInvitations select i.InviterId)
                .Concat(from i in representativeInvitations select i.InviterId);
            _views = _memberViewsQuery.GetPersonalViews(LoggedInUserId, allIds);

            if (friendInvitations.Count > 0)
            {
                rptInvitations.Visible = true;
                rptInvitations.DataSource = friendInvitations;
                rptInvitations.DataBind();
            }
            else
            {
                rptInvitations.Visible = false;
                litNoInvitationsMessage.Visible = true;
                litNoInvitationsMessage.Text = NoPendingInvitations;
            }

            if (representativeInvitations.Count > 0)
            {
                phRepresentative.Visible = true;
                rptRepresentativeInvitations.DataSource = representativeInvitations;
                rptRepresentativeInvitations.DataBind();
            }
            else
            {
                phRepresentative.Visible = false;
            }
        }

        protected static ReadOnlyUrl GetInviterProfileUrl(Invitation invitation)
        {
            return GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, invitation.InviterId.ToString());
        }

        protected ReadOnlyUrl GetInviterImageUrl(Invitation invitation)
        {
            return _views[invitation.InviterId].GetPhotoUrlOrDefault();
        }

        protected string GetInviterFirstName(Invitation invitation)
        {
            return HtmlUtil.TextToHtml(((ICommunicationRecipient)_views[invitation.InviterId]).FirstName);
        }

        protected string GetInviterFullName(Invitation invitation)
        {
            return HtmlUtil.TextToHtml(((ICommunicationRecipient)_views[invitation.InviterId]).FullName);
        }
    }
}
