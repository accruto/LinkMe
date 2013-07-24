using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An IRepositoryBrowserInfo implementation that directly accesses .NET assemblies (System.Reflection.Assembly objects).
	/// </summary>
	public class NetAssemblyBrowserInfo : ElementBrowserInfo, IRepositoryBrowserInfo
	{
		internal const int ImageIndexAssembly = 0;

		private Assembly m_assembly = null;
		private NetBrowserManager m_manager = null;
		// A dictionary of namespaces in this assembly. Key: namespace name. Value: NetNamespaceBrowserInfo object.
		private SortedList m_namespaces = null;
		// A container for the types in this assembly that have no namespace.
		private NetNamespaceBrowserInfo m_nullNamespace = null;
		private DescriptionText m_description = null;

		internal NetAssemblyBrowserInfo()
		{
		}

		internal NetAssemblyBrowserInfo(Assembly assembly, NetBrowserManager manager)
		{
			Debug.Assert(assembly != null && manager != null, "assembly != null && manager != null");

			m_assembly = assembly;
			m_manager = manager;
		}

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return m_assembly.FullName; }
		}

		public override string NodeText
		{
			get { return m_assembly.GetName().Name; }
		}

		public override DescriptionText Description
		{
			get
			{
				if (m_description == null)
				{
					m_description = GetDescription();
					Debug.Assert(m_description != null, "m_description != null");
				}

				return m_description;
			}
		}

		public override int ImageIndex
		{
			get { return ImageIndexAssembly; }
		}

		#endregion

		#region IRepositoryBrowserInfo Members

		public INamespaceBrowserInfo[] Namespaces
		{
			get
			{
				SortedList namespaces = GetNamespaces();

				INamespaceBrowserInfo[] array = new INamespaceBrowserInfo[namespaces.Count];
				namespaces.Values.CopyTo(array, 0);

				return array;
			}
		}

		public ITypeBrowserInfo[] Types
		{
			get
			{
				NetNamespaceBrowserInfo nullNs = GetNullNamespace();
				return (nullNs == null ? null : nullNs.Types);
			}
		}

		public bool HasChildren
		{
			// Assume that all assemblies have some contents. Not strictly true, but if the assembly is
			// empty the user will just have to expand it to find that out.
			get { return true; }
		}

		ObjectBrowserManager IRepositoryBrowserInfo.Manager
		{
			get { return m_manager; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (!(value is NetBrowserManager))
				{
					throw new ArgumentException("This implementation can only accept a 'NetBrowserManager'"
						+ " object for its manager, but a '" + value.GetType().FullName + "' object was passed in.");
				}

				Manager = (NetBrowserManager)value;
			}
		}

		public virtual bool RepositoryEquals(IRepositoryBrowserInfo other)
		{
			NetAssemblyBrowserInfo otherAssembly = other as NetAssemblyBrowserInfo;
			if (other == null)
				return false;

			return (m_assembly == otherAssembly.m_assembly);
		}

		public virtual void OnRefresh()
		{
			ClearCache();
		}

		#endregion

		internal static int GetAccessModifierOffset(AccessModifiers access)
		{
			switch (access)
			{
				case AccessModifiers.Public:
					return 0;
				case AccessModifiers.ProtectedInternal:
					return 1;
				case AccessModifiers.Protected:
					return 2;
				case AccessModifiers.Internal:
					return 3;
				case AccessModifiers.Private:
					return 4;
				default:
					throw new ApplicationException("Unexpected AccessModifiers value: " + access.ToString());
			}
		}

		public virtual string FilePath
		{
			get { return m_assembly.Location; }
		}

		public virtual string FullName
		{
			get { return m_assembly.FullName; }
		}

		internal virtual NetBrowserManager Manager
		{
			get { return m_manager; }
			set
			{
				Debug.Assert(value != null, "value != null");
				m_manager = value;
			}
		}

		private NetBrowserSettings Settings
		{
			get { return (NetBrowserSettings)Manager.Settings; }
		}

		internal void ClearCache()
		{
			// When the ShowNonPublic settings changes we have to clear our whole namespace cache. Some
			// namespaces may contain only internal types, so this would cause them to be added or removed.

			m_namespaces = null;
			m_description = null;
		}

		internal void OnTypeOrderChanged()
		{
			if (m_namespaces != null)
			{
				foreach (NetNamespaceBrowserInfo nsInfo in m_namespaces.Values)
				{
					nsInfo.OnTypeOrderChanged();
				}
			}
		}

		internal virtual NetNamespaceBrowserInfo GetNamespace(string name)
		{
			Debug.Assert(name != null, "name != null");

			if (m_namespaces == null)
			{
				GetNamespacesAndTypes();
				Debug.Assert(m_namespaces != null, "m_namespaces != null");
			}

			return (NetNamespaceBrowserInfo)m_namespaces[name];
		}

		internal virtual SortedList GetNamespaces()
		{
			if (m_namespaces == null)
			{
				GetNamespacesAndTypes();
				Debug.Assert(m_namespaces != null, "m_namespaces != null");
			}

			return m_namespaces;
		}

		internal virtual NetNamespaceBrowserInfo GetNullNamespace()
		{
			if (m_namespaces == null)
			{
				GetNamespacesAndTypes();
				Debug.Assert(m_namespaces != null, "m_namespaces != null");
			}

			return m_nullNamespace;
		}

		private void GetNamespacesAndTypes()
		{
			SortedList namespaces = new SortedList();
			NetNamespaceBrowserInfo nullNamespace = null;

			foreach (Type type in m_assembly.GetTypes())
			{
				if (!Settings.TypeShouldBeVisible(type))
					continue; // The user has chosen not to see types with this visibility.

				string nsName = type.Namespace;

				if (nsName == null)
				{
					// Types with a null namespace appear directly under the repository, but to simplify
					// implementation create a NetNamespaceBrowserInfo named NullNamespaceName for them.

					if (type.FullName.StartsWith("<PrivateImplementationDetails>"))
						continue; // Ignore <PrivateImplementationDetails>.
					else
					{
						if (nullNamespace == null)
						{
							nullNamespace = new NetNamespaceBrowserInfo(this, NetBrowserSettings.NullNamespaceName);
						}

						nullNamespace.AddType(Manager.GetTypeInfo(type, nullNamespace));
					}
				}
				else
				{
					// A "normal" type - it has a namespace.

					NetNamespaceBrowserInfo ns = (NetNamespaceBrowserInfo)namespaces[nsName];

					if (ns == null)
					{
						ns = new NetNamespaceBrowserInfo(this, nsName);
						namespaces.Add(nsName, ns);
					}

					ns.AddType(Manager.GetTypeInfo(type, ns));
				}
			}

			m_namespaces = namespaces;
			m_nullNamespace = nullNamespace;
		}

		private DescriptionText GetDescription()
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				sb.Append("Assembly ");
				sb.AppendName(m_assembly.GetName().Name);

				sb.EndFirstLine();
				sb.Append("     ");
				sb.Append(m_assembly.Location);
				sb.EndLine();
				sb.EndLine();

				Manager.AppendCustomAttributes(sb, m_assembly);

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the assembly description for assembly '"
					+ m_assembly.FullName + "'.", ex);
			}
		}
	}
}
