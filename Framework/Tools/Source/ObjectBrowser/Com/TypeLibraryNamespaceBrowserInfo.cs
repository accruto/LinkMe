using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	/// <summary>
	/// An INamespaceBrowserInfo implementation for a COM type library "namespace", which is really just a container
	/// for all the types in the type library.
	/// </summary>
	internal class TypeLibraryNamespaceBrowserInfo : ElementBrowserInfo, INamespaceBrowserInfo, IDisposable
	{
		private const int ImageIndexNamespace = 1;

		private TypeLibraryBrowserInfo m_typeLibrary;
		private string m_name;
		private SortedList m_types = new SortedList();
		private DescriptionText m_description = null;

		internal TypeLibraryNamespaceBrowserInfo(TypeLibraryBrowserInfo typeLibrary, string name)
		{
			Debug.Assert(typeLibrary != null && name != null, "typeLibrary != null && name != null");

			m_typeLibrary = typeLibrary;
			m_name = name;
		}

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return m_name; }
		}

		public override string NodeText
		{
			get { return m_name; }
		}

		public override DescriptionText Description
		{
			get { return GetDescription(m_typeLibrary); }
		}

		public override int ImageIndex
		{
			get { return ImageIndexNamespace; }
		}

		#endregion

		#region INamespaceBrowserInfo Members

		public INamespaceBrowserInfo Parent
		{
			get { return null; } // There is only a single "namespace" in a type library.
		}

		public virtual IRepositoryBrowserInfo Repository
		{
			get { return m_typeLibrary; }
		}

		public virtual ITypeBrowserInfo[] Types
		{
			get
			{
				ITypeBrowserInfo[] array = new ITypeBrowserInfo[m_types.Count];
				m_types.Keys.CopyTo(array, 0);

				return array;
			}
		}

		public INamespaceBrowserInfo[] Namespaces
		{
			get { return null; }
		}

		public bool HasChildren
		{
			get { return true; }
		}

		public bool AutoCheckRelatives
		{
			get { return true; }
		}

		public bool IncludeThisInChecked
		{
			get { return false; }
		}

		public bool IncludeContentsInChecked
		{
			get { return true; }
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (m_types != null)
			{
				foreach (ComTypeBrowserInfo type in m_types.Keys)
				{
					type.Dispose();
				}

				m_types = null;
			}

			m_typeLibrary = null;
		}

		#endregion

		public string FullName
		{
			get { return m_name; }
		}

		internal virtual ComBrowserManager Manager
		{
			get { return m_typeLibrary.Manager; }
		}

		internal void AddType(ComTypeBrowserInfo type)
		{
			Debug.Assert(type != null, "type != null");
			m_types.Add(type, type.FullName);
		}

		internal virtual ComTypeBrowserInfo GetType(string name)
		{
			Debug.Assert(m_types != null, "m_types != null");

			// Since the type is the key and the name is the value we need to look up the key by value.

			int index = m_types.IndexOfValue(name);
			return (index == -1 ? null : (ComTypeBrowserInfo)m_types.GetKey(index));
		}

		internal DescriptionText GetDescription(TypeLibraryBrowserInfo parent)
		{
			if (m_description == null)
			{
				m_description = GetDescriptionInternal(parent);
				Debug.Assert(m_description != null, "m_description != null");
			}

			return m_description;
		}

		internal void OnTypeOrderChanged()
		{
			// The type order has changed, so re-sort our type list (if any).

			if (m_types != null)
			{
				m_types = new SortedList(m_types);
			}
		}

		private DescriptionText GetDescriptionInternal(TypeLibraryBrowserInfo parent)
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				sb.Append("namespace ");
				sb.AppendName(m_name);

				sb.EndFirstLine();
				sb.Append(@"     Member of ");
				sb.AppendLink(m_typeLibrary.NodeText, parent);
				sb.EndLine();

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the namespace declaration for namespace '"
					+ DisplayName + "' (type library '" + m_typeLibrary.DisplayName + "') .", ex);
			}
		}
	}
}
