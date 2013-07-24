using System.ComponentModel;

namespace LinkMe.Framework.Tools.Controls
{
	public delegate void TabPageIndexEventHandler(object sender, TabPageIndexEventArgs e);

	/// <summary>
	/// Provides data for the TabControl SelectedIndexChanging event.
	/// </summary>
	public class TabPageIndexEventArgs : CancelEventArgs
	{
		private int m_newTabPageIndex;

		public TabPageIndexEventArgs(int newTabPageIndex)
			: base(false)
		{
			m_newTabPageIndex = newTabPageIndex;
		}

		public int NewTabPageIndex
		{
			get { return m_newTabPageIndex; }
		}
	}
}
