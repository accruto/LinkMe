using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An INamespaceBrowserInfo implementation for .NET assembly namespaces.
	/// </summary>
	internal class NetNamespaceBrowserInfo : ElementBrowserInfo, INamespaceBrowserInfo, IDisposable
	{
		private const int ImageIndexNamespace = 1;

		private NetAssemblyBrowserInfo m_assembly;
		private string m_name;
		private SortedList m_types = new SortedList();
		private DescriptionText m_description = null;

		internal NetNamespaceBrowserInfo()
		{
		}

		internal NetNamespaceBrowserInfo(NetAssemblyBrowserInfo assembly, string name)
		{
			Debug.Assert(assembly != null && name != null, "assembly != null && name != null");

			m_assembly = assembly;
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
			get { return GetDescription(m_assembly); }
		}

		public override int ImageIndex
		{
			get { return ImageIndexNamespace; }
		}

		#endregion

		#region INamespaceBrowserInfo Members

		public INamespaceBrowserInfo Parent
		{
			// .NET namespaces are shown as flat, so none have parents as far as the Object Browser is concerned.
			get { return null; }
		}

		public virtual IRepositoryBrowserInfo Repository
		{
			get { return m_assembly; }
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
			// Since we can only infer the existence of a namespace from a type all namespaces have types in them.
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
			m_assembly = null;
		}

		#endregion

		internal virtual NetBrowserManager Manager
		{
			get { return m_assembly.Manager; }
		}

		internal ICollection TypesInternal
		{
			get { return m_types.Keys; }
		}

		internal virtual bool IsNullNamespace
		{
			get { return (m_name == NetBrowserSettings.NullNamespaceName); }
		}

		internal void AddType(NetTypeBrowserInfo type)
		{
			Debug.Assert(type != null, "type != null");
			m_types.Add(type, type.FullName);
		}

		internal virtual NetTypeBrowserInfo GetType(string name)
		{
			Debug.Assert(m_types != null, "m_types != null");

			// Since the type is the key and the name is the value we need to look up the key by value.

			int index = m_types.IndexOfValue(name);
			return (index == -1 ? null : (NetTypeBrowserInfo)m_types.GetKey(index));
		}

		internal DescriptionText GetDescription(NetAssemblyBrowserInfo parent)
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

		private DescriptionText GetDescriptionInternal(NetAssemblyBrowserInfo parent)
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				sb.Append("namespace ");
				sb.AppendName(m_name);

				sb.EndFirstLine();
				sb.Append(@"     Member of ");
				sb.AppendLink(m_assembly.NodeText, parent);
				sb.EndLine();

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the namespace declaration for namespace '"
					+ DisplayName + "' (assembly '" + m_assembly.DisplayName + "') .", ex);
			}
		}
	}
}
