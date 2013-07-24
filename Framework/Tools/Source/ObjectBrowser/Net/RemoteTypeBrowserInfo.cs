using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An ITypeInfo implementation that acts as a proxy to access a .NET assembly in another AppDomain.
	/// </summary>
	internal class RemoteTypeBrowserInfo : NetTypeBrowserInfo
	{
		private NetTypeBrowserInfo m_wrapped;
		private RemoteNamespaceBrowserInfo m_namespace;
		private string m_name;
		private DescriptionText m_description = null;

		internal RemoteTypeBrowserInfo(NetTypeBrowserInfo wrapped, RemoteNamespaceBrowserInfo ns, string name)
		{
			Debug.Assert(wrapped != null && ns != null && name != null,
				"wrapped != null && ns != null && name != null");

			m_wrapped = wrapped;
			m_namespace = ns;
			m_name = name;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is RemoteTypeBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			RemoteTypeBrowserInfo other = (RemoteTypeBrowserInfo)obj;

			return Wrapped.CompareTo(other.Wrapped);
		}

		#endregion

		#region IElementInfo Members

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
			get
			{
				if (m_description == null)
				{
					m_description = Wrapped.GetDescription(m_namespace,
						((RemoteNetBrowserManager)Manager).GetRemoteType(Wrapped.GetBaseTypeForDescription()));
					Debug.Assert(m_description != null, "m_description != null");
				}

				return m_description;
			}
		}

		public override int ImageIndex
		{
			get { return Wrapped.ImageIndex; }
		}

		#endregion

		#region ITypeInfo Members

		public override INamespaceBrowserInfo Namespace
		{
			get { return (m_namespace.IsNullNamespace ? null : m_namespace); }
		}

		public override IRepositoryBrowserInfo Repository
		{
			get { return m_namespace.Repository; }
		}

		public override bool HasBaseTypes
		{
			get { return Wrapped.HasBaseTypes; }
		}

		public override ITypeBrowserInfo[] BaseTypes
		{
			get
			{
				RemoteNetBrowserManager manager = (RemoteNetBrowserManager)Manager;
				ICollection realBaseTypes = Wrapped.BaseTypesInternal;
				ITypeBrowserInfo[] array = new ITypeBrowserInfo[realBaseTypes.Count];

				int i = 0;
				foreach (NetTypeBrowserInfo realType in realBaseTypes)
				{
					array[i++] = manager.GetRemoteType(realType);
				}

				return array;
			}
		}

		public override IMemberBrowserInfo[] Members
		{
			get
			{
				ICollection realMembers = Wrapped.MembersInternal;
				IMemberBrowserInfo[] array = new IMemberBrowserInfo[realMembers.Count];

				int i = 0;
				foreach (NetMemberBrowserInfo realMember in realMembers)
				{
					array[i++] = new RemoteMemberBrowserInfo(realMember, this, realMember.NodeText);
				}

				return array;
			}
		}

		#endregion

		public override string FullName
		{
			get { return Wrapped.FullName; }
		}

		internal override NetBrowserManager Manager
		{
			get { return m_namespace.Manager; }
		}

		public override bool Equals(object obj)
		{
			NetTypeBrowserInfo other = obj as NetTypeBrowserInfo;
			if (other == null)
				return false;

			// The other object may be a RemoteTypeBrowserInfo or just a NetTypeBrowserInfo.

			RemoteTypeBrowserInfo otherRemote = other as RemoteTypeBrowserInfo;
			if (otherRemote == null)
				return object.Equals(Wrapped, other);
			else
				return object.Equals(Wrapped, otherRemote.Wrapped);
		}

		public override int GetHashCode()
		{
			return (Wrapped == null ? base.GetHashCode() : Wrapped.GetHashCode());
		}

		public override bool IsSubclassOf(Type c)
		{
			return Wrapped.IsSubclassOf(c);
		}

		internal override NetMemberBrowserInfo GetMember(string nodeText)
		{
			return Wrapped.GetMember(nodeText);
		}

		private NetTypeBrowserInfo Wrapped
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

		private void CreateWrappedObject()
		{
			m_wrapped = m_namespace.GetType(m_name);
			Debug.Assert(m_wrapped != null, "m_wrapped != null");
		}
	}
}
