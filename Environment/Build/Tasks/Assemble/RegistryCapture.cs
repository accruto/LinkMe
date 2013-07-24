using System.IO;
using System.ComponentModel;
using Microsoft.Win32;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class RegistryCapture
        : System.IDisposable
    {
        private string m_rootKey;

        public RegistryCapture(string name)
        {
            m_rootKey = Constants.Registry.Key.Software + "\\" + name;

            // Delete the root key if it already exists.

            DeleteRootKey();

            // Start capturing all the root registry hives.

            // For some reason on a x64 build the registry handles are invalid, even though the API's return 0.
            // Not sure why, but I don't think this is really needed now anyway so turn it off for now.

//			Start(Constants.Registry.Key.HKeyClassesRoot, m_rootKey + "\\" + Constants.Registry.Key.ClassesRoot);
//			Start(Constants.Registry.Key.HKeyCurrentUser, m_rootKey + "\\" + Constants.Registry.Key.CurrentUser);
//			Start(Constants.Registry.Key.HKeyUsers, m_rootKey + "\\" + Constants.Registry.Key.Users);
//			Start(Constants.Registry.Key.HKeyLocalMachine, m_rootKey + "\\" + Constants.Registry.Key.LocalMachine);
        }

        public RegistryCaptureKey Capture()
        {
            Stop();

            // Read all changes.

            RegistryCaptureKey captureKey;
            using ( RegistryKey rootKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(m_rootKey) )
            {
                captureKey = GetKey(rootKey);
            }

            // Delete the root key.

            DeleteRootKey();
            return captureKey;
        }

        void System.IDisposable.Dispose()
        {
            Stop();
        }

        private void Stop()
        {
            // Stop capturing all the root registry hives.

//			Stop(Constants.Registry.Key.HKeyLocalMachine);
//			Stop(Constants.Registry.Key.HKeyUsers);
//			Stop(Constants.Registry.Key.HKeyCurrentUser);
//			Stop(Constants.Registry.Key.HKeyClassesRoot);
        }

        private RegistryCaptureKey GetKey(RegistryKey key)
        {
            string path;
            int pos = key.Name.IndexOf(m_rootKey);
            if ( pos != -1 )
                path = key.Name.Substring(pos + m_rootKey.Length);
            else
                path = key.Name;
            if ( path.StartsWith("\\") )
                path = path.Substring(1);

            RegistryCaptureKey captureKey = new RegistryCaptureKey(path);

            // Iterate over all values.

            foreach ( string name in key.GetValueNames() )
            {
                switch ( key.GetValueKind(name) )
                {
                    case RegistryValueKind.String:
                        captureKey.Add(new RegistryCaptureStringValue(name, key.GetValue(name) as string));
                        break;

                    case RegistryValueKind.DWord:
                        captureKey.Add(new RegistryCaptureDWordValue(name, (int) key.GetValue(name)));
                        break;

                    case RegistryValueKind.ExpandString:
                        captureKey.Add(new RegistryCaptureExpandStringValue(name, key.GetValue(name) as string));
                        break;

                    case RegistryValueKind.MultiString:
                        captureKey.Add(new RegistryCaptureMultiStringValue(name, key.GetValue(name) as string[]));
                        break;

                    case RegistryValueKind.Unknown:
                    case RegistryValueKind.Binary:
                    case RegistryValueKind.QWord:
                        captureKey.Add(new RegistryCaptureValue(name, key.GetValue(name)));
                        break;
                }
            }

            // Iterate over sub keys.

            foreach ( string subKeyName in key.GetSubKeyNames() )
            {
                using ( RegistryKey subKey = key.OpenSubKey(subKeyName) )
                {
                    captureKey.Add(GetKey(subKey));
                }
            }

            return captureKey;
        }


        private void Start(System.UIntPtr hive, string overrideKey)
        {
            System.IntPtr key = System.IntPtr.Zero;

            try
            {
                key = OpenRegistryKey(Constants.Registry.Key.HKeyLocalMachine, overrideKey);
                if ( UnsafeNativeMethods.RegOverridePredefKey(hive, key) != 0 )
                    throw new Win32Exception();
            }
            finally
            {
                if ( key != System.IntPtr.Zero )
                {
                    if ( UnsafeNativeMethods.RegCloseKey(key) != 0 )
                        throw new Win32Exception();
                }
            }
        }

        private void Stop(System.UIntPtr hive)
        {
            if ( UnsafeNativeMethods.RegOverridePredefKey(hive, System.IntPtr.Zero) != 0 )
                throw new Win32Exception();
        }

        private void DeleteRootKey()
        {
            try
            {
                Microsoft.Win32.Registry.LocalMachine.DeleteSubKeyTree(m_rootKey);
            }
            catch ( System.ArgumentException )
            {
            }
        }

        private System.IntPtr OpenRegistryKey(System.UIntPtr key, string path)
        {
            uint sam = Constants.Registry.StandardRightsAll
                       | Constants.Registry.GenericRead
                       | Constants.Registry.GenericWrite
                       | Constants.Registry.GenericExecute
                       | Constants.Registry.GenericAll;
            System.IntPtr newKey;
            uint disposition;

            int error = UnsafeNativeMethods.RegCreateKeyEx(key, path, 0, null, 0, sam, 0, out newKey, out disposition);
            if ( error != 0 )
                throw new Win32Exception(error);
            return newKey;
        }
    }
}