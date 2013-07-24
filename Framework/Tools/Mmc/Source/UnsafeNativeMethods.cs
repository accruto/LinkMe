using System;
using System.Security;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace LinkMe.Framework.Tools.Mmc
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class UnsafeNativeMethods
	{
		private UnsafeNativeMethods()
		{
		}

		/// <summary>
		/// Used to create an IStream over global memory
		/// </summary>
		[DllImport("ole32.dll", SetLastError=true)]
		internal static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease,
			out IStream ppstm);
	}
}
