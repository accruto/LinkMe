using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// The object browser manager used to create and cache browser elements for .NET assemblies.
	/// </summary>
	public class NetBrowserManager : ObjectBrowserManager
	{
		private Hashtable m_cachedAssemblies = new Hashtable();
		private Hashtable m_cachedTypes = new Hashtable();

		public NetBrowserManager()
		{
		}

		public virtual NetAssemblyBrowserInfo GetAssemblyInfo(Assembly assembly)
		{
			if (assembly == null)
				throw new ArgumentNullException("assembly");

			NetAssemblyBrowserInfo info = (NetAssemblyBrowserInfo)m_cachedAssemblies[assembly];

			if (info == null)
			{
				info = new NetAssemblyBrowserInfo(assembly, this);
				m_cachedAssemblies.Add(assembly, info);
			}

			return info;
		}

		public virtual NetAssemblyBrowserInfo GetAssemblyInfo(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			// Would be nice to use Assembly.LoadFile() here instead of LoadFrom() to allow loading multiple
			// copies of an assembly, but with LoadFile the CLR doesn't find referenced assemblies in the
			// same path as the loaded assembly - they have to be in the GAC.

			return GetAssemblyInfo(Assembly.LoadFrom(filePath));
		}

		protected internal override void ClearCache()
		{
			ClearAssemblyCache();

			m_cachedAssemblies.Clear();
			m_cachedTypes.Clear();

			base.ClearCache();
		}

		protected internal override DragDropEffects OnDragOver(DragDropEffects allowedEffect, int keyState,
			IDataObject data)
		{
			return (((allowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy &&
				data.GetDataPresent(DataFormats.FileDrop)) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		protected internal override IRepositoryBrowserInfo[] OnDragDrop(DragDropEffects effect, int keyState,
			IDataObject data)
		{
			Debug.Assert(effect == DragDropEffects.Copy, "effect == DragDropEffects.Copy");

			object[] files = (object[])data.GetData(DataFormats.FileDrop);
			if (files == null)
				return null;

			// The number of repositories we return may not be the same as the number of files dragged in.
			// If some of them fail to load (eg. they are not .NET assemblies) still return the rest.

			ArrayList repositories = new ArrayList();

			foreach (string file in files)
			{
				try
				{
					repositories.Add(GetAssemblyInfo(file));
				}
				catch (System.Exception)
				{
				}
			}

			return (IRepositoryBrowserInfo[])repositories.ToArray(typeof(IRepositoryBrowserInfo));
		}

		protected internal override void OnMemberOrderChanged()
		{
			foreach (NetTypeBrowserInfo typeInfo in m_cachedTypes.Values)
			{
				typeInfo.OnMemberOrderChanged();
			}

			base.OnMemberOrderChanged();
		}

		protected internal override void OnShowNonPublicChanged()
		{
			// Clear the cached namespaces and types, because those took into account the public/non-public setting.

			ClearAssemblyCache();
			m_cachedTypes.Clear();

			base.OnShowNonPublicChanged();
		}

		protected internal override void OnTypeOrderChanged()
		{
			foreach (NetAssemblyBrowserInfo assemblyInfo in m_cachedAssemblies.Values)
			{
				assemblyInfo.OnTypeOrderChanged();
			}

			base.OnTypeOrderChanged();
		}

		protected override bool SettingsObjectAcceptable(ObjectBrowserSettings settings)
		{
			return (settings is NetBrowserSettings);
		}

		internal NetTypeBrowserInfo GetTypeInfo(Type type, NetNamespaceBrowserInfo ns)
		{
			Debug.Assert(type != null && ns != null, "type != null && ns != null");

			NetTypeBrowserInfo typeInfo = (NetTypeBrowserInfo)m_cachedTypes[type];

			if (typeInfo == null)
			{
				typeInfo = new NetTypeBrowserInfo(type, ns);
				m_cachedTypes.Add(type, typeInfo);
			}

			return typeInfo;
		}

		internal NetTypeBrowserInfo GetTypeInfo(Type type)
		{
			Debug.Assert(type != null, "type != null");

			NetTypeBrowserInfo typeInfo = (NetTypeBrowserInfo)m_cachedTypes[type];

			if (typeInfo == null)
			{
				NetAssemblyBrowserInfo assembly = GetAssemblyInfo(type.Assembly);
				Debug.Assert(assembly != null, "assembly != null");

				string nsName = (type.Namespace == null ? NetBrowserSettings.NullNamespaceName : type.Namespace);
				NetNamespaceBrowserInfo ns = assembly.GetNamespace(nsName);
				Debug.Assert(ns != null, "ns != null");

				// Retrieving the namespaces may add types to CachedTypes, so try to find this type again.

				typeInfo = (NetTypeBrowserInfo)m_cachedTypes[type];

				if (typeInfo == null)
				{
					typeInfo = new NetTypeBrowserInfo(type, ns);
					m_cachedTypes.Add(type, typeInfo);
				}
			}

			return typeInfo;
		}

		internal virtual NetTypeBrowserInfo GetTypeInfoForLink(string assemblyPath, string fullName)
		{
			// Both the assembly path and type name have to be specified as inputs to this method.
			// The AssemblyQualifiedName of the type is not sufficient, because calling GetType() on it
			// could load an assembly with the same name, but from a different location. The loaded type
			// will then NOT be the same as the original type from which this qualified name was obtained.

			Assembly assembly = Assembly.LoadFrom(assemblyPath);
			Debug.Assert(assembly != null, "assembly != null");

			return GetTypeInfo(assembly.GetType(fullName, true, false));
		}

		internal void AppendCustomAttributes(DescriptionBuilder sb, ICustomAttributeProvider provider)
		{
			const string attributeSuffix = "Attribute";

			Debug.Assert(sb != null && provider != null, "sb != null && provider != null");

			NetBrowserSettings settings = (NetBrowserSettings)Settings;

			foreach (Attribute attr in provider.GetCustomAttributes(false))
			{
				Type attrType = attr.GetType();

				// Remove the "Attribute" suffix from the name.

				string name = attrType.Name;
				if (name.EndsWith(attributeSuffix))
				{
					name = name.Substring(0, name.Length - attributeSuffix.Length);
				}

				sb.Append(settings.GetKeyword(NetBrowserSettings.KeywordAttributeStart));
				sb.AppendLink(name, GetTypeInfoForLink(
					attrType.Assembly.Location, attrType.FullName));
				sb.Append(settings.GetKeyword(NetBrowserSettings.KeywordAttributeEnd));
				sb.EndLine();
			}
		}

		private void ClearAssemblyCache()
		{
			foreach (NetAssemblyBrowserInfo assemblyInfo in m_cachedAssemblies.Values)
			{
				assemblyInfo.ClearCache();
			}
		}
	}
}
