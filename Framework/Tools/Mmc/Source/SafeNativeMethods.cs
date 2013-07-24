using System;
using System.Security;
using System.Runtime.InteropServices;

namespace LinkMe.Framework.Tools.Mmc
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeNativeMethods
	{
		private SafeNativeMethods()
		{
		}

		/// <summary>
		/// Get the clipboard format ids
		/// </summary>
		[DllImport("user32.dll", CharSet=CharSet.Unicode)]
		internal static extern ushort RegisterClipboardFormat(string format);
	}
}
