using System;
using System.IO;
using System.Reflection;
using LinkMe.Environment.Build.Tasks.Assemble;
using Microsoft.Build.Utilities;

namespace LinkMe.Environment.Build.Tasks.Package
{
    internal class ModuleMerger
        : Worker
    {
        public ModuleMerger(bool isWin64, ModuleOptions options, TaskLoggingHelper log)
        {
            m_isWin64 = isWin64;
            m_options = options;
            m_log = log;
            CreateWorker();
        }

        private void CreateWorker()
        {
            string wixPath = StaticEnvironment.GetFilePath(Constants.Product.Sdk, Constants.Folder.Bin, "wix.dll", "LinkMe.Framework.Sdk");

            // If not found then use this assembly's location.

            if (wixPath == null)
                wixPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            m_domain = new RemoteDomain(Path.GetDirectoryName(wixPath));
            m_wixWorker = m_domain.CreateWixWorker();

            // Use the obj sub-folder of the project path as the build folder.

            string buildFolder = Path.Combine(Path.GetDirectoryName(m_options.ProjectFullPath), Constants.Folder.Obj);
            string msmFile = m_options.MergeModuleFile;
            if (Path.GetExtension(msmFile) == ".msm")
                msmFile = msmFile.Substring(0, msmFile.Length - 4) + (m_isWin64 ? ".x64" : ".x86") + ".msm";
            else
                msmFile += (m_isWin64 ? ".x64" : ".x86");

            System.Version version = m_options.Version == null ? null : (System.Version) m_options.Version.Clone();
            string manufacturer = m_options.Manufacturer;
            m_wixWorker.Initialise(m_isWin64, buildFolder, msmFile, m_options.Guid, version, manufacturer);
        }

        public void Load(string catalogueFile)
        {
            m_wixWorker.Load(catalogueFile);
        }

        public void CreateMergeModule()
        {
            m_wixWorker.Build();
        }

        public override void Dispose()
        {
            base.Dispose();

            m_wixWorker = null;
            if (m_domain != null)
            {
                ((IDisposable)m_domain).Dispose();
                m_domain = null;
            }
        }

        private RemoteDomain m_domain;
        private WixRemoteWorker m_wixWorker;
        private readonly bool m_isWin64;
        private readonly ModuleOptions m_options;
        private readonly TaskLoggingHelper m_log;
    }
}