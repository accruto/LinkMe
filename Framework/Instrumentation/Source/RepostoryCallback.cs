using System.Diagnostics;
using LinkMe.Framework.Instrumentation.Management;
using LinkMe.Framework.Instrumentation.Management.Connection;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation
{
	public class RepositoryCallback
		:	IRepositoryCallback
	{
		private int _errorCount;

		internal RepositoryCallback()
		{
		}

		#region IRepositoryCallback Members

		public void HandleNotificationException(System.Exception ex)
		{
			const string method = "HandleNotificationException";

			if (_errorCount < Constants.Errors.MaximumErrorsToLog)
			{
				_errorCount++;
				HandleException(method, string.Format(
					"Failed to handle an Instrumentation repository notification:{0}{1}",
					System.Environment.NewLine, ex));
			}
		}

		public void EventStatusChanged(CatalogueElements elementType, string fullName)
		{
			const string method = "EventStatusChanged";

			try
			{
				switch (elementType)
				{
					case CatalogueElements.Namespace:
						InstrumentationManager.UpdateNamespace(fullName);
						break;

					case CatalogueElements.Source:
						InstrumentationManager.UpdateSource(fullName);
						break;

					default:
						Debug.Fail("Unexpected value of elementType: " + elementType);
						break;
				}
			}
			catch (System.Exception ex)
			{
				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					HandleException(method, string.Format(
						"Failed to process an EventStatusChanged notification for {0} '{1}':{2}{3}",
						elementType, fullName, System.Environment.NewLine, ex));
				}
			}
		}

		public void EventChanged(string eventName, bool isEnabled)
		{
			const string method = "EventChanged";

			try
			{
				InstrumentationManager.UpdateEvent(eventName, isEnabled);
			}
			catch (System.Exception ex)
			{
				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					HandleException(method, string.Format(
						"Failed to process an EventChanged notification for event '{0}':{1}{2}",
						eventName, System.Environment.NewLine, ex));
				}
			}
		}

		public void SourceCreated(string fullyQualifiedReference)
		{
			const string method = "SourceCreated";

			try
			{
				InstrumentationManager.SourceCreated(fullyQualifiedReference);
			}
			catch (System.Exception ex)
			{
				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					HandleException(method, string.Format(
						"Failed to process a SourceCreated notification for source '{0}':{1}{2}",
						fullyQualifiedReference, System.Environment.NewLine, ex));
				}
			}
		}

		public void SourceDeleted(string fullyQualifiedReference)
		{
			const string method = "SourceDeleted";

			try
			{
				InstrumentationManager.SourceDeleted(fullyQualifiedReference);
			}
			catch (System.Exception ex)
			{
				if (_errorCount < Constants.Errors.MaximumErrorsToLog)
				{
					_errorCount++;
					HandleException(method, string.Format(
						"Failed to process a SourceDeleted notification for source '{0}':{1}{2}",
						fullyQualifiedReference, System.Environment.NewLine, ex));
				}
			}		
		}

		#endregion

		private void HandleException(string method, string message)
		{
			if (_errorCount >= Constants.Errors.MaximumErrorsToLog)
			{
				message += System.Environment.NewLine + System.Environment.NewLine
					+ "No further repository notification errors will be logged.";
			}

			InstrumentationManager.GetInternalMessageHandler().HandleEventMessage(
				new EventMessage(null, Event.Warning, GetType(), method, message));
		}
	}
}
