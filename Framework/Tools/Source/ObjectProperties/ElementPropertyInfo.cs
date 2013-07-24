using System.Windows.Forms;
using System.Collections;

namespace LinkMe.Framework.Tools.ObjectProperties
{
	/// <summary>
	/// A base implementation of IElementBrowserInfo.
	/// </summary>
	public abstract class ElementPropertyInfo
		:	System.MarshalByRefObject,
			IElementPropertyInfo
	{
		private object m_element;
		private IElementPropertyInfo m_parentInfo;
		private IObjectPropertyInfo m_objectInfo;
		private ElementPropertyPage m_propertyPage;
		private string m_currentView = null;
		private ArrayList m_elements = null;
		private string m_imageResource;

		protected ElementPropertyInfo(IElementPropertyInfo parentInfo, string imageResource, object element)
		{
			m_element = element;
			m_parentInfo = parentInfo;
			if ( parentInfo != null )
				m_objectInfo = parentInfo.ObjectInfo;
			else
				m_objectInfo = this as IObjectPropertyInfo;
			m_imageResource = imageResource;
		}

		#region IElementPropertyInfo Members

		public object Element
		{
			get { return m_element; }
		}

		public abstract string Name
		{
			get;
		}

		public int ImageIndex
		{
			get { return m_objectInfo.Settings.GetImageIndex(m_imageResource); }
		}

		public IElementPropertyInfo Parent
		{
			get { return m_parentInfo; }
		}

		public IElementPropertyInfo[] Elements
		{
			get
			{
				if ( m_elements == null )
					m_elements = GetElements();
				IElementPropertyInfo[] array = new IElementPropertyInfo[m_elements.Count];
				m_elements.CopyTo(array, 0);
				return array;
			}
		}

		public IObjectPropertyInfo ObjectInfo
		{
			get { return m_objectInfo; }
		}

		public virtual bool CanDeleteElement
		{
			get { return false; }
		}

		public virtual void DeleteElement()
		{
		}

		public virtual bool CanCreateElement
		{
			get { return false; }
		}

		public virtual string CreateElementName
		{
			get { return string.Empty; }
		}

		public virtual IElementPropertyInfo CreateElement()
		{
			return null;
		}

		public virtual bool CanRenameElement
		{
			get { return false; }
		}

		public virtual void RenameElement(string newName)
		{
		}

		public abstract void RefreshParentElement(object parentElement);

		public virtual string[] Views
		{
			get { return null; }
		}

		public virtual void ViewElement(string view)
		{
			m_currentView = view;
			m_propertyPage = null;
		}

		public string CurrentView
		{
			get { return m_currentView; }
		}

		public ElementPropertyPage PropertyPage
		{
			get
			{
				if ( m_propertyPage == null )
				{
					if ( m_currentView == null )
						m_propertyPage = CreatePropertyPage();
					else
						m_propertyPage = CreatePropertyPage(m_currentView);
				}

				if ( m_propertyPage != null )
					m_propertyPage.Refresh(this);
				return m_propertyPage;
			}
		}

		protected virtual ElementPropertyPage CreatePropertyPage()
		{
			return null;
		}

		protected virtual ElementPropertyPage CreatePropertyPage(string view)
		{
			return null;
		}

		protected virtual ArrayList GetElements()
		{
			return new ArrayList();
		}

		#endregion

		protected void SetElement(object element)
		{
			m_element = element;
		}

		protected void ClearElements()
		{
			m_elements = null;
		}

		protected IElementPropertyInfo FindElement(string name)
		{
			foreach ( IElementPropertyInfo elementInfo in Elements )
			{
				if ( elementInfo.Name == name )
					return elementInfo;
			}

			return null;
		}

		#region IComparable Members

		/// <summary>
		/// The base implementation compares the node text first using case-insensitive comparison and then,
		/// if the elements are equal, case-sensitive comparison.
		/// </summary>
		public virtual int CompareTo(object obj)
		{
			IElementPropertyInfo other = obj as IElementPropertyInfo;
			if (other == null)
			{
				throw new System.ArgumentException(string.Format("Only another IElementPropertyInfo object can be"
					+ " compared to a '{0}' object, but '{1}' was passed in.", GetType().FullName,
					(obj == null ? "<null>" : obj.GetType().FullName)), "obj");
			}

			int result = string.Compare(Name, other.Name, true);
			if (result != 0)
				return result;

			return string.Compare(Name, other.Name, false);
		}

		#endregion

		public override object InitializeLifetimeService()
		{
			return null;
		}
	}
}
