using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	/// <summary>
	/// A message handler that does nothing.
	/// </summary>
	public class NullMessageHandler
		:	BaseMessageHandler
	{
	    protected override void HandleEventMessage(EventMessage eventMessage)
		{
		}

		protected override void HandleEventMessages(EventMessages eventMessages)
		{
		}
	}
}
