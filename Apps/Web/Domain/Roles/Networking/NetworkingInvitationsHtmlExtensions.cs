using System.Web;
using LinkMe.Apps.Asp.Navigation;
using LinkMe.Domain;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Requests;
using LinkMe.Domain.Roles.Representatives;
using LinkMe.Domain.Users.Members.Friends;
using LinkMe.Web.Members.Friends;

namespace LinkMe.Web.Domain.Roles.Networking
{
    public static class NetworkingInvitationsHtmlExtensions
    {
        private const string FriendInviteAcceptedFormat = "You are now linked to <a href=\"{0}\">{1}</a>{2} network.";
        private const string RepresentativeInviteAcceptedFormat = "You are now <a href=\"{0}\">{1}</a>{2} representative.";

        public static string GetInvitationAcceptedHtml(this FriendInvitation invitation, IRegisteredUser inviter)
        {
            return GetInvitationAcceptedHtml(invitation, inviter, FriendInviteAcceptedFormat);
        }

        public static string GetInvitationAcceptedHtml(this RepresentativeInvitation invitation, IRegisteredUser inviter)
        {
            return GetInvitationAcceptedHtml(invitation, inviter, RepresentativeInviteAcceptedFormat);
        }

        private static string GetInvitationAcceptedHtml(this Invitation invitation, IRegisteredUser inviter, string format)
        {
            var profileUrl = NavigationManager.GetUrlForPage<ViewFriend>(ViewFriend.FriendIdParameter, invitation.InviterId.ToString()).ToString();
            var inviterNameHtml = HttpUtility.HtmlEncode(inviter.FullName);
            return string.Format(format, profileUrl, inviterNameHtml, inviterNameHtml.GetNamePossessiveSuffix());
        }
    }
}