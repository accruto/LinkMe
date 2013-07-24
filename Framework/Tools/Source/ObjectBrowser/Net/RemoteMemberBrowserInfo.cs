using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An IMemberInfo implementation that acts as a proxy to access a .NET assembly in another AppDomain.
	/// </summary>
	internal class RemoteMemberBrowserInfo : NetMemberBrowserInfo
	{
		private NetMemberBrowserInfo m_wrapped;
		private RemoteTypeBrowserInfo m_type;
		private string m_nodeText;

		internal RemoteMemberBrowserInfo(NetMemberBrowserInfo wrapped, RemoteTypeBrowserInfo type, string nodeText)
		{
			Debug.Assert(wrapped != null && type != null && nodeText != null,
				"wrapped != null && type != null && nodeText != null");

			m_wrapped = wrapped;
			m_type = type;
			m_nodeText = nodeText;
		}

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			if (!(obj is RemoteMemberBrowserInfo))
				throw new ArgumentException("Object must be of type '" + GetType().FullName + "'.", "obj");

			RemoteMemberBrowserInfo other = (RemoteMemberBrowserInfo)obj;

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
			get
			{
				Debug.Assert(m_nodeText == Wrapped.NodeText, "m_nodeText == Wrapped.NodeText");
				return m_nodeText;
			}
		}

		public override DescriptionText Description
		{
			get { return Wrapped.GetDescription(Manager, m_type); }
		}

		public override int ImageIndex
		{
			get { return Wrapped.ImageIndex; }
		}

		#endregion

		#region IMemberInfo Members

		public override ITypeBrowserInfo Type
		{
			get { return m_type; }
		}

		#endregion

		public override string Name
		{
			get { return Wrapped.Name; }
		}

		public override MemberTypes MemberType
		{
			get { return Wrapped.MemberType; }
		}

		private NetBrowserManager Manager
		{
			get { return (NetBrowserManager)m_type.Manager; }
		}

		private NetMemberBrowserInfo Wrapped
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
			NetMemberBrowserInfo other = obj as NetMemberBrowserInfo;
			if (other == null)
				return false;

			// The other object may be a RemoteMemberBrowserInfo or just a NetMemberBrowserInfo.

			RemoteMemberBrowserInfo otherRemote = other as RemoteMemberBrowserInfo;
			if (otherRemote == null)
				return object.Equals(Wrapped, other);
			else
				return object.Equals(Wrapped, otherRemote.Wrapped);
		}

		public override int GetHashCode()
		{
			return (Wrapped == null ? base.GetHashCode() : Wrapped.GetHashCode());
		}

		public override string[] GetParameterTypes()
		{
			return Wrapped.GetParameterTypes();
		}

		private void CreateWrappedObject()
		{
			m_wrapped = m_type.GetMember(m_nodeText);
			Debug.Assert(m_wrapped != null, "m_wrapped != null");
		}
	}
}
