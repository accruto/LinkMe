using System;
using System.IO;
using LinkMe.Environment.Build.Tasks.Resources;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Assemble
{
    internal class DllAssembler
        : ArtifactAssembler
    {
        public DllAssembler(Artifact artifact, string sourceFullPath, string destinationFullPath, AssembleOptions options, TaskLoggingHelper log, GacUtil gac)
            :	base(artifact, sourceFullPath, destinationFullPath, options, log)
        {
            m_gac = gac;
        }

        public override void Assemble(Action action)
        {
            // Copy the source itself.

            Copy(SourceFullPath);

            // Do the registration.

            InstallAssembly(action, SourceFullPath);
            RegisterDll(action, SourceFullPath);
            RegisterPackages(action, SourceFullPath);

            // Copy associated files.

            SetAssociatedFile();
            CopyAssociatedFile(SourceFullPath, Constants.File.Xml.Extension, ExtensionAction.Replace);
            CopyAssociatedFile(SourceFullPath, Constants.File.Pdb.Extension, ExtensionAction.Replace);
            ClearAssociatedFile();
        }

        public override void Clean(Action action)
        {
            // Clean associated files.

            SetAssociatedFile();
            CleanAssociatedFile(DestinationFullPath, Constants.File.Pdb.Extension, ExtensionAction.Replace);
            CleanAssociatedFile(DestinationFullPath, Constants.File.Xml.Extension, ExtensionAction.Replace);
            ClearAssociatedFile();

            if ( System.IO.File.Exists(SourceFullPath) )
            {
                // Do the unregistration.

                UnregisterPackages(action, SourceFullPath);
                UnregisterDll(action, SourceFullPath);
                UninstallAssembly(action, SourceFullPath);
            }

            if ( System.IO.File.Exists(DestinationFullPath) )
            {
                // Delete the file itself.

                Delete(DestinationFullPath);
            }
        }

        private static RemoteDomain CreateRemoteDomain(string fullPath)
        {
            // The remote domain needs to load this assembly so give it an appropriate app base.

//            string appBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            //          if (appBase.StartsWith("file:\\"))
            //            appBase = appBase.Substring("file:\\".Length);
            return new RemoteDomain(Path.GetDirectoryName(fullPath));
        }

        private void InstallAssembly(Action action, string fullPath)
        {
            using (RemoteDomain domain = CreateRemoteDomain(fullPath))
            {
                if (domain.IsAssembly(fullPath))
                {
                    Artifact.SetMetadata(Constants.Catalogue.Artifact.Assembly, true);

                    // Check whether to do this.

                    if (action == Action.Assemble && ShouldInstallInGac())
                    {
                        // Install it.

                        if (m_gac.InstallAssemblyFile(fullPath))
                            LogMessage(Messages.InstalledToGac, fullPath);
                        else
                            throw new ApplicationException("The assembly '" + fullPath + "' cannot be installed in the gac.");
                    }
                }
            }
        }

        private void UninstallAssembly(Action action, string fullPath)
        {
            // Check whether to do this.

            if ( action == Action.Assemble && ShouldInstallInGac() )
            {
                // Uninstall it.

                if ( m_gac.UninstallAssemblyFile(fullPath) )
                    LogMessage(Messages.UninstalledFromGac, fullPath);
            }
        }

        private void RegisterDll(Action action, string fullPath)
        {
            using (RemoteDomain domain = CreateRemoteDomain(fullPath))
            {
                // This needs to be done in a remote AppDomain because it loads the assembly.

                if (domain.CanRegisterDll(fullPath))
                {
                    if (!IsAssociatedFile())
                        Artifact.SetMetadata(Constants.Catalogue.Artifact.Register, true);

                    // Only register if required.

                    if (action == Action.Assemble)
                    {
                        string message;
                        if ((message = domain.RegisterDll(fullPath)) != null)
                            LogMessage(message);
                    }
                }
            }
        }

        private void UnregisterDll(Action action, string fullPath)
        {
            using (RemoteDomain domain = CreateRemoteDomain(fullPath))
            {
                // This needs to be done in a remote AppDomain because it loads the assembly.

                if (domain.CanRegisterDll(fullPath))
                {
                    // Only register if required.

                    if (action == Action.Assemble)
                    {
                        string log;
                        if ((log = domain.UnregisterDll(fullPath)) != null)
                            LogMessage(log);
                    }
                }
            }
        }

        private void RegisterPackages(Action action, string fullPath)
        {
            using (RemoteDomain domain = CreateRemoteDomain(fullPath))
            {
                // This needs to be done in a remote AppDomain because it loads the assembly.

                if (domain.CanRegisterPackages(fullPath))
                {
                    if (!IsAssociatedFile())
                        Artifact.SetMetadata(Constants.Catalogue.Artifact.RegisterPackages, true);

                    // Only register if required.

                    if (action == Action.Assemble)
                    {
                        string log;
                        if ((log = domain.RegisterPackages(fullPath)) != null)
                            LogMessage(log);
                    }
                }
            }
        }

        private void UnregisterPackages(Action action, string fullPath)
        {
            using (RemoteDomain domain = CreateRemoteDomain(fullPath))
            {
                // This needs to be done in a remote AppDomain because it loads the assembly.

                if (domain.CanRegisterPackages(fullPath))
                {
                    // Only unregister if required.

                    if (action == Action.Assemble)
                    {
                        string log;
                        if ((log = domain.UnregisterPackages(fullPath)) != null)
                            LogMessage(log);
                    }
                }
            }
        }

        private bool ShouldInstallInGac()
        {
            // Determine whether this dll needs to be installed in the GAC by looking at the project.

            string installInGacString = Artifact.GetMetadata(Constants.Catalogue.Artifact.InstallInGac);
            if ( !string.IsNullOrEmpty(installInGacString) )
                return bool.Parse(installInGacString);
            else
                return false;
        }

        private readonly GacUtil m_gac;
    }
}