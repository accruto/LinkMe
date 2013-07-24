using System.Diagnostics;
using System.IO;
using System.Messaging;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class MsmqMessageHandler
		:	BaseMessageHandler
	{
        public MsmqMessageHandler(string queue)
        {
            _queue = queue;
        }

		protected override void HandleEventMessage(EventMessage eventMessage)
		{
			SetMessageSequence(eventMessage);

			// Serialize the message.

			var message = new System.Messaging.Message();
			var writer = new BinaryWriter(message.BodyStream);
			eventMessage.Write(writer);

			// Send it.

			using ( var queue = new MessageQueue("FormatName:" + _queue, false) )
			{
				queue.Send(message, Constants.MessageComponents.Msmq.MessageLabel);
			}
		}

		protected override void HandleEventMessages(EventMessages eventMessages)
		{
			SetMessageSequences(eventMessages);

			using ( var queue = new MessageQueue("FormatName:" + _queue, false) )
			{
				// If there are many messages break them up into batches.

				for (int index = 0; index < eventMessages.Count; index += Constants.MessageComponents.Msmq.MaxEventMessagesPerMqMessage)
				{
					EventMessages messageBatch = eventMessages.GetRange(index, Constants.MessageComponents.Msmq.MaxEventMessagesPerMqMessage);
					Debug.Assert(messageBatch != null, "messageBatch != null");

					// Serialize the message batch.

					var message = new System.Messaging.Message();
					var writer = new BinaryWriter(message.BodyStream);
					messageBatch.Write(writer);

					// Send it.

					queue.Send(message, Constants.MessageComponents.Msmq.MessagesLabel);
				}
			}
		}

		private readonly string _queue;
	}
}
