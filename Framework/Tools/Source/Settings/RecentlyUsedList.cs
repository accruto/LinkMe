using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

namespace LinkMe.Framework.Tools.Settings
{
	/// <summary>
	/// Maintains a list of "recently used" menu items. A separator is inserted before the
	/// recently used items (if there are any).
	/// </summary>
	public class RecentlyUsedList : ISettingsObject
	{
		public event RecentlyUsedClickEventHandler Click;

		private const string m_itemElementName = "recentlyUsedItem";
		private const string m_displayNameAttribute = "displayName";

		private Menu m_menu = null; // The menu to add items to.
		private int m_firstItemIndex = -1; // Index in the above menu at which the first item appears (the separator).
		private int m_size = -1; // The maximum number of items
		// Lists of item names and display names in the same order as the menu items.
		private StringCollection m_items = new StringCollection();
		private StringCollection m_displayNames = new StringCollection();

		public RecentlyUsedList()
		{
		}

		public RecentlyUsedList(Menu menu, int firstItemIndex)
		{
			m_menu = menu;
			m_firstItemIndex = firstItemIndex;
		}

		#region ISettingsObject Members

		public bool SettingsEqual(ISettingsObject obj)
		{
			RecentlyUsedList other = obj as RecentlyUsedList;
			if (other == null)
				return false;

			return (StringCollectionsEqual(m_items, other.m_items)
				&& StringCollectionsEqual(m_displayNames, other.m_displayNames));
		}

		public void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix,
			string readingFromPath)
		{
			CheckInitialiased();
			Clear();

			XmlNodeList xmlItems = xmlSetting.SelectNodes(xmlPrefix + m_itemElementName, xmlNsManager);

			if (xmlItems.Count > 0)
			{
				m_menu.MenuItems.Add(m_firstItemIndex, new MenuItem("-")); // Add a separator

				// Add items in the order that they appear in the XML

				int index = 1;
				foreach (XmlNode xmlItem in xmlItems)
				{
					// Read the displayName attribute, if present, otherwise use the default display name for
					// the item text.

					XmlNode xmlDisplayName = xmlItem.Attributes.GetNamedItem(m_displayNameAttribute);
					string displayName = (xmlDisplayName == null ? GetDisplayName(xmlItem.InnerText)
						: xmlDisplayName.InnerText);

					MenuItem newItem = new MenuItem("&" + index.ToString() + " " + displayName,
						new EventHandler(item_Click));
					m_menu.MenuItems.Add(m_firstItemIndex + index, newItem);

					m_items.Add(xmlItem.InnerText);
					m_displayNames.Add(displayName);
					index++;
				}

				CheckSize();
			}
		}

		public void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteStartElement(writer.LookupPrefix(xmlns), "recentlyUsedList", xmlns);
			WriteXmlSettingContents(writer, xmlns, writingToPath);
			writer.WriteEndElement();
		}

		public void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
		{
			Debug.Assert(m_items.Count == m_displayNames.Count, "m_items.Count == m_displayNames.Count");

			for (int index = 0; index < m_items.Count; index++)
			{
				writer.WriteStartElement(m_itemElementName, xmlns);

				if (m_displayNames[index] != GetDisplayName(m_items[index]))
				{
					// The display name is different from the default display name for the item name, so write it.
					writer.WriteAttributeString(m_displayNameAttribute, m_displayNames[index]);
				}

				writer.WriteString(m_items[index]);

				writer.WriteEndElement();
			}
		}

		#endregion

		public Menu Menu
		{
			get { return m_menu; }
			set
			{
				if (m_menu != null && m_menu != value)
					throw new InvalidOperationException("The Menu has already been set and cannot be changed.");

				m_menu = value;
			}
		}

		public int FirstItemIndex
		{
			get { return m_firstItemIndex; }
			set
			{
				if (m_firstItemIndex != -1 && m_firstItemIndex != value)
				{
					throw new InvalidOperationException("The first item index has already been set and"
						+ " cannot be changed.");
				}

				m_firstItemIndex = value;
			}
		}

		public int Size
		{
			get { return m_size; }
			set
			{
				m_size = value;
				CheckSize();
			}
		}

		#region Static methods

		private static bool StringCollectionsEqual(StringCollection one, StringCollection two)
		{
			if (one.Count != two.Count)
				return false;

			for (int index = 0; index < one.Count; index++)
			{
				if (one[index] != two[index])
					return false;
			}

			return true;
		}

		private static string GetDisplayName(string text)
		{
			const int maxLength = 80;
			const string replacementText = "...";

			Debug.Assert(text != null, "text != null");

			if (text.Length > maxLength)
			{
				text = text.Substring(0, maxLength / 2) + replacementText
					+ text.Substring(text.Length - maxLength / 2 - replacementText.Length);
			}

			return text.Replace("&", "&&"); // & has special meaning for menu item text, so escape it.
		}

		#endregion

		public void ItemUsed(string item)
		{
			ItemUsed(item, null);
		}

		public void ItemUsed(string item, string displayName)
		{
			if (item == null || item.Length == 0)
				throw new ApplicationException("The recently used item text must not be null or an empty string.");

			CheckInitialiased();

			int existingIndex = m_items.IndexOf(item);
			if (existingIndex != -1)
			{
				RemoveItem(existingIndex); // Remove existing item first
			}

			// Create the "display name" (ie. menu item text). Even if the user passed in a display name
			// call GetDisplayName() on it to make sure it's not too long.

			displayName = (displayName == null ? GetDisplayName(item) : GetDisplayName(displayName));

			// Add new item.

			AddItem(item, displayName);

			// Remove the last item if we're over the maximum size.

			if (m_items.Count > m_size)
			{
				RemoveItem(m_items.Count - 1);
			}

			// Update the text of all existing items.

			for (int index = 2; index <= m_displayNames.Count; index++)
			{
				MenuItem existing = m_menu.MenuItems[m_firstItemIndex + index];
				existing.Text = "&" + index.ToString() + " " + m_displayNames[index - 1];
			}
		}

		public void Clear()
		{
			CheckInitialiased();

			while (m_items.Count > 0)
			{
				RemoveItem(0);
			}

			Debug.Assert(m_displayNames.Count == 0, "m_displayNames.Count == 0");
		}

		protected virtual void OnClick(RecentlyUsedClickEventArgs e)
		{
			CheckInitialiased();

			if (Click != null)
			{
				Click(this, e);
			}
		}

		private void item_Click(object sender, EventArgs e)
		{
			int itemIndex = ((MenuItem)sender).Index - m_firstItemIndex - 1;
			OnClick(new RecentlyUsedClickEventArgs(m_items[itemIndex]));
		}

		private void CheckSize()
		{
			if (m_size != -1)
			{
				// If we're over the new size limit remove items from the bottom of the list
				// (the least recently used).

				while (m_items.Count > m_size)
				{
					RemoveItem(m_items.Count - 1);
				}
			}
		}

		private void AddItem(string item, string displayName)
		{
			Debug.Assert(item != null && displayName != null, "item != null && displayName != null");
			Debug.Assert(m_items.Count == m_displayNames.Count, "m_items.Count == m_displayNames.Count");

			if (m_items.Count == 0)
			{
				m_menu.MenuItems.Add(m_firstItemIndex, new MenuItem("-")); // Add a separator
			}

			MenuItem newItem = new MenuItem("&1 " + displayName, new EventHandler(item_Click));
			m_menu.MenuItems.Add(m_firstItemIndex + 1, newItem);

			m_items.Insert(0, item);
			m_displayNames.Insert(0, displayName);
		}

		private void RemoveItem(int index)
		{
			Debug.Assert(m_items.Count == m_displayNames.Count, "m_items.Count == m_displayNames.Count");

			m_menu.MenuItems.RemoveAt(m_firstItemIndex + index + 1);

			m_items.RemoveAt(index);
			m_displayNames.RemoveAt(index);

			if (m_items.Count == 0)
			{
				m_menu.MenuItems.RemoveAt(m_firstItemIndex); // Remove the separator
			}
		}

		private void CheckInitialiased()
		{
			if (m_menu == null || m_firstItemIndex == -1)
			{
				throw new InvalidOperationException("The RecentlyUsedList object cannot be used until"
					+ " the Menu and FirstItemIndex properties have been set.");
			}
		}
	}
}
