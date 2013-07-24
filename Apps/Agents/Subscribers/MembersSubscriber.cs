using LinkMe.Apps.Agents.Users.Members;
using LinkMe.Apps.Agents.Users.Members.Handlers;
using LinkMe.Domain.Accounts;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.PublishedEvents;
using PublishedEvents=LinkMe.Apps.Agents.Users.Members.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class MembersSubscriber
    {
        private readonly IMembersHandler _membersHandler;

        public MembersSubscriber(IMembersHandler membersHandler)
        {
            _membersHandler = membersHandler;
        }

        [SubscribesTo(PublishedEvents.MemberCreated)]
        public void OnMemberCreated(object sender, MemberCreatedEventArgs args)
        {
            _membersHandler.OnMemberCreated(args.MemberId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserDisabled)]
        public void OnUserDisabled(object sender, UserAccountEventArgs args)
        {
            if (args.UserType == UserType.Member)
                _membersHandler.OnMemberDisabled(args.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserActivated)]
        public void OnUserActivated(object sender, UserAccountEventArgs args)
        {
            if (args.UserType == UserType.Member)
                _membersHandler.OnMemberActivated(args.UserId);
        }

        [SubscribesTo(LinkMe.Domain.Accounts.PublishedEvents.UserDeactivated)]
        public void OnUserDeactivated(object sender, UserDeactivatedEventArgs args)
        {
            if (args.UserType == UserType.Member)
                _membersHandler.OnMemberDeactivated(args.UserId);
        }
    }
}