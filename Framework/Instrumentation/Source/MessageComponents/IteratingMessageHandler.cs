using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class IteratingMessageHandler
		:	BaseMessageHandler
	{
        private readonly IMessageHandler[] _nextMessageHandlers;

	    public IteratingMessageHandler(IMessageHandler[] nextMessageHandlers)
	    {
	        _nextMessageHandlers = nextMessageHandlers;
	    }

	    protected override void HandleEventMessage(EventMessage eventMessage)
		{
			// Iterate over the next message handlers.

			foreach ( var messageHandler in _nextMessageHandlers )
				messageHandler.HandleEventMessage(eventMessage);
		}

		protected override void HandleEventMessages(EventMessages eventMessages)
		{
			// Iterate over the next message handlers.

			foreach ( var messageHandler in _nextMessageHandlers )
				messageHandler.HandleEventMessages(eventMessages);
		}
	}
}
