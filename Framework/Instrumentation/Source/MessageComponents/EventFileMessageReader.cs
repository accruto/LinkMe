using System.IO;
using System.Threading;
using System.Xml;
using LinkMe.Framework.Instrumentation.Constants;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Xml;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class EventFileMessageReader
		:	BaseMessageReader
	{
	    protected override void Read()
		{
			using (var reader = new StreamReader(_fileName))
			{
				// Similar to EventMessages.ReadOuterXml(), but send the messages to the handler as we
				// read them.

				var adaptor = new XmlReadAdaptor(new XmlTextReader(reader), Xml.Namespace);

				if ( adaptor.IsReadingElement(Xml.EventMessagesElement) )
				{
					var messages = new EventMessages();

					while ( adaptor.ReadElement(Xml.EventMessageElement) )
					{
						// Check the reader wait handles - should we stop or pause?

						if (StopEvent.WaitOne(0, false))
							return;

						if (PauseEvent.WaitOne(0, false))
						{
							// Paused - wait for stop or continue.

							var pauseHandles = new[] { StopEvent, ContinueEvent };
							switch (WaitHandle.WaitAny(pauseHandles))
							{
								case 0:
									return; // Stop.

								case 1:
									break; // Continue.
							}
						}

						var message = new EventMessage();
						message.ReadXml(adaptor.XmlReader);
						adaptor.ReadEndElement();

						// Send to the message handler, if we have enough.

						messages.Add(message);

						if (messages.Count == Constants.MessageComponents.EventFile.MessageReaderBatchSize)
						{
							HandleEventMessages(messages);
							messages = new EventMessages();
						}
					}

					// Send any remaining messages to the message handler.

					if (messages.Count > 0)
					{
						HandleEventMessages(messages);
					}
				}
			}

			OnExistingMessagesRead(System.EventArgs.Empty);
		}

		protected override void ReadAll()
		{
			var messages = new EventMessages();

			using (var reader = new StreamReader(_fileName))
			{
				// Read all messages from the file.

				XmlSerializer.Deserialize(messages, new XmlTextReader(reader));
			}

			// Pass them on.

			HandleEventMessages(messages);
		}

		private readonly string _fileName = string.Empty;
	}
}
