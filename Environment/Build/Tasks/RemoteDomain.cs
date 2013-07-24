using System;

namespace LinkMe.Environment.Build.Tasks
{
    internal class RemoteDomain
        : IDisposable
    {
        private AppDomain m_domain;
        private RemoteWorker m_worker;

        public RemoteDomain(string appBase)
        {
            // Create the domain.

            AppDomainSetup info = new AppDomainSetup();
            info.ApplicationBase = appBase;
            m_domain = AppDomain.CreateDomain("RemoteWorkerDomain", null, info);

            // Create the worker.

            m_worker = (RemoteWorker)m_domain.CreateInstanceFrom(typeof(RemoteWorker).Assembly.CodeBase, typeof(RemoteWorker).FullName).Unwrap();
        }

        void IDisposable.Dispose()
        {
            try
            {
                m_worker = null;

                if (m_domain != null)
                {
                    AppDomain.Unload(m_domain);
                    m_domain = null;
                }
            }
            catch (Exception)
            {
            }
        }

        public bool IsAssembly(string fullPath)
        {
            return m_worker.IsAssembly(fullPath);
        }

        public bool CanRegisterDll(string fullPath)
        {
            return m_worker.CanRegisterDll(fullPath);
        }

        public string RegisterDll(string fullPath)
        {
            return m_worker.RegisterDll(fullPath);
        }

        public string UnregisterDll(string fullPath)
        {
            return m_worker.UnregisterDll(fullPath);
        }

        public bool CanRegisterPackages(string fullPath)
        {
            return m_worker.CanRegisterDll(fullPath);
        }

        public string RegisterPackages(string fullPath)
        {
            return m_worker.RegisterPackages(fullPath);
        }

        public string UnregisterPackages(string fullPath)
        {
            return m_worker.UnregisterPackages(fullPath);
        }

        public WixRemoteWorker CreateWixWorker()
        {
            return m_worker.CreateWixWorker();
        }
    }
}