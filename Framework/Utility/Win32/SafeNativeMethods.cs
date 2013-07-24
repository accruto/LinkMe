using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;

namespace LinkMe.Framework.Utility.Win32
{
	[SuppressUnmanagedCodeSecurity]
	internal sealed class SafeNativeMethods
	{
		private SafeNativeMethods()
		{
		}

		[DllImport("oleaut32.dll", CharSet=CharSet.Unicode, PreserveSig=false)]
		internal static extern ITypeLib LoadTypeLibEx(string szFile, REGKIND regkind);

		[DllImport("oleaut32.dll", CharSet=CharSet.Unicode, PreserveSig=false)]
		internal static extern void QueryPathOfRegTypeLib(ref System.Guid libid, ushort wVerMajor,
			ushort wVerMinor, uint lcid, ref string path);

		[DllImport("kernel32.dll")]
		internal static extern bool QueryPerformanceCounter(out long value);

		[DllImport("kernel32.dll")]
		internal static extern bool QueryPerformanceFrequency(out long value);

		[DllImport("kernel32.dll", CharSet=CharSet.Auto)]
		internal static extern uint GetModuleFileName(System.IntPtr hModule,
			[Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpFilename, uint nSize);
	}
}
