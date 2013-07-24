using System;

namespace LinkMe.Framework.Tools.Controls
{
	public delegate void DisplayModeChangedEventHandler(object sender, DisplayModeChangedEventArgs e);

	/// <summary>
	/// Provides data for the DisplayModeChanged event.
	/// </summary>
	public class DisplayModeChangedEventArgs : EventArgs
	{
		private bool m_fullSize;

		public DisplayModeChangedEventArgs(bool fullSize)
		{
			m_fullSize = fullSize;
		}

		public bool FullSize
		{
			get { return m_fullSize; }
		}
	}
}
