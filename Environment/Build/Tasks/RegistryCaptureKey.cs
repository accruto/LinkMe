using System.Collections.Generic;

namespace LinkMe.Environment.Build.Tasks
{
    public class RegistryCaptureValue
    {
        public RegistryCaptureValue(string name, object value)
        {
            m_name = name;
            m_value = value;
        }

        public string Name
        {
            get { return m_name; }
        }

        public object Value
        {
            get { return m_value; }
            set { m_value = value; }
        }

        private string m_name;
        private object m_value;
    }

    public class RegistryCaptureStringValue
        : RegistryCaptureValue
    {
        public RegistryCaptureStringValue(string name, string value)
            : base(name, value)
        {
        }

        public new string Value
        {
            get { return (string) base.Value; }
        }
    }

    public class RegistryCaptureDWordValue
        : RegistryCaptureValue
    {
        public RegistryCaptureDWordValue(string name, int value)
            : base(name, value)
        {
        }

        public new int Value
        {
            get { return (int) base.Value; }
        }
    }

    public class RegistryCaptureBinaryValue
        : RegistryCaptureValue
    {
        public RegistryCaptureBinaryValue(string name, byte[] value)
            : base(name, value)
        {
        }

        public new byte[] Value
        {
            get { return (byte[]) base.Value; }
        }
    }

    public class RegistryCaptureExpandStringValue
        : RegistryCaptureValue
    {
        public RegistryCaptureExpandStringValue(string name, string value)
            : base(name, value)
        {
        }

        public new string Value
        {
            get { return (string) base.Value; }
        }
    }

    public class RegistryCaptureMultiStringValue
        : RegistryCaptureValue
    {
        public RegistryCaptureMultiStringValue(string name, string[] value)
            : base(name, value)
        {
        }

        public new string[] Value
        {
            get { return (string[]) base.Value; }
        }
    }

    public class RegistryCaptureKey
    {
        public RegistryCaptureKey(string path)
        {
            m_path = path;
            m_subKeys = new SortedList<string, RegistryCaptureKey>();
            m_values = new SortedList<string, RegistryCaptureValue>();
        }

        public string Path
        {
            get { return m_path; }
        }

        public string Root
        {
            get
            {
                int pos = m_path.IndexOf("\\");
                if ( pos == -1 )
                    return m_path;
                else
                    return m_path.Substring(0, pos);
            }
        }

        public string RootRelativePath
        {
            get
            {
                int pos = m_path.IndexOf("\\");
                if ( pos == -1 )
                    return string.Empty;
                else
                    return m_path.Substring(pos + 1);
            }
        }

        public string Name
        {
            get
            {
                int pos = m_path.LastIndexOf('\\');
                if ( pos == -1 )
                    return m_path;
                else
                    return m_path.Substring(pos + 1);
            }
        }

        public IList<RegistryCaptureKey> SubKeys
        {
            get { return m_subKeys.Values; }
        }

        public IList<RegistryCaptureValue> Values
        {
            get { return m_values.Values; }
        }

        public void Add(RegistryCaptureKey key)
        {
            m_subKeys[key.Name] = key;
        }

        public void Add(RegistryCaptureValue value)
        {
            m_values[value.Name] = value;
        }

        private string m_path;
        private SortedList<string, RegistryCaptureKey> m_subKeys;
        private SortedList<string, RegistryCaptureValue> m_values;
    }
}