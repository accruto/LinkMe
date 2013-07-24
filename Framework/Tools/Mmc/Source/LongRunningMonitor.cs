using System;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Mmc
{
	/// <summary>
	/// A class that sets the cursor to an hourglass when instantiated and back to the default cursor
	/// when disposed. This is a special implementation for an MMC snap-in - there is a generic implementation
	/// in the LinkMe.Framework.Tools assembly.
	/// </summary>
	public class LongRunningMonitor : IDisposable
	{
		private Snapin m_snapin;

		public LongRunningMonitor(Snapin snapin)
		{
			if (snapin == null)
				throw new ArgumentNullException("snapin");

			m_snapin = snapin;

			// DO NOT call DoEvents() in this method. This causes re-entracy problems in IComponent.Notify()
			// and IComponentData.Notify() in the snap-in.

			m_snapin.Cursor = Cursors.WaitCursor; // Display an hourglass.
			// Need to set the current cursor as well, otherwise the above has no effect.
			Cursor.Current = Cursors.WaitCursor;
		}

		#region IDisposable Members

		public void Dispose()
		{
			m_snapin.Cursor = Cursors.Default;
			Cursor.Current = Cursors.Default;
		}

		#endregion
	}
}
