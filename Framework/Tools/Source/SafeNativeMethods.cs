using System;
using System.Security;
using System.Runtime.InteropServices;

using LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeNativeMethods
	{
		private SafeNativeMethods()
		{
		}

		[DllImport("user32.dll")]
		internal static extern int DestroyIcon(HandleRef hIcon);

		[DllImport("gdi32.dll")]
		internal static extern IntPtr CreateDIBSection(HandleRef hdc, ref BITMAPINFO pbmi, uint iUsage,
			out IntPtr ppvBits, IntPtr hSection, uint dwOffset);
	}
}
