using System;
using System.Web;
using LinkMe.Framework.Instrumentation;
using LinkMe.Framework.Utility.Exceptions;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Apps.Asp.Exceptions
{
	public static class ExceptionManager
	{
		private static readonly EventSource EventSource = new EventSource(typeof (ExceptionManager));

		public static void HandleException(Exception ex, IErrorHandler errorHandler)
		{
		    const string method = "HandleException";

            // The top-level HttpUnhandledException is useless and just makes all the errors look the same
            // in the log. Skip it and just log the real exception.

            if (ex is HttpUnhandledException && ex.InnerException != null)
                ex = ex.InnerException;

            EventSource.Raise(ex is UserException ? (Event) Event.NonCriticalError : Event.CriticalError, method, "An exception has occurred.", ex, errorHandler);
		}
	}
}