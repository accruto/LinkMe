using System;

namespace LinkMe.Framework.Tools.Controls
{
	/// <summary>
	/// Provides data for the ComboBox SelectedIndexChanged event.
	/// </summary>
	public class SelectedIndexChangedEventArgs : EventArgs
	{
		private int m_previousSelectedIndex;

		public SelectedIndexChangedEventArgs(int previousSelectedIndex)
		{
			m_previousSelectedIndex = previousSelectedIndex;
		}

		public int PreviousSelectedIndex
		{
			get { return m_previousSelectedIndex; }
		}
	}
}
