using System;
using System.Collections.Generic;
using System.GAC;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace LinkMe.Environment
{
    public class GacUtil
        : IDisposable
    {
        public GacUtil()
        {
        }

        ~GacUtil()
        {
            Dispose(false);
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private IAssemblyCache GAC
        {
            get
            {
                if ( m_disposed )
                    throw new ObjectDisposedException(typeof(GacUtil).FullName);
                if ( m_gac == null )
                    m_gac = AssemblyCache.CreateAssemblyCache();
                return m_gac;
            }
        }

        public bool InstallAssemblyFile(string filePath)
        {
            if ( filePath == null )
                throw new ArgumentNullException("filePath");
            if ( !File.Exists(filePath) )
                throw new FileNotFoundException(StringResourceManager.Format(Resources.Exceptions.GacUtilAssemblyFileNotFound, filePath), filePath);
            return GAC.InstallAssembly((uint) IASSEMBLYCACHE_INSTALL_FLAG.IASSEMBLYCACHE_INSTALL_FLAG_FORCE_REFRESH, filePath, null) == 0;
        }

        public bool UninstallAssembly(string assemblyName)
        {
            if ( assemblyName == null )
                throw new ArgumentNullException("assemblyName");
            uint disposition;

            int ret = GAC.UninstallAssembly(0, assemblyName, null, out disposition);
            return ret == 0;
        }

        public bool UninstallAssemblyFile(string filePath)
        {
            if ( filePath == null )
                throw new ArgumentNullException("filePath");

            AssemblyName assemblyName = null;
            try
            {
                assemblyName = AssemblyName.GetAssemblyName(filePath);
            }
            catch ( Exception )
            {
            }

            if (assemblyName == null)
                return false;

            return UninstallAssembly(assemblyName.FullName);
        }

        public IList<string> GetAllAssemblies()
        {
            List<string> assemblies = new List<string>();

            // Enumerate.

            IAssemblyEnum assemblyEnum = AssemblyCache.CreateGACEnum();
            IAssemblyName assemblyName;
            assemblyEnum.GetNextAssembly(IntPtr.Zero, out assemblyName, 0);
            while (assemblyName != null)
            {
                // Grab the name and the version.

                uint size = 10000;
                StringBuilder sb = new StringBuilder((int)size);
                int ret = assemblyName.GetDisplayName(sb, ref size, ASM_DISPLAY_FLAGS.VERSION);
                if (ret == 0)
                    assemblies.Add(sb.ToString());

                assemblyEnum.GetNextAssembly(IntPtr.Zero, out assemblyName, 0);
            }

            return assemblies;
        }

        protected virtual void Dispose(bool disposing)
        {
            if ( !m_disposed )
            {
                if ( !disposing )
                {
                    if ( m_gac != null )
                    {
                        Marshal.ReleaseComObject(m_gac);
                        m_gac = null;
                    }
                }
            }

            m_disposed = true;
        }

        private IAssemblyCache m_gac;
        private bool m_disposed;
    }
}