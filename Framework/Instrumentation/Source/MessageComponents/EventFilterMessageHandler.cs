using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class EventFilterMessageHandler
		:	FilterMessageHandler
	{
        public EventFilterMessageHandler(IMessageHandler nextMessageHandler, bool isExact, string pattern)
            : base(nextMessageHandler, new EventFilter(isExact ? PatternType.Exact : PatternType.Regex, pattern))
	    {
	    }

	    protected override string GetPattern(EventMessage message)
		{
			return message.Event;
		}
	}
}
