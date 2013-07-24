using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

using LinkMe.Framework.Utility;
using Win32 = LinkMe.Framework.Utility.Win32;
using IDataObject = System.Windows.Forms.IDataObject;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	/// <summary>
	/// The object browser manager used to create and cache browser elements for COM type libraries.
	/// </summary>
	public class ComBrowserManager : ObjectBrowserManager
	{
		private Hashtable m_cachedTypeLibraries = new Hashtable();
		private Hashtable m_cachedTypes = new Hashtable();

		public ComBrowserManager()
		{
		}

		public override void Dispose()
		{
			// Dispose of all our type libraries - Dispose() should only be called on the manager when
			// the owner object browser is being disposed.

			foreach (TypeLibraryBrowserInfo typeLib in m_cachedTypeLibraries.Values)
			{
				typeLib.Dispose();
			}

			base.Dispose();
		}

		public virtual TypeLibraryBrowserInfo GetTypeLibraryInfo(string filePath)
		{
			if (filePath == null)
				throw new ArgumentNullException("filePath");

			return GetTypeLibraryInfo(filePath, ComInterop.LoadTypeLibrary(filePath));
		}

		public virtual TypeLibraryBrowserInfo GetTypeLibraryInfo(ITypeLib typeLibrary)
		{
			if (typeLibrary == null)
				throw new ArgumentNullException("typeLibrary");

			TypeLibraryBrowserInfo info = (TypeLibraryBrowserInfo)m_cachedTypeLibraries[typeLibrary];

			if (info == null)
			{
				info = new TypeLibraryBrowserInfo(typeLibrary, this);
				m_cachedTypeLibraries.Add(typeLibrary, info);
			}

			return info;
		}

		public virtual TypeLibraryBrowserInfo GetTypeLibraryInfo(string filePath, ITypeLib typeLibrary)
		{
			if (typeLibrary == null)
				throw new ArgumentNullException("typeLibrary");

			TypeLibraryBrowserInfo info = (TypeLibraryBrowserInfo)m_cachedTypeLibraries[typeLibrary];

			if (info == null)
			{
				info = new TypeLibraryBrowserInfo(filePath, typeLibrary, this);
				m_cachedTypeLibraries.Add(typeLibrary, info);
			}

			return info;
		}

		protected internal override void ClearCache()
		{
			// Do not dispose of the type libraries here, because the object browser may still be showing them.

			foreach (TypeLibraryBrowserInfo typeLib in m_cachedTypeLibraries.Values)
			{
				typeLib.ClearCache();
			}
			m_cachedTypeLibraries.Clear();

			foreach (ComTypeBrowserInfo type in m_cachedTypes.Values)
			{
				type.Dispose();
			}
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
			// If some of them fail to load (eg. they are not COM type libraries) still return the rest.

			ArrayList repositories = new ArrayList();

			foreach (string file in files)
			{
				try
				{
					repositories.Add(GetTypeLibraryInfo(file));
				}
				catch (System.Exception)
				{
				}
			}

			return (IRepositoryBrowserInfo[])repositories.ToArray(typeof(IRepositoryBrowserInfo));
		}

		protected internal override void OnMemberOrderChanged()
		{
			foreach (ComTypeBrowserInfo typeInfo in m_cachedTypes.Values)
			{
				typeInfo.OnMemberOrderChanged();
			}

			base.OnMemberOrderChanged();
		}

		protected internal override void OnTypeOrderChanged()
		{
			foreach (TypeLibraryBrowserInfo libraryInfo in m_cachedTypeLibraries.Values)
			{
				libraryInfo.OnTypeOrderChanged();
			}

			base.OnTypeOrderChanged();
		}

		protected override bool SettingsObjectAcceptable(ObjectBrowserSettings settings)
		{
			return (settings is ComBrowserSettings);
		}

		internal ComTypeBrowserInfo GetTypeInfo(ITypeInfo typeInfo, TypeLibraryNamespaceBrowserInfo ns)
		{
			Debug.Assert(typeInfo != null && ns != null, "typeInfo != null && ns != null");

			ComTypeBrowserInfo ComTypeBrowserInfo = (ComTypeBrowserInfo)m_cachedTypes[typeInfo];

			if (ComTypeBrowserInfo == null)
			{
				ComTypeBrowserInfo = new ComTypeBrowserInfo(typeInfo, ns);
				m_cachedTypes.Add(typeInfo, ComTypeBrowserInfo);
			}

			return ComTypeBrowserInfo;
		}

		internal ComTypeBrowserInfo GetTypeInfo(ITypeInfo typeInfo)
		{
			Debug.Assert(typeInfo != null, "typeInfo != null");

			ComTypeBrowserInfo ComTypeBrowserInfo = (ComTypeBrowserInfo)m_cachedTypes[typeInfo];

			if (ComTypeBrowserInfo == null)
			{
				ITypeLib typeLib;
				int index;
				typeInfo.GetContainingTypeLib(out typeLib, out index);

				TypeLibraryBrowserInfo typeLibrary = GetTypeLibraryInfo(typeLib);
				Debug.Assert(typeLibrary != null, "typeLibrary != null");

				TypeLibraryNamespaceBrowserInfo ns = typeLibrary.GetNamespace();
				Debug.Assert(ns != null, "ns != null");

				// Retrieving the namespaces may add types to CachedTypes, so try to find this type again.

				ComTypeBrowserInfo = (ComTypeBrowserInfo)m_cachedTypes[typeInfo];

				if (ComTypeBrowserInfo == null)
				{
					ComTypeBrowserInfo = new ComTypeBrowserInfo(typeInfo, ns);
					m_cachedTypes.Add(typeInfo, ComTypeBrowserInfo);
				}
			}

			return ComTypeBrowserInfo;
		}
	}
}
