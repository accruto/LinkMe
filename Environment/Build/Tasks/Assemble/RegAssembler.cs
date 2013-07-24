using System.Xml;
using Microsoft.Win32;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class RegAssembler
        : ArtifactAssembler
    {
        public RegAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log)
            : base(artifact, sourceFullPath, destinationFullPath, options, log)
        {
        }

        public override void Assemble(Action action)
        {
            // Copy the source itself.

            Copy(SourceFullPath);

            // Do the registration.

            Register(action, DestinationFullPath);
        }

        public override void Clean(Action action)
        {
            if ( System.IO.File.Exists(DestinationFullPath) )
            {
                // Do the unregistration.

                Unregister(action, DestinationFullPath);

                // Delete the file itself.

                Delete(DestinationFullPath);
            }
        }

        private void Register(Action action, string fullPath)
        {
            // Only register if required.

            if ( action == Action.Assemble )
            {
                // Load the registry file.

                RegistryFile registryFile = new RegistryFile();
                registryFile.Load(fullPath);

                foreach ( RegistryCaptureKey registryKey in registryFile.Keys )
                    Register(registryKey);
            }
        }

        private void Register(RegistryCaptureKey key)
        {
            RegistryKey rootKey = null;
            switch ( key.Root )
            {
                case Constants.Registry.Key.ClassesRoot:
                    rootKey = Microsoft.Win32.Registry.ClassesRoot;
                    break;

                case Constants.Registry.Key.CurrentUser:
                    rootKey = Microsoft.Win32.Registry.CurrentUser;
                    break;

                case Constants.Registry.Key.Users:
                    rootKey = Microsoft.Win32.Registry.Users;
                    break;

                case Constants.Registry.Key.LocalMachine:
                    rootKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            // Create the key.

            if ( rootKey != null )
            {
                RegistryKey registryKey = rootKey.CreateSubKey(key.RootRelativePath);

                // Iterate over values.

                foreach ( RegistryCaptureValue value in key.Values )
                {
                    if ( value is RegistryCaptureStringValue )
                    {
                        registryKey.SetValue(value.Name, value.Value, RegistryValueKind.String);
                    }
                    else if ( value is RegistryCaptureDWordValue )
                    {
                        registryKey.SetValue(value.Name, value.Value, RegistryValueKind.DWord);
                    }
                    else if ( value is RegistryCaptureBinaryValue )
                    {
                        registryKey.SetValue(value.Name, value.Value, RegistryValueKind.Binary);
                    }
                    else if ( value is RegistryCaptureExpandStringValue )
                    {
                        registryKey.SetValue(value.Name, value.Value, RegistryValueKind.ExpandString);
                    }
                    else if ( value is RegistryCaptureMultiStringValue )
                    {
                        registryKey.SetValue(value.Name, value.Value, RegistryValueKind.MultiString);
                    }
                    else
                    {
                        registryKey.SetValue(value.Name, value.Value);
                    }
                }
            }
        }

        private void Unregister(Action action, string fullPath)
        {
            // Only unregister if required.

            if ( action == Action.Assemble )
            {
                // Load the registry file.

                RegistryFile registryFile = new RegistryFile();
                registryFile.Load(fullPath);

                foreach ( RegistryCaptureKey registryKey in registryFile.Keys )
                    Unregister(registryKey);
            }
        }

        private void Unregister(RegistryCaptureKey key)
        {
            RegistryKey rootKey = null;
            switch ( key.Root )
            {
                case Constants.Registry.Key.ClassesRoot:
                    rootKey = Microsoft.Win32.Registry.ClassesRoot;
                    break;

                case Constants.Registry.Key.CurrentUser:
                    rootKey = Microsoft.Win32.Registry.CurrentUser;
                    break;

                case Constants.Registry.Key.Users:
                    rootKey = Microsoft.Win32.Registry.Users;
                    break;

                case Constants.Registry.Key.LocalMachine:
                    rootKey = Microsoft.Win32.Registry.LocalMachine;
                    break;
            }

            // Create the key.

            if ( rootKey != null )
            {
                try
                {
                    rootKey.DeleteSubKeyTree(key.RootRelativePath);
                }
                catch ( System.Exception )
                {
                }
            }
        }
    }
}