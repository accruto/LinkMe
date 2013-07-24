using System;

using Telstra.Enterprise.Ais.Configuration.Management;

namespace Telstra.Enterprise.Ais.Host
{
	/// <summary>
	/// Summary description for ServiceManager.
	/// </summary>
	internal class ContainerManager
		:	System.MarshalByRefObject
	{
		public void Initialize(string domainName, string containerName)
		{
			// Set domain.

			SetDomain(domainName);

			// Initialize the container

			Container.CurrentContainer.Initialize(containerName);
		}

		public void Finalize()
		{
			Container.CurrentContainer.Finalize();
		}

		public void Start()
		{
			Container.CurrentContainer.Start();
		}

		public void Stop()
		{
			Container.CurrentContainer.Stop();
		}

		public void Pause()
		{
			Container.CurrentContainer.Pause();
		}

		public void Continue()
		{
			Container.CurrentContainer.Continue();
		}

		private void SetDomain(string domainName)
		{
			// TODO: Should use an exception from Ais.Utility.

			if ( domainName == null || domainName.Length == 0 )
				throw new ArgumentException("Domain name cannot be null or empty");

			ConfigurationManager.CurrentEnvironmentScope.SetDomain(domainName);
		}
	}
}
