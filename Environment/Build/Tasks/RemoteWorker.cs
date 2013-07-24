using System.Reflection;
using LinkMe.Environment.Build.Tasks.Assemble;
using LinkMe.Environment.Build.Tasks.Resources;

namespace LinkMe.Environment.Build.Tasks
{
    internal class RemoteWorker
        :	System.MarshalByRefObject
    {
        public RemoteWorker()
        {
            m_domain = System.AppDomain.CurrentDomain;
        }

        public System.AppDomain AppDomain
        {
            get { return m_domain; }
        }

        public override object InitializeLifetimeService()
        {
            return null;
        }

        public string GetPublisherPolicyFileName(string fullPath)
        {
            // Use LoadFile since the assembly is just being examined and not executed.

            Assembly assembly = Assembly.LoadFile(fullPath);
            AssemblyName name = assembly.GetName();
            System.Version version = name.Version;
            return string.Format("policy.{0}.{1}.{2}.dll", version.Major.ToString(), version.Minor.ToString(), name.Name);
        }

        public bool IsAssembly(string fullPath)
        {
            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
                return true;
            }
            catch ( System.BadImageFormatException )
            {
                return false;
            }
        }

        public bool CanRegisterDll(string fullPath)
        {
            // It may be a .NET DLL or a COM dll. Try .NET first.

            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
            }
            catch ( System.BadImageFormatException )
            {
            }

            // Check based on whether it is an assembly or not.

            if ( assembly != null )
                return ComUtil.CanRegisterForInterop(assembly);
            else
                return false;
            //	return RegisterUtil.CanRegisterDll(fullPath);
        }

        public string RegisterDll(string fullPath)
        {
            // It may be a .NET DLL or a COM dll. Try .NET first.

            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
            }
            catch ( System.BadImageFormatException )
            {
            }

            if ( assembly != null )
            {
                // If needed register the assembly for COM interop.

                if ( ComUtil.CanRegisterForInterop(assembly) )
                {
                    ComUtil.RegisterForInterop(assembly);
                    return string.Format(Messages.RegisteredForInterop, fullPath);
                }
            }
            else
            {
                // Try registering the file as a COM DLL.

                //RegisterUtil.RegisterDll(fullPath);
                //return string.Format(Resources.Messages.RegisteredForCom, fullPath);
            }

            return null;
        }

        public string UnregisterDll(string fullPath)
        {
            // It may be a .NET DLL or an unmanaged one. Try .NET first (most likely).

            Assembly assembly = null;
            try
            {
                // Use LoadFile since the assembly is just being examined and not executed.

                assembly = Assembly.LoadFile(fullPath);
            }
            catch ( System.BadImageFormatException )
            {
            }

            if ( assembly != null )
            {
                // If needed register the assembly for COM interop.

                if ( ComUtil.CanRegisterForInterop(assembly) )
                {
                    ComUtil.UnregisterForInterop(assembly);
                    return string.Format(Messages.UnregisteredForInterop, fullPath);
                }
            }
            else
            {
                // Register the file as a COM DLL.

                //RegisterUtil.UnregisterDll(fullPath);
                //return string.Format(Resources.Messages.UnregisteredForCom, fullPath);
            }

            return null;
        }

        public bool CanRegisterPackages(string fullPath)
        {
            return VsUtil.CanRegisterPackages(fullPath);
        }

        public string RegisterPackages(string fullPath)
        {
            VsUtil.RegisterPackages(fullPath);
            return string.Format(Messages.RegisteredPackages, fullPath);
        }

        public string UnregisterPackages(string fullPath)
        {
            VsUtil.UnregisterPackages(fullPath);
            return string.Format(Messages.UnregisteredPackages, fullPath);
        }

        public WixRemoteWorker CreateWixWorker()
        {
            return new WixRemoteWorker();
        }

        private System.AppDomain m_domain;
    }
}