using LinkMe.Domain.Requests;

namespace LinkMe.Apps.Agents.Users.Members.Handlers
{
    public interface IFriendsHandler
    {
        void OnInvitationSent(Invitation invitation);
        void OnInvitationAccepted(Invitation invitation);
    }
}
