using System;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools
{
	/// <summary>
	/// A class that sets the cursor to an hourglass when instantiated and back to the previous cursor
	/// when disposed. If the cursor is already an hourglass then it is set to the default cursor when disposed.
	/// </summary>
	public class LongRunningMonitor : IDisposable
	{
		private Control m_control;
		private Cursor m_previous;

		public LongRunningMonitor(Control control)
		{
			if (control == null)
				throw new ArgumentNullException("control");

			m_control = control;

			if (control.Cursor == Cursors.WaitCursor)
			{
				m_previous = Cursors.Default;
			}
			else
			{
				m_previous = control.Cursor;
				control.Cursor = Cursors.WaitCursor;
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (m_control != null)
			{
				if (m_control.Cursor != m_previous)
				{
					m_control.Cursor = m_previous;
				}
				m_control = null;
			}
		}

		#endregion
	}
}
