using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

using LinkMe.Framework.Tools.Settings;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	/// <summary>
	/// Base class for object browser settings object. Derive from this class to implement settings
	/// specific to your objects. The class is a Component to allow derived classes to create ImageLists
	/// in the VS.NET Designer.
	/// </summary>
	public class ObjectPropertySettings
		:	Component,
			ISettingsObject,
			ICloneable
	{
		/// <summary>
		/// Raised to request the object browser to refresh the entire contents of both TreeViews.
		/// This event does not need to be raised when on of the *Changed events is raised.
		/// </summary>
		public event EventHandler Refresh;
		private Images m_images = new Images();

		protected ObjectPropertySettings()
		{
		}

		#region ISettingsObject Members

		public bool SettingsEqual(ISettingsObject obj)
		{
			// Not likely to be used any time soon, so just the objects are different for now.
			return false;
		}

		public virtual void ReadXmlSettings(XmlNode xmlSetting, XmlNamespaceManager xmlNsManager, string xmlPrefix, string readingFromPath)
		{
		}

		public virtual void WriteXmlSettings(XmlWriter writer, string xmlns, string writingToPath)
		{
			writer.WriteStartElement("objectBrowser", xmlns);
			WriteXmlSettingContents(writer, xmlns, writingToPath);
			writer.WriteEndElement();
		}

		public virtual void WriteXmlSettingContents(XmlWriter writer, string xmlns, string writingToPath)
		{
		}

		#endregion

		#region ICloneable Members

		public virtual object Clone()
		{
			throw new NotImplementedException("The derived object browser settings class '"
				+ GetType().FullName + "' must override Clone().");
		}

		#endregion

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

		protected void AddImageResource(string imageResource)
		{
			if ( m_images.IndexOf(imageResource) == -1 )
				m_images.AddResource(imageResource);
		}

		public int GetImageIndex(string imageResource)
		{
			return m_images.IndexOf(imageResource);
		}

		public Images ElementIcons
		{
			get { return m_images; }
		}

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

		protected virtual void CloneFrom(ObjectPropertySettings source)
		{
		}
	}
}
