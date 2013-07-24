using System.Diagnostics;
using System.IO;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Host.Instrumentation
{
	public class MessageHandlerSink
        : SyncChannelSink
	{
		private readonly IMessageHandler _messageHandler;

        public MessageHandlerSink(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

		protected override object PreProcess(object message, out object data)
		{
			const string method = "PreProcess";

			Debug.Assert(_messageHandler != null, "_messageHandler != null");

			if (message == null)
				throw new NullParameterException(GetType(), method, "message");

			// The message should be a Stream containing an EventMessage - deserialize it.

			var stream = message as Stream;
			if (stream == null)
				throw new InvalidParameterTypeException(GetType(), method, "message", typeof(Stream), message);

			var eventMessage = new EventMessage();
			eventMessage.Read(new BinaryReader(stream));

			// Send it to the message handler.

			_messageHandler.HandleEventMessage(eventMessage);

			data = null;
			return message;
		}

		protected override object PostProcess(object message, object data)
		{
			return message;
		}
	}
}
