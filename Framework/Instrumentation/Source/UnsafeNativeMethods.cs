using System;
using System.Runtime.InteropServices;
using System.Security;

namespace LinkMe.Framework.Instrumentation
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class UnsafeNativeMethods
	{
		[DllImport("mqrt.dll", CharSet=CharSet.Unicode)]
		internal static extern int MQReceiveMessage(IntPtr hSource, uint dwTimeout, int dwAction, IntPtr pMessageProps,
			IntPtr lpOverlapped, IntPtr fnReceiveCallback, IntPtr hCursor, IntPtr pTransaction);

		private UnsafeNativeMethods()
		{
		}
	}
}
