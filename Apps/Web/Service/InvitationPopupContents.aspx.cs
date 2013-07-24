using System;
using LinkMe.Apps.Presentation.Errors;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Commands;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Queries;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Web.UI;
using LinkMe.Web.UI.Controls.Networkers.OverlayPopups;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Web.Service
{
	public partial class InvitationPopupContents : LinkMePage
	{
	    private readonly IMembersQuery _membersQuery = Container.Current.Resolve<IMembersQuery>();
	    private readonly IMemberFriendsCommand _memberFriendsCommand = Container.Current.Resolve<IMemberFriendsCommand>();
        private readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
       
		private static readonly EventSource EventSource = new EventSource(typeof(InvitationPopupContents));

        private const string InviteAlreadyExistsFormat = "You have already sent an invitation to {0}. Please give that member some time to respond. If they have not responded by {1}, you will be able to send another invitation as a reminder.";
        private const string AlreadyFriendsFormat = "You are already friends with {0}.";
        private const string AlreadyDeclinedFormat = "{0} has already declined an invitation from you.";

        public const string InviteeIdParameter = "inviteeId";
        public const string SendInvitationParameter = "sendInvitation";
        public const string MessageParameter = "message";
		private const string CouldNotFindMember = "The member you selected could not be found.";
		private const string CannotAddSelfToFriends = "Sorry. We can't add you to your own friends list.";
		private const string NotAcceptingInvitations = "Sorry, that member is not currently accepting invitations.";

	    protected override UserType[] AuthorizedUserTypes
	    {
            get { return null; }
	    }

	    protected override bool GetRequiresActivation()
        {
            return false;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			LoadControl();
		}

		private void LoadControl()
		{
            const string method = "LoadControl";

            bool doSendInvitation = (Request.Params[SendInvitationParameter] == "true");

            Guid inviteeId = ParseUtil.ParseUserInputGuid(Request.Params[InviteeIdParameter], "invitee ID");

            if (inviteeId == LoggedInMember.Id)
			{
				Controls.Add(GetInformationMessage(CannotAddSelfToFriends, true));
				return;
			}

            var invitee = _membersQuery.GetMember(inviteeId);
			if (invitee == null)
			{
				Controls.Add(GetInformationMessage(CouldNotFindMember, true));
                EventSource.Raise(Event.Error, method, string.Format("Tried to invite nonexistent member. inviteeId: {0}", inviteeId));
				return;
			}

		    var view = _memberViewsQuery.GetPersonalView(LoggedInMember.Id, invitee);
            if (view.ActualContactDegree == PersonalContactDegree.FirstDegree)
			{
                Controls.Add(GetInformationMessage(string.Format(AlreadyFriendsFormat, invitee.FullName), false));
				return;
			}

            var invitation = _memberFriendsQuery.GetFriendInvitation(LoggedInMember.Id, invitee.Id);

            if (invitation != null && !_memberFriendsCommand.CanSendInvitation(invitation))
			{
                Controls.Add(GetInformationMessage(GetSendingNotAllowedReason(invitee, invitation), false));
				return;
			}

            if (view.CanAccess(PersonalVisibility.SendInvites))
            {
                if (doSendInvitation)
                {
                    Controls.Add(Send(invitee.Id));
                }
                else
                {
                    Controls.Add(GetAddToFriends(invitee));
                }
            }
            else
            {
                Controls.Add(GetInformationMessage(NotAcceptingInvitations, false));
            }
		}

        private AddToFriends GetAddToFriends(Member invitee)
		{
			var atf = (AddToFriends) LoadControl("~/ui/controls/networkers/OverlayPopups/AddToFriends.ascx");

			atf.Invitee = invitee;

			return atf;
		}

		private InformationMessage GetInformationMessage(string message, bool isError)
		{
			var im = (InformationMessage)LoadControl("~/ui/controls/networkers/OverlayPopups/InformationMessage.ascx");

			im.MessageHtml = message;
			im.IsErrorMessage = isError;

			return im;
		}

        private string GetSendingNotAllowedReason(ICommunicationRecipient invitee, FriendInvitation invitation)
        {
            var name = HtmlUtil.TextToHtml(invitee.FullName);
            switch (invitation.Status)
            {
                case RequestStatus.Accepted:
                    return string.Format(AlreadyFriendsFormat, name);

                case RequestStatus.Declined:
                    return string.Format(AlreadyDeclinedFormat, name);

                case RequestStatus.Pending:
                    return string.Format(InviteAlreadyExistsFormat, name, _memberFriendsCommand.GetAllowedSendingTime(invitation));

                default:
                    throw new InvalidOperationException("SendingAllowed should not be false while Status is " + invitation.Status);
            }
        }

        private InformationMessage Send(Guid inviteeId)
		{
            const string method = "Send";

            var invitation = new FriendInvitation
            {
                InviterId = LoggedInMember.Id,
                InviteeId = inviteeId,
                Text = Request.Params[MessageParameter]
            };

		    try
			{
                _memberFriendsCommand.SendInvitation(invitation);
                return GetInformationMessage("Your invitation was sent successfully.", false);
			}
			catch (Exception ex)
			{
                EventSource.Raise(Event.Error, method, "Exception occurred while sending invitation", ex, new StandardErrorHandler());
				return GetInformationMessage("There was a problem sending your invitation.", true);
				//throw;
			}

		}
	}
}
