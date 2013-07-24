using System;
using System.Security;
using System.Runtime.InteropServices;

using LinkMe.Framework.Utility.Win32;

namespace LinkMe.Framework.Tools
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class UnsafeNativeMethods
	{
		private UnsafeNativeMethods()
		{
		}

		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		internal static extern bool PostMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, ref CHARFORMAT2W lParam);

		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

		[DllImport("user32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
		internal static extern IntPtr SendMessage(HandleRef hWnd, int msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError=true, ExactSpelling=true)]
		internal static extern IntPtr GetWindow(HandleRef hwnd, int cmd);

		[DllImport("user32.dll", SetLastError=true)]
		internal static extern IntPtr GetWindow(IntPtr hwnd, int cmd);

		[DllImport("user32.dll")]
		internal static extern IntPtr SetCursor(HandleRef hCursor);

		[DllImport("gdi32.dll")]
		internal static extern IntPtr CreateCompatibleDC(HandleRef hdc);
	
		[DllImport("gdi32.dll")]
		internal static extern uint DeleteDC(HandleRef hdc);

		[DllImport("gdi32.dll")]
		internal static extern int GetObject(HandleRef hgdiobj, int cbBuffer, ref DIBSECTION lpvObject);

		[DllImport("gdi32.dll")]
		internal static extern int DeleteObject(HandleRef hObject);

		[DllImport("gdi32.dll")]
		internal static extern int GetDeviceCaps(HandleRef hdc, int nIndex);

		[DllImport("gdi32.dll")]
		internal static extern int SetDIBits(HandleRef hdc, HandleRef hbmp, uint uStartScan, int uScanLines,
			IntPtr lpvBits, ref BITMAPINFO lpbmi, uint fuColorUse);
	}
}
