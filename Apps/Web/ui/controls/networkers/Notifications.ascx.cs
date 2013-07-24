using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Domain.Users.Members.Friends.Queries;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Domain.Users.Members.Views.Queries;
using LinkMe.Framework.Communications;
using LinkMe.Framework.Utility;
using LinkMe.Web.Members.Friends;
using LinkMe.Framework.Utility.Unity;

namespace LinkMe.Web.UI.Controls.Networkers
{
    public partial class Notifications
        :   LinkMeUserControl,
            ISectionControl
    {
        private const string FriendInvitationRequestFormat = "{0} has asked to be your friend";
        private const string RepresentativeInvitationRequestFormat = "{0} has asked you to be their representative";
        private const string InviterFormat = "<a href=\"{0}\">{1}</a>";

        private static readonly IMemberFriendsQuery _memberFriendsQuery = Container.Current.Resolve<IMemberFriendsQuery>();
        private static readonly IMemberViewsQuery _memberViewsQuery = Container.Current.Resolve<IMemberViewsQuery>();
        private IList _notifications;
        private PersonalViews _views;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Determine the list of _notifications for the member.

            LoadNotifications();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // If we have no _notifications, there's nothing to check
            // and nothing to DataBind.

            if (_notifications.Count > 0)
                Page.LoadComplete += CheckNotifications;
        }

        private void CheckNotifications(object sender, EventArgs e)
        {
            // Invitations may have been accepted or declined since we loaded them.
            // Unfortunately they must be loaded by OnLoad because SideBarContainer
            // calls ISectionConrol.ShowSection, which relies on _notifications being
            // loaded. Otherwise we'd load them later.

            if (IsPostBack)
                LoadNotifications();

            if (_notifications.Count == 0)
            {
                phNoNotifications.Visible = true;
            }
            else
            {
                rptNotifications.DataSource = _notifications;
                rptNotifications.DataBind();
            }
        }

        #region ISectionControl

        bool ISectionControl.ShowSection
        {
            get { return _notifications.Count != 0; }
        }

        string ISectionControl.SectionTitle
        {
            get
            {
                // Returning null indicates to use the configured title.

                return null;
            }
        }

        #endregion

        protected void rptNotifications_ItemCreated(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem != null)
            {
                // Add the text for each notification.

                var literal = (Literal) e.Item.FindControl("ltlNotificationsText");
                if (literal != null)
                {
                    var friendInvitation = e.Item.DataItem as FriendInvitation;
                    if (friendInvitation != null)
                    {
                        literal.Text = GetFriendText(friendInvitation);
                    }
                    else
                    {
                        var representativeInvitation = e.Item.DataItem as RepresentativeInvitation;
                        if (representativeInvitation != null)
                            literal.Text = GetRepresentativeText(representativeInvitation);
                    }
                }
            }
        }

        private void LoadNotifications()
        {
            var member = LoggedInMember;
            if (member != null)
            {
                // Get set of pending invitations.

                _notifications = new List<object>();

                var friendInvitations = _memberFriendsQuery.GetFriendInvitations(member.Id, member.GetBestEmailAddress().Address);
                foreach (var invitation in friendInvitations)
                    _notifications.Add(invitation);

                var representativeInvitations = _memberFriendsQuery.GetRepresentativeInvitations(member.Id, member.GetBestEmailAddress().Address);
                foreach (var invitation in representativeInvitations)
                    _notifications.Add(invitation);

                var contactIds = (from i in friendInvitations select i.InviterId)
                    .Concat(from i in representativeInvitations select i.InviterId);
                _views = _memberViewsQuery.GetPersonalViews(member.Id, contactIds);
            }
            else
            {
                _notifications = new List<object>();
                _views = new PersonalViews();
            }
        }

        private string GetFriendText(Invitation invitation)
        {
            return string.Format(FriendInvitationRequestFormat, GetInviterText(invitation.InviterId));
        }

        private string GetRepresentativeText(Invitation invitation)
        {
            return string.Format(RepresentativeInvitationRequestFormat, GetInviterText(invitation.InviterId));
        }

        private string GetInviterText(Guid inviterId)
        {
            return string.Format(InviterFormat, GetUrlForPage<Invitations>(), HtmlUtil.TextToHtml(((ICommunicationRecipient)_views[inviterId]).FullName));
        }
    }
}