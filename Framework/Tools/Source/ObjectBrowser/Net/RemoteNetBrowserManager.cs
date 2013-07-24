using System;
using System.Collections;
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
	/// An ObjectBrowserManager implementation that acts as a proxy to access a real NetBrowserManager
	/// object in another AppDomain.
	/// </summary>
	public class RemoteNetBrowserManager : NetBrowserManager
	{
		private const string m_remoteDomainName = "RemoteNetBrowserDomain";

		private AppDomain m_remoteDomain;
		private NetBrowserManager m_manager;
		private Hashtable m_assemblies = new Hashtable(); // <full name (string), location (string)>

		#region Constructors

		public RemoteNetBrowserManager()
		{
			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
		}

		public RemoteNetBrowserManager(AppDomain remoteDomain)
			: this()
		{
			RemoteDomain = remoteDomain;
		}

		#endregion

		public AppDomain RemoteDomain
		{
			get { return m_remoteDomain; }
			set
			{
				if (m_remoteDomain != value)
				{
					m_remoteDomain = value;
					m_manager = null;
				}
			}
		}

		internal NetBrowserManager RealManager
		{
			get
			{
				CheckRemoteManagerObject();
				return m_manager;
			}
		}

		public override NetAssemblyBrowserInfo GetAssemblyInfo(Assembly assembly)
		{
			// Since "assembly" is already in the current domain don't bother with a RemoteNetAssembly object.

			AddAssembly(assembly.FullName, assembly.Location);
			return RealManager.GetAssemblyInfo(assembly);
		}

		public override NetAssemblyBrowserInfo GetAssemblyInfo(string filePath)
		{
			return new RemoteAssemblyBrowserInfo(filePath, this);
		}

		protected internal override void ClearCache()
		{
			RealManager.ClearCache();
		}

		protected internal override IRepositoryBrowserInfo[] OnDragDrop(DragDropEffects effect, int keyState, IDataObject data)
		{
			return RealManager.OnDragDrop(effect, keyState, data);
		}

		protected internal override DragDropEffects OnDragOver(DragDropEffects allowedEffect, int keyState, IDataObject data)
		{
			return RealManager.OnDragOver(allowedEffect, keyState, data);
		}

		protected internal override void OnMemberOrderChanged()
		{
			RealManager.OnMemberOrderChanged();
		}

		protected internal override void OnShowMembersChanged()
		{
			RealManager.OnShowMembersChanged();
		}

		protected internal override void OnShowNonPublicChanged()
		{
			RealManager.OnShowNonPublicChanged();
		}

		protected internal override void OnTypeOrderChanged()
		{
			RealManager.OnTypeOrderChanged();
		}

		protected internal override IRepositoryBrowserInfo RefreshRepository(IRepositoryBrowserInfo repository)
		{
			return RealManager.RefreshRepository(repository);
		}

		protected override bool SettingsObjectAcceptable(ObjectBrowserSettings settings)
		{
			return (settings is RemoteNetBrowserSettings);
		}

		internal override void SetSettings(ObjectBrowserSettings value)
		{
			base.SetSettings(value);

			RemoteNetBrowserSettings remoteSettings = (RemoteNetBrowserSettings)value;
			Debug.Assert(remoteSettings.RemoteDomain == RemoteDomain, string.Format(
				"The RemoteDomain for the RemoteNetBrowserManager object is set to '{0}', but the"
				+ " RemoteDomain for the RemoteNetBrowserSettings object passed to it is '{1}'.",
				(RemoteDomain == null ? "<null>" : RemoteDomain.FriendlyName),
				(remoteSettings.RemoteDomain == null ? "<null>" : remoteSettings.RemoteDomain.FriendlyName)));

			RealManager.SetSettings(remoteSettings.RealSettings);
		}

		internal override NetTypeBrowserInfo GetTypeInfoForLink(string assemblyPath, string fullName)
		{
			return GetRemoteType(RealManager.GetTypeInfoForLink(assemblyPath, fullName));
		}

		internal RemoteTypeBrowserInfo GetRemoteType(NetTypeBrowserInfo realType)
		{
			if (realType == null)
				return null;

			NetNamespaceBrowserInfo realNs = realType.NamespaceInternal;

			RemoteAssemblyBrowserInfo assembly = new RemoteAssemblyBrowserInfo(((NetAssemblyBrowserInfo)realNs.Repository).FilePath, this);
			RemoteNamespaceBrowserInfo ns = new RemoteNamespaceBrowserInfo(realNs, assembly, realNs.DisplayName);

			return new RemoteTypeBrowserInfo(realType, ns, realType.FullName);
		}

		internal void AddAssembly(string fullName, string filePath)
		{
			m_assemblies[fullName] = filePath;
		}

		private void CreateRemoteManagerObject()
		{
			m_manager = (NetBrowserManager)m_remoteDomain.CreateInstanceAndUnwrap(
				typeof(NetBrowserManager).Assembly.FullName, typeof(NetBrowserManager).FullName);
			Debug.Assert(m_manager != null, "m_manager != null");

			m_manager.SetSettings(Settings);
		}

		private void CheckRemoteManagerObject()
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
				m_remoteDomain = AppDomain.CreateDomain(m_remoteDomainName); // Re-create the remote domain.
				CreateRemoteManagerObject();
			}
			else if (m_manager == null)
			{
				CreateRemoteManagerObject();
			}
		}

		private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			IDictionaryEnumerator enumerator = m_assemblies.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string fullName = (string)enumerator.Key;
				if (args.Name == fullName)
				{
					// The current AppDomain has attempted to resolve one of the assemblies we loaded
					// and failed. Give it the assembly - this probably should not have happened (otherwise
					// what's the point of using RemoteNetBrowserManager?), but it's probably better to load
					// the assembly than to fail altogether. The only legitimate use for this so far is to
					// properly report a ReflectionTypeLoadException, which contains the types it was loading.

					string location = (string)enumerator.Value;
					Debug.Fail(string.Format("Loading assembly '{0}' ('{1}') into AppDomain '{2}'."
						+ " Should this happen?", fullName, location, AppDomain.CurrentDomain.FriendlyName));

					return Assembly.LoadFrom(location);
				}
			}

			return null;
		}
	}
}
