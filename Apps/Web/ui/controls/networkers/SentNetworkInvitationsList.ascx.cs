using System;
using System.Linq;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class SentNetworkInvitationsList : LinkMeUserControl
    {
        private readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private Member _member;
        private PersonalViews _views;
        private RepresentativeInvitation _representativeInvitation;

        public Member Member
        {
            get { return _member; }
            set { _member = value; }
        }

        protected RepresentativeInvitation RepresentativeInvitation
        {
            get { return _representativeInvitation; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var invitations = _memberFriendsQuery.GetFriendInvitations(_member.Id);
            _representativeInvitation = _memberFriendsQuery.GetRepresentativeInvitationByInviter(Member.Id);

            var contactIds = from i in invitations where i.InviteeId != null select i.InviteeId.Value;
            if (_representativeInvitation != null && _representativeInvitation.InviteeId != null)
                contactIds = contactIds.Concat(new[] {_representativeInvitation.InviteeId.Value});

            _views = _memberViewsQuery.GetPersonalViews(_member.Id, contactIds);

            rptSentInvitations.DataSource = invitations;
            rptSentInvitations.DataBind();

            if (invitations.Count == 0)
            {
                rptSentInvitations.Visible = false;
                phNoSentInvitations.Visible = true;
                phSentInvitations.Visible = false;
            }

            phSentRepresentativeInvitation.Visible = _representativeInvitation != null;
        }

        protected string GetInviteeDisplayName(Invitation invitation)
        {
            if (invitation == null)
                throw new ArgumentNullException("invitation");

            if (invitation.InviteeId == null)
                return TextUtil.TruncateForDisplay(invitation.InviteeEmailAddress, 40);

            return ((ICommunicationRecipient)_views[invitation.InviteeId]).FullName;
        }

        protected static string GetInvitationSentDate(Invitation invitation)
        {
            if (invitation == null)
                throw new ArgumentNullException("invitation");

            return invitation.LastSentTime.Value.ToString("d MMMM yyyy");
        }
    }
}