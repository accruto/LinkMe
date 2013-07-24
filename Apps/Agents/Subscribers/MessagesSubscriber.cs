using LinkMe.Apps.Agents.Users.Members.Handlers;
using LinkMe.Domain.Users.Employers.Contacts;
using LinkMe.Framework.Utility.PublishedEvents;

namespace LinkMe.Apps.Agents.Subscribers
{
    public class MessagesSubscriber
    {
        private readonly IMessagesHandler _messagesHandler;

        public MessagesSubscriber(IMessagesHandler messagesHandler)
        {
            _messagesHandler = messagesHandler;
        }

        // Messages

        [SubscribesTo(PublishedEvents.MessageCreated)]
        public void OnMessageCreated(object sender, MessageCreatedEventArgs args)
        {
            _messagesHandler.OnMessageSent(args.EmployerId, args.MemberId, args.RepresentativeId, args.Message);
        }
    }
}