using System;
using System.Collections;

using Telstra.Enterprise.Ais.Instrumentation;

namespace Telstra.Enterprise.Ais.Host
{
	/// <summary>
	/// Summary description for ServiceManager.
	/// </summary>
	public class ServiceManager
	{
		/// <summary> 
		/// An AppDomain for each container.
		/// </summary>
		private Hashtable m_appDomains = new Hashtable(15);

		/// <summary> 
		/// Instrumentation for Service.
		/// </summary>
		static private EventSource m_eventSource;

		/// <summary>
		/// Set things in motion so your service can do its work.
		/// </summary>
		public void OnStart(string[] domainContainers)
		{
			m_eventSource = new EventSource(typeof(ServiceManager));

			// Method entry trace

			const string method = "OnStart";

			if (m_eventSource.IsEnabled(Events.MethodEnter))
				m_eventSource.Raise(Events.MethodEnter, method,
					Events.Parameter("containerDomainNames", domainContainers));

			// Create an AppDomain for each container and intialize the container
			foreach (string domainContainer in domainContainers)
			{
				string[] domainContainerInfo = domainContainer.Split('.');
				if(domainContainerInfo.Length != 2)
					throw new ArgumentException("Domain or Container name missing in " + domainContainer);

				string domainName = domainContainerInfo[0];
				string containerName = domainContainerInfo[1];

				System.AppDomain appDomain = CreateDomain(domainName, containerName);
				if(appDomain == null)
					throw new ApplicationException("Unable to create app domains for containers");

				m_appDomains.Add(containerName, appDomain);
			}

			// Start all AppDomains
			foreach ( System.AppDomain appDomain in m_appDomains.Values )
				StartDomain(appDomain);

			// Method exit trace

			if (m_eventSource.IsEnabled(Events.MethodExit))
				m_eventSource.Raise(Events.MethodExit, method);
		}
 
		/// <summary>
		/// Stop this service.
		/// </summary>
		public void OnStop()
		{
			// Method entry trace

			const string method = "OnStop";

			if (m_eventSource.IsEnabled(Events.MethodEnter))
				m_eventSource.Raise(Events.MethodEnter, method);

			// Stop all containers in AppDomains
			foreach ( System.AppDomain appDomain in m_appDomains.Values )
				StopDomain(appDomain);

			// Unload all AppDomains
			foreach ( System.AppDomain appDomain in m_appDomains.Values )
				UnloadDomain(appDomain);

			m_appDomains.Clear();

			// Method exit trace

			if (m_eventSource.IsEnabled(Events.MethodExit))
				m_eventSource.Raise(Events.MethodExit, method);
		}
 
		/// <summary>
		/// Pause this service.
		/// </summary>
		public void OnPause()
		{
			// Method entry trace

			const string method = "OnPause";

			if (m_eventSource.IsEnabled(Events.MethodEnter))
				m_eventSource.Raise(Events.MethodEnter, method);

			// Pause on all containers in AppDomains
			foreach ( System.AppDomain appDomain in m_appDomains.Values )
				PauseDomain(appDomain);

			// Method exit trace

			if (m_eventSource.IsEnabled(Events.MethodExit))
				m_eventSource.Raise(Events.MethodExit, method);
		}
 
		/// <summary>
		/// Continue this service.
		/// </summary>
		public void OnContinue()
		{
			// Method entry trace

			const string method = "OnContinue";

			if (m_eventSource.IsEnabled(Events.MethodEnter))
				m_eventSource.Raise(Events.MethodEnter, method);

			// Continue on all containers in AppDomains
			foreach ( System.AppDomain appDomain in m_appDomains.Values )
				ContinueDomain(appDomain);

			// Method exit trace

			if (m_eventSource.IsEnabled(Events.MethodExit))
				m_eventSource.Raise(Events.MethodExit, method);
		}

		private System.AppDomain CreateDomain(string domainName, string containerName)
		{
			System.AppDomain appDomain = null;
			try
			{
				// Create the AppDomain for each container and initialize the container with AppDomain

				appDomain = System.AppDomain.CreateDomain(containerName);

				// Create an instance of the container in that domain and initialize it.

				string fullName = typeof(ContainerManager).Assembly.FullName;
				string typeName = typeof(ContainerManager).FullName;

				ContainerManager manager = (ContainerManager) appDomain.CreateInstanceAndUnwrap(fullName, typeName);

				if(manager != null)
				{
					appDomain.SetData("ContainerManager", manager);
					manager.Initialize(domainName, containerName);
				}
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
				if ( appDomain != null )
				{
					System.AppDomain.Unload(appDomain);
					appDomain = null;
				}
			}
			return appDomain;
		}

		private void UnloadDomain(System.AppDomain appDomain)
		{
			try
			{
				// Get the service manager instance and finalize

				ContainerManager manager = (ContainerManager) appDomain.GetData("ContainerManager");

				manager.Finalize();

				if ( appDomain != null )
				{
					System.AppDomain.Unload(appDomain);
				}
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
			}
		}

		private void StartDomain(System.AppDomain appDomain)
		{
			try
			{
				// Get the service manager instance and finalize

				ContainerManager manager = (ContainerManager) appDomain.GetData("ContainerManager");

				manager.Start();
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
			}
		}

		private void StopDomain(System.AppDomain appDomain)
		{
			try
			{
				// Get the service manager instance and finalize

				ContainerManager manager = (ContainerManager) appDomain.GetData("ContainerManager");

				manager.Stop();
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
			}
		}

		private void PauseDomain(System.AppDomain appDomain)
		{
			try
			{
				// Get the service manager instance and finalize

				ContainerManager manager = (ContainerManager) appDomain.GetData("ContainerManager");

				manager.Pause();
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
			}
		}

		private void ContinueDomain(System.AppDomain appDomain)
		{
			try
			{
				// Get the service manager instance and finalize

				ContainerManager manager = (ContainerManager) appDomain.GetData("ContainerManager");

				manager.Continue();
			}
			catch (System.Exception ex)
			{
				string errorString = GenerateErrorString(ex);
			}
		}

		private string GenerateErrorString(System.Exception e)
		{
			string errorString = e.Message;
			e = e.InnerException;
			while ( e != null )
			{
				errorString += System.Environment.NewLine + e.Message;
				e = e.InnerException;
			}

			return errorString;
		}
	}
}
