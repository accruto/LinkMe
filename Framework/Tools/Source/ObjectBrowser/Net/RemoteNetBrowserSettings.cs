using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

using LinkMe.Framework.Tools.ObjectBrowser;

namespace LinkMe.Framework.Tools.ObjectBrowser.Net
{
	/// <summary>
	/// An ObjectBrowserSettings implementation that acts as a proxy to access a real NetBrowserSettings
	/// object in another AppDomain.
	/// </summary>
	public class RemoteNetBrowserSettings : NetBrowserSettings
	{
		private const string RemoteDomainName = "RemoteNetBrowserDomain";

		private AppDomain m_remoteDomain;
		private NetBrowserSettings m_settings;

		#region Constructors

		public RemoteNetBrowserSettings()
			: this(Language.CSharp, null)
		{
		}

		public RemoteNetBrowserSettings(Language language, AppDomain remoteDomain)
			: base(language)
		{
			RemoteDomain = remoteDomain;
		}

		public RemoteNetBrowserSettings(bool showMembers, bool showTypeCheckBoxes, Language language,
			AppDomain remoteDomain)
			: base(showMembers, showTypeCheckBoxes, language)
		{
			RemoteDomain = remoteDomain;
		}

		#endregion

		[Browsable(false)]
		public AppDomain RemoteDomain
		{
			get { return m_remoteDomain; }
			set
			{
				if (m_remoteDomain != value)
				{
					m_remoteDomain = value;
					m_settings = null;
				}
			}
		}

		internal NetBrowserSettings RealSettings
		{
			get
			{
				CheckRemoteSettingsObject();
				return m_settings;
			}
		}

		public override object Clone()
		{
			RemoteNetBrowserSettings cloned = new RemoteNetBrowserSettings();
			cloned.CloneFrom(this);
			return cloned;
		}

		protected override void CloneFrom(ObjectBrowserSettings source)
		{
			base.CloneFrom(source);

			RemoteNetBrowserSettings sourceSettings = (RemoteNetBrowserSettings)source;

			m_remoteDomain = sourceSettings.m_remoteDomain;
			m_settings = (sourceSettings.m_settings == null ? null :
				(NetBrowserSettings)sourceSettings.m_settings.Clone());
		}

		// When settings change (type order, member order, etc.) they need to be propogated to the real (wrapped)
		// settings object, but the sequence is important:
		// 1. Set our own value, so any children accessing the value inside event handlers get the new one.
		// 2. Set the value on the wrapped settings object, which updates all the children.
		// 3. Raise the On...Changed event, which updates the UI.
		// To ensure that the changes happen in this order override the On...Changed() method and set
		// RealSettings.<setting> in there before calling base.On...Changed().

		protected override void OnMemberOrderChanged(EventArgs e)
		{
			if (m_settings != null)
			{
				RealSettings.MemberOrder = MemberOrder;
			}

			base.OnMemberOrderChanged(e);
		}

		protected override void OnTypeOrderChanged(EventArgs e)
		{
			if (m_settings != null)
			{
				RealSettings.TypeOrder = TypeOrder;
			}

			base.OnTypeOrderChanged(e);
		}

		protected override void OnShowMembersChanged(EventArgs e)
		{
			if (m_settings != null)
			{
				RealSettings.ShowMembers = ShowMembers;
			}

			base.OnShowMembersChanged(e);
		}

		protected override void OnShowNonPublicChanged(EventArgs e)
		{
			if (m_settings != null)
			{
				RealSettings.ShowNonPublic = ShowNonPublic;
			}

			base.OnShowNonPublicChanged(e);
		}

		private void CreateRemoteSettingsObject()
		{
			m_settings = (NetBrowserSettings)m_remoteDomain.CreateInstanceAndUnwrap(
				typeof(NetBrowserSettings).Assembly.FullName, typeof(NetBrowserSettings).FullName,
				false, BindingFlags.Instance | BindingFlags.Public, null,
				new object[] { ShowMembers, ShowTypeCheckBoxes, Language }, null, null);
			Debug.Assert(m_settings != null, "m_settings != null");

			m_settings.ShowMembers = ShowMembers;
			m_settings.TypeOrder = TypeOrder;
			m_settings.MemberOrder = MemberOrder;
			m_settings.ShowNonPublic = ShowNonPublic;
		}

		private void CheckRemoteSettingsObject()
		{
			bool reload;

			try
			{
				reload = (m_remoteDomain == null || m_remoteDomain.IsFinalizingForUnload());
			}
			catch (AppDomainUnloadedException)
			{
				reload = true;
			}

			if (reload)
			{
				m_remoteDomain = AppDomain.CreateDomain(RemoteDomainName); // Re-create the remote domain.
				CreateRemoteSettingsObject();
			}
			else if (m_settings == null)
			{
				CreateRemoteSettingsObject();
			}
		}
	}
}
