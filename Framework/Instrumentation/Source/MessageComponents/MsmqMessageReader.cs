using System;
using System.Diagnostics;
using System.IO;
using System.Messaging;
using System.Threading;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class MsmqMessageReader
		:	BaseMessageReader,
			IRemoveMessages
	{
		private readonly string _queue;
		private bool _raiseExistingRead = true; // Raise the event when all existing messages are read.
		private bool _removeWhenReading;
		private string _lastPeekedMessageId;

	    public MsmqMessageReader(string queue)
	    {
	        _queue = queue;
	    }

        #region IRemoveMessages Members

		public bool RemoveWhenReading
		{
			get { return _removeWhenReading; }
			set
			{
				// Stop and re-start reading for the change to take affect.

				if (_removeWhenReading != value)
				{
					bool reading = (State == ReaderState.Reading);
					if (reading)
					{
						StopReading();
					}

					_removeWhenReading = value; 

					if (reading)
					{
						StartReading();
					}
				}
			}
		}

		void IRemoveMessages.RemoveAll()
		{
			RemoveAll();
		}

		#endregion

		protected override void RemoveAllImpl()
		{
			// Purge all messages from the queue.

			using ( MessageQueue queue = GetQueue() )
			{
				queue.Purge();
			}
		}

		protected override void ReadAll()
		{
			using ( MessageQueue queue = GetQueue() )
			{
				using ( MessageEnumerator enumerator = queue.GetMessageEnumerator2() )
				{
					enumerator.Reset();

					// Gather all messages.

					var messages = new EventMessages();
					while ( enumerator.MoveNext() )
					{
						if ( RemoveWhenReading )
							GetEventMessages(messages, enumerator.RemoveCurrent());
						else
							GetEventMessages(messages, enumerator.Current);
					}

					// Pass all of them on.

					HandleEventMessages(messages);
				}
			}
		}

		protected override void Reset()
		{
			_lastPeekedMessageId = null;
			_raiseExistingRead = true;
		}

		protected override void Read()
		{
			if (RemoveWhenReading)
			{
				ReceiveRead();
			}
			else
			{
				PeekRead();
			}
		}

		protected override void ResumeReading()
		{
			_raiseExistingRead = true;
			base.ResumeReading();
		}

		private MessageQueue GetQueue()
		{
			return new MessageQueue("FormatName:" + _queue, false);
		}

		private void ReceiveRead()
		{
			using ( MessageQueue queue = GetQueue() )
			{
				// When starting try to read all messages on the queue.

				bool moreToRead = ReadAndRemoveMessages(queue);

				// Continue reading messages until Stop() is called.

				var readHandles = new[] { StopEvent,  PauseEvent };
				var pauseHandles = new[] { StopEvent, ContinueEvent };

				bool stopping = false;
				while ( !stopping )
				{
					int timeToWait = (moreToRead ? 0 : Constants.MessageComponents.Msmq.TimeToWaitForNewMessages);
					switch ( WaitHandle.WaitAny(readHandles, timeToWait, false) )
					{
						case 0:

							// Stopping, so exit the loop.

							stopping = true;
							break;

						case 1:

							// Paused, wait for it to start again.

							switch ( WaitHandle.WaitAny(pauseHandles) )
							{
								case 0:

									// Stopping, so exit the loop.

									stopping = true;
									break;

								case 1:

									// Continuing, read whatever messages may have been added.

									moreToRead = ReadAndRemoveMessages(queue);
									break;
							}

							break;

						case WaitHandle.WaitTimeout:

							// Read the messages.

							moreToRead = ReadAndRemoveMessages(queue);
							break;
					}
				}
			}
		}

		private void PeekRead()
		{
			using ( MessageQueue queue = GetQueue() )
			{
				using ( MessageEnumerator enumerator = queue.GetMessageEnumerator2() )
				{
					// Initialise.

					enumerator.Reset();
					bool firstMessage = true;
					bool current = false;

					// When starting try to read all messages on the queue.

					bool moreToRead = ReadMessages(queue, enumerator, ref firstMessage, ref current);

					// Continue reading messages until Stop() is called.

					var readHandles = new[] { StopEvent,  PauseEvent };
					var pauseHandles = new[] { StopEvent, ContinueEvent };

					bool stopping = false;
					while ( !stopping )
					{
						int timeToWait = (moreToRead ? 0 : Constants.MessageComponents.Msmq.TimeToWaitForNewMessages);
						switch ( WaitHandle.WaitAny(readHandles, timeToWait, false) )
						{
							case 0:

								// Stopping, so exit the loop.

								stopping = true;
								break;

							case 1:

								// Paused, wait for it to start again.
							
								switch ( WaitHandle.WaitAny(pauseHandles) )
								{
									case 0:

										// Stopping, so exit the loop.

										stopping = true;
										break;

									case 1:

										// Continuing, read whatever messages may have been added.

										moreToRead = ReadMessages(queue, enumerator, ref firstMessage, ref current);
										break;
								}

								break;

							case WaitHandle.WaitTimeout:

								// Read the messages.

								moreToRead = ReadMessages(queue, enumerator, ref firstMessage, ref current);
								break;
						}
					}
				}
			}
		}

		private bool ReadAndRemoveMessages(MessageQueue queue)
		{
			// If there are no more messages and it's the first attempt to read then raise ExistingMessagesRead,
			// otherwise just return. To avoid throwing and catching exceptions (MQ error "IOTimeout") all the
			// time while there are no messages call the native API directly to find out whether there is at
			// least one message in the queue.

			bool messageExists = PeekFirstMessage(queue);
			if (!messageExists)
			{
				if (_raiseExistingRead)
				{
					_raiseExistingRead = false; // Don't raise again until we've read some messages.
					OnExistingMessagesRead(EventArgs.Empty);
				}

				return false;
			}

			_raiseExistingRead = true; // We have more messages.

			// Read one batch of messages (or as many as are available, if less than the batch size) and
			// return. Passing messages to the message handler in batches may save the handler some overhead
			// (eg. updating the UI in the Event Viewer).

			var messages = new EventMessages();
			int count = 0;
			var receiveTimeout = new TimeSpan(0, 0, 0, 0, 100); // 100 ms

			while (count < Constants.MessageComponents.Msmq.MessageReaderBatchSize)
			{
				System.Messaging.Message mqMessage;

				try
				{
					mqMessage = queue.Receive(receiveTimeout);
					Debug.Assert(mqMessage != null, "mqMessage != null");
				}
				catch (MessageQueueException ex)
				{
				    if (ex.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout)
					{
						// No more messages in the queue.

						messageExists = false;
						break;
					}
				    
                    throw;
				}

			    // If RemoveWhenReading was previously set to false and some messages were read, but now the user
				// has set it to true then we don't want to send those same messages to the message handler again,
				// so just ignore them.

				if (_lastPeekedMessageId == null)
				{
					count += GetEventMessages(messages, mqMessage);
				}
				else if (mqMessage.Id == _lastPeekedMessageId)
				{
					_lastPeekedMessageId = null; // Handle all messages after this one.
				}
			}

			// Handle any messages in this batch.

			Debug.Assert(messages.Count > 0, "messages.Count > 0");
			HandleEventMessages(messages);

			// Raise the event if all messages were read.

			if (!messageExists && _raiseExistingRead)
			{
				_raiseExistingRead = false; // Don't raise again until we've read some messages.
				OnExistingMessagesRead(EventArgs.Empty);
			}

			// If the last Receive() returned a message then tell the caller to continue reading.

			return messageExists;
		}

		private static bool PeekFirstMessage(MessageQueue queue)
		{
			int hr = UnsafeNativeMethods.MQReceiveMessage(queue.ReadHandle, 0, NativeMethods.MQ_ACTION_PEEK_CURRENT,
				IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

			switch (hr)
			{
				case NativeMethods.MQ_OK:
					return true;

				case (int)MessageQueueErrorCode.IOTimeout:
					return false; // No more messages.

				default:
					Debug.WriteLine("MQReceiveMessage returned " + ((MessageQueueErrorCode)hr));
					return true; // Try to call MessageQueue.Receive() and let it throw a proper exception.
			}
		}

		private bool ReadMessages(MessageQueue queue, MessageEnumerator enumerator, ref bool firstMessage,
			ref bool current)
		{
			// If there are no more messages and it's the first attempt to read then raise ExistingMessagesRead,
			// otherwise just return.

			bool messageExists = MoveToNextMessage(queue, enumerator, firstMessage, ref current);
			if (!messageExists)
			{
				if (_raiseExistingRead)
				{
					_raiseExistingRead = false; // Don't raise again until we've read some messages.
					OnExistingMessagesRead(EventArgs.Empty);
				}

				return false;
			}

			firstMessage = false;
			_raiseExistingRead = true; // We have more messages.

			// Read one batch of messages (or as many as are available, if less than the batch size) and
			// return. Passing messages to the message handler in batches may save the handler some overhead
			// (eg. updating the UI in the Event Viewer).

			var messages = new EventMessages();
			int count = 0;

			do
			{
				System.Messaging.Message mqMessage;

				try
				{
					mqMessage = enumerator.Current;
				}
				catch (MessageQueueException ex)
				{
				    if (ex.MessageQueueErrorCode == MessageQueueErrorCode.MessageAlreadyReceived)
					{
						throw new ApplicationException("The MsmqMessageReader attempted to"
							+ " read a message from the queue that has already been read and removed."
							+ " This usually means that another application is receiving messages"
							+ " from the same queue (" + _queue + ").", ex);
					}
				    
                    throw;
				}

			    count += GetEventMessages(messages, mqMessage);
				_lastPeekedMessageId = mqMessage.Id;

				if (count >= Constants.MessageComponents.Msmq.MessageReaderBatchSize)
					break;

				messageExists = MoveToNextMessage(queue, enumerator, firstMessage, ref current);
			}
			while (messageExists);

			// Handle any messages in this batch.

			Debug.Assert(messages.Count > 0, "messages.Count > 0");
			HandleEventMessages(messages);

			// Raise the event if all messages were read.

			if (!messageExists && _raiseExistingRead)
			{
				_raiseExistingRead = false; // Don't raise again until we've read some messages.
				OnExistingMessagesRead(EventArgs.Empty);
			}

			return messageExists;
		}

		private static bool MoveToNextMessage(MessageQueue queue, MessageEnumerator enumerator, bool firstMessage, ref bool current)
		{
			// This function effectively replaces MessageEnumerator.MoveNext(). The problem with that function is
			// that once it reaches the end of the queue it closes the cursor, so the next call to MoveNext()
			// starts from the start again whereas we want to keep trying to retrieve the next message when it
			// arrives.

			// When moving to the very first message call MoveNext() on the enumerator, so that it updates its
			// own internal state. Afterwards manually check for the next message (which will advance the cursor,
			// so there's no need to call MoveNext()). When we reach the end of the queue (IOTimeout is returned) the
			// cursor is positioned PAST the last message, so next time we need to read the CURRENT message, not
			// the next one (current is set to true). Once a new message arrives in the queue read the NEXT
			// message (current is set to false).

			Debug.Assert(queue != null && enumerator != null, "queue != null && enumerator != null");

			if (firstMessage)
			{
                try
                {
                    return enumerator.MoveNext();
                }
                catch (MessageQueueException ex)
                {
                    if ((uint)ex.MessageQueueErrorCode == 0x80070005)
                    {
                        throw new ApplicationException(string.Format("Access is denied to queue '{0}'."
                            + " Ensure that you have 'Receive Message' permissions for the queue.",
                            queue.FormatName), ex);
                    }
                    
                    throw new ApplicationException(string.Format("Failed to read from queue '{0}'.",
                                                                 queue.FormatName), ex);
                }
			}
		    
            int action = (current ? NativeMethods.MQ_ACTION_PEEK_CURRENT : NativeMethods.MQ_ACTION_PEEK_NEXT);

		    int hr = UnsafeNativeMethods.MQReceiveMessage(queue.ReadHandle, 0, action, IntPtr.Zero,
		                                                  IntPtr.Zero, IntPtr.Zero, enumerator.CursorHandle, IntPtr.Zero);

		    switch (hr)
		    {
		        case NativeMethods.MQ_OK:
		            current = false;
		            return true;

		        case (int)MessageQueueErrorCode.IOTimeout:
		            current = true;
		            return false; // No more messages.

		        default:
		            Debug.WriteLine("MQReceiveMessage returned " + ((MessageQueueErrorCode)hr));
		            return false;
		    }
		}

		private static int GetEventMessages(EventMessages eventMessages, System.Messaging.Message message)
		{
			Debug.Assert(message != null, "message != null");

			var reader = new BinaryReader(message.BodyStream);

			switch ( message.Label )
			{
				case Constants.MessageComponents.Msmq.MessageLabel:

					// A single message.

					var newEventMessage = new EventMessage();
					newEventMessage.Read(reader);
					eventMessages.Add(newEventMessage);
					return 1;

				case Constants.MessageComponents.Msmq.MessagesLabel:

					// Multiple messages.

					var newEventMessages = new EventMessages();
					newEventMessages.Read(reader);
					foreach ( EventMessage eventMessage in newEventMessages )
						eventMessages.Add(eventMessage);
					return newEventMessages.Count;

				default:

					Debug.WriteLine("Unexpected MSMQ message label '" + message.Label
						+ "' - not recognised as a LinkMe Instrumentation message.");
					return 0;
			}
		}
	}
}
