using System;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Web.UI;

namespace LinkMe.Web.Members.Friends
{
    public partial class InviteFriends
        : LinkMePage
    {
        // Make sure to HTML-encode everything, since scripts are allowed!
        protected override bool DoRequestValidation
        {
            get { return false; }
        }

        #region Page Actions

        private void btnSendInvitations_Click(object sender, EventArgs e)
        {
            ShowInviteFriends();
            ucInviteFriends.SendInvitations();
        }

        private static void btnCancelSendInvitations_Click(object sender, EventArgs e)
        {
            NavigationManager.Redirect(HttpContext.Current.GetHomeUrl());
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            WireUpHandlers();
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return new[] { UserType.Member }; }
        }

        protected override bool GetRequiresActivation()
        {
            return true;
        }

        protected override UserType GetActiveUserType()
        {
            return UserType.Member;
        }

        private void WireUpHandlers()
        {
            btnSendInvitations.Click += btnSendInvitations_Click;
            btnCancelSendInvitations.Click += btnCancelSendInvitations_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                ShowInviteFriends();

                // Populate invites from the find friends from address book
                if (Session["ffbeInviteEmails"] != null)
                {
                    ucInviteFriends.EmailAddresses = Session["ffbeInviteEmails"].ToString();
                    Session["ffbeInviteEmails"] = null;
                }
            }
        }

        private void ShowInviteFriends()
        {
            phInviteFriends.Visible = true;
        }
    }
}