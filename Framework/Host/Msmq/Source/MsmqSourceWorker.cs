using System;
using System.Threading;
using System.Messaging;
using LinkMe.Framework.Instrumentation;

namespace LinkMe.Framework.Host.Msmq
{
	internal class MsmqSourceWorker
	{
		#region Private Fields

		private readonly IChannelSink _sink;
		private readonly MessageQueue _queue;
		private readonly TimeSpan _receiveTimeout;
		private readonly TimeSpan _retryTimeout;
		private readonly WaitHandle _stopSignal;

		private readonly Thread _thread;
		private bool _stopping;

		// We don't want to log error messages every time we retry (which may be several times per second!)
		// Just log the first message after Start() is called.
		private bool _logTraceBeforeRetrying;
		private bool _logErrorBeforeRetrying;
		private readonly EventSource _eventSource;

		#endregion

        public MsmqSourceWorker(EventSource eventSource, IChannelSink channel, string queuePath, TimeSpan receiveTimeout, TimeSpan retryTimeout, WaitHandle stopSignal)
		{
            _eventSource = eventSource;
			_sink = channel;
			_queue = new MessageQueue("FormatName:" + queuePath);
			_receiveTimeout = receiveTimeout;
			_retryTimeout = retryTimeout;
			_stopSignal = stopSignal;

			_thread = new Thread(Run);
		}

		#region Public Methods

		public void Start()
		{
			_stopping = false;
			_thread.Start();
		}

		public void Stop()
		{
			_stopping = true;
            try
            {
                _queue.Close();
            }
            catch (Exception)
            {
                // Ignore eveything. This is just a prod to try to get the component to stop quicker.
            }
		}

		public void Join()
		{
			_thread.Join();
		}

		#endregion

		#region Private Methods

		private void Run()
		{
			_logErrorBeforeRetrying = true;
			_logTraceBeforeRetrying = true;

			bool canContinue = true;

			while ( canContinue )
			{
				Message message = ReadMessage(out canContinue);

				if ( message != null )
					ProcessMessage(message);
			}
		}

		private Message ReadMessage(out bool canContinue)
		{
			const string method = "ReadMessage";

			canContinue = true;
			Message message = null;

            // Check whether the component is trying to stop before looking for another message.

            if (_stopping)
            {
                if (_eventSource.IsEnabled(Event.Trace))
                    _eventSource.Raise(Event.Trace, method, "The component is stopping so no need to try to receive a message.", Event.Arg("_receiveTimeout", _receiveTimeout));
                canContinue = false;
            }

			try
			{
                if (canContinue)
                    message = _queue.Receive(_receiveTimeout);
			}
			catch ( MessageQueueException e )
			{
				if ( e.MessageQueueErrorCode == MessageQueueErrorCode.OperationCanceled )
				{
					if ( _stopping )
					{
						if ( _eventSource.IsEnabled(Event.Trace) )
							_eventSource.Raise(Event.Trace, method, "The receive operation has been cancelled, shutting down.",  Event.Arg("_receiveTimeout", _receiveTimeout));
						canContinue = false;
					}
					else
					{
						// This is not expected, close the queue, log and then try again.
					
						if ( _logErrorBeforeRetrying && _eventSource.IsEnabled(Event.Error) )
						{
							_logErrorBeforeRetrying = false;
							_eventSource.Raise(Event.Error, method, "An error has occurred whilst waiting to receive a message from the queue. Closing the queue and then trying again.", 
								Event.Arg("MessageQueueErrorCode", e.MessageQueueErrorCode),
								Event.Arg("_receiveTimeout", _receiveTimeout),
								Event.Arg("_retryTimeout", _retryTimeout));
						}

						_queue.Close();

						canContinue = _stopSignal.WaitOne(_retryTimeout, false)? false : true;
					}
				}
				else if ( e.MessageQueueErrorCode == MessageQueueErrorCode.IOTimeout )
				{
					// Timed out, close the queue in case there is something wrong with it and try again.

					if ( _logTraceBeforeRetrying && _eventSource.IsEnabled(Event.Trace) )
					{
						_logTraceBeforeRetrying = false;
						_eventSource.Raise(Event.Trace, method, "Time out whilst waiting to receive a message from the queue. Closing the queue and then trying again.", Event.Arg("_receiveTimeout", _receiveTimeout));
					}

					_queue.Close();

					canContinue = _stopSignal.WaitOne(0, false) ? false : true;
				}
				else
				{
					// Some error which is not expected, close the queue, log and then try again.
				
					if ( _logErrorBeforeRetrying && _eventSource.IsEnabled(Event.Error) )
					{
						_logErrorBeforeRetrying = false;
						_eventSource.Raise(Event.Error, method, "An error has occurred whilst waiting to receive a message from the queue. Closing the queue and then trying again.", 
							Event.Arg("MessageQueueErrorCode", e.MessageQueueErrorCode),
							Event.Arg("_receiveTimeout", _receiveTimeout),
							Event.Arg("_retryTimeout", _retryTimeout));
					}

					_queue.Close();

					canContinue = _stopSignal.WaitOne(_retryTimeout, false) ? false : true;
				}
			}
			catch ( Exception e )
			{
				// Wait the retry time and then try again.

				if ( _logErrorBeforeRetrying && _eventSource.IsEnabled(Event.Error) )
				{
					_logErrorBeforeRetrying = false;
                    _eventSource.Raise(Event.Error, method, "Cannot get a message from the queue because of an unexpected error.", e);
				}

				canContinue = _stopSignal.WaitOne(_retryTimeout, false)? false : true;
			}

			return message;
		}

		private void ProcessMessage(Message message)
		{
			_sink.BeginProcessRequest(message.BodyStream, ChannelCallback, null);
		}

		private void ChannelCallback(IAsyncResult result)
		{
			const string method = "ChannelCallback";

			try
			{
				// We have nothing to do here, but must ensure the channel resources cleanup.

				_sink.EndProcessRequest(result);
			}
			catch ( Exception e )
			{
				if ( _eventSource.IsEnabled(Event.Error) )
                    _eventSource.Raise(Event.Error, method, "The channel has thrown an exception.", e);
			}
		}

		#endregion
	}
}
