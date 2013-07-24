using System.Threading;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public abstract class BaseMessageHandler
		:	IMessageHandler
	{
		private int _currentSequence;

		#region IMessageHandler Members

		IMessageHandler IMessageHandler.GetMessageHandler(string source)
		{
			return GetMessageHandler(source);
		}

		void IMessageHandler.HandleEventMessage(EventMessage eventMessage)
		{
			HandleEventMessage(eventMessage);
		}

		void IMessageHandler.HandleEventMessages(EventMessages eventMessages)
		{
			HandleEventMessages(eventMessages);
		}

		#endregion

		protected virtual IMessageHandler GetMessageHandler(string source)
		{
			return this;
		}

		protected abstract void HandleEventMessage(EventMessage message);

		protected virtual void HandleEventMessages(EventMessages messages)
		{
			foreach ( EventMessage message in messages )
				HandleEventMessage(message);
		}

		/// <summary>
		/// This method should be called by all derived message handlers that write the message to a repository
		/// to set the message sequence (if not already set). It should not be called by message handlers that
		/// only call other message handlers.
		/// </summary>
		protected void SetMessageSequence(EventMessage message)
		{
			if (message.Sequence == 0)
			{
                int sequence = Interlocked.Increment(ref _currentSequence);
				message.SetSequence(sequence);
			}
		}

		protected void SetMessageSequences(EventMessages messages)
		{
			foreach (EventMessage message in messages)
			{
				SetMessageSequence(message);
			}
		}
	}
}
