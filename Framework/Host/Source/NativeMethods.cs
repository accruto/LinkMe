using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace LinkMe.Framework.Host
{
	[SuppressUnmanagedCodeSecurity, ComVisible(false)]
	internal class SafeNativeMethods
	{
		// Methods
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool CloseServiceHandle(IntPtr handle);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool GetServiceDisplayName(IntPtr SCMHandle, string shortName, StringBuilder displayName, ref int displayNameLength);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool GetServiceKeyName(IntPtr SCMHandle, string displayName, StringBuilder shortName, ref int shortNameLength);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		public static extern int LsaClose(IntPtr objectHandle);

		[DllImport("advapi32.dll")]
		public static extern int LsaFreeMemory(IntPtr ptr);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		public static extern int LsaNtStatusToWinError(int ntStatus);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static extern int MessageBox(IntPtr hWnd, string text, string caption, int type);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr OpenSCManager(string machineName, string databaseName, SCM_ACCESS access);

		[Flags]
		public enum SCM_ACCESS
			:	uint
		{
			STANDARD_RIGHTS_REQUIRED		= 0xF0000,
			SC_MANAGER_CONNECT				= 0x00001,
			SC_MANAGER_CREATE_SERVICE		= 0x00002,
			SC_MANAGER_ENUMERATE_SERVICE	= 0x00004,
			SC_MANAGER_LOCK					= 0x00008,
			SC_MANAGER_QUERY_LOCK_STATUS	= 0x00010,
			SC_MANAGER_MODIFY_BOOT_CONFIG	= 0x00020,
			SC_MANAGER_ALL_ACCESS			= STANDARD_RIGHTS_REQUIRED
				| SC_MANAGER_CONNECT
				| SC_MANAGER_CREATE_SERVICE
				| SC_MANAGER_ENUMERATE_SERVICE
				| SC_MANAGER_LOCK
				| SC_MANAGER_QUERY_LOCK_STATUS
				| SC_MANAGER_MODIFY_BOOT_CONFIG
		}
	}

	internal class NativeMethods
	{
		// Methods
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr CreateService(IntPtr databaseHandle, string serviceName, string displayName, SERVICE_ACCESS access, int serviceType, int startType, int errorControl, string binaryPath, string loadOrderGroup, IntPtr pTagId, string dependencies, string servicesStartName, string password);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool DeleteService(IntPtr serviceHandle);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool LookupAccountName(string systemName, string accountName, byte[] sid, int[] sidLen, char[] refDomainName, int[] domNameLen, [In, Out] int[] sidNameUse);

		//[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		//public static extern int LsaAddAccountRights(IntPtr policyHandle, byte[] accountSid, LSA_UNICODE_STRING userRights, int countOfRights);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		public static extern int LsaEnumerateAccountRights(IntPtr policyHandle, byte[] accountSid, out IntPtr pLsaUnicodeStringUserRights, out int RightsCount);

		//[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		//public static extern int LsaOpenPolicy(LSA_UNICODE_STRING systemName, IntPtr pointerObjectAttributes, int desiredAccess, out IntPtr pointerPolicyHandle);

		//[DllImport("advapi32.dll", CharSet=CharSet.Unicode)]
		//public static extern int LsaRemoveAccountRights(IntPtr policyHandle, byte[] accountSid, bool allRights, LSA_UNICODE_STRING userRights, int countOfRights);

		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr OpenService(IntPtr databaseHandle, string serviceName, SERVICE_ACCESS access);
		
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr RegisterServiceCtrlHandler(string serviceName, Delegate callback);
		
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr RegisterServiceCtrlHandlerEx(string serviceName, Delegate callback, IntPtr userData);
		
		//[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		//public static extern unsafe bool SetServiceStatus(IntPtr serviceStatusHandle, SERVICE_STATUS* status);
		
		[DllImport("advapi32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool StartServiceCtrlDispatcher(IntPtr entry);
		
		[DllImport("advapi32.dll", EntryPoint="ChangeServiceConfig2W", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern bool ChangeServiceDescription(IntPtr serviceHandle, int infoLevel, SERVICE_DESCRIPTION serviceDescription);

		[DllImport("advapi32.dll", EntryPoint="QueryServiceStatus", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr QueryServiceStatus(IntPtr serviceHandle, ref SERVICE_STATUS serviceStatus);

		[DllImport("advapi32.dll", EntryPoint="QueryServiceStatusEx", CharSet=CharSet.Unicode, SetLastError=true)]
		public static extern IntPtr QueryServiceStatusEx(IntPtr serviceHandle, SC_STATUS_TYPE infoLevel, ref SERVICE_STATUS_PROCESS serviceStatusProcess, int serviceStatusProcessSize, ref uint bytesNeeded);

		[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]  
		public class SERVICE_DESCRIPTION
		{
			public string Description;
		}

		[StructLayout(LayoutKind.Sequential, Pack=1)]
		public struct SERVICE_STATUS
		{
			public static readonly int SizeOf = Marshal.SizeOf(typeof(SERVICE_STATUS));
			public SERVICE_TYPES ServiceType;
			public SERVICE_STATE CurrentState;  
			public uint ControlsAccepted;  
			public uint Win32ExitCode;  
			public uint ServiceSpecificExitCode;  
			public uint CheckPoint;  
			public uint WaitHint;
		}

		[StructLayout(LayoutKind.Sequential, Pack=1)]
		public struct SERVICE_STATUS_PROCESS
		{
			public static readonly int SizeOf = Marshal.SizeOf(typeof(SERVICE_STATUS_PROCESS));
			public SERVICE_TYPES ServiceType;
			public SERVICE_STATE CurrentState;
			public uint ControlsAccepted;
			public uint Win32ExitCode;
			public uint ServiceSpecificExitCode;
			public uint CheckPoint;
			public uint WaitHint;
			public uint ProcessId;
			public uint ServiceFlags;
		}

		[Flags]
		public enum SERVICE_TYPES
			:	int
		{
			SERVICE_KERNEL_DRIVER			= 0x00000001,
			SERVICE_FILE_SYSTEM_DRIVER		= 0x00000002,
			SERVICE_ADAPTER					= 0x00000004,
			SERVICE_RECOGNIZER_DRIVER		= 0x00000008,
			SERVICE_DRIVER					= SERVICE_KERNEL_DRIVER | SERVICE_FILE_SYSTEM_DRIVER| SERVICE_RECOGNIZER_DRIVER,
			SERVICE_WIN32_OWN_PROCESS		= 0x00000010,
			SERVICE_WIN32_SHARE_PROCESS		= 0x00000020,
			SERVICE_WIN32					= SERVICE_WIN32_OWN_PROCESS | SERVICE_WIN32_SHARE_PROCESS
		}

		[Flags]
		public enum SERVICE_STATE
			:	int
		{
			SERVICE_ACTIVE      = 0x00000001,
			SERVICE_INACTIVE    = 0x00000002,
			SERVICE_STATE_ALL   = SERVICE_ACTIVE | SERVICE_INACTIVE
		}

		[Flags]
		public enum SC_STATUS_TYPE
			:	int
		{
			SC_STATUS_PROCESS_INFO	= 0x00000000,
		};

		[Flags]
		public enum SERVICE_ACCESS
			:	uint
		{
			DELETE							= 0x10000,
			STANDARD_RIGHTS_REQUIRED		= 0xF0000,
			SERVICE_QUERY_CONFIG			= 0x00001,
			SERVICE_CHANGE_CONFIG			= 0x00002,
			SERVICE_QUERY_STATUS			= 0x00004,
			SERVICE_ENUMERATE_DEPENDENTS	= 0x00008,
			SERVICE_START					= 0x00010,
			SERVICE_STOP					= 0x00020,
			SERVICE_PAUSE_CONTINUE			= 0x00040,
			SERVICE_INTERROGATE				= 0x00080,
			SERVICE_USER_DEFINED_CONTROL	= 0x00100,
			SERVICE_ALL_ACCESS				= STANDARD_RIGHTS_REQUIRED
				| SERVICE_QUERY_CONFIG
				| SERVICE_CHANGE_CONFIG
				| SERVICE_QUERY_STATUS
				| SERVICE_ENUMERATE_DEPENDENTS
				| SERVICE_START
				| SERVICE_STOP
				| SERVICE_PAUSE_CONTINUE
				| SERVICE_INTERROGATE
				| SERVICE_USER_DEFINED_CONTROL
		}
	}
}
