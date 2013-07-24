using System;
using System.Windows.Forms;

namespace LinkMe.Framework.Instrumentation.Tools.EventViewer
{
	internal class ConnectMenuItem : MenuItem
	{
		private string m_messageReaderName;

		public ConnectMenuItem(string messageReaderName, string displayName, EventHandler onClickEventHandler)
			: base(displayName + "...", onClickEventHandler)
		{
			m_messageReaderName = messageReaderName;
		}

		public ConnectMenuItem()
		{
			// This constructor is used by WinForms in merging the menus - do not remove it.
		}

		public string MessageReaderName
		{
			get { return m_messageReaderName; }
		}
	}
}
