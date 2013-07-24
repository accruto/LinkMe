using System.Collections;
using System.Diagnostics;

using LinkMe.Framework.Instrumentation.Message;

namespace LinkMe.Framework.Instrumentation
{
	internal class SourceState
	{
		internal SourceState(string fullyQualifiedReference, IMessageHandler messageHandler, BitArray eventStatus)
		{
			m_fullyQualifiedReference = fullyQualifiedReference;
			m_messageHandler = messageHandler;
			m_eventStatus = eventStatus;
		}

		public string FullyQualifiedReference
		{
			get { return m_fullyQualifiedReference; }
		}

		public IMessageHandler MessageHandler
		{
			get { return m_messageHandler; }
		}

		public bool IsEnabled(Event instrumentationEvent)
		{
			return m_eventStatus.Get(instrumentationEvent.Index);
		}

		internal void SetMessageHandler(IMessageHandler messageHandler)
		{
			m_messageHandler = messageHandler;
		}

		internal void SetEventStatus(BitArray value)
		{
			m_eventStatus = value;
		}

		private IMessageHandler m_messageHandler;
		private string m_fullyQualifiedReference;
		private BitArray m_eventStatus;
	}
}
