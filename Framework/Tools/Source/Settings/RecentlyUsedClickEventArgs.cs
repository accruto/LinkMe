using System;
using System.ComponentModel;

namespace LinkMe.Framework.Tools.Settings
{
	public delegate void RecentlyUsedClickEventHandler(object sender, RecentlyUsedClickEventArgs e);

	/// <summary>
	/// Provides data for the RecentlyUsedClick event.
	/// </summary>
	public class RecentlyUsedClickEventArgs : EventArgs
	{
		private string m_itemName;

		internal RecentlyUsedClickEventArgs(string itemName)
		{
			m_itemName = itemName;
		}

		public string ItemName
		{
			get { return m_itemName; }
		}
	}
}
