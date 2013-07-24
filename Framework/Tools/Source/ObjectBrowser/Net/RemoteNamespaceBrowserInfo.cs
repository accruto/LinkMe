using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An INamespaceBrowserInfo implementation that acts as a proxy to access a .NET assembly in another AppDomain.
	/// </summary>
	internal class RemoteNamespaceBrowserInfo : NetNamespaceBrowserInfo
	{
		private NetNamespaceBrowserInfo m_wrapped;
		private RemoteAssemblyBrowserInfo m_assembly;
		private string m_name;

		internal RemoteNamespaceBrowserInfo(NetNamespaceBrowserInfo wrapped, RemoteAssemblyBrowserInfo assembly, string name)
		{
			Debug.Assert(wrapped != null && assembly != null && name != null,
				"wrapped != null && assembly != null && name != null");

			m_wrapped = wrapped;
			m_assembly = assembly;
			m_name = name;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is RemoteNamespaceBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			RemoteNamespaceBrowserInfo other = (RemoteNamespaceBrowserInfo)obj;

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
			get { return Wrapped.GetDescription(m_assembly); }
		}

		#endregion

		#region INamespaceBrowserInfo Members

		public override IRepositoryBrowserInfo Repository
		{
			get { return m_assembly; }
		}

		public override ITypeBrowserInfo[] Types
		{
			get
			{
				ICollection realTypes = Wrapped.TypesInternal;
				ITypeBrowserInfo[] array = new ITypeBrowserInfo[realTypes.Count];

				int i = 0;
				foreach (NetTypeBrowserInfo realType in realTypes)
				{
					array[i++] = new RemoteTypeBrowserInfo(realType, this, realType.FullName);
				}

				return array;
			}
		}

		#endregion

		internal override NetBrowserManager Manager
		{
			get { return m_assembly.Manager; }
		}

		internal override bool IsNullNamespace
		{
			get { return (m_name == NetBrowserSettings.NullNamespaceName); }
		}

		private NetNamespaceBrowserInfo Wrapped
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
			NetNamespaceBrowserInfo other = obj as NetNamespaceBrowserInfo;
			if (other == null)
				return false;

			// The other object may be a RemoteNamespaceBrowserInfo or just a NetNamespaceBrowserInfo.

			RemoteNamespaceBrowserInfo otherRemote = other as RemoteNamespaceBrowserInfo;
			if (otherRemote == null)
				return object.Equals(Wrapped, other);
			else
				return object.Equals(Wrapped, otherRemote.Wrapped);
		}

		public override int GetHashCode()
		{
			return (Wrapped == null ? base.GetHashCode() : Wrapped.GetHashCode());
		}

		internal override NetTypeBrowserInfo GetType(string name)
		{
			return Wrapped.GetType(name);
		}

		private void CreateWrappedObject()
		{
			m_wrapped = m_assembly.GetNamespace(m_name);
			Debug.Assert(m_wrapped != null, "m_wrapped != null");
		}
	}
}
