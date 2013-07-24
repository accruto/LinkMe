using System;
using System.Collections;

namespace LinkMe.Framework.Tools.Mmc
{
	public delegate void MenuCommandHandler(object item, SnapinNode node);

	public class ContextMenuItem
	{
		public ContextMenuItem(string name, string statusText, bool isChecked, bool isEnabled, MenuCommandHandler handler)
			:	this(name, statusText, false, isChecked, isEnabled, handler)
		{
		}

		public ContextMenuItem(string name, string statusText, bool isChecked, MenuCommandHandler handler)
			:	this(name, statusText, false, isChecked, true, handler)
		{
		}

		public ContextMenuItem(string name, string statusText, MenuCommandHandler handler)
			:	this(name, statusText, false, false, true, handler)
		{
		}

		protected ContextMenuItem(string name, string statusText, bool isSeparator, bool isChecked, bool isEnabled, MenuCommandHandler handler)
		{
			m_name = name;
			m_statusText = statusText;
			m_isSeparator = isSeparator;
			m_isChecked = isChecked;
			m_isEnabled = isEnabled;
			if ( handler != null )
				m_handler += handler;
			m_commandId = 0;
			m_visible = true;
			m_isDefault = false;
		}

		public string Name
		{
			get { return m_name; }
		}

		public bool Visible
		{
			get { return m_visible; }
		}

		public string StatusText
		{
			get { return m_statusText; }
		}

		public int CommandId
		{
			get { return m_commandId; }
			set { m_commandId = value; }
		}
		
		public bool IsDefault
		{
			get { return m_isDefault; }
		}

		public bool IsSeparator
		{
			get { return m_isSeparator; }
		}

		public bool IsChecked
		{
			get { return m_isChecked; }
		}

		public bool IsEnabled
		{
			get { return m_isEnabled; }
		}

		public virtual void OnCommand(SnapinNode node)
		{
			if ( m_handler != null )
				m_handler(this, node);
		}

		private string m_name;
		private bool m_visible;
		private bool m_isDefault;
		private string m_statusText;
		private int m_commandId;
		private bool m_isSeparator;
		private bool m_isChecked;
		private bool m_isEnabled;
		private event MenuCommandHandler m_handler;
	}

	public class SeparatorMenuItem
		:	ContextMenuItem
	{
		public SeparatorMenuItem()
			:	base(string.Empty, string.Empty, true, false, true, null)
		{
		}
	}
}
