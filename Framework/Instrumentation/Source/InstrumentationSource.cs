using System;
using LinkMe.Framework.Instrumentation.Message;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Framework.Instrumentation
{
	public abstract class InstrumentationSource
	{
		#region Constructors

		protected InstrumentationSource(System.Type type)
		{
			_state = InstrumentationManager.GetSourceState(type);
		}

		#endregion

		#region Properties

		public string FullyQualifiedReference
		{
			get { return _state.FullyQualifiedReference; }
		}

		# endregion

		#region Operations

		protected bool SourceIsEnabled(Event instrumentationEvent)
		{
			return _state.IsEnabled(instrumentationEvent);
		}

		protected void SourceRaise(Event instrumentationEvent, System.Type type)
		{
			HandleEventMessage(new EventMessage(this, instrumentationEvent, type));
		}

		protected void SourceRaise(Event instrumentationEvent, System.Type type, string method)
		{
			HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method));
		}

		protected void SourceRaise(Event instrumentationEvent, System.Type type, string method,
            params EventArg[] args)
		{
			HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, null, null, CreateParameters(args)));
		}

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, Exception exception, IErrorHandler errorHandler, params EventArg[] args)
        {
            HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, exception, errorHandler, CreateParameters(args)));
        }

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, string message)
		{
			HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, message));
		}

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, string message, Exception exception, IErrorHandler errorHandler)
        {
            HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, message, exception, errorHandler));
        }

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, string message,
            params EventArg[] args)
		{
            HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, message, null, null, CreateParameters(args)));
		}

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, string message, Exception exception, IErrorHandler errorHandler, params EventArg[] args)
        {
            HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, message, exception, errorHandler, CreateParameters(args)));
        }

        protected void SourceRaise(Event instrumentationEvent, System.Type type, string method, string message, Exception exception, params EventArg[] args)
        {
            HandleEventMessage(new EventMessage(this, instrumentationEvent, type, method, message, exception, null, CreateParameters(args)));
        }

        #endregion

        private static EventParameter[] CreateParameters(EventArg[] args)
        {
            if (args == null || args.Length == 0)
                return null;

            var parameters = new EventParameter[args.Length];
            for (int index = 0; index < args.Length; ++index)
                parameters[index] = new EventParameter(args[index].Name, args[index].Value);
            return parameters;
        }

        private void HandleEventMessage(EventMessage eventMessage)
		{
			const string method = "HandleEventMessage";
			try
			{
				// Pass to the message handler.

				_state.MessageHandler.HandleEventMessage(eventMessage);
			}
			catch (Exception ex)
			{
				// Don't propagate anything outside Instrumentation. If an exception is thrown send a
				// message to the internal message handler, but only up to a maximum of 10 times.

				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					string message = "The following error occurred in " + _state.MessageHandler.GetType().Name
						+ " while trying to handle an event message:" + Environment.NewLine
						+ ex;

					if (_errorCount >= Constants.Errors.MaximumErrorsToLog)
					{
						message += Environment.NewLine + Environment.NewLine
							+ "No further message handling errors will be logged.";
					}

					InstrumentationManager.GetInternalMessageHandler().HandleEventMessage(new EventMessage(this, Event.Warning, GetType(), method, message, ex, null));
				}
			}
		}

		#region Members

		private static int _errorCount;
		private readonly SourceState _state;

		#endregion
	}
}
