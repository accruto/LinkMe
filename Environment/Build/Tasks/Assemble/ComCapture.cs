namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class ComCapture
    {
        public static RegistryCaptureKey RegisterDll(string fullPath)
        {
            // Capture the registration.

            RegistryCaptureKey captureKey;
            using (RegistryCapture registryCapture = new RegistryCapture(Constants.Wix.RegistryCaptureName))
            {
                //RegisterUtil.RegisterDll(fullPath);
                captureKey = registryCapture.Capture();
            }

            return captureKey;
        }

        public static RegistryCaptureKey RegisterTypeLib(string fullPath)
        {
            // Capture the registration.

            using (RegistryCapture registryCapture = new RegistryCapture(Constants.Wix.RegistryCaptureName))
            {
                //RegisterUtil.RegisterTypeLibrary(fullPath);
                return registryCapture.Capture();
            }
        }
    }
}