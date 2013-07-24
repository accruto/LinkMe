using System.Runtime.InteropServices;
using System.Security;
//using System.Text;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class UnsafeNativeMethods
    {
        private UnsafeNativeMethods()
        {
        }

        [DllImport("advapi32.dll", EntryPoint = "RegCreateKeyExW", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern int RegCreateKeyEx(System.UIntPtr key, string subkey, uint reserved, string className, uint options, uint desiredSam, uint securityAttributes, out System.IntPtr openedKey, out uint disposition);

        [DllImport("advapi32.dll", EntryPoint = "RegOverridePredefKey", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern int RegOverridePredefKey(System.UIntPtr key, System.IntPtr newKey);

        [DllImport("advapi32.dll", EntryPoint = "RegCloseKey", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        internal static extern int RegCloseKey(System.IntPtr key);
    }
}