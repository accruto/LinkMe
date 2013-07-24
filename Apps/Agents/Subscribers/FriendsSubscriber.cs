using LinkMe.Apps.Agents.Users.Members.Handlers;
using LinkMe.Domain.Requests;
using LinkMe.Framework.Utility.PublishedEvents;
using PublishedEvents=LinkMe.Domain.Users.Members.Friends.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class FriendsSubscriber
    {
        private readonly IFriendsHandler _friendsHandler;

        public FriendsSubscriber(IFriendsHandler friendsHandler)
        {
            _friendsHandler = friendsHandler;
        }

        [SubscribesTo(PublishedEvents.InvitationSent)]
        public void OnInvitationSent(object sender, EventArgs<Invitation> args)
        {
            _friendsHandler.OnInvitationSent(args.Value);
        }

        [SubscribesTo(PublishedEvents.InvitationAccepted)]
        public void OnFriendInvitationAccepted(object sender, EventArgs<Invitation> args)
        {
            _friendsHandler.OnInvitationAccepted(args.Value);
        }
    }
}