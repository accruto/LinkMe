using System.ComponentModel;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace LinkMe.Framework.Host
{
	public class ServiceStatus
	{
		public ServiceStatus()
		{
			_installed = false;
			_status = ServiceControllerStatus.Stopped;
			_processId = 0;
		}

		public ServiceStatus(bool installed, ServiceControllerStatus status, uint processId)
		{
			_installed = installed;
			_status = status;
			_processId = processId;
		}

		public bool Installed
		{
			get { return _installed; }
		}

		public ServiceControllerStatus Status
		{
			get { return _status; }
		}

		public uint ProcessId
		{
			get { return _processId; }
		}

		private readonly bool _installed;
		private readonly ServiceControllerStatus _status;
        private readonly uint _processId;
	}

	public class ServiceUtil
	{
		private ServiceUtil()
		{
		}

		public static bool DoesServiceExist(string computer, string service)
		{
			System.IntPtr hScManager = SafeNativeMethods.OpenSCManager(computer, null, SafeNativeMethods.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if ( hScManager == System.IntPtr.Zero )
				throw new System.InvalidOperationException("OpenSCManager", new Win32Exception());

			System.IntPtr hService = System.IntPtr.Zero;
			try
			{
				hService = NativeMethods.OpenService(hScManager, service, NativeMethods.SERVICE_ACCESS.SERVICE_USER_DEFINED_CONTROL);
				if ( hService == System.IntPtr.Zero )
				{
					int error = Marshal.GetLastWin32Error();
					if ( error == ERROR_SERVICE_DOES_NOT_EXIST )
						return false;
					else
						throw new Win32Exception(error);
				}
				else
					return true;
			}
			finally
			{
				if ( hService != System.IntPtr.Zero )
					SafeNativeMethods.CloseServiceHandle(hService);
				SafeNativeMethods.CloseServiceHandle(hScManager);
			}
		}

		public static void CreateService(string computer, string service, string displayName, string description, bool shareProcess, ServiceStartMode startType, string binaryPath, string dependencies, string account, string password)
		{
			System.IntPtr hScManager = SafeNativeMethods.OpenSCManager(computer, null, SafeNativeMethods.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if ( hScManager == System.IntPtr.Zero )
				throw new System.InvalidOperationException("OpenSCManager", new Win32Exception());

			System.IntPtr hService = System.IntPtr.Zero;
			try
			{
				int serviceType = shareProcess ? SERVICE_WIN32_SHARE_PROCESS : SERVICE_WIN32_OWN_PROCESS;
				if ( account != null && account.Length == 0 )
					account = null; // the LocalSystem account

				hService = NativeMethods.CreateService(hScManager, service, displayName, NativeMethods.SERVICE_ACCESS.SERVICE_ALL_ACCESS, serviceType, (int) startType, 1, binaryPath, null, System.IntPtr.Zero, dependencies, account, password);
				if ( hService == System.IntPtr.Zero )
					throw new Win32Exception();

				var serviceDescription = new NativeMethods.SERVICE_DESCRIPTION {Description = description};
			    NativeMethods.ChangeServiceDescription(hService, SERVICE_CONFIG_DESCRIPTION, serviceDescription);
			}
			finally
			{
				if ( hService != System.IntPtr.Zero )
					SafeNativeMethods.CloseServiceHandle(hService);
				SafeNativeMethods.CloseServiceHandle(hScManager);
			}
		}

		public static bool RemoveService(string computer, string service)
		{
			System.IntPtr hScManager = SafeNativeMethods.OpenSCManager(computer, null, SafeNativeMethods.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
			if ( hScManager == System.IntPtr.Zero )
				throw new System.InvalidOperationException("OpenSCManager", new Win32Exception());

			System.IntPtr hService = System.IntPtr.Zero;
			try
			{
				hService = NativeMethods.OpenService(hScManager, service, NativeMethods.SERVICE_ACCESS.DELETE);
				if ( hService == System.IntPtr.Zero )
					return false;

				if ( !NativeMethods.DeleteService(hService) )
					throw new Win32Exception();
			}
			finally
			{
				if ( hService != System.IntPtr.Zero )
					SafeNativeMethods.CloseServiceHandle(hService);
				SafeNativeMethods.CloseServiceHandle(hScManager);
			}

			return true;
		}

		public static ServiceStatus GetStatus(string computer, string service)
		{
		    if ( DoesServiceExist(computer, service) )
			{
				// Grab the status.

				var controller = new ServiceController(service, computer);
				ServiceControllerStatus status = controller.Status;

				// Grab the process id.

				uint processId = 0;
				if ( status != ServiceControllerStatus.Stopped )
				{
					System.IntPtr hScManager = SafeNativeMethods.OpenSCManager(computer, null, SafeNativeMethods.SCM_ACCESS.SC_MANAGER_ALL_ACCESS);
					if ( hScManager == System.IntPtr.Zero )
						throw new System.InvalidOperationException("OpenSCManager", new Win32Exception());

					System.IntPtr hService = System.IntPtr.Zero;
					try
					{
						hService = NativeMethods.OpenService(hScManager, service, NativeMethods.SERVICE_ACCESS.SERVICE_QUERY_STATUS);
						if ( hService != System.IntPtr.Zero )
						{
							var serviceStatusProcess = new NativeMethods.SERVICE_STATUS_PROCESS();
							uint bytesNeeded = 0;
							if ( NativeMethods.QueryServiceStatusEx(hService, NativeMethods.SC_STATUS_TYPE.SC_STATUS_PROCESS_INFO, ref serviceStatusProcess, NativeMethods.SERVICE_STATUS_PROCESS.SizeOf, ref bytesNeeded) != System.IntPtr.Zero )
								processId = serviceStatusProcess.ProcessId;
							else
								throw new Win32Exception();
						}
					}
					finally
					{
						if ( hService != System.IntPtr.Zero )
							SafeNativeMethods.CloseServiceHandle(hService);
						SafeNativeMethods.CloseServiceHandle(hScManager);
					}
				}

				return new ServiceStatus(true, status, processId);
			}
		    
            return new ServiceStatus();
		}

	    public static void Start(string computer, string service, bool wait, System.TimeSpan timeout)
		{
			var controller = new ServiceController(service, computer);
			controller.Start();
			if ( wait )
				controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
		}

		public static void Stop(string computer, string service, bool wait, System.TimeSpan timeout)
		{
			var controller = new ServiceController(service, computer);
			controller.Stop();
			if ( wait )
				controller.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
		}

		public static void Pause(string computer, string service, bool wait, System.TimeSpan timeout)
		{
			var controller = new ServiceController(service, computer);
			controller.Pause();
			if ( wait )
				controller.WaitForStatus(ServiceControllerStatus.Paused, timeout);
		}

        public static void Continue(string computer, string service, bool wait, System.TimeSpan timeout)
		{
			var controller = new ServiceController(service, computer);
			controller.Continue();
			if ( wait )
				controller.WaitForStatus(ServiceControllerStatus.Running, timeout);
		}

		private const int SERVICE_WIN32_OWN_PROCESS = 0x00000010;
		private const int SERVICE_WIN32_SHARE_PROCESS = 0x00000020;
		private const int SERVICE_CONFIG_DESCRIPTION = 1;
		private const int ERROR_SERVICE_DOES_NOT_EXIST = 1060;
	}
}
