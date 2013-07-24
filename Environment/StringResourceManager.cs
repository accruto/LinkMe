using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace LinkMe.Environment
{
    public sealed class StringResourceManager
    {
        private static object s_lock;
        private static Dictionary<System.Type, StringResourceManager> s_managers = new Dictionary<System.Type, StringResourceManager>();
        private ResourceManager m_resourceManager;

        private StringResourceManager(System.Type source)
        {
            m_resourceManager = new ResourceManager(source);
        }

        public static StringResourceManager GetManager(System.Type source)
        {
            lock ( Lock )
            {
                if ( s_managers.ContainsKey(source) )
                {
                    return s_managers[source];
                }
                else
                {
                    StringResourceManager manager = new StringResourceManager(source);
                    s_managers[source] = manager;
                    return manager;
                }
            }
        }

        private static object Lock
        {
            get
            {
                if ( s_lock == null )
                {
                    object lockObject = new object();
                    Interlocked.CompareExchange(ref s_lock, lockObject, null);
                }

                return s_lock;
            }
        }

        public string GetString(string name, params object[] args)
        {
            if ( m_resourceManager == null )
                return null;

            string resource = m_resourceManager.GetString(name);
            if ( args != null && args.Length > 0 )
                return string.Format(CultureInfo.CurrentCulture, resource, args);
            else
                return resource;
        }

        public string GetString(string name)
        {
            return m_resourceManager != null ? m_resourceManager.GetString(name) : null;
        }

        public static string GetString(System.Type source, string name, params object[] args)
        {
            StringResourceManager manager = GetManager(source);
            return manager.GetString(name, args);
        }

        public static string GetString(System.Type source, string name)
        {
            StringResourceManager manager = GetManager(source);
            return manager.GetString(name);
        }

        public static string Format(string resource, params object[] args)
        {
            if ( args != null && args.Length > 0 )
                return string.Format(CultureInfo.CurrentCulture, resource, args);
            else
                return resource;
        }
    }

    [System.AttributeUsage(System.AttributeTargets.All)]
    public sealed class ResourceCategoryAttribute
        : CategoryAttribute
    {
        public ResourceCategoryAttribute(System.Type source, string category)
            : base(category)
        {
            m_manager = StringResourceManager.GetManager(source);
        }

        protected override string GetLocalizedString(string value)
        {
            string result = m_manager.GetString(value);
            return result != null ? result : value;
        }

        private StringResourceManager m_manager;
    }

    [System.AttributeUsage(System.AttributeTargets.All)]
    public sealed class ResourceDisplayNameAttribute
        : DisplayNameAttribute
    {
        public ResourceDisplayNameAttribute(System.Type source, string name)
            : base(name)
        {
            m_manager = StringResourceManager.GetManager(source);
        }

        public override string DisplayName
        {
            get
            {
                string displayName = m_manager.GetString(base.DisplayName);
                return displayName != null ? displayName : base.DisplayName;
            }
        }

        private StringResourceManager m_manager;
    }

    [System.AttributeUsage(System.AttributeTargets.All)]
    public sealed class ResourceDescriptionAttribute
        : DescriptionAttribute
    {
        public ResourceDescriptionAttribute(System.Type source, string name)
            : base(name)
        {
            m_manager = StringResourceManager.GetManager(source);
        }

        public override string Description
        {
            get
            {
                string description = m_manager.GetString(base.Description);
                return description != null ? description : base.Description;
            }
        }

        private StringResourceManager m_manager;
    }
}