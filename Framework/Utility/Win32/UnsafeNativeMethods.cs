using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace LinkMe.Framework.Utility.Win32
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class UnsafeNativeMethods
	{
		private UnsafeNativeMethods()
		{
		}

		[DllImport("kernel32.dll", CharSet=CharSet.Auto, SetLastError=true)]
		internal static extern uint GetTempFileName(string tmpPath, string prefix, uint uniqueIdOrZero,
			[Out] StringBuilder tmpFileName);

		[DllImport("ole32.dll", ExactSpelling=true, PreserveSig=false)]
		[return: MarshalAs(UnmanagedType.Interface)]
		internal static extern object CoCreateInstance([In] ref System.Guid rclsid,
			System.IntPtr pUnkOuter, CLSCTX dwClsContext, [In] ref System.Guid riid);

		[DllImport("ole32.dll")]
		internal static extern int CoInitializeSecurity(System.IntPtr pVoid, int cAuthSvc,
			System.IntPtr asAuthSvc, System.IntPtr pReserved1, RPC_C_AUTHN_LEVEL dwAuthnLevel,
			RPC_C_IMP_LEVEL dwImpLevel, System.IntPtr pAuthList, int dwCapabilities, System.IntPtr pReserved3);
	}
}
