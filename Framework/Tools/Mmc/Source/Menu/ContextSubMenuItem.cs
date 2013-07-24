using System;
using System.Collections;

namespace LinkMe.Framework.Tools.Mmc
{
	public class ContextSubMenuItem
		:	ContextMenuItem
	{
		public ContextSubMenuItem(string name)
			:	base(name, string.Empty, null)
		{
		}

		public void AddMenuItem(ContextMenuItem item)
		{
			MenuItems.Add(item);
		}

		public ArrayList MenuItems
		{
			get
			{
				if ( m_menuItems == null )
					m_menuItems = new ArrayList();
				return m_menuItems;
			}
		}

		private ArrayList m_menuItems;
	}
}
