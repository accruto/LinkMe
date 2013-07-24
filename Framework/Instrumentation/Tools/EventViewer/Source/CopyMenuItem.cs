using System;
using System.Windows.Forms;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class CopyMenuItem : MenuItem
	{
		private string m_messageHandlerName;
		private bool m_selectedOnly;

		public CopyMenuItem(string messageHandlerName, bool selectedOnly, string displayName, EventHandler onClickEventHandler)
			: base(displayName + "...", onClickEventHandler)
		{
			m_messageHandlerName = messageHandlerName;
			m_selectedOnly = selectedOnly;
		}

		public CopyMenuItem()
		{
			// This constructor is used by WinForms in merging the menus - do not remove it.
		}

		public string MessageHandlerName
		{
			get { return m_messageHandlerName; }
		}

		public bool SelectedOnly
		{
			get { return m_selectedOnly; }
		}
	}
}
