using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using LinkMe.Framework.Utility;
using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Com
{
	/// <summary>
	/// An IRepositoryBrowserInfo implementation that reads COM type libraries.
	/// </summary>
	public class TypeLibraryBrowserInfo : ElementBrowserInfo, IRepositoryBrowserInfo, IDisposable
	{
		#region Nested types

		/// <summary>
		/// A class that contains an unmanaged resource and no references to other classes, so it can have a
		/// destructor without too much of a performance penalty - as recommended in Rico Mariani's blog.
		/// </summary>
		private class ComLibWrapper : IDisposable
		{
			private ITypeLib m_typeLib;

			public ComLibWrapper(ITypeLib typeLib)
			{
				Debug.Assert(typeLib != null, "typeLib != null");
				m_typeLib = typeLib;
			}

			~ComLibWrapper()
			{
				DisposeInternal();
			}

			#region IDisposable Members

			public void Dispose()
			{
				DisposeInternal();
				GC.SuppressFinalize(this);
			}

			#endregion

			public ITypeLib TypeLib
			{
				get { return m_typeLib; }
			}

			private void DisposeInternal()
			{
				if (m_typeLib != null)
				{
					Marshal.ReleaseComObject(m_typeLib);
					m_typeLib = null;
				}
			}
		}

		#endregion

		private const int ImageIndexAssembly = 0;

		private string m_filePath;
		private ComLibWrapper m_typeLibrary;
		private ComBrowserManager m_manager;
		// The only namespace in this type library.
		private TypeLibraryNamespaceBrowserInfo m_namespace = null;
		private DescriptionText m_description = null;

		internal TypeLibraryBrowserInfo(ITypeLib typeLibrary, ComBrowserManager manager)
			: this(ComInterop.GetTypeLibPath(typeLibrary), typeLibrary, manager)
		{
		}

		internal TypeLibraryBrowserInfo(string filePath, ITypeLib typeLibrary, ComBrowserManager manager)
		{
			Debug.Assert(typeLibrary != null && manager != null, "typeLibrary != null && manager != null");

			m_filePath = filePath;
			m_typeLibrary = new ComLibWrapper(typeLibrary);
			m_manager = manager;
		}

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return System.IO.Path.GetFileNameWithoutExtension(m_filePath); }
		}

		public override string NodeText
		{
			get { return DisplayName; }
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
			get { return new INamespaceBrowserInfo[] { GetNamespace() }; }
		}

		public ITypeBrowserInfo[] Types
		{
			get { return null; }
		}

		public bool HasChildren
		{
			// Assume that all type libraries have some contents.
			get { return true; }
		}

		ObjectBrowserManager IRepositoryBrowserInfo.Manager
		{
			get { return m_manager; }
			set
			{
				if (value == null)
					throw new ArgumentNullException("value");
				if (!(value is ComBrowserManager))
				{
					throw new ArgumentException("This implementation can only accept a 'ComBrowserManager'"
						+ " object for its manager, but a '" + value.GetType().FullName + "' object was passed in.");
				}

				Manager = (ComBrowserManager)value;
			}
		}

		public virtual bool RepositoryEquals(IRepositoryBrowserInfo other)
		{
			TypeLibraryBrowserInfo otherTypeLibrary = other as TypeLibraryBrowserInfo;
			if (other == null)
				return false;

			return (m_typeLibrary == otherTypeLibrary.m_typeLibrary);
		}

		public virtual void OnRefresh()
		{
			ClearCache();
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (m_typeLibrary != null)
			{
				m_typeLibrary.Dispose();
				m_typeLibrary = null;
			}

			ClearCache();
		}

		#endregion

		public virtual string FilePath
		{
			get { return m_filePath; }
		}

		internal virtual ComBrowserManager Manager
		{
			get { return m_manager; }
			set
			{
				Debug.Assert(value != null, "value != null");
				m_manager = value;
			}
		}

		internal virtual void ClearCache()
		{
			if (m_namespace != null)
			{
				m_namespace.Dispose();
				m_namespace = null;
			}
		}

		internal void OnTypeOrderChanged()
		{
			if (m_namespace != null)
			{
				m_namespace.OnTypeOrderChanged();
			}
		}

		internal virtual TypeLibraryNamespaceBrowserInfo GetNamespace()
		{
			if (m_namespace == null)
			{
				m_namespace = GetNamespaceInternal();
				Debug.Assert(m_namespace != null, "m_namespace != null");
			}

			return m_namespace;
		}

		private TypeLibraryNamespaceBrowserInfo GetNamespaceInternal()
		{
			// There is one namespace for the type library itself.

			TypeLibraryNamespaceBrowserInfo ns = new TypeLibraryNamespaceBrowserInfo(this,
				Marshal.GetTypeLibName(m_typeLibrary.TypeLib));

			// Iterate through all the TypeInfos.

			for ( int index = 0; index < m_typeLibrary.TypeLib.GetTypeInfoCount(); ++index )
			{
				ITypeInfo typeInfo;
				m_typeLibrary.TypeLib.GetTypeInfo(index, out typeInfo);

				// Add all TypeInfos except for modules and aliases.

				ObjectType infoType = ComTypeBrowserInfo.GetObjectType(typeInfo);
				if (infoType != ObjectType.Module && infoType != ObjectType.Alias)
				{
					ns.AddType(Manager.GetTypeInfo(typeInfo, ns));
				}
			}

			return ns;
		}

		private string GetDocString()
		{
			string name;
			string docString;
			int helpContext;
			string helpFile;

			m_typeLibrary.TypeLib.GetDocumentation(-1, out name, out docString, out helpContext, out helpFile);

			return docString;
		}

		private DescriptionText GetDescription()
		{
			try
			{
				DescriptionBuilder sb = new DescriptionBuilder(true);

				sb.Append("Type Library ");
				sb.AppendName(DisplayName);

				sb.EndFirstLine();
				sb.Append("     ");
				sb.Append(m_filePath);
				sb.EndLine();

				string description = GetDocString();
				if (description != null && description.Length != 0)
				{
					sb.AppendHeading("Description:");
					sb.Append(description);
				}

				return sb.GetText();
			}
			catch (System.Exception ex)
			{
				throw new ApplicationException("Failed to write the type library description for type library '"
					+ m_filePath + "'.", ex);
			}
		}
	}
}
