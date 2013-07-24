using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public abstract class FilterMessageHandler
		:	BaseMessageHandler
	{
        private readonly IMessageHandler _nextMessageHandler;
        private readonly Filter _filter;

	    protected FilterMessageHandler(IMessageHandler nextMessageHandler, Filter filter)
	    {
	        _nextMessageHandler = nextMessageHandler;
	        _filter = filter;
	    }

	    protected override void HandleEventMessage(EventMessage eventMessage)
		{
			// Pass on to the appropriate message handler if there is one.

            if (_filter.Pattern.IsMatch(GetPattern(eventMessage)))
                _nextMessageHandler.HandleEventMessage(eventMessage);
		}

	    protected abstract string GetPattern(EventMessage message);
	}
}
