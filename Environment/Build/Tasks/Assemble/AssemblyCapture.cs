using System.IO;
using System.Reflection;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class AssemblyCapture
    {
        public static RegistryCaptureKey RegisterAssembly(string fullPath)
        {
            // Capture the registration.

            RegistryCaptureKey captureKey;
            using (RegistryCapture registryCapture = new RegistryCapture(Constants.Wix.RegistryCaptureName))
            {
                Assembly assembly = Assembly.LoadFile(fullPath);
                ComUtil.RegisterForInterop(assembly);
                captureKey = registryCapture.Capture();
            }

            // The assembly may have been loaded from the GAC which will be reflected in paths etc.  Need to fix them up.

            string fileName = Path.GetFileName(fullPath);
            ResolvePaths(captureKey, fileName, fullPath);

            return captureKey;
        }

        public static RegistryCaptureKey RegisterPackages(string fullPath)
        {
            // Capture the registration.

            RegistryCaptureKey captureKey;
            using (RegistryCapture registryCapture = new RegistryCapture(Constants.Wix.RegistryCaptureName))
            {
                VsUtil.RegisterPackages(fullPath);
                captureKey = registryCapture.Capture();
            }

            return captureKey;
        }

        private static void ResolvePaths(RegistryCaptureKey key, string fileName, string fullPath)
        {
            // Iterate.

            foreach (RegistryCaptureKey subKey in key.SubKeys)
                ResolvePaths(subKey, fileName, fullPath);

            foreach (RegistryCaptureValue registryValue in key.Values)
            {
                // This is a dodgy test but will do for now.

                string value = registryValue.Value as string;
                if (value != null && value.EndsWith(fileName) && value != fileName)
                    registryValue.Value = fullPath;
            }
        }
    }
}