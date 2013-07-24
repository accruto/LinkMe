using System.Diagnostics;
using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation.MessageComponents
{
	public class EventLogMessageHandler
		:	BaseMessageHandler,
			System.IDisposable
	{
		EventLog _eventLog;

		public EventLogMessageHandler()
		{
			_eventLog = new EventLog(Constants.SystemLog.LogName, ".", Constants.SystemLog.EventSource);
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (_eventLog != null)
			{
				_eventLog.Dispose();
				_eventLog = null;
			}
		}

		#endregion

		protected override void HandleEventMessage(EventMessage eventMessage)
		{
			EventLogEntryType entryType;
			switch (eventMessage.Event)
			{
				case Constants.Events.CriticalError:
				case Constants.Events.Error:
					entryType = EventLogEntryType.Error;
					break;

				case Constants.Events.Warning:
					entryType = EventLogEntryType.Warning;
					break;

				default:
					entryType = EventLogEntryType.Information;
					break;
			}

			_eventLog.WriteEntry(eventMessage.Message, entryType);
		}
	}
}
