using System.Runtime.InteropServices;

using LinkMe.Framework.Utility.Exceptions;

namespace LinkMe.Framework.Utility
{
	/// <summary>
	/// Provides access to some functionality of the unmanaged Common Language Runtime API.
	/// </summary>
	public sealed class ClrUtil
	{
		private ClrUtil()
		{
		}

		public static System.AppDomain GetAppDomain(string friendlyName)
		{
			const string method = "GetAppDomain";

			if ( friendlyName == null )
				throw new NullParameterException(typeof(ClrUtil), method, "friendlyName");

			System.AppDomain appDomain = null;

			// Enumerate all AppDomains inside the process using the COM hosting interface.

			ICorRuntimeHost corHost = (ICorRuntimeHost) new CorRuntimeHost();

			System.IntPtr hEnum;
			corHost.EnumDomains(out hEnum);

			while ( true )
			{
				object nextDomain;
				corHost.NextDomain(hEnum, out nextDomain);
				if ( nextDomain == null )
					break;

				System.AppDomain domain = (System.AppDomain) nextDomain;
				if ( domain.FriendlyName == friendlyName )
				{
					appDomain = domain;
					break;
				}
			}

			corHost.CloseEnum(hEnum);
			Marshal.ReleaseComObject(corHost);

			return appDomain;
		}

		public static System.AppDomain GetDefaultAppDomain()
		{
			ICorRuntimeHost corHost = (ICorRuntimeHost) new CorRuntimeHost();

			object appDomain;
			corHost.GetDefaultDomain(out appDomain);

			return (System.AppDomain) appDomain;
		}

		public static void SetMaxThreads(int maxWorkerThreads, int maxIOThreads)
		{
			ICorThreadpool threadPoolControl = (ICorThreadpool) new CorRuntimeHost();
			threadPoolControl.CorSetMaxThreads((uint)maxWorkerThreads, (uint)maxIOThreads);
		}
	}

	[Guid("CB2F6723-AB3A-11D2-9C40-00C04FA30A3E")]
	[ComImport]
	internal class CorRuntimeHost
	{
	}
 
	[Guid("CB2F6722-AB3A-11d2-9C40-00C04FA30A3E")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ICorRuntimeHost
	{
		void CreateLogicalThreadState();        
		void DeleteLogicalThreadState();        
		void _SwitchInLogicalThreadState();        
		void _SwitchOutLogicalThreadState();        
		void LocksHeldByLogicalThread(out uint count);        
		void _MapFile();        
		void _GetConfiguration();        
		void Start();        
		void Stop();        
		void _CreateDomain();        
		void GetDefaultDomain([MarshalAs(UnmanagedType.IUnknown)] out object appDomain);        
		void EnumDomains(out System.IntPtr hEnum);        
		void NextDomain(System.IntPtr hEnum, [MarshalAs(UnmanagedType.IUnknown)] out object appDomain);        
		void CloseEnum(System.IntPtr hEnum);        
		void _CreateDomainEx();        
		void _CreateDomainSetup();        
		void _CreateEvidence();        
		void _UnloadDomain();        
		void _CurrentDomain();
	}
 
	[Guid("84680D3A-B2C1-46e8-ACC2-DBC0A359159A")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ICorThreadpool
	{
		void _RegisterWaitForSingleObject();
		void _UnregisterWait();
		void _QueueUserWorkItem();
		void _CreateTimer();
		void _ChangeTimer();
		void _DeleteTimer();
		void _BindIoCompletionCallback();
		void _CallOrQueueUserWorkItem();
		void CorSetMaxThreads(uint MaxWorkerThreads, uint MaxIOCompletionThreads);
		void CorGetMaxThreads(out uint MaxWorkerThreads, out uint MaxIOCompletionThreads);
		void CorGetAvailableThreads(out uint AvailableWorkerThreads, out uint AvailableIOCompletionThreads);
	}
}
