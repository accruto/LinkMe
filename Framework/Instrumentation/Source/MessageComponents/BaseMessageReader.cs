using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Instrumentation.Message;
using SourceFilter=LinkMe.Framework.Instrumentation.Message.SourceFilter;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	/// <summary>
	/// Base implementation of IMessageReader.
	/// </summary>
	public abstract class BaseMessageReader
		:	IMessageReader
	{
		#region Nested types

		protected enum ReaderState
		{
			Idle,
			Reading,
			Paused
		}

		#endregion

		private IMessageHandler _messageHandler;
        private IMessageNotifyHandler _messageNotifyHandler;
        private ReaderState _state = ReaderState.Idle;
		private Thread _readerThread;
		private AutoResetEvent _readerStopEvent;
		private ManualResetEvent _readerPauseEvent;
		private ManualResetEvent _readerContinueEvent;
        private Filters _filters;

		#region IMessageReader Members

		public event System.EventHandler ExistingMessagesRead;
		public event ThreadExceptionEventHandler ReaderThreadException;

		void IMessageReader.SetMessageHandler(IMessageHandler messageHandler)
		{
			// Check.

			const string method = "SetMessageHandler";
			if ( messageHandler == null )
				throw new NullParameterException(GetType(), method, "messageHandler");

			_messageHandler = messageHandler;
		    _messageNotifyHandler = _messageHandler as IMessageNotifyHandler;
		}

		void IMessageReader.ReadAll()
		{
			ReadAll();
		}

        void IMessageReader.Start(Filters filters)
		{
			Reset();
			StartReading(filters);
			_state = ReaderState.Reading;
		}

		void IMessageReader.Stop()
		{
			StopReading();
			_state = ReaderState.Idle;
		}

		void IMessageReader.Pause()
		{
			PauseReading();
			_state = ReaderState.Paused;
		}

		void IMessageReader.Resume()
		{
			ResumeReading();
			_state = ReaderState.Reading;
		}

		#endregion

		protected WaitHandle StopEvent
		{
			get { return _readerStopEvent; }
		}

		protected WaitHandle PauseEvent
		{
			get { return _readerPauseEvent; }
		}

		protected WaitHandle ContinueEvent
		{
			get { return _readerContinueEvent; }
		}

		protected ReaderState State
		{
			get { return _state; }
		}

        protected Filters Filters
        {
            get { return _filters; }
        }

		protected virtual void ReadAll()
		{
		}

		protected void RemoveAll()
		{
			ReaderState state = _state;

			switch (state)
			{
				case ReaderState.Reading:
				case ReaderState.Paused:

					// Stop the reader before clearing all the messages.

					StopReading();
					break;
			}

			RemoveAllImpl();

			// If the reader was reading before then start it reading again.

			if (state == ReaderState.Reading)
			{
				StartReading();
			}
		}

		protected virtual void RemoveAllImpl()
		{
		}

		/// <summary>
		/// Called when the user starts the message reader. The derived class should reset its state in this method
		/// (not in StartReading, which may be called internally).
		/// </summary>
		protected virtual void Reset()
		{
		}

        protected virtual void StartReading()
        {
            StartReading(null);
        }

        private void StartReading(Filters filters)
        {
			if (_readerThread != null)
			{
				throw new System.InvalidOperationException("The '" + GetType().Name + "' message reader cannot"
					+ " start reading, because it has already started.");
			}

			// Read messages from another thread.

            _filters = filters;
            _readerStopEvent = new AutoResetEvent(false);
			_readerPauseEvent = new ManualResetEvent(false);
			_readerContinueEvent = new ManualResetEvent(true);
			_readerThread = new Thread(ReadThread) {Priority = ThreadPriority.BelowNormal};
            _readerThread.Start();
		}

		protected virtual void StopReading()
		{
			if ( _readerThread != null )
			{
				// Signal the thread to stop and then wait.

				_readerStopEvent.Set();

				if ( !_readerThread.Join(5000) )
				{
					// Try to abort the thread.

					_readerThread.Abort();
				}

				// Clean up.

				_readerStopEvent.Close();
				_readerStopEvent = null;
				_readerPauseEvent.Close();
				_readerPauseEvent = null;
				_readerContinueEvent.Close();
				_readerContinueEvent = null;
				_readerThread = null;
			}
		}

		protected virtual void PauseReading()
		{
			// Set the event, it is up to the reader to react.

			_readerContinueEvent.Reset();
			_readerPauseEvent.Set();
			_state = ReaderState.Paused;
		}

		protected virtual void ResumeReading()
		{
			// Reset the state, it is up to the reader to react.

			_readerPauseEvent.Reset();
			_readerContinueEvent.Set();
			_state = ReaderState.Reading;
		}

		protected virtual void OnExistingMessagesRead(System.EventArgs e)
		{
			System.EventHandler handler = ExistingMessagesRead;
			if (handler != null)
			{
				handler(this, e);
			}
		}

		protected virtual void OnReaderThreadException(ThreadExceptionEventArgs e)
		{
			ThreadExceptionEventHandler handler = ReaderThreadException;
			if (handler != null)
			{
				handler(this, e);
			}
		}

	    protected bool SupportsNotify
	    {
            get { return _messageNotifyHandler != null; }
	    }

        protected void HandleEventMessage(EventMessageIdentifier identifier)
        {
            _messageNotifyHandler.HandleEventMessage(identifier);
        }

        protected void HandleEventMessages(EventMessageIdentifiers identifiers)
        {
            _messageNotifyHandler.HandleEventMessages(identifiers);
        }

        protected void HandleEventMessage(EventMessage message)
		{
            // Filter.

            if (Match(message))
                _messageHandler.HandleEventMessage(message);
		}

	    protected void HandleEventMessages(EventMessages messages)
		{
            var filteredMessages = Matches(messages);
            if (filteredMessages.Count > 0)
                _messageHandler.HandleEventMessages(filteredMessages);
		}

        private bool Match(EventMessage message)
        {
            if (_filters == null)
                return true;

            foreach (var filter in _filters)
            {
                if (filter is SourceFilter)
                {
                    if (!Match(message.Source, filter))
                        return false;
                }
                else if (filter is EventFilter)
                {
                    if (!Match(message.Event, filter))
                        return false;
                }
                else if (filter is TypeFilter)
                {
                    if (!Match(message.Type, filter))
                        return false;
                }
                else if (filter is MethodFilter)
                {
                    if (!Match(message.Method, filter))
                        return false;
                }
                else if (filter is MessageFilter)
                {
                    if (!Match(message.Message, filter))
                        return false;
                }
                else if (filter is DetailFilter)
                {
                    if (!Match(message.Details, filter))
                        return false;
                }
                else if (filter is ParameterFilter)
                {
                    if (!Match())
                        return false;
                }
            }

            return true;
        }

        private static bool Match(string value, Filter filter)
        {
            return filter.Pattern.IsMatch(value);
        }

        private static bool Match(Utility.Event.EventDetails details, Filter filter)
        {
            if (details == null || details.Count == 0)
                return true;

            foreach (var detail in details)
            {
                foreach (var detailValue in detail.Values)
                {
                    if (detailValue.Name == filter.Name && detailValue.Value != null && Match(detailValue.Value.ToString(), filter))
                        return true;
                }
            }

            return false;
        }

        private static bool Match()
        {
            return true;
        }

        private EventMessages Matches(IEnumerable<EventMessage> messages)
	    {
            var filteredMessages = new EventMessages();
            foreach (var message in messages)
            {
                if (Match(message))
                    filteredMessages.Add(message);
            }
                
            return filteredMessages;
        }

	    protected abstract void Read();

		/// <summary>
		/// Sets the sequence number on a message to the specified value. This method should be called by
		/// derived readers that store the sequence number separately from the message itself (ie. do not
		/// serialize the EventMessage object to binary or XML.
		/// </summary>
		protected void SetMessageSequence(EventMessage message, int sequence)
		{
			Debug.Assert(message.Sequence == 0, "Setting the Sequence value on a message to "
				+ sequence + ", but it's already set to " + message.Sequence);
			message.SetSequence(sequence);
		}

		private void ReadThread()
		{
			try
			{
				Read();
			}
			catch ( ThreadInterruptedException )
			{
				// Interrupted - silently exit.
			}
			catch ( ThreadAbortException )
			{
				// Aborted - silently exit.
			}
			catch ( System.Exception ex )
			{
				// Raise an event to notify the caller of this exception. Without this the exception would be
				// unhandled.

				OnReaderThreadException(new ThreadExceptionEventArgs(ex));
			}
		}
	}
}
