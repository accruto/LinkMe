using System;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Framework.Instrumentation
{
	public class EventSource
		:	InstrumentationSource
	{
		#region Constructors

		public EventSource(System.Type type)
			:	base(type)
		{
		}

		#endregion

		#region Operations

		/// <summary>
		/// Checks whether the Instrumentation event for this event source
		/// is enabled or not.
		/// </summary>
		/// <param name="instrumentationEvent">The instrument event reference.</param>
		/// <returns>true if the event is enabled otherwise returns false.</returns>
		public bool IsEnabled(Event instrumentationEvent)
		{
			if (this == null)
			{
				// Yes, it's possible for "this" to be null - just not when called from C# or VB.NET code.

				throw new ArgumentNullException("this", "IsEnabled() was called on an EventSource that"
					+ " is null. Most likely, a null EventSource was passed as an argument to an"
					+ " auto-instrumented method.");
			}

			return SourceIsEnabled(instrumentationEvent);
		}

		/// <summary>
		/// Raises the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		public void Raise(Event instrumentationEvent)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="method">The name of the method.</param>
		public void Raise(Event instrumentationEvent, string method)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="method">The method name</param>
		/// <param name="args">The args in the form of name/value pair.</param>
		public void Raise(Event instrumentationEvent, string method, params EventArg[] args)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, args);
		}

		/// <summary>
		/// Rasie the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="method">The name of the method</param>
		/// <param name="message">The message to be logged.</param>
		public void Raise(Event instrumentationEvent, string method, string message)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, message);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="method">The name of the method.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="args">The args in the form of name/value pair.</param>
        public void Raise(Event instrumentationEvent, string method, string message, params EventArg[] args)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, message, args);
		}

        public void Raise(Event instrumentationEvent, string method, Exception exception, IErrorHandler errorHandler, params EventArg[] args)
        {
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, exception, errorHandler, args);
        }

        public void Raise(Event instrumentationEvent, string method, string message, Exception exception, IErrorHandler errorHandler)
        {
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, message, exception, errorHandler);
        }

        public void Raise(Event instrumentationEvent, string method, string message, Exception exception, IErrorHandler errorHandler, params EventArg[] args)
        {
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, message, exception, errorHandler, args);
        }

        public void Raise(Event instrumentationEvent, string method, string message, Exception exception, params EventArg[] args)
        {
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, null, method, message, exception, args);
        }

        /// <summary>
		/// Raises the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="type">The .NET type that is raising the event.</param>
		public void Raise(Event instrumentationEvent, System.Type type)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, type);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="type">The .NET type that is raising the event.</param>
		/// <param name="method">The name of the method.</param>
		public void Raise(Event instrumentationEvent, System.Type type, string method)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, type, method);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="type">The .NET type that is raising the event.</param>
		/// <param name="method">The method name</param>
		/// <param name="args">The args in the form of name/value pair.</param>
		public void Raise(Event instrumentationEvent, System.Type type, string method, params EventArg[] args)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, type, method, args);
		}

		/// <summary>
		/// Rasie the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="type">The .NET type that is raising the event.</param>
		/// <param name="method">The name of the method</param>
		/// <param name="message">The message to be logged.</param>
		public void Raise(Event instrumentationEvent, System.Type type, string method, string message)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, type, method, message);
		}

		/// <summary>
		/// Raise the event for the source.
		/// </summary>
		/// <param name="instrumentationEvent">The instrumentation event reference.</param>
		/// <param name="type">The .NET type that is raising the event.</param>
		/// <param name="method">The name of the method.</param>
		/// <param name="message">The message to be logged.</param>
		/// <param name="args">The args in the form of name/value pair.</param>
		public void Raise(Event instrumentationEvent, System.Type type, string method, string message, params EventArg[] args)
		{
            if (SourceIsEnabled(instrumentationEvent))
                SourceRaise(instrumentationEvent, type, method, message, args);
		}

		#endregion
	}

    public class EventSource<T>
        : EventSource
    {
        public EventSource()
            : base(typeof(T))
        {
        }
    }
}

