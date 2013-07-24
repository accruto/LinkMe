using System.IO;
using System.Diagnostics;
using System.Globalization;
using LinkMe.Environment.Build.Tasks.Assemble;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Registry=Microsoft.Win32.Registry;

namespace LinkMe.Environment.Build.Tasks
{
    internal sealed class VsRegistrationKey
        : RegistrationAttribute.Key
    {
        private RegistryKey m_key;

        internal VsRegistrationKey(RegistryKey key)
        {
            if ( key == null )
                throw new System.ArgumentNullException("key");
            m_key = key;
        }

        public override void Close()
        {
            m_key.Close();
        }

        public override RegistrationAttribute.Key CreateSubkey(string name)
        {
            return new VsRegistrationKey(m_key.CreateSubKey(name));
        }

        public override void SetValue(string name, object value)
        {
            m_key.SetValue(name, value);
        }
    }

    internal class VsRegistryKey
        : System.IDisposable
    {
        public VsRegistryKey(string registryRootPath)
        {
            m_registryRoot = Registry.LocalMachine.CreateSubKey(registryRootPath);
        }

        public void Dispose()
        {
            Close();
        }

        public void Close()
        {
            m_registryRoot.Close();
        }

        public string GetCodeBase(System.Type componentType)
        {
            return componentType != null ? new System.Uri(componentType.Assembly.CodeBase).LocalPath : null;
        }

        public string GetComponentPath(System.Type componentType)
        {
            return componentType != null ? Path.GetDirectoryName(new System.Uri(componentType.Assembly.CodeBase).LocalPath) : null;
        }

        public string GetInprocServerPath(System.Type componentType)
        {
            if ( componentType == null )
                return null;

            try
            {
                foreach ( ProcessModule module in Process.GetCurrentProcess().Modules )
                {
                    if (string.Compare(module.ModuleName, Constants.Project.InprocServerDll, System.StringComparison.OrdinalIgnoreCase) == 0)
                        return module.FileName;
                }

                return Constants.Project.InprocServerDll;
            }
            catch ( System.Exception )
            {
                return Constants.Project.InprocServerDll;
            }
        }

        public RegistrationAttribute.Key CreateKey(string name)
        {
            return new VsRegistrationKey(m_registryRoot.CreateSubKey(name));
        }

        public void RemoveKey(string name)
        {
            using ( RegistryKey subKey = m_registryRoot.OpenSubKey(name) )
            {
                if ( subKey != null )
                    m_registryRoot.DeleteSubKeyTree(name);
            }
        }

        public void RemoveKeyIfEmpty(string name)
        {
            using ( RegistryKey subKey = m_registryRoot.OpenSubKey(name) )
            {
                if ( subKey != null && (subKey.GetSubKeyNames().Length == 0 && subKey.GetValueNames().Length == 0) )
                    m_registryRoot.DeleteSubKey(name);
            }
        }

        public void RemoveValue(string name, string value)
        {
            using ( RegistryKey key = m_registryRoot.OpenSubKey(name, true) )
            {
                if ( key != null )
                    key.DeleteValue(value, false);
            }
        }

        private RegistryKey m_registryRoot;
    }

    internal sealed class VsRegistrationContext
        : RegistrationAttribute.RegistrationContext,
          System.IDisposable
    {
        private System.Type m_componentType;
        private VsRegistryKey m_key;
        private TextWriter m_log;
        private RegistrationMethod m_method;

        public VsRegistrationContext(VsRegistryKey key, RegistrationMethod method)
        {
            m_key = key;
            m_method = method;
        }

        public override RegistrationAttribute.Key CreateKey(string name)
        {
            return m_key.CreateKey(name);
        }

        public void Dispose()
        {
            if ( m_log != null )
            {
                m_log.Dispose();
                m_log = null;
            }

            if ( m_key != null )
            {
                m_key.Dispose();
                m_key = null;
            }
        }

        public override string EscapePath(string str)
        {
            return str;
        }

        public override void RemoveKey(string name)
        {
            m_key.RemoveKey(name);
        }

        public override void RemoveKeyIfEmpty(string name)
        {
            m_key.RemoveKeyIfEmpty(name);
        }

        public override void RemoveValue(string key, string value)
        {
            m_key.RemoveValue(key, value);
        }

        internal void SetType(System.Type type)
        {
            m_componentType = type;
        }

        public override string CodeBase
        {
            get { return m_key.GetCodeBase(m_componentType); }
        }

        public override string ComponentPath
        {
            get { return m_key.GetComponentPath(m_componentType); }
        }

        public override System.Type ComponentType
        {
            get { return m_componentType; }
        }

        public override string InprocServerPath
        {
            get { return m_key.GetInprocServerPath(m_componentType); }
        }

        public override TextWriter Log
        {
            get
            {
                if ( m_log == null )
                    m_log = new StringWriter(CultureInfo.CurrentUICulture);
                return m_log;
            }
        }

        public override RegistrationMethod RegistrationMethod
        {
            get { return m_method; }
        }
    }
}