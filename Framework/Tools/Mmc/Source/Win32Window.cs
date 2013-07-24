using System.Windows.Forms;

namespace LinkMe.Framework.Tools.Mmc
{
	internal class Win32Window
		:	IWin32Window
	{
		public Win32Window(System.IntPtr hwnd)
		{
			m_hwnd = hwnd;
		}

		System.IntPtr IWin32Window.Handle
		{
			get { return m_hwnd; }
		}

		private System.IntPtr m_hwnd;
	}
}
