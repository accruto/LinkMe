using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

using LinkMe.Framework.Tools.ObjectBrowser;
using LinkMe.Framework.Tools.Settings;

namespace LinkMe.Framework.Tools.ObjectBrowser
{
	/// <summary>
	/// Base class for object browser settings object. Derive from this class to implement settings
	/// specific to your objects. The class is a Component to allow derived classes to create ImageLists
	/// in the VS.NET Designer.
	/// </summary>
	public class ObjectBrowserSettings : Component, ISettingsObject, ICloneable
	{
		/// <summary>
		/// Raised to request the object browser to refresh the entire contents of both TreeViews.
		/// This event does not need to be raised when on of the *Changed events is raised.
		/// </summary>
		public event EventHandler Refresh;
		public event EventHandler ShowMembersChanged;
		public event EventHandler TypeOrderChanged;
		public event EventHandler MemberOrderChanged;
		public event EventHandler ShowNonPublicChanged;

		private const bool m_defaultShowNonPublic = true;
		private const bool m_defaultShowMembers = true;
		private const bool m_defaultShowTypeCheckboxes = false;
		private const TypeOrder m_defaultTypeOrder = TypeOrder.Alphabetical;
		private const MemberOrder m_defaultMemberOrder = MemberOrder.Alphabetical;

		private bool m_showMembers;
		private bool m_showTypeCheckBoxes;
		private bool m_showNonPublic = m_defaultShowNonPublic;
		private TypeOrder m_typeOrder = m_defaultTypeOrder;
		private MemberOrder m_memberOrder = m_defaultMemberOrder;

		protected ObjectBrowserSettings()
			: this(m_defaultShowMembers, m_defaultShowTypeCheckboxes)
		{
		}

		protected ObjectBrowserSettings(bool showMembers, bool showTypeCheckBoxes)
		{
			m_showMembers = showMembers;
			m_showTypeCheckBoxes = showTypeCheckBoxes;
		}

		#region ISettingsObject Members

		public virtual bool SettingsEqual(ISettingsObject obj)
		{
			ObjectBrowserSettings other = obj as ObjectBrowserSettings;
			if (other == null)
				return false;
			
			return (ShowMembers == other.ShowMembers && ShowTypeCheckBoxes == other.ShowTypeCheckBoxes
				&& ShowNonPublic == other.ShowNonPublic && TypeOrder == other.TypeOrder
				&& MemberOrder == other.MemberOrder);
		}

		public virtual void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix, string readingFromPath)
		{
			XmlNode xmlValue = xmlSetting.SelectSingleNode(xmlPrefix + "typeOrder", xmlNsManager);
			if (xmlValue != null)
			{
				TypeOrder = (TypeOrder)Enum.Parse(typeof(TypeOrder), xmlValue.InnerText);
			}

			xmlValue = xmlSetting.SelectSingleNode(xmlPrefix + "memberOrder", xmlNsManager);
			if (xmlValue != null)
			{
				MemberOrder = (MemberOrder)Enum.Parse(typeof(MemberOrder), xmlValue.InnerText);
			}

			if (ObjectAccessSupported)
			{
				xmlValue = xmlSetting.SelectSingleNode(xmlPrefix + "showNonPublic", xmlNsManager);
				if (xmlValue != null)
				{
					ShowNonPublic = XmlConvert.ToBoolean(xmlValue.InnerText);
				}
			}
		}

		public virtual void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteStartElement("objectBrowser", xmlns);
			WriteXmlSettingContents(writer, xmlns, writingToPath);
			writer.WriteEndElement();
		}

		public virtual void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
		{
			// Don't save the ShowMembers value, because this is not something the user can change - it can
			// only be set from code.

			writer.WriteElementString("typeOrder", xmlns, XmlConvert.ToString((int)TypeOrder));
			writer.WriteElementString("memberOrder", xmlns, XmlConvert.ToString((int)MemberOrder));

			if (ObjectAccessSupported)
			{
				writer.WriteElementString("showNonPublic", xmlns, XmlConvert.ToString(ShowNonPublic));
			}
		}

		#endregion

		#region ICloneable Members

		public virtual object Clone()
		{
			throw new NotImplementedException("The derived object browser settings class '"
				 + GetType().FullName + "' must override Clone().");
		}

		#endregion

		/// <summary>
		/// True if the object browser supports the ShowNonPublic setting.
		/// </summary>
		[Browsable(false)]
		public virtual bool ObjectAccessSupported
		{
			get { return false; }
		}

		/// <summary>
		/// True to show CheckBoxes next to the nodes in the type TreeView to allow the user to select multiple types.
		/// This value is set in the constructor and cannot be changed.
		/// </summary>
		[Browsable(false)]
		public bool ShowTypeCheckBoxes
		{
			get { return m_showTypeCheckBoxes; }
		}

		/// <summary>
		/// True to show the members TreeView as well as types TreeView, false to show only the types TreeView.
		/// The default is true.
		/// </summary>
		[DefaultValue(m_defaultShowMembers)]
		public virtual bool ShowMembers
		{
			get { return m_showMembers; }
			set
			{
				if (value != m_showMembers)
				{
					m_showMembers = value;
					OnShowMembersChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// The order in which elements in the left pane are displayed. The default is Alphabetical.
		/// </summary>
		[DefaultValue(m_defaultTypeOrder)]
		public virtual TypeOrder TypeOrder
		{
			get { return m_typeOrder; }
			set
			{
				if (value != m_typeOrder)
				{
					m_typeOrder = value;
					OnTypeOrderChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// The order in which elements in the right pane are displayed. The default is Alphabetical.
		/// </summary>
		[DefaultValue(m_defaultMemberOrder)]
		public virtual MemberOrder MemberOrder
		{
			get { return m_memberOrder; }
			set
			{
				if (value != m_memberOrder)
				{
					m_memberOrder = value;
					OnMemberOrderChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>
		/// True to show all types and members regardless of their visibility, false to show only public ones.
		/// The default is true.
		/// </summary>
		[DefaultValue(m_defaultShowNonPublic)]
		public virtual bool ShowNonPublic
		{
			get { return m_showNonPublic; }
			set
			{
				if (value != m_showNonPublic)
				{
					m_showNonPublic = value;
					OnShowNonPublicChanged(EventArgs.Empty);
				}
			}
		}

		protected internal virtual ImageList TypeIcons
		{
			get { return null; }
		}

		protected internal virtual ImageList MemberIcons
		{
			get { return null; }
		}

		#region Static methods

		/// <summary>
		/// Compares two types given the priority specified by the typePriority array. Types closer to the start
		/// of the array are considered "greater". For example, if the array is { string, int } then "string" is
		/// considered higher then "int", so CompareTypes(int, string) will return -1.
		/// </summary>
		protected static int CompareTypes(Type typeA, Type typeB, Type[] typePriority)
		{
			Debug.Assert(typePriority != null && typeA != null && typeB != null,
				"typePriority != null && typeA != null && typeB != null");
			Debug.Assert(typePriority.Length > 1, "Meaningless call to CompareTypes() - the typePriority array"
				+ " should contain at least 2 types.");

			foreach (Type type in typePriority)
			{
				bool typeBmatches = (typeB == type || typeB.IsSubclassOf(type));

				if (typeA == type || typeA.IsSubclassOf(type))
					return (typeBmatches ? 0 : -1);
				else if (typeBmatches)
					return 1;
			}

			Debug.Fail("Neither of the types passed to CompareTypes() were not in the typePriority array: '"
				+ typeA.FullName + "' and '" + typeB.FullName + "'.");
			return 0;
		}

		#endregion

		public override object InitializeLifetimeService()
		{
			return null;
		}

		protected virtual void OnRefresh(EventArgs e)
		{
			if (Refresh != null)
			{
				Refresh(this, e);
			}
		}

		protected virtual void OnShowMembersChanged(EventArgs e)
		{
			if (ShowMembersChanged != null)
			{
				ShowMembersChanged(this, e);
			}
		}

		protected virtual void OnTypeOrderChanged(EventArgs e)
		{
			if (TypeOrderChanged != null)
			{
				TypeOrderChanged(this, e);
			}
		}

		protected virtual void OnMemberOrderChanged(EventArgs e)
		{
			if (MemberOrderChanged != null)
			{
				MemberOrderChanged(this, e);
			}
		}

		protected virtual void OnShowNonPublicChanged(EventArgs e)
		{
			if (ShowNonPublicChanged != null)
			{
				ShowNonPublicChanged(this, e);
			}
		}

		protected virtual void CloneFrom(ObjectBrowserSettings source)
		{
			m_memberOrder = source.m_memberOrder;
			m_showMembers = source.m_showMembers;
			m_showNonPublic = source.m_showNonPublic;
			m_showTypeCheckBoxes = source.m_showTypeCheckBoxes;
			m_typeOrder = source.m_typeOrder;
		}
	}
}
