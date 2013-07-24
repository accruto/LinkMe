using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;
using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An IRepositoryBrowserInfo implementation that acts as a proxy to access a .NET assembly in another AppDomain.
	/// </summary>
	public class RemoteAssemblyBrowserInfo : NetAssemblyBrowserInfo
	{
		private NetAssemblyBrowserInfo m_wrapped;
		private RemoteNetBrowserManager m_manager;
		private string m_filePath;

		public RemoteAssemblyBrowserInfo(string filePath, RemoteNetBrowserManager manager)
		{
			const string method = ".ctor";

			if (filePath == null)
				throw new NullParameterException(GetType(), method, "filePath");
			if (manager == null)
				throw new NullParameterException(GetType(), method, "manager");

			m_filePath = filePath;
			m_manager = manager;

			CreateWrappedObject();

			m_manager.AddAssembly(Wrapped.FullName, filePath);
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is RemoteAssemblyBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			RemoteAssemblyBrowserInfo other = (RemoteAssemblyBrowserInfo)obj;

			return Wrapped.CompareTo(other.Wrapped);
		}

		#endregion

		#region IElementBrowserInfo Members

		public override string DisplayName
		{
			get { return Wrapped.DisplayName; }
		}

		public override string NodeText
		{
			get { return Wrapped.NodeText; }
		}

		public override DescriptionText Description
		{
			get { return Wrapped.Description; }
		}

		#endregion

		#region IRepositoryBrowserInfo Members

		public override bool RepositoryEquals(IRepositoryBrowserInfo other)
		{
			NetAssemblyBrowserInfo otherAssembly = other as NetAssemblyBrowserInfo;
			if (otherAssembly == null)
				return false;

			// The other object may be a RemoteAssemblyBrowserInfo or just a AssemblyBrowserInfo.

			RemoteAssemblyBrowserInfo otherRemote = otherAssembly as RemoteAssemblyBrowserInfo;
			if (otherRemote == null)
				return (Wrapped.RepositoryEquals(otherAssembly));
			else
				return (Wrapped.RepositoryEquals(otherRemote.Wrapped));
		}

		public override void OnRefresh()
		{
			Wrapped.OnRefresh();
		}

		#endregion

		private NetAssemblyBrowserInfo Wrapped
		{
			get
			{
				try
				{
					m_wrapped.GetLifetimeService();
				}
				catch (AppDomainUnloadedException)
				{
					CreateWrappedObject();
				}

				return m_wrapped;
			}
		}

		public override bool Equals(object obj)
		{
			NetAssemblyBrowserInfo other = obj as NetAssemblyBrowserInfo;
			if (other == null)
				return false;

			// The other object may be a RemoteAssemblyBrowserInfo or just a NetAssemblyBrowserInfo.

			RemoteAssemblyBrowserInfo otherRemote = other as RemoteAssemblyBrowserInfo;
			if (otherRemote == null)
				return object.Equals(Wrapped, other);
			else
				return object.Equals(Wrapped, otherRemote.Wrapped);
		}

		public override int GetHashCode()
		{
			return (Wrapped == null ? base.GetHashCode() : Wrapped.GetHashCode());
		}

		public override string FilePath
		{
			get
			{
				Debug.Assert(string.Compare(m_filePath, Wrapped.FilePath, true) == 0,
					"m_filePath = '" + m_filePath + "', but Wrapped.FilePath = '" + Wrapped.FilePath + "'");

				return m_filePath;
			}
		}

		public override string FullName
		{
			get { return Wrapped.FullName; }
		}

		internal override NetBrowserManager Manager
		{
			get { return m_manager; }
			set
			{
				m_manager = (RemoteNetBrowserManager)value;
				Wrapped.Manager = m_manager.RealManager;
			}
		}

		internal override SortedList GetNamespaces()
		{
			SortedList namespaces = Wrapped.GetNamespaces();

			SortedList remoteNamespaces = new SortedList(namespaces.Count);

			IDictionaryEnumerator enumerator = namespaces.GetEnumerator();
			enumerator.Reset();

			while (enumerator.MoveNext())
			{
				NetNamespaceBrowserInfo value = (NetNamespaceBrowserInfo)enumerator.Value;
				remoteNamespaces.Add(enumerator.Key, new RemoteNamespaceBrowserInfo(value, this, value.DisplayName));
			}

			return remoteNamespaces;
		}

		internal override NetNamespaceBrowserInfo GetNullNamespace()
		{
			NetNamespaceBrowserInfo nullNs = Wrapped.GetNullNamespace();
			return (nullNs == null ? null : new RemoteNamespaceBrowserInfo(nullNs, this, nullNs.DisplayName));
		}

		internal override NetNamespaceBrowserInfo GetNamespace(string name)
		{
			return Wrapped.GetNamespace(name);
		}

		private void CreateWrappedObject()
		{
			m_wrapped = m_manager.RealManager.GetAssemblyInfo(m_filePath);
			Debug.Assert(m_wrapped != null, "m_wrapped != null");
		}
	}
}
