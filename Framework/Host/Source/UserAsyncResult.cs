using System;
using System.Threading;
using System.Diagnostics;

using Telstra.Enterprise.Ais.Context;

namespace Telstra.Enterprise.Ais.Host
{
	// TODO: Who is this user?

	public class UserAsyncResult
		:	MarshalByRefObject,
			IAsyncResult
	{
		#region Private Fields

		private AsyncCallback m_userCallback;
		private object m_userState;
		private bool m_completed;
		private bool m_completedSynchronously;
		private object m_completeEvent;
		private object m_message;
		private ISubContext m_context;
		private System.Exception m_exception;
		private int m_setCompleteCount;

		#endregion

		public UserAsyncResult(AsyncCallback userCallback, object userState)
		{
			m_userCallback = userCallback;
			m_userState = userState;
		}

		public void SetComplete(object message, bool completedSynchronously, System.Exception exception)
		{
			if ( Interlocked.Increment(ref m_setCompleteCount) == 1 ) // prevent possible race conditions in the caller's code
			{
				m_completed = true;
				m_completedSynchronously = completedSynchronously;
				m_message = message;
				m_context = CurrentContext.DetachRequestContext();
				m_exception = (exception == null) ? null : exception.GetBaseException();

				if ( m_completeEvent != null )
					((ManualResetEvent) m_completeEvent).Set();

				if ( m_userCallback != null )
					m_userCallback(this);
			}
		}

		public object End()
		{
			if ( !m_completed )
				GetWaitHandle().WaitOne();

			CurrentContext.AttachRequestContext(m_context);

			if ( m_exception != null )
				throw new ApplicationException("The host component has thrown an exception", m_exception);

			return m_message;
		}

		public object End(out System.Exception exception)
		{
			if ( !m_completed )
				GetWaitHandle().WaitOne();

			CurrentContext.AttachRequestContext(m_context);

			exception = m_exception;
			return m_message;
		}

		#region IAsyncResult Members

		object IAsyncResult.AsyncState
		{
			get { return m_userState; }
		}

		bool IAsyncResult.CompletedSynchronously
		{
			get	{ return m_completedSynchronously; }
		}

		WaitHandle IAsyncResult.AsyncWaitHandle
		{
			get { return GetWaitHandle(); }
		}

		bool IAsyncResult.IsCompleted
		{
			get { return m_completed; }
		}

		#endregion

		#region Private Methods

		private WaitHandle GetWaitHandle()
		{
			bool wasCompleted = m_completed;

			// Create the event if it's not already created.

			if ( m_completeEvent == null )
				Interlocked.CompareExchange(ref m_completeEvent, new ManualResetEvent(wasCompleted), null);

			Debug.Assert(m_completeEvent != null, "m_completeEvent != null");
			ManualResetEvent completeEvent = (ManualResetEvent) m_completeEvent;

			// Set the event if the operation was completed while we were here.

			if ( !wasCompleted && m_completed )
				completeEvent.Set();

			return completeEvent;
		}

		#endregion
	}

}
